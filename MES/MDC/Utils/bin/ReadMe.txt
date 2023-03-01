using Utils;
using Utils.HBaseClass;

 /// <summary>
        /// HBase数据库操作类
        /// </summary>
        private GetHBaseDataClass GHDC;


            HBase_DataServer_IP = "192.168.41.4";


            // 初始化HBASE数据库查询类
            GHDC = new GetHBaseDataClass(HBase_DataServer_IP);


  DataTable processData = GHDC.GetProductionRouteByCode(txtAnalysisCode.Text);//Code1查询结果


DataTable ProcessTable = new DataTable();
                #region data_01
                ProcessTable.Columns.Add("Process_LCDCode");//玻璃码
                ProcessTable.Columns.Add("Process_productionOrder");//生产订单
                ProcessTable.Columns.Add("Process_productLineCode");//产线编码
                ProcessTable.Columns.Add("Process_productDate");//生产日期
                ProcessTable.Columns.Add("Process_number");//工序序号
                ProcessTable.Columns.Add("Process_name");//工序名称
                ProcessTable.Columns.Add("Process_BLCode");//背光编码
                ProcessTable.Columns.Add("Process_FPCCode");//FPC编码
                ProcessTable.Columns.Add("Process_ICCode");//ic编码
                ProcessTable.Columns.Add("Process_QRCode");//54位编码
                ProcessTable.Columns.Add("Process_TPCode");//TP编码
                ProcessTable.Columns.Add("Process_finishesCode");//成品料号
                ProcessTable.Columns.Add("Process_finishesModel");//成品规格型号
                ProcessTable.Columns.Add("Process_clientProductNo");//客户料号
                #endregion
                #region data_06
                ProcessTable.Columns.Add("Process_logNumber");//产品已过工序的序号
                ProcessTable.Columns.Add("Process_logCode");//产品已过工序的编码
                #endregion

