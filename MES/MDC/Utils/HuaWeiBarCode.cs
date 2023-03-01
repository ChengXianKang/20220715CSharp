using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Utils.HBaseClass;

namespace Utils
{
    public class HuaWeiBarCode
    {
        ////获取物料SN编码
        //public static string Get_MaterialSNCode(string SearchCode, string IsRepeat)
        //{
        //    YJ.Data.SqlServerProvider Conn = new YJ.Data.SqlServerProvider();
        //    string CodeNumber = "";
        //    DataTable SearchTable = new DataTable();
        //    DataView processLineView = new DataView();
        //    string result = "{\"status\":\"0\"}";
        //    try
        //    {
        //        if (string.IsNullOrEmpty(SearchCode))
        //        {
        //            result = string.Format("{{\"status\":\"0\",\"msg\":\"error\",\"result\":\"{0}\"}}", "查询编码不能为空");
        //            return result;
        //        }
        //        SearchTable = GetHBaseDataClass.Instance.GetProductionRouteByCode(SearchCode);
        //        if (SearchTable == null)
        //        {
        //            result = string.Format("{{\"status\":\"0\",\"msg\":\"error\",\"result\":\"{0}\"}}", "内部查询玻璃绑定信息失败,如多次出现此错误,请关掉程序重新打开");
        //            return result;
        //        }
        //        if (SearchTable.DefaultView.Count <= 0)
        //        {
        //            result = string.Format("{{\"status\":\"0\",\"msg\":\"error\",\"result\":\"{0}\"}}", "未找到该条码的玻璃绑定信息");
        //            return result;
        //        }
        //        else
        //        {
        //            //if (IsRepeat == "Unchecked")
        //            //{
        //            //    if (!string.IsNullOrEmpty(SearchTable.DefaultView[0]["Code_intactCode"].ToString()))
        //            //    {
        //            //        result = string.Format("{{\"status\":\"0\",\"msg\":\"error\",\"result\":\"{0}\"}}", "已生成成品编码,请勿重复扫描");
        //            //        return result;
        //            //    }
        //            //}
        //            if (string.IsNullOrEmpty(SearchTable.DefaultView[0]["Code_cusCode"].ToString()))
        //            {
        //                result = string.Format("{{\"status\":\"0\",\"msg\":\"error\",\"result\":\"{0}\"}}", "对应的客户料号为空");
        //                return result;
        //            }
        //            else if (string.IsNullOrEmpty(SearchTable.DefaultView[0]["Code_lineCode"].ToString()))
        //            {
        //                result = string.Format("{{\"status\":\"0\",\"msg\":\"error\",\"result\":\"{0}\"}}", "对应的产线编码为空");
        //                return result;
        //            }
        //            else if (string.IsNullOrEmpty(SearchTable.DefaultView[0]["Code_glassCode"].ToString()))
        //            {
        //                result = string.Format("{{\"status\":\"0\",\"msg\":\"error\",\"result\":\"{0}\"}}", "对应的玻璃编码为空");
        //                return result;
        //            }
        //            else if (string.IsNullOrEmpty(SearchTable.DefaultView[0]["Code_blCode"].ToString()))
        //            {
        //                result = string.Format("{{\"status\":\"0\",\"msg\":\"error\",\"result\":\"{0}\"}}", "对应的背光编码为空");
        //                return result;
        //            }
        //            if (SearchTable.DefaultView[0]["Code_lineCode"].ToString() != CheckResult.LINE_CODE)
        //            {
        //                result = string.Format("{{\"status\":\"0\",\"msg\":\"error\",\"result\":\"{0}\"}}", "检测到产品生产产线与当前喷码产线不一致");
        //                return result;
        //            }

        //            #region 如果当前玻璃对应的工单信息与系统缓存的工单信息不一致。则需要重新初始化检测规则和工序链信息
        //            if (CheckResult.PRODUCT_CODE != SearchTable.DefaultView[0]["Code_productCode"].ToString() || CheckResult.MATERIAL_CODE != SearchTable.DefaultView[0]["Code_materialCode"].ToString())
        //            {
        //                //初始化工单产品对应的工序链信息
        //                YJ.Log.FileLog.log.Debug("当前玻璃对应的工单信息与系统缓存的工单信息不一致,需要重新初始化检测规则和工序链信息");
        //                YJ.Log.FileLog.log.Debug("初始化前工序链" + CheckResult.PROCESSLINE_CODE + "  初始化前工单" + CheckResult.PRODUCT_CODE + "  初始化前产品编号" + CheckResult.MATERIAL_CODE);
        //                string ProcessLineString = "exec [Service_InitMaterialProcessLineNew] 'ProcessKey','" + SearchTable.DefaultView[0]["Code_lineCode"].ToString() + "','" + SearchTable.DefaultView[0]["Code_productCode"].ToString() + "'";
        //                DataSet ProcessLineDataSet = Conn.ExecuteDataSet(ProcessLineString, "Base");
        //                if (ProcessLineDataSet.Tables.Count == 2)
        //                {
        //                    processLineView = ProcessLineDataSet.Tables[0].DefaultView;
        //                }
        //                if (processLineView.Count > 0)
        //                {
        //                    CheckResult.MATERIAL_CODE = processLineView[0]["materialCode"].ToString();
        //                    CheckResult.PRODUCT_CODE = processLineView[0]["productCode"].ToString();
        //                    CheckResult.PROCESSLINE_VIEW = processLineView;
        //                }
        //                else
        //                {
        //                    result = string.Format("{{\"status\":\"0\",\"msg\":\"error\",\"result\":\"{0}\"}}", "当前产品工单信息与系统默认信息不匹配,重新初始化工序链信息时出现错误");
        //                    return result;
        //                }
        //                int rowIndex = 0;
        //                CheckResult.PROCESSLINE_VIEW.Sort = "processCode ASC";
        //                try
        //                {
        //                    rowIndex = CheckResult.PROCESSLINE_VIEW.Find("032");
        //                }
        //                catch
        //                {
        //                    result = string.Format("{{\"status\":\"0\",\"msg\":\"error\",\"result\":\"{0}\"}}", "当前产品工单信息与系统默认信息不匹配,重新初始化工序链信息时发现不存在喷码工序");
        //                    return result;
        //                }
        //                if (rowIndex < 0)
        //                {
        //                    result = string.Format("{{\"status\":\"0\",\"msg\":\"error\",\"result\":\"{0}\"}}", "当前产品工单信息与系统默认信息不匹配,重新初始化工序链信息时发现不存在喷码工序");
        //                    return result;
        //                }
        //                CheckResult.PROCESSLINE_CODE = "";
        //                CheckResult.PROCESSCODE_DICTIONARY.Clear();
        //                for (int i = 0; i < CheckResult.PROCESSLINE_VIEW.Count; i++)
        //                {
        //                    if (YJ.Common.Utils.StrToInt(CheckResult.PROCESSLINE_VIEW[i]["processSort"].ToString(), 0) < YJ.Common.Utils.StrToInt(CheckResult.PROCESSLINE_VIEW[rowIndex]["processSort"].ToString(), 0))
        //                    {
        //                        //初始化工序链工序编号集合
        //                        CheckResult.PROCESSLINE_CODE += CheckResult.PROCESSLINE_VIEW[i]["processCode"].ToString() + ",";
        //                        CheckResult.PROCESSCODE_DICTIONARY.Add(CheckResult.PROCESSLINE_VIEW[i]["processCode"].ToString(), CheckResult.PROCESSLINE_VIEW[i]["processName"].ToString());
        //                    }
        //                }
        //                YJ.Log.FileLog.log.Debug("初始化后工序链" + CheckResult.PROCESSLINE_CODE + "  初始化后工单" + CheckResult.PRODUCT_CODE + "  初始化后产品编号" + CheckResult.MATERIAL_CODE);
        //            }
        //            #endregion

        //            //根据工序编号检测是否漏工序（主要检测方式,年前玻璃徐下面的检测）
        //            if (!string.IsNullOrWhiteSpace(SearchTable.DefaultView[0]["Code_LogCode"].ToString()))
        //            {
        //                string ResultString = ComparerLogProcessCode(SearchTable.DefaultView[0]["Code_LogCode"].ToString(), CheckResult.PROCESSLINE_CODE, CheckResult.PROCESSCODE_DICTIONARY);
        //                if (ResultString.Contains("False"))
        //                {
        //                    result = string.Format("{{\"status\":\"0\",\"msg\":\"error\",\"result\":\"{0}\"}}", "系统检测漏工序异常");
        //                    return result;
        //                }
        //                else
        //                {
        //                    if (ResultString != "")
        //                    {
        //                        result = string.Format("{{\"status\":\"0\",\"msg\":\"error\",\"result\":\"{0}\"}}", "检测到漏 " + ResultString + ",尚未处理。");
        //                        return result;
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                result = string.Format("{{\"status\":\"0\",\"msg\":\"error\",\"result\":\"{0}\"}}", "检测到当前产品未经过任何工序。");
        //                return result;
        //            }
        //            //else //根据工序序号检测是否漏工序(由于年前没有Code_LogCode 字段所以需要这种检测,检测缺点：如果工序链发生变化,会导致序号发生变化,导致检测不准确。)
        //            //{
        //            //    string ResultString = ComparerLogProcessNumber(SearchTable.DefaultView[0]["Code_LogNumber"].ToString(), CheckResult.PROCESSLINE_NUMBER, CheckResult.PROCESSNUMBER_DICTIONARY);
        //            //    if (ResultString.Contains("False"))
        //            //    {
        //            //        result = string.Format("{{\"status\":\"0\",\"msg\":\"error\",\"result\":\"{0}\"}}", "系统检测漏工序异常");
        //            //        return result;
        //            //    }
        //            //    else
        //            //    {
        //            //        if (ResultString != "")
        //            //        {
        //            //            result = string.Format("{{\"status\":\"0\",\"msg\":\"error\",\"result\":\"{0}\"}}", "检测到漏 " + ResultString + ",尚未处理。");
        //            //            return result;
        //            //        }
        //            //    }
        //            //}
        //            if (string.IsNullOrEmpty(SearchTable.DefaultView[0]["Code_intactCode"].ToString()))
        //            {
        //                ////返回条码信息
        //                //CodeNumber = GetSNCode(SearchTable, SearchTable.DefaultView[0]["Code_cusCode"].ToString());
        //                //if (CodeNumber.Contains("err"))
        //                //{
        //                //    result = string.Format("{{\"status\":\"0\",\"msg\":\"error\",\"result\":\"{0}\"}}", CodeNumber);
        //                //}
        //                //else
        //                //{
        //                //    result = string.Format("{{\"status\":\"1\",\"msg\":\"success\",\"result\":\"{0}\",\"hwmaterielcode\":\"{1}\",\"lcdcode\":\"{2}\",\"tpcode\":\"{3}\",\"blcode\":\"{4}\"}}", CodeNumber, SearchTable.DefaultView[0]["Code_cusCode"].ToString(), SearchTable.DefaultView[0]["Code_glassCode"].ToString(), SearchTable.DefaultView[0]["Code_tpCode"].ToString(), SearchTable.DefaultView[0]["Code_blCode"].ToString());
        //                //    YJ.Log.FileLog.log.Debug(result);
        //                //}
        //                result = string.Format("{{\"status\":\"0\",\"msg\":\"error\",\"result\":\"{0}\"}}", "当前产品未生成成品二维码");
        //                return result;
        //            }
        //            else
        //            {
        //                ////重新生成条码并返回
        //                //CodeNumber = GetSNCode(SearchTable, SearchTable.DefaultView[0]["Code_cusCode"].ToString());
        //                //if (CodeNumber.Contains("err"))
        //                //{
        //                //    result = string.Format("{{\"status\":\"0\",\"msg\":\"error\",\"result\":\"{0}\"}}", CodeNumber);
        //                //}
        //                //else
        //                //{
        //                //    result = string.Format("{{\"status\":\"1\",\"msg\":\"success\",\"result\":\"{0}\",\"hwmaterielcode\":\"{1}\",\"lcdcode\":\"{2}\",\"tpcode\":\"{3}\",\"blcode\":\"{4}\"}}", CodeNumber, SearchTable.DefaultView[0]["Code_cusCode"].ToString(), SearchTable.DefaultView[0]["Code_glassCode"].ToString(), SearchTable.DefaultView[0]["Code_tpCode"].ToString(), SearchTable.DefaultView[0]["Code_blCode"].ToString());
        //                //    YJ.Log.FileLog.log.Debug(result);
        //                //}
        //                result = string.Format("{{\"status\":\"1\",\"msg\":\"success\",\"result\":\"{0}\",\"hwmaterielcode\":\"{1}\",\"lcdcode\":\"{2}\",\"tpcode\":\"{3}\",\"blcode\":\"{4}\"}}", SearchTable.DefaultView[0]["Code_intactCode"].ToString(), SearchTable.DefaultView[0]["Code_cusCode"].ToString(), SearchTable.DefaultView[0]["Code_glassCode"].ToString(), SearchTable.DefaultView[0]["Code_tpCode"].ToString(), SearchTable.DefaultView[0]["Code_blCode"].ToString());
        //                YJ.Log.FileLog.log.Debug(result);
        //            }
        //        }
        //        return result;
        //    }
        //    catch (Exception exp)
        //    {
        //        result = string.Format("{{\"status\":\"0\",\"msg\":\"error\",\"result\":\"{0}\"}}", exp.Message);
        //        YJ.Log.FileLog.log.Error("获取物料SN编码失败!", exp);
        //    }
        //    return result;
        //}

//        /// <summary>
//        /// 获取玻璃成品码
//        /// </summary>
//        /// <param name="line">产线编码</param>
//        /// <param name="lcd">LCD编码</param>
//        /// <param name="fpc">FPC编码</param>
//        /// <param name="tp">TP编码</param>
//        /// <param name="bl">背光编码</param>
//        /// <param name="ic">IC编码</param>
//        /// <param name="cus">客户料号</param>
//        /// <returns>成品码</returns>
//        public static string GetSNCode(string line, string lcd, string fpc, string tp, string bl, string ic, string cus)
//        {
//            YJ.Data.SqlServerProvider Conn = new YJ.Data.SqlServerProvider();
//            string ResultCode = "";
//            try
//            {
//                //条码组成 40位 ： 华为物料编码（8位）+模组信息（10位）+玻璃信息（8位）+LCDIC信息（4位）+背光信息（10位）
//                //条码组成 54位 ： 华为物料编码（8位）+TP模组信息（6位）+LCD模组信息(包含全贴合流水号)（10位）+TPCG信息（4位） +TPIC信息（4位）+LCD玻璃信息（8位）+LCDIC信息（3位）+背光信息（10位）+标识位（1位）B

//                //                      8位   
//                string tpCode = "";//TP信息                 6位   40位的没有
//                string txdcode = "";//模组信息(成品编码)    10位
//                string tpCGCode = "";//TP信息CG             4位   40位的没有
//                string tpICCode = "";//TP信息IC             4位   40位的没有
//                string lcdCode = "";//玻璃信息              8位
//                string lcdICCode = "";//玻璃IC信息          3位   40位的是4位
//                string blCode = "";//背光信息               10位
//                string ModuleCode = "";//模组信息（供应商编码改变时用）
//                string EndFlag = "";//末位标志符
               
//                #region 获取模组信息(包含全贴合流水号)10位
//                string sqlString = string.Format(@"EXEC TXD_Get_hwCode_Pro '{0}','{1}','{2}'", fpc, lcd, line);
//                DataView sqlView = Conn.ExecuteDataView(sqlString, "Base");
//                if (sqlView.Count > 0)
//                {
//                    if (sqlView[0]["dataNumber"].ToString().Length != 10)
//                    {
//                        ResultCode = "err:客户料号：" + cus + ",获取模组信息流水码长度错误,默认长度10";
//                        return ResultCode;
//                    }
//                    else
//                    {
//                        txdcode = sqlView[0]["dataNumber"].ToString();
//                    }
//                }
//                else
//                {
//                    return "err:获取模组信息流水码失败";
//                }
//                #endregion
//                string SprayCodeString = string.Format(@"SELECT CustomerCode,CodeType,Module_Supplier,TSC_TPCode,TPCode_Length,TPCode_tpBegin,
//                    TPCode_tpEnd,TSC_TPICCode,TPCode_icBegin,TPCode_icEnd,TSC_TPCGCode,TPCode_cgBegin,TPCode_cgEnd,
//                    LCDCode_Batch,LCDCode_Length,LCDCode_lcdBegin,LCDCode_lcdEnd,LCDCode_lcdBegin2,LCDCode_lcdEnd2,
//                    ICCode_Batch,ICCode_Length,ICCode_icBegin,ICCode_icEnd,TSC_BLCode,BLCode_Length,BLCode_blBegin,
//                    BLCode_blEnd,BLCode_blBegin2,BLCode_blEnd2,EndFlag,TXD_SprayCodeCheck.SCC_Digit,SCC_Character FROM TXD_SprayCodeConfig LEFT JOIN TXD_SprayCodeCheck ON ID=SCC_TSCTid WHERE CustomerCode='{0}'", cus);
//                DataView SprayCodeView = Conn.ExecuteDataView(SprayCodeString, "Base");
//                if (SprayCodeView.Count > 0)
//                {
//                    string BarType = SprayCodeView[0]["CodeType"].ToString();
//                    if (BarType == "long")//54位成品编码
//                    {
//                        if (string.IsNullOrEmpty(tp))
//                        {
//                            ResultCode = "err:对应的TP编码为空";
//                            return ResultCode;
//                        }
//                        else
//                        {
//                            #region 获取TP编码 配置TP长度26   TPCode_Length    默认26
//                            int TPCode_Length = YJ.Common.Utils.StrToInt(SprayCodeView[0]["TPCode_Length"].ToString(), 0);
//                            if (tp.Length < TPCode_Length)
//                            {
//                                ResultCode = "err:客户料号：" + cus + ",TP编码长度不合法,默认最小长度" + TPCode_Length.ToString() + "位";
//                                return ResultCode;
//                            }
//                            else
//                            {
//                                #region 获取TP模组信息（6位）  TPCode_tpBegin 默认8  TPCode_tpEnd  默认6
//                                if (SprayCodeView[0]["TSC_TPCode"].ToString() != "")
//                                {
//                                    tpCode = SprayCodeView[0]["TSC_TPCode"].ToString();
//                                }
//                                else
//                                {
//                                    int TPCode_tpBegin = YJ.Common.Utils.StrToInt(SprayCodeView[0]["TPCode_tpBegin"].ToString(), 0);
//                                    int TPCode_tpEnd = YJ.Common.Utils.StrToInt(SprayCodeView[0]["TPCode_tpEnd"].ToString(), 0);
//                                    tpCode = tp.Substring(TPCode_tpBegin, TPCode_tpEnd);
//                                }
//                                #endregion
//                                #region 获取TPIC信息（4位）    TPCode_icBegin 默认14  TPCode_icEnd 默认4
//                                if (SprayCodeView[0]["TSC_TPICCode"].ToString() != "")
//                                {
//                                    tpICCode = SprayCodeView[0]["TSC_TPICCode"].ToString();
//                                }
//                                else
//                                {
//                                    int TPCode_icBegin = YJ.Common.Utils.StrToInt(SprayCodeView[0]["TPCode_icBegin"].ToString(), 0);
//                                    int TPCode_icEnd = YJ.Common.Utils.StrToInt(SprayCodeView[0]["TPCode_icEnd"].ToString(), 0);
//                                    tpICCode = tp.Substring(TPCode_icBegin, TPCode_icEnd);
//                                }
//                                #endregion
//                                #region 获取TPCG信息（4位）    TPCode_cgBegin 默认18  TPCode_cgEnd 默认4
//                                if (SprayCodeView[0]["TSC_TPCGCode"].ToString() != "")
//                                {
//                                    tpCGCode = SprayCodeView[0]["TSC_TPCGCode"].ToString();
//                                }
//                                else
//                                {
//                                    int TPCode_cgBegin = YJ.Common.Utils.StrToInt(SprayCodeView[0]["TPCode_cgBegin"].ToString(), 0);
//                                    int TPCode_cgEnd = YJ.Common.Utils.StrToInt(SprayCodeView[0]["TPCode_cgEnd"].ToString(), 0);
//                                    tpCGCode = tp.Substring(TPCode_cgBegin, TPCode_cgEnd);
//                                }
//                                #endregion
//                            }
//                            #endregion
//                        }
//                    }
//                    if (BarType == "middle")
//                    {
//                        #region 获取TPCG信息（4位）    TPCode_cgBegin 默认18  TPCode_cgEnd 默认4
//                        if (SprayCodeView[0]["TSC_TPCGCode"].ToString() != "")
//                        {
//                            tpCGCode = SprayCodeView[0]["TSC_TPCGCode"].ToString();
//                        }
//                        else
//                        {
//                            ResultCode = "err:客户料号：" + cus + ",TPcg未维护";
//                            return ResultCode;
//                        }
//                        #endregion
//                    }
//                    #region 获取玻璃信息  配置LCD长度11  LCDCode_Length   默认11
//                    int LCDCode_Length = YJ.Common.Utils.StrToInt(SprayCodeView[0]["LCDCode_Length"].ToString(), 0);
//                    if (lcd.Length < LCDCode_Length)
//                    {
//                        ResultCode = "err:客户料号：" + cus + ",LCD编码长度不合法,默认长度" + LCDCode_Length.ToString() + "位";
//                        return ResultCode;
//                    }
//                    else
//                    {
//                        #region 获取LCD玻璃信息（8位）LCDCode_lcdBegin  LCDCode_lcdEnd
//                        if (!string.IsNullOrEmpty(SprayCodeView[0]["LCDCode_Batch"].ToString()))
//                        {
//                            lcdCode = SprayCodeView[0]["LCDCode_Batch"].ToString();
//                        }
//                        else
//                        {
//                            int LCDCode_lcdBegin = YJ.Common.Utils.StrToInt(SprayCodeView[0]["LCDCode_lcdBegin"].ToString(), 0);
//                            int LCDCode_lcdEnd = YJ.Common.Utils.StrToInt(SprayCodeView[0]["LCDCode_lcdEnd"].ToString(), 0);
//                            int LCDCode_lcdBegin2 = YJ.Common.Utils.StrToInt(SprayCodeView[0]["LCDCode_lcdBegin2"].ToString(), 0);
//                            int LCDCode_lcdEnd2 = YJ.Common.Utils.StrToInt(SprayCodeView[0]["LCDCode_lcdEnd2"].ToString(), 0);
//                            lcdCode = lcd.Substring(LCDCode_lcdBegin, LCDCode_lcdEnd) + "0" + lcd.Substring(LCDCode_lcdBegin2, LCDCode_lcdEnd2);
//                        }
//                        #endregion
//                    }
//                    #endregion
//                    #region 获取LCDIC信息（3位）  ICCode_Length  默认4 ICCode_Batch 默认IC批次
//                    int ICCode_Length = YJ.Common.Utils.StrToInt(SprayCodeView[0]["ICCode_Length"].ToString(), 0);
//                    if (ic.Length < ICCode_Length)
//                    {
//                        lcdICCode = SprayCodeView[0]["ICCode_Batch"].ToString();
//                    }
//                    else
//                    {
//                        if (SprayCodeView[0]["ICCode_Batch"].ToString() != "")
//                        {
//                            lcdICCode = SprayCodeView[0]["ICCode_Batch"].ToString();
//                        }
//                        else
//                        {
//                            //ICCode_icBegin  默认1 ICCode_icEnd 默认3
//                            int ICCode_icBegin = YJ.Common.Utils.StrToInt(SprayCodeView[0]["ICCode_icBegin"].ToString(), 0);
//                            int ICCode_icEnd = YJ.Common.Utils.StrToInt(SprayCodeView[0]["ICCode_icEnd"].ToString(), 0);
//                            lcdICCode = ic.Substring(ICCode_icBegin, ICCode_icEnd);
//                        }
//                    }
//                    #endregion
//                    #region 获取背光信息    BLCode_Length 默认21
//                    int BLCode_Length = YJ.Common.Utils.StrToInt(SprayCodeView[0]["BLCode_Length"].ToString(), 0);
//                    if (bl.Length < BLCode_Length)
//                    {
//                        ResultCode = "err:客户料号：" + cus + ",背光编码长度不合法,默认长度" + BLCode_Length.ToString() + "位";
//                        return ResultCode;
//                    }
//                    else
//                    {
//                        #region 获取背光信息（10位）BLCode_blBegin 默认 0 BLCode_blEnd 默认10  EndFlag 默认B
//                        if (SprayCodeView[0]["TSC_BLCode"].ToString() != "")
//                        {
//                            blCode = SprayCodeView[0]["TSC_BLCode"].ToString();
//                        }
//                        else
//                        {

//                            int BLCode_blBegin = YJ.Common.Utils.StrToInt(SprayCodeView[0]["BLCode_blBegin"].ToString(), 0);
//                            int BLCode_blEnd = YJ.Common.Utils.StrToInt(SprayCodeView[0]["BLCode_blEnd"].ToString(), 0);
//                            int BLCode_blBegin2 = YJ.Common.Utils.StrToInt(SprayCodeView[0]["BLCode_blBegin2"].ToString(), 0);
//                            int BLCode_blEnd2 = YJ.Common.Utils.StrToInt(SprayCodeView[0]["BLCode_blEnd2"].ToString(), 0);
//                            blCode = bl.Substring(BLCode_blBegin, BLCode_blEnd) + bl.Substring(BLCode_blBegin2, BLCode_blEnd2);
//                        }
//                        #endregion
//                    }
//                    #endregion
//                    #region 获取末位标志位
//                    EndFlag = SprayCodeView[0]["EndFlag"].ToString();
//                    #endregion
//                    #region 替换模组信息供应商
//                    if (SprayCodeView[0]["Module_Supplier"].ToString() != "")
//                    {
//                        int SupplierLength = (SprayCodeView[0]["Module_Supplier"].ToString().Trim(' ')).Length;
//                        if (SupplierLength > 10)
//                        {
//                            ResultCode = "err:客户料号：" + cus + ",模组供应商信息长度不合法,默认长度不超过10位";
//                            return ResultCode;
//                        }
//                        else
//                        {
//                            ModuleCode = SprayCodeView[0]["Module_Supplier"].ToString().Trim(' ') + txdcode.Substring(SupplierLength, txdcode.Length - SupplierLength);
//                        }
//                    }
//                    else
//                    {
//                        //检测条码固定位置字符是否一致
//                        ModuleCode = txdcode;
//                    }
//                    #endregion
//                    //检测条码固定位置字符是否一致
//                    if (BarType == "long")
//                    {
//                        #region 根据规则拼接54位码 华为料号(默认8位)+Tp条码(默认6位)+模组信息(默认10位)+TPcg(默认4位)+TPic(默认4位)+Lcd信息(默认8位)+LcdIc(默认3位)+Bl信息(默认10位)+标志位(默认1位)
//                        ResultCode = ComparerSprayCodeRules(cus + tpCode + ModuleCode + tpCGCode + tpICCode + lcdCode + lcdICCode + blCode + EndFlag, SprayCodeView);
//                        #endregion
//                    }
//                    else if (BarType == "middle")
//                    {
//                        #region 根据规则拼接47位码 华为料号(8位)+模组信息(10位)+Lcd信息(8位)+LcdIc(6位)+TPcg(默认4位)+Bl信息(10位)+标志位(1位)
//                        ResultCode = ComparerSprayCodeRules(cus + ModuleCode + lcdCode + lcdICCode + tpCGCode + blCode + EndFlag, SprayCodeView);
//                        #endregion
//                    }
//                    else if (BarType == "short")
//                    {
//                        #region 根据规则拼接40位码 华为料号(默认8位)+模组信息(默认10位)+Lcd信息(默认8位)+LcdIc(默认6位)+Bl信息(默认10位)+标志位(默认1位)
//                        ResultCode = ComparerSprayCodeRules(cus + ModuleCode + lcdCode + lcdICCode + blCode + EndFlag, SprayCodeView);
//                        #endregion
//                    }
//                }
//                else
//                {
//                    ResultCode = "err:客户料号：" + cus + ",尚未配置成品码解析规则,请配置解码规则";
//                    return ResultCode;
//                }
//            }
//            catch (Exception exp)
//            {
//                ResultCode = "error" + exp.Message.ToString();
//            }
//            return ResultCode;
//        }


        /// <summary>
        /// 获取玻璃成品码
        /// </summary>
        /// <param name="line">产线编码</param>
        /// <param name="lcd">LCD编码</param>
        /// <param name="fpc">FPC编码</param>
        /// <param name="tp">TP编码</param>
        /// <param name="bl">背光编码</param>
        /// <param name="ic">IC编码</param>
        /// <param name="cus">客户料号</param>
        /// <returns>成品码</returns>
        public static string GetSNCode(string line, string lcd, string fpc, string tp, string bl, string ic, string cus)
        {
            ////2020-08-07 背光编码来料问题临时处理，o替换成0
            //bl = bl.Substring(0, 1) + bl.Substring(1, 1).Replace('O', '0') + bl.Substring(2, 3) + bl.Substring(5, 1).Replace('O', '0') + bl.Substring(6, bl.Length - 6);
            
            YJ.Data.SqlServerProvider Conn = new YJ.Data.SqlServerProvider();
            string ResultCode = "";
            try
            {
                //条码组成 40位 ： 华为物料编码（8位）+模组信息（10位）+玻璃信息（8位）+LCDIC信息（4位）+背光信息（10位）
                //条码组成 54位 ： 华为物料编码（8位）+TP模组信息（6位）+LCD模组信息(包含全贴合流水号)（10位）+TPCG信息（4位） +TPIC信息（4位）+LCD玻璃信息（8位）+LCDIC信息（3位）+背光信息（10位）+标识位（1位）B

                //                      8位   
                string tpCode = "";//TP信息                 6位   40位的没有
                string txdcode = "";//模组信息(成品编码)    10位
                string tpCGCode = "";//TP信息CG             4位   40位的没有
                string tpICCode = "";//TP信息IC             4位   40位的没有
                string lcdCode = "";//玻璃信息              8位
                string lcdICCode = "";//玻璃IC信息          3位   40位的是4位
                string blCode = "";//背光信息               10位
                string ModuleCode = "";//模组信息（供应商编码改变时用）
                string EndFlag = "";//末位标志符

                //#region 获取模组信息(包含全贴合流水号)10位
                //string sqlString = string.Format(@"EXEC TXD_Get_hwCode_Pro '{0}','{1}','{2}'", fpc, lcd, line);
                //DataView sqlView = Conn.ExecuteDataView(sqlString, "Base");
                //if (sqlView.Count > 0)
                //{
                //    if (sqlView[0]["dataNumber"].ToString().Length != 10)
                //    {
                //        ResultCode = "err:客户料号：" + cus + ",获取模组信息流水码长度错误,默认长度10";
                //        return ResultCode;
                //    }
                //    else
                //    {
                //        txdcode = sqlView[0]["dataNumber"].ToString();
                //    }
                //}
                //else
                //{
                //    return "err:获取模组信息流水码失败";
                //}
                //#endregion
                string SprayCodeString = string.Format(@"SELECT CustomerCode,CodeType,Module_Supplier,TSC_TPCode,TPCode_Length,TPCode_tpBegin,
                    TPCode_tpEnd,TSC_TPICCode,TPCode_icBegin,TPCode_icEnd,TSC_TPCGCode,TPCode_cgBegin,TPCode_cgEnd,
                    LCDCode_Batch,LCDCode_Length,LCDCode_lcdBegin,LCDCode_lcdEnd,LCDCode_lcdBegin2,LCDCode_lcdEnd2,
                    ICCode_Batch,ICCode_Length,ICCode_icBegin,ICCode_icEnd,TSC_BLCode,BLCode_Length,BLCode_blBegin,
                    BLCode_blEnd,BLCode_blBegin2,BLCode_blEnd2,EndFlag,TXD_SprayCodeCheck.SCC_Digit,SCC_Character FROM TXD_SprayCodeConfig LEFT JOIN TXD_SprayCodeCheck ON ID=SCC_TSCTid WHERE CustomerCode='{0}'", cus);
                DataView SprayCodeView = Conn.ExecuteDataView(SprayCodeString, "Base");
                if (SprayCodeView.Count > 0)
                {
                    string BarType = SprayCodeView[0]["CodeType"].ToString();

                    #region 获取21位成品码
                    if (BarType == "short21")//21位成品编码
                    {
                        #region 获取模组信息(包含全贴合流水号)11位
                        string sqlStr = string.Format(@"EXEC TXD_Get_hwCode_Pro_2 '{0}','{1}','{2}'", fpc, lcd, line);
                        DataView sqlVw = Conn.ExecuteDataView(sqlStr, "Base");
                        if (sqlVw.Count > 0)
                        {
                            if (sqlVw[0]["dataNumber"].ToString().Length != 11)
                            {
                                ResultCode = "err:客户料号：" + cus + ",获取模组信息流水码长度错误,默认长度11";
                                return ResultCode;
                            }
                            else
                            {
                                txdcode = sqlVw[0]["dataNumber"].ToString();
                            }
                        }
                        else
                        {
                            return "err:获取模组信息流水码失败";
                        }
                        #endregion

                        #region 获取末位标志位
                        EndFlag = SprayCodeView[0]["EndFlag"].ToString();
                        #endregion

                        string QrCode = cus + txdcode + EndFlag;
                        return QrCode;
                    }
                    #endregion 获取21位成品码

                    #region 获取模组信息(包含全贴合流水号)10位
                    string sqlString = string.Format(@"EXEC TXD_Get_hwCode_Pro '{0}','{1}','{2}'", fpc, lcd, line);
                    DataView sqlView = Conn.ExecuteDataView(sqlString, "Base");
                    if (sqlView.Count > 0)
                    {
                        if (sqlView[0]["dataNumber"].ToString().Length != 10)
                        {
                            ResultCode = "err:客户料号：" + cus + ",获取模组信息流水码长度错误,默认长度10";
                            return ResultCode;
                        }
                        else
                        {
                            txdcode = sqlView[0]["dataNumber"].ToString();
                        }
                    }
                    else
                    {
                        return "err:获取模组信息流水码失败";
                    }
                    #endregion

                    if (BarType == "long")//54位成品编码
                    {
                        if (string.IsNullOrEmpty(tp))
                        {
                            ResultCode = "err:对应的TP编码为空";
                            return ResultCode;
                        }
                        else
                        {
                            #region 获取TP编码 配置TP长度26   TPCode_Length    默认26
                            int TPCode_Length = YJ.Common.Utils.StrToInt(SprayCodeView[0]["TPCode_Length"].ToString(), 0);
                            if (tp.Length < TPCode_Length)
                            {
                                ResultCode = "err:客户料号：" + cus + ",TP编码长度不合法,默认最小长度" + TPCode_Length.ToString() + "位";
                                return ResultCode;
                            }
                            else
                            {
                                #region 获取TP模组信息（6位）  TPCode_tpBegin 默认8  TPCode_tpEnd  默认6
                                if (SprayCodeView[0]["TSC_TPCode"].ToString() != "")
                                {
                                    tpCode = SprayCodeView[0]["TSC_TPCode"].ToString();
                                }
                                else
                                {
                                    int TPCode_tpBegin = YJ.Common.Utils.StrToInt(SprayCodeView[0]["TPCode_tpBegin"].ToString(), 0);
                                    int TPCode_tpEnd = YJ.Common.Utils.StrToInt(SprayCodeView[0]["TPCode_tpEnd"].ToString(), 0);
                                    tpCode = tp.Substring(TPCode_tpBegin, TPCode_tpEnd);
                                }
                                #endregion
                                #region 获取TPIC信息（4位）    TPCode_icBegin 默认14  TPCode_icEnd 默认4
                                if (SprayCodeView[0]["TSC_TPICCode"].ToString() != "")
                                {
                                    tpICCode = SprayCodeView[0]["TSC_TPICCode"].ToString();
                                }
                                else
                                {
                                    int TPCode_icBegin = YJ.Common.Utils.StrToInt(SprayCodeView[0]["TPCode_icBegin"].ToString(), 0);
                                    int TPCode_icEnd = YJ.Common.Utils.StrToInt(SprayCodeView[0]["TPCode_icEnd"].ToString(), 0);
                                    tpICCode = tp.Substring(TPCode_icBegin, TPCode_icEnd);
                                }
                                #endregion
                                #region 获取TPCG信息（4位）    TPCode_cgBegin 默认18  TPCode_cgEnd 默认4
                                if (SprayCodeView[0]["TSC_TPCGCode"].ToString() != "")
                                {
                                    tpCGCode = SprayCodeView[0]["TSC_TPCGCode"].ToString();
                                }
                                else
                                {
                                    int TPCode_cgBegin = YJ.Common.Utils.StrToInt(SprayCodeView[0]["TPCode_cgBegin"].ToString(), 0);
                                    int TPCode_cgEnd = YJ.Common.Utils.StrToInt(SprayCodeView[0]["TPCode_cgEnd"].ToString(), 0);
                                    tpCGCode = tp.Substring(TPCode_cgBegin, TPCode_cgEnd);
                                }
                                #endregion
                            }
                            #endregion
                        }
                    }
                    if (BarType == "middle")
                    {
                        #region 获取TPCG信息（4位）    TPCode_cgBegin 默认18  TPCode_cgEnd 默认4
                        if (SprayCodeView[0]["TSC_TPCGCode"].ToString() != "")
                        {
                            tpCGCode = SprayCodeView[0]["TSC_TPCGCode"].ToString();
                        }
                        else
                        {
                            ResultCode = "err:客户料号：" + cus + ",TPcg未维护";
                            return ResultCode;
                        }
                        #endregion
                    }
                    #region 获取玻璃信息  配置LCD长度11  LCDCode_Length   默认11
                    int LCDCode_Length = YJ.Common.Utils.StrToInt(SprayCodeView[0]["LCDCode_Length"].ToString(), 0);
                    if (lcd.Length < LCDCode_Length)
                    {
                        ResultCode = "err:客户料号：" + cus + ",LCD编码长度不合法,默认长度" + LCDCode_Length.ToString() + "位";
                        return ResultCode;
                    }
                    else
                    {
                        #region 获取LCD玻璃信息（8位）LCDCode_lcdBegin  LCDCode_lcdEnd
                        if (!string.IsNullOrEmpty(SprayCodeView[0]["LCDCode_Batch"].ToString()))
                        {
                            lcdCode = SprayCodeView[0]["LCDCode_Batch"].ToString();
                        }
                        else
                        {
                            int LCDCode_lcdBegin = YJ.Common.Utils.StrToInt(SprayCodeView[0]["LCDCode_lcdBegin"].ToString(), 0);
                            int LCDCode_lcdEnd = YJ.Common.Utils.StrToInt(SprayCodeView[0]["LCDCode_lcdEnd"].ToString(), 0);
                            int LCDCode_lcdBegin2 = YJ.Common.Utils.StrToInt(SprayCodeView[0]["LCDCode_lcdBegin2"].ToString(), 0);
                            int LCDCode_lcdEnd2 = YJ.Common.Utils.StrToInt(SprayCodeView[0]["LCDCode_lcdEnd2"].ToString(), 0);
                            lcdCode = lcd.Substring(LCDCode_lcdBegin, LCDCode_lcdEnd) + "0" + lcd.Substring(LCDCode_lcdBegin2, LCDCode_lcdEnd2);
                        }
                        #endregion
                    }
                    #endregion
                    #region 获取LCDIC信息（3位）  ICCode_Length  默认4 ICCode_Batch 默认IC批次
                    int ICCode_Length = YJ.Common.Utils.StrToInt(SprayCodeView[0]["ICCode_Length"].ToString(), 0);
                    if (ic.Length < ICCode_Length)
                    {
                        lcdICCode = SprayCodeView[0]["ICCode_Batch"].ToString();
                    }
                    else
                    {
                        if (SprayCodeView[0]["ICCode_Batch"].ToString() != "")
                        {
                            lcdICCode = SprayCodeView[0]["ICCode_Batch"].ToString();
                        }
                        else
                        {
                            //ICCode_icBegin  默认1 ICCode_icEnd 默认3
                            int ICCode_icBegin = YJ.Common.Utils.StrToInt(SprayCodeView[0]["ICCode_icBegin"].ToString(), 0);
                            int ICCode_icEnd = YJ.Common.Utils.StrToInt(SprayCodeView[0]["ICCode_icEnd"].ToString(), 0);
                            lcdICCode = ic.Substring(ICCode_icBegin, ICCode_icEnd);
                        }
                    }
                    #endregion
                    #region 获取背光信息    BLCode_Length 默认21
                    int BLCode_Length = YJ.Common.Utils.StrToInt(SprayCodeView[0]["BLCode_Length"].ToString(), 0);
                    if (bl.Length < BLCode_Length)
                    {
                        ResultCode = "err:客户料号：" + cus + ",背光编码长度不合法,默认长度" + BLCode_Length.ToString() + "位";
                        return ResultCode;
                    }
                    else
                    {
                        #region 获取背光信息（10位）BLCode_blBegin 默认 0 BLCode_blEnd 默认10  EndFlag 默认B
                        if (SprayCodeView[0]["TSC_BLCode"].ToString() != "")
                        {
                            blCode = SprayCodeView[0]["TSC_BLCode"].ToString();
                        }
                        else
                        {

                            int BLCode_blBegin = YJ.Common.Utils.StrToInt(SprayCodeView[0]["BLCode_blBegin"].ToString(), 0);
                            int BLCode_blEnd = YJ.Common.Utils.StrToInt(SprayCodeView[0]["BLCode_blEnd"].ToString(), 0);
                            int BLCode_blBegin2 = YJ.Common.Utils.StrToInt(SprayCodeView[0]["BLCode_blBegin2"].ToString(), 0);
                            int BLCode_blEnd2 = YJ.Common.Utils.StrToInt(SprayCodeView[0]["BLCode_blEnd2"].ToString(), 0);
                            blCode = bl.Substring(BLCode_blBegin, BLCode_blEnd) + bl.Substring(BLCode_blBegin2, BLCode_blEnd2);
                        }
                        #endregion
                    }
                    #endregion
                    #region 获取末位标志位
                    EndFlag = SprayCodeView[0]["EndFlag"].ToString();
                    #endregion
                    #region 替换模组信息供应商
                    if (SprayCodeView[0]["Module_Supplier"].ToString() != "")
                    {
                        int SupplierLength = (SprayCodeView[0]["Module_Supplier"].ToString().Trim(' ')).Length;
                        if (SupplierLength > 10)
                        {
                            ResultCode = "err:客户料号：" + cus + ",模组供应商信息长度不合法,默认长度不超过10位";
                            return ResultCode;
                        }
                        else
                        {
                            ModuleCode = SprayCodeView[0]["Module_Supplier"].ToString().Trim(' ') + txdcode.Substring(SupplierLength, txdcode.Length - SupplierLength);
                        }
                    }
                    else
                    {
                        //检测条码固定位置字符是否一致
                        ModuleCode = txdcode;
                    }
                    #endregion
                    //检测条码固定位置字符是否一致
                    if (BarType == "long")
                    {
                        #region 根据规则拼接54位码 华为料号(默认8位)+Tp条码(默认6位)+模组信息(默认10位)+TPcg(默认4位)+TPic(默认4位)+Lcd信息(默认8位)+LcdIc(默认3位)+Bl信息(默认10位)+标志位(默认1位)
                        ResultCode = ComparerSprayCodeRules(cus + tpCode + ModuleCode + tpCGCode + tpICCode + lcdCode + lcdICCode + blCode + EndFlag, SprayCodeView);
                        #endregion
                    }
                    else if (BarType == "middle")
                    {
                        #region 根据规则拼接47位码 华为料号(8位)+模组信息(10位)+Lcd信息(8位)+LcdIc(6位)+TPcg(默认4位)+Bl信息(10位)+标志位(1位)
                        ResultCode = ComparerSprayCodeRules(cus + ModuleCode + lcdCode + lcdICCode + tpCGCode + blCode + EndFlag, SprayCodeView);
                        #endregion
                    }
                    else if (BarType == "short")
                    {
                        #region 根据规则拼接40位码 华为料号(默认8位)+模组信息(默认10位)+Lcd信息(默认8位)+LcdIc(默认6位)+Bl信息(默认10位)+标志位(默认1位)
                        ResultCode = ComparerSprayCodeRules(cus + ModuleCode + lcdCode + lcdICCode + blCode + EndFlag, SprayCodeView);
                        #endregion
                    }
                }
                else
                {
                    ResultCode = "err:客户料号：" + cus + ",尚未配置成品码解析规则,请配置解码规则";
                    return ResultCode;
                }
            }
            catch (Exception exp)
            {
                ResultCode = "error" + exp.Message.ToString();
            }
            return ResultCode;
        }
        ////去物料表通过成品编码获取华为料号
        //// DataTable dt = HBASEHelper.GetDataTable(sn);
        //ProductionRouteInfo production = new ModuleMes().GetProductionRouteInfo(sn);
        //if (production != null && !string.IsNullOrEmpty(production.Process_LCDCode))
        //{
        //    if (string.IsNullOrEmpty(production.Process_QRCode))
        //    {

        //        //查询华为料号， 维护在：成品物料明细-描述 栏位。
        //        string strsql = string.Format("SELECT x.* FROM YJ_TXDMES.dbo.Store_Material x WHERE x.SM_nbCode = ('{0}')", production.Process_finishesCode);
        //        DataTable dt = DbHelper.ExecuteTable(strsql, null);
        //        if (dt != null && dt.Rows.Count > 0)
        //        {
        //            string hwcode = dt.Rows[0]["SM_Describe"].ToString();
        //            if (!string.IsNullOrEmpty(hwcode)) //范例： 2302TNNH_1A
        //            {
        //                var codes = hwcode.Split('_');
        //                if (codes.Length > 1)
        //                {
        //                    //获取动态码流水号等。
        //                    string strp2 = HBASEHelper.GetHWcode(production.Process_FPCCode, production.Process_LCDCode, production.Process_productLineCode);
        //                    if (!string.IsNullOrEmpty(strp2))
        //                    {
        //                        string QR = codes[0] + strp2 + codes[1];
        //                        Msg("生成成品码：" + QR);
        //                        //成品码绑定，FPC+QR , 工序序号固定: 039 ,用虚拟IP。
        //                        if (!string.IsNullOrEmpty(vrip))
        //                        {
        //                            //条码绑定
        //                            SentoMes(true, "#", vrip, production.Process_LCDCode, production.Process_FPCCode, QR);
        //                            //发送指令
        //                            return SetCmd("01", Cls.DataHelper.ASCIIToHEX(QR), serialPort1);

        //public static string Get_hwCode_Pro(string fpcCode, string glassCode, string lineCode)
        //{
        //    string result = string.Empty;
        //    #region 获取模组信息(包含全贴合流水号)11位
        //    string sqlString = string.Format(@"EXEC TXD_Get_hwCode_Pro_2 '{0}','{1}','{2}'", fpcCode, glassCode, lineCode);
        //    //Log4Helper.Info("GetShipCode_sql:" + sqlString);
        //    // DataView sqlView = Conn.ExecuteDataView(sqlString, "Base");
        //    DataView sqlView = DbHelper.ExecuteTable(sqlString, null).DefaultView;
        //    if (sqlView.Count > 0)
        //    {
        //        if (sqlView[0]["dataNumber"].ToString().Length != 11)
        //        {
        //            Log4Helper.Info("err:产品：" + glassCode + ",获取模组信息流水码长度错误,默认长度11");
        //            return string.Empty;
        //        }
        //        else
        //        {
        //            return sqlView[0]["dataNumber"].ToString();
        //        }
        //    }
        //    else
        //    {
        //        return result;
        //    }
        //    #endregion
        //}




        public static string GetSNCode(DataTable dt, string HWMaterialCode)
        {
            YJ.Data.SqlServerProvider Conn = new YJ.Data.SqlServerProvider();
            string ResultCode = "";
            try
            {
                //条码组成 40位 ： 华为物料编码（8位）+模组信息（10位）+玻璃信息（8位）+LCDIC信息（4位）+背光信息（10位）
                //条码组成 54位 ： 华为物料编码（8位）+TP模组信息（6位）+LCD模组信息(包含全贴合流水号)（10位）+TPCG信息（4位） +TPIC信息（4位）+LCD玻璃信息（8位）+LCDIC信息（3位）+背光信息（10位）+标识位（1位）B

                //                      8位   
                string tpCode = "";//TP信息                 6位   40位的没有
                string txdcode = "";//模组信息(成品编码)    10位
                string tpCGCode = "";//TP信息CG             4位   40位的没有
                string tpICCode = "";//TP信息IC             4位   40位的没有
                string lcdCode = "";//玻璃信息              8位
                string lcdICCode = "";//玻璃IC信息          3位   40位的是4位
                string blCode = "";//背光信息               10位
                string ModuleCode = "";//模组信息（供应商编码改变时用）
                string EndFlag = "";//末位标志符
                DataView SearchView = new DataView();
                SearchView = dt.DefaultView;
                #region 获取模组信息(包含全贴合流水号)10位
                string sqlString = string.Format(@"EXEC TXD_Get_hwCode_Pro '{0}','{1}','{2}'", SearchView[0]["Code_fpcCode"].ToString(), SearchView[0]["Code_glassCode"].ToString(), SearchView[0]["Code_lineCode"].ToString());
                DataView sqlView = Conn.ExecuteDataView(sqlString, "Base");
                if (sqlView.Count > 0)
                {
                    if (sqlView[0]["dataNumber"].ToString().Length != 10)
                    {
                        ResultCode = "err:客户料号：" + HWMaterialCode + ",获取模组信息流水码长度错误,默认长度10";
                        return ResultCode;
                    }
                    else
                    {
                        txdcode = sqlView[0]["dataNumber"].ToString();
                    }
                }
                else
                {
                    return "err:获取模组信息流水码失败";
                }
                #endregion
                string SprayCodeString = string.Format(@"SELECT CustomerCode,CodeType,Module_Supplier,TSC_TPCode,TPCode_Length,TPCode_tpBegin,
                    TPCode_tpEnd,TSC_TPICCode,TPCode_icBegin,TPCode_icEnd,TSC_TPCGCode,TPCode_cgBegin,TPCode_cgEnd,
                    LCDCode_Batch,LCDCode_Length,LCDCode_lcdBegin,LCDCode_lcdEnd,LCDCode_lcdBegin2,LCDCode_lcdEnd2,
                    ICCode_Batch,ICCode_Length,ICCode_icBegin,ICCode_icEnd,TSC_BLCode,BLCode_Length,BLCode_blBegin,
                    BLCode_blEnd,BLCode_blBegin2,BLCode_blEnd2,EndFlag,TXD_SprayCodeCheck.SCC_Digit,SCC_Character FROM TXD_SprayCodeConfig LEFT JOIN TXD_SprayCodeCheck ON ID=SCC_TSCTid WHERE CustomerCode='{0}'", HWMaterialCode);
                DataView SprayCodeView = Conn.ExecuteDataView(SprayCodeString, "Base");
                if (SprayCodeView.Count > 0)
                {
                    string BarType = SprayCodeView[0]["CodeType"].ToString();
                    if (BarType == "long")//54位成品编码
                    {
                        if (string.IsNullOrEmpty(SearchView[0]["Code_tpCode"].ToString()))
                        {
                            ResultCode = "err:对应的TP编码为空";
                            return ResultCode;
                        }
                        else
                        {
                            #region 获取TP编码 配置TP长度26   TPCode_Length    默认26
                            int TPCode_Length = YJ.Common.Utils.StrToInt(SprayCodeView[0]["TPCode_Length"].ToString(), 0);
                            if (SearchView[0]["Code_tpCode"].ToString().Length < TPCode_Length)
                            {
                                ResultCode = "err:客户料号：" + HWMaterialCode + ",TP编码长度不合法,默认最小长度" + TPCode_Length.ToString() + "位";
                                return ResultCode;
                            }
                            else
                            {
                                #region 获取TP模组信息（6位）  TPCode_tpBegin 默认8  TPCode_tpEnd  默认6
                                if (SprayCodeView[0]["TSC_TPCode"].ToString() != "")
                                {
                                    tpCode = SprayCodeView[0]["TSC_TPCode"].ToString();
                                }
                                else
                                {
                                    int TPCode_tpBegin = YJ.Common.Utils.StrToInt(SprayCodeView[0]["TPCode_tpBegin"].ToString(), 0);
                                    int TPCode_tpEnd = YJ.Common.Utils.StrToInt(SprayCodeView[0]["TPCode_tpEnd"].ToString(), 0);
                                    tpCode = SearchView[0]["Code_tpCode"].ToString().Substring(TPCode_tpBegin, TPCode_tpEnd);
                                }
                                #endregion
                                #region 获取TPIC信息（4位）    TPCode_icBegin 默认14  TPCode_icEnd 默认4
                                if (SprayCodeView[0]["TSC_TPICCode"].ToString() != "")
                                {
                                    tpICCode = SprayCodeView[0]["TSC_TPICCode"].ToString();
                                }
                                else
                                {
                                    int TPCode_icBegin = YJ.Common.Utils.StrToInt(SprayCodeView[0]["TPCode_icBegin"].ToString(), 0);
                                    int TPCode_icEnd = YJ.Common.Utils.StrToInt(SprayCodeView[0]["TPCode_icEnd"].ToString(), 0);
                                    tpICCode = SearchView[0]["Code_tpCode"].ToString().Substring(TPCode_icBegin, TPCode_icEnd);
                                }
                                #endregion
                                #region 获取TPCG信息（4位）    TPCode_cgBegin 默认18  TPCode_cgEnd 默认4
                                if (SprayCodeView[0]["TSC_TPCGCode"].ToString() != "")
                                {
                                    tpCGCode = SprayCodeView[0]["TSC_TPCGCode"].ToString();
                                }
                                else
                                {
                                    int TPCode_cgBegin = YJ.Common.Utils.StrToInt(SprayCodeView[0]["TPCode_cgBegin"].ToString(), 0);
                                    int TPCode_cgEnd = YJ.Common.Utils.StrToInt(SprayCodeView[0]["TPCode_cgEnd"].ToString(), 0);
                                    tpCGCode = SearchView[0]["Code_tpCode"].ToString().Substring(TPCode_cgBegin, TPCode_cgEnd);
                                }
                                #endregion
                            }
                            #endregion
                        }
                    }
                    if (BarType == "middle")
                    {
                        #region 获取TPCG信息（4位）    TPCode_cgBegin 默认18  TPCode_cgEnd 默认4
                        if (SprayCodeView[0]["TSC_TPCGCode"].ToString() != "")
                        {
                            tpCGCode = SprayCodeView[0]["TSC_TPCGCode"].ToString();
                        }
                        else
                        {
                            ResultCode = "err:客户料号：" + HWMaterialCode + ",TPcg未维护";
                            return ResultCode;
                        }
                        #endregion
                    }
                    #region 获取玻璃信息  配置LCD长度11  LCDCode_Length   默认11
                    int LCDCode_Length = YJ.Common.Utils.StrToInt(SprayCodeView[0]["LCDCode_Length"].ToString(), 0);
                    if (SearchView[0]["Code_glassCode"].ToString().Length < LCDCode_Length)
                    {
                        ResultCode = "err:客户料号：" + HWMaterialCode + ",LCD编码长度不合法,默认长度" + LCDCode_Length.ToString() + "位";
                        return ResultCode;
                    }
                    else
                    {
                        #region 获取LCD玻璃信息（8位）LCDCode_lcdBegin  LCDCode_lcdEnd
                        if (!string.IsNullOrEmpty(SprayCodeView[0]["LCDCode_Batch"].ToString()))
                        {
                            lcdCode = SprayCodeView[0]["LCDCode_Batch"].ToString();
                        }
                        else
                        {
                            int LCDCode_lcdBegin = YJ.Common.Utils.StrToInt(SprayCodeView[0]["LCDCode_lcdBegin"].ToString(), 0);
                            int LCDCode_lcdEnd = YJ.Common.Utils.StrToInt(SprayCodeView[0]["LCDCode_lcdEnd"].ToString(), 0);
                            int LCDCode_lcdBegin2 = YJ.Common.Utils.StrToInt(SprayCodeView[0]["LCDCode_lcdBegin2"].ToString(), 0);
                            int LCDCode_lcdEnd2 = YJ.Common.Utils.StrToInt(SprayCodeView[0]["LCDCode_lcdEnd2"].ToString(), 0);
                            lcdCode = SearchView[0]["Code_glassCode"].ToString().Substring(LCDCode_lcdBegin, LCDCode_lcdEnd) + "0" + SearchView[0]["Code_glassCode"].ToString().Substring(LCDCode_lcdBegin2, LCDCode_lcdEnd2);
                        }
                        #endregion
                    }
                    #endregion
                    #region 获取LCDIC信息（3位）  ICCode_Length  默认4 ICCode_Batch 默认IC批次
                    int ICCode_Length = YJ.Common.Utils.StrToInt(SprayCodeView[0]["ICCode_Length"].ToString(), 0);
                    if (SearchView[0]["Code_icCode"].ToString().Length < ICCode_Length)
                    {
                        lcdICCode = SprayCodeView[0]["ICCode_Batch"].ToString();
                    }
                    else
                    {
                        if (SprayCodeView[0]["ICCode_Batch"].ToString() != "")
                        {
                            lcdICCode = SprayCodeView[0]["ICCode_Batch"].ToString();
                        }
                        else
                        {
                            //ICCode_icBegin  默认1 ICCode_icEnd 默认3
                            int ICCode_icBegin = YJ.Common.Utils.StrToInt(SprayCodeView[0]["ICCode_icBegin"].ToString(), 0);
                            int ICCode_icEnd = YJ.Common.Utils.StrToInt(SprayCodeView[0]["ICCode_icEnd"].ToString(), 0);
                            lcdICCode = SearchView[0]["Code_icCode"].ToString().Substring(ICCode_icBegin, ICCode_icEnd);
                        }
                    }
                    #endregion
                    #region 获取背光信息    BLCode_Length 默认21
                    int BLCode_Length = YJ.Common.Utils.StrToInt(SprayCodeView[0]["BLCode_Length"].ToString(), 0);
                    if (SearchView[0]["Code_blCode"].ToString().Length < BLCode_Length)
                    {
                        ResultCode = "err:客户料号：" + HWMaterialCode + ",背光编码长度不合法,默认长度" + BLCode_Length.ToString() + "位";
                        return ResultCode;
                    }
                    else
                    {
                        #region 获取背光信息（10位）BLCode_blBegin 默认 0 BLCode_blEnd 默认10  EndFlag 默认B
                        if (SprayCodeView[0]["TSC_BLCode"].ToString() != "")
                        {
                            blCode = SprayCodeView[0]["TSC_BLCode"].ToString();
                        }
                        else
                        {

                            int BLCode_blBegin = YJ.Common.Utils.StrToInt(SprayCodeView[0]["BLCode_blBegin"].ToString(), 0);
                            int BLCode_blEnd = YJ.Common.Utils.StrToInt(SprayCodeView[0]["BLCode_blEnd"].ToString(), 0);
                            int BLCode_blBegin2 = YJ.Common.Utils.StrToInt(SprayCodeView[0]["BLCode_blBegin2"].ToString(), 0);
                            int BLCode_blEnd2 = YJ.Common.Utils.StrToInt(SprayCodeView[0]["BLCode_blEnd2"].ToString(), 0);
                            blCode = SearchView[0]["Code_blCode"].ToString().Substring(BLCode_blBegin, BLCode_blEnd) + SearchView[0]["Code_blCode"].ToString().Substring(BLCode_blBegin2, BLCode_blEnd2);
                        }
                        #endregion
                    }
                    #endregion
                    #region 获取末位标志位
                    EndFlag = SprayCodeView[0]["EndFlag"].ToString();
                    #endregion
                    #region 替换模组信息供应商
                    if (SprayCodeView[0]["Module_Supplier"].ToString() != "")
                    {
                        int SupplierLength = (SprayCodeView[0]["Module_Supplier"].ToString().Trim(' ')).Length;
                        if (SupplierLength > 10)
                        {
                            ResultCode = "err:客户料号：" + HWMaterialCode + ",模组供应商信息长度不合法,默认长度不超过10位";
                            return ResultCode;
                        }
                        else
                        {
                            ModuleCode = SprayCodeView[0]["Module_Supplier"].ToString().Trim(' ') + txdcode.Substring(SupplierLength, txdcode.Length - SupplierLength);
                        }
                    }
                    else
                    {
                        //检测条码固定位置字符是否一致
                        ModuleCode = txdcode;
                    }
                    #endregion
                    //检测条码固定位置字符是否一致
                    if (BarType == "long")
                    {
                        #region 根据规则拼接54位码 华为料号(默认8位)+Tp条码(默认6位)+模组信息(默认10位)+TPcg(默认4位)+TPic(默认4位)+Lcd信息(默认8位)+LcdIc(默认3位)+Bl信息(默认10位)+标志位(默认1位)
                        ResultCode = ComparerSprayCodeRules(HWMaterialCode + tpCode + ModuleCode + tpCGCode + tpICCode + lcdCode + lcdICCode + blCode + EndFlag, SprayCodeView);
                        #endregion
                    }
                    else if (BarType == "middle")
                    {
                        #region 根据规则拼接47位码 华为料号(8位)+模组信息(10位)+Lcd信息(8位)+LcdIc(6位)+TPcg(默认4位)+Bl信息(10位)+标志位(1位)
                        ResultCode = ComparerSprayCodeRules(HWMaterialCode + ModuleCode + lcdCode + lcdICCode + tpCGCode + blCode + EndFlag, SprayCodeView);
                        #endregion
                    }
                    else if (BarType == "short")
                    {
                        #region 根据规则拼接40位码 华为料号(默认8位)+模组信息(默认10位)+Lcd信息(默认8位)+LcdIc(默认6位)+Bl信息(默认10位)+标志位(默认1位)
                        ResultCode = ComparerSprayCodeRules(HWMaterialCode + ModuleCode + lcdCode + lcdICCode + blCode + EndFlag, SprayCodeView);
                        #endregion
                    }
                }
                else
                {
                    ResultCode = "err:客户料号：" + HWMaterialCode + ",尚未配置成品码解析规则,请配置解码规则";
                    return ResultCode;
                }
            }
            catch (Exception exp)
            {
                ResultCode = "error" + exp.Message.ToString();
            }
            return ResultCode;
        }

//        /// <summary>
//        /// 漏工序检测（根据工序编号检测）
//        /// </summary>
//        /// <param name="logCode">玻璃已过工序编号集合</param>
//        /// <param name="nowCode">玻璃应过工序编号集合</param>
//        /// <param name="processDic">玻璃应过工序编号集合键值对</param>
//        /// <returns>玻璃未过工序编号集合</returns>
//        public static string ComparerLogProcessCode(string logCode, string nowCode, Dictionary<string, string> processDic)
//        {
//            try
//            {
//                string ComparerResult = "";
//                string[] LogCodeList = logCode.Trim(',').Split(',');
//                string[] ProcessCodeList = nowCode.Trim(',').Split(',');
//                var DiffList = ProcessCodeList.Except(LogCodeList).ToList();
//                for (int i = 0; i < DiffList.Count; i++)
//                {
//                    if (processDic.ContainsKey(DiffList[i]))
//                    {
//                        ComparerResult += processDic[DiffList[i]].ToString() + " ,";
//                    }
//                }
//                return ComparerResult.Trim(',');
//            }
//            catch (Exception exp)
//            {
//                return "False" + exp.Message.ToString();
//            }
//        }

//        /// <summary>
//        /// 漏工序检测（根据工序序号检测）
//        /// </summary>
//        /// <param name="logCode">玻璃已过工序序号集合</param>
//        /// <param name="nowCode">玻璃应过工序序号集合</param>
//        /// <param name="processDic">玻璃应过工序序号集合键值对</param>
//        /// <returns>玻璃未过工序集合</returns>
//        public static string ComparerLogProcessNumber(string logNumber, string nowNumber, Dictionary<string, string> processNumberDic)
//        {
//            try
//            {
//                string ComparerResult = "";
//                string[] LogNumberList = logNumber.Trim(',').Split(',');
//                string[] ProcessNumberList = nowNumber.Trim(',').Split(',');
//                var DiffList = ProcessNumberList.Except(LogNumberList).ToList();
//                for (int i = 0; i < DiffList.Count; i++)
//                {
//                    if (processNumberDic.ContainsKey(DiffList[i]))
//                    {
//                        ComparerResult += processNumberDic[DiffList[i]].ToString() + " ,";
//                    }
//                }
//                return ComparerResult.Trim(',');
//            }
//            catch (Exception exp)
//            {
//                return "False" + exp.Message.ToString();
//            }
//        }

        /// <summary>
        /// 检测成品码固定位数字符是否符合规则
        /// </summary>
        /// <param name="QrCode">玻璃已过工序序号集合</param>
        /// <param name="SprayCodeView">玻璃应过工序序号集合</param>
        /// <returns>条码固定位置字符一致则返回条码,否则返回错误信息</returns>
        public static string ComparerSprayCodeRules(string QrCode, DataView SprayCodeView)
        {
            try
            {
                string ReturnString = QrCode;
                if (SprayCodeView.Count > 0)
                {
                    for (int i = 0; i < SprayCodeView.Count; i++)
                    {
                        //检测位置在条码长度范围之内
                        if (YJ.Common.Utils.StrToInt(SprayCodeView[i]["SCC_Digit"].ToString(), 0) <= QrCode.Length)
                        {
                            if (SprayCodeView[i]["SCC_Character"].ToString() != QrCode.Substring(YJ.Common.Utils.StrToInt(SprayCodeView[i]["SCC_Digit"].ToString(), 1) - 1, 1) && !string.IsNullOrWhiteSpace(SprayCodeView[i]["SCC_Character"].ToString()))
                            {
                                ReturnString = "err:客户料号：" + SprayCodeView[i]["CustomerCode"].ToString() + ",二维码：" + QrCode + ",检测位置字符不匹配,检测位置：【检测位置:" + YJ.Common.Utils.StrToInt(SprayCodeView[i]["SCC_Digit"].ToString(), 0).ToString() + ",检测字符:" + SprayCodeView[i]["SCC_Character"].ToString() + ",实际字符:" + QrCode.Substring(YJ.Common.Utils.StrToInt(SprayCodeView[i]["SCC_Digit"].ToString(), 1) - 1, 1) + "】";
                                break;
                            }
                        }
                        else
                        {
                            ReturnString = "err:客户料号：" + SprayCodeView[i]["CustomerCode"].ToString() + ",二维码：" + QrCode + ",检测位置超出条码长度,检测位置：【检测位置:" + YJ.Common.Utils.StrToInt(SprayCodeView[i]["SCC_Digit"].ToString(), 0).ToString() + " ,实际长度:" + QrCode.Length.ToString() + "】";
                            break;
                        }
                    }
                }
                return ReturnString;
            }
            catch (Exception exp)
            {
                return "False" + exp.Message.ToString();
            }
        }
    }
}
