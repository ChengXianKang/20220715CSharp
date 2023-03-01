using Utils;
using Utils.HBaseClass;

 /// <summary>
        /// HBase���ݿ������
        /// </summary>
        private GetHBaseDataClass GHDC;


            HBase_DataServer_IP = "192.168.41.4";


            // ��ʼ��HBASE���ݿ��ѯ��
            GHDC = new GetHBaseDataClass(HBase_DataServer_IP);


  DataTable processData = GHDC.GetProductionRouteByCode(txtAnalysisCode.Text);//Code1��ѯ���


DataTable ProcessTable = new DataTable();
                #region data_01
                ProcessTable.Columns.Add("Process_LCDCode");//������
                ProcessTable.Columns.Add("Process_productionOrder");//��������
                ProcessTable.Columns.Add("Process_productLineCode");//���߱���
                ProcessTable.Columns.Add("Process_productDate");//��������
                ProcessTable.Columns.Add("Process_number");//�������
                ProcessTable.Columns.Add("Process_name");//��������
                ProcessTable.Columns.Add("Process_BLCode");//�������
                ProcessTable.Columns.Add("Process_FPCCode");//FPC����
                ProcessTable.Columns.Add("Process_ICCode");//ic����
                ProcessTable.Columns.Add("Process_QRCode");//54λ����
                ProcessTable.Columns.Add("Process_TPCode");//TP����
                ProcessTable.Columns.Add("Process_finishesCode");//��Ʒ�Ϻ�
                ProcessTable.Columns.Add("Process_finishesModel");//��Ʒ����ͺ�
                ProcessTable.Columns.Add("Process_clientProductNo");//�ͻ��Ϻ�
                #endregion
                #region data_06
                ProcessTable.Columns.Add("Process_logNumber");//��Ʒ�ѹ���������
                ProcessTable.Columns.Add("Process_logCode");//��Ʒ�ѹ�����ı���
                #endregion

