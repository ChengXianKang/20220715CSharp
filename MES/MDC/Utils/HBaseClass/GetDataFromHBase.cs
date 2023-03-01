using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Thrift.Transport;
using Thrift.Protocol;
using System.Text;
using System.Net.Mail;
using System.Data;
using Newtonsoft.Json.Linq;
using System.Configuration;
using Utils.Common;

namespace Utils.HBaseClass
{
    public class GetDataFromHBase
    {
        //实例化Socket连接
        public TTransport transport;//连接的IP、端口 
        //实例化一个协议对象
        private TProtocol tProtocol;
        //实例化一个Hbase的Client对象
        public Hbase.Client client;
        private DataView dvSql = new DataView();
        /// <summary>
        /// 唯一实例
        /// </summary>
        private static GetDataFromHBase _instance;
        /// <summary>
        /// 线程锁
        /// </summary>
        private static object _instanceLock = new object();
        /// <summary>
        /// HBase服务器IP地址
        /// </summary>
        private string _HBase_DataServer_IP;
        /// <summary>
        /// 数据查询重试次数
        /// </summary>
        private int _retryCount;

        /// <summary>
        /// 获取当前的唯一实例
        /// </summary>
        public static GetDataFromHBase Instance
        {
            get
            {
                //第一次检测实例是否已经存在，避免不必要的线程等待
                if (_instance == null)
                {
                    //锁住线程
                    lock (_instanceLock)
                    {
                        //第二次检测实例是否已经存在，避免通过了第一次检测的线程重复创建实例
                        if (_instance == null)
                        {
                            _instance = new GetDataFromHBase();
                        }
                    }
                }
                return _instance;
            }
        }

        /// <summary>
        /// HBase服务器IP地址
        /// </summary>
        public string HBase_DataServer_IP
        {
            get
            {
                return _HBase_DataServer_IP;
            }
            set
            {
                _HBase_DataServer_IP = value;
            }
        }

        /// <summary>
        /// GetDataFromHBase的构造函数
        /// </summary>
        /// <param name="HBase_DataServer_IP">HBase数据库IP地址</param>
        private GetDataFromHBase()
        {
            try
            {
                _retryCount = YJ.Common.Utils.StrToInt(YJ.Common.Utils.GetAppConfig("RetryCount", "appSettings"), 1);
                _HBase_DataServer_IP = YJ.Common.Utils.GetAppConfig("HBase_DataServer_IP", "appSettings");
            }
            catch (Exception ex)
            {
                LogHelper.Error("", ex);
                // APP.config中无HBase_DataServer_IP字段
            }
            if (string.IsNullOrEmpty(_HBase_DataServer_IP))
            {
                // 未配置服务器地址HBase_DataServer_IP，则使用默认HBase数据库IP地址172.20.23.231
                _HBase_DataServer_IP = "172.20.23.231";
#if DEBUG
                _HBase_DataServer_IP = "172.20.23.231";
                //_HBase_DataServer_IP = "192.168.23.159";
#endif
            }
            
            //实例化Socket连接
            transport = new TSocket(_HBase_DataServer_IP, 9090);//连接的IP、端口 
            //实例化一个协议对象
            tProtocol = new TBinaryProtocol(transport);
            //实例化一个Hbase的Client对象
            client = new Hbase.Client(tProtocol);
        }

        /// <summary>
        /// 打开连接
        /// </summary>
        public void OpenConnect()
        {
            try
            {
                transport.Close();
                transport.Dispose();
            }
            catch (Exception exp)
            {
                LogHelper.Error("HBase关闭连接失败：" + exp.Message, exp);
            }
            try
            {
                //实例化Socket连接
                transport = new TSocket(_HBase_DataServer_IP, 9090);//连接的IP、端口 
                //实例化一个协议对象
                tProtocol = new TBinaryProtocol(transport);
                //实例化一个Hbase的Client对象
                client = new Hbase.Client(tProtocol);
                transport.Open();
            }
            catch (Exception exp)
            {
                LogHelper.Error("HBase打开连接失败：" + exp.Message, exp);
            }
        }

        /// <summary>
        /// 关闭连接
        /// </summary>
        public void CloseConnect()
        {
            try
            {
                if (transport != null)
                {
                    transport.Close();
                    transport.Dispose();
                }
            }
            catch (Exception exp)
            {
                LogHelper.Error("HBase关闭连接失败：" + exp.Message, exp);
            }
        }
        
        /// <summary>
        /// 单一rowKey查询一条数据
        /// </summary>
        /// <param name="rowKey">rowkey</param>
        public List<TRowResult> GetRowsWithSingleRowKey(string _rowKey, string table)
        {
            //根据表名，RowKey名来获取结果集
            List<TRowResult> reslut = null;
            int n = 0;
            while (reslut == null && n <= _retryCount)
            {
                try
                {
                    n++;
                    if (!transport.IsOpen)
                    {
                        OpenConnect();
                    }
                    reslut = client.getRow(Encoding.UTF8.GetBytes(table), Encoding.UTF8.GetBytes(_rowKey), null);
                }
                catch (Exception exp)
                {
                    LogHelper.Error("单一RowKey查询失败(" + n.ToString() + ")：" + exp.Message, exp);
                    OpenConnect();
                }
            }
            return reslut;
        }

        /// <summary>
        /// 多个RowKey查询多条数据
        /// </summary>
        /// <param name="rowKey">RowKey</param>
        public List<TRowResult> GetRowsWithManyRowKey(string _rowKey, string table)
        {
            List<TRowResult> reslut = null;
            int n = 0;
            while (reslut == null && n <= _retryCount)
            {
                try
                {
                    n++;
                    if (!transport.IsOpen)
                    {
                        OpenConnect();
                    }
                    List<byte[]> rows = new List<byte[]>(0);
                    string[] rowKey = _rowKey.Split(',');
                    for (int i = 0; i < rowKey.Length; i++)
                    {
                        rows.Add(Encoding.UTF8.GetBytes(rowKey[i]));
                    }
                    reslut = client.getRows(Encoding.UTF8.GetBytes(table), rows, null);
                }
                catch (Exception exp)
                {
                    LogHelper.Error("多RowKey查询失败：" + exp.Message, exp);
                    OpenConnect();
                }
            }
            return reslut;
        }

        /// <summary>
        /// 扫描查询数据
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        private List<TRowResult> ScannerGetList(object param)
        {
            try
            {
                int ScannerID = Convert.ToInt32(param);
                return client.scannerGetList(ScannerID, 2160000);
            }
            catch (Exception ex)
            {
                LogHelper.Error("ScannerGetList(object param)", ex);
                return new List<TRowResult>();
            }
        }
        /// <summary>
        /// 通过RowKey的范围获取数据
        /// </summary>
        /// <param name="stRowkey"></param>
        /// <param name="endRowkey"></param>
        /// <remarks>结果集包含StartRowKey列值，不包含EndRowKey的列值</remarks>
        public List<TRowResult> GetRowsWithRowKeyRange(string stRowkey, string endRowkey, string table)
        {
            //YJ.Log.FileLog.log.Error(stRowkey + endRowkey + table);
            List<TRowResult> result = null;
            int n = 0;
            while (result == null && n <= _retryCount)
            {
                try
                {
                    n++;
                    if (transport == null || !transport.IsOpen)
                    {
                        transport.Open();
                    }
                    List<byte[]> columns = new List<byte[]>(0);
                    columns.Add(Encoding.UTF8.GetBytes("logValue"));
                    int ScannerID = client.scannerOpenWithStop(Encoding.UTF8.GetBytes(table), Encoding.UTF8.GetBytes(stRowkey), Encoding.UTF8.GetBytes(endRowkey), columns, null);
                    //result = client.scannerGetList(ScannerID, 50000);
                    result = OutTimeClass.OutTimeSomeParemReturn(new OutTimeClass.SomeParamsReturnDelegate(ScannerGetList), 10000, ScannerID) as List<TRowResult>;
                }
                catch (Exception exp)
                {
                    LogHelper.Error("第" + n + "次rowKey范围查询失败.", exp);
                    OpenConnect();
                }

                if (result == null && n > _retryCount)
                {
                    LogHelper.Error("查询超时，无法连接HBase。", new Exception());
                    OpenConnect();
                }
            }
            return result;
        }
        /// <summary>
        /// rowKey左前缀查询
        /// </summary>
        /// <param name="_rowKey">rowKey前缀</param>
        public List<TRowResult> GetRowsWithPrefix(string _rowKey, string table)
        {
            List<TRowResult> reslut = null;
            int n = 0;
            while (reslut == null && n <= _retryCount)
            {
                try
                {
                    n++;
                    if (!transport.IsOpen)
                    {
                        OpenConnect();
                    }
                    List<byte[]> columns = new List<byte[]>(0);
                    columns.Add(Encoding.UTF8.GetBytes("logValue"));
                    int ScannerID = client.scannerOpenWithPrefix(Encoding.UTF8.GetBytes(table), Encoding.UTF8.GetBytes(_rowKey), columns, null);
                    reslut = client.scannerGetList(ScannerID, 2160000);
                }
                catch (Exception exp)
                {
                    LogHelper.Error("rowKey左前缀查询失败：" + exp.Message, exp);
                    OpenConnect();
                }
            }
            return reslut;
        }


        /// <summary>
        /// 更新产品过站工序信息记录
        /// </summary>
        /// <param name="tablename"></param>
        /// <param name="rowkey"></param>
        /// <param name="_mutations"></param>
        /// <returns></returns>
        public bool MutateRowHBase(string tablename, string rowkey, List<Mutation> _mutations)
        {
            try
            {
                if (!transport.IsOpen)
                {
                    OpenConnect();
                }
                client.mutateRow(Encoding.UTF8.GetBytes(tablename), Encoding.UTF8.GetBytes(rowkey), _mutations, null);
                return true;
            }
            catch (Exception exp)
            {
                LogHelper.Error("单一RowKey更新产品过站记录" + exp.Message, exp);
                OpenConnect();
                return false;
            }
        }


        /// <summary>
        /// 删除条码绑定信息记录
        /// </summary>
        /// <param name="tablename">表名</param>
        /// <param name="rowkey">Key</param>
        /// <param name="column">列名</param>
        /// <returns></returns>
        public bool DeleteRowHBase(string tablename, string rowkey, string column)
        {
            try
            {
                if (!transport.IsOpen)
                {
                    OpenConnect();
                }
                client.deleteAll(Encoding.UTF8.GetBytes(tablename), Encoding.UTF8.GetBytes(rowkey), Encoding.UTF8.GetBytes(column), null);
                return true;
            }
            catch (Exception exp)
            {
                LogHelper.Error("单一RowKey删除条码绑定信息" + exp.Message, exp);
                OpenConnect();
                return false;
            }
        }
    }
}