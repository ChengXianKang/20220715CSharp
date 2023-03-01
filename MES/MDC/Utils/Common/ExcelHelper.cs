using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using NPOI;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.Util;

namespace Utils
{
    public class ExcelHelper
    {
        /// <summary>
        /// 把DataGridView的数据(仅保留可见列)存入DataTable
        /// </summary>
        /// <param name="myDGV">DataGridView对象</param>
        /// <returns></returns>
        public static DataTable ToDataTable(DataGridView myDGV)
        {
            DataTable dt = new DataTable();
            for (int i = 0; i < myDGV.ColumnCount; i++)
            {
                if (myDGV.Columns[i].Visible)
                {
                    dt.Columns.Add(myDGV.Columns[i].HeaderText);
                }
            }
            //写入数值
            for (int r = 0; r < myDGV.Rows.Count; r++)
            {
                List<object> values = new List<object>();
                for (int i = 0; i < myDGV.ColumnCount; i++)
                {
                    if (myDGV.Columns[i].Visible)
                    {
                        values.Add(myDGV.Rows[r].Cells[i].Value);
                    }
                }
                dt.Rows.Add(values.ToArray());
            }
            return dt;
        }

        /// <summary>
        /// 将DataGridView数据导出到Excel
        /// </summary>
        /// <param name="dataGridView">DataGridView对象</param>
        /// <param name="caption">标题</param>
        /// <returns></returns>
        public static void ExportExcel(DataGridView dataGridView, string caption)
        {
            DataTable dt = ToDataTable(dataGridView);
            ExportExcel(dt, caption);
        }

        /// <summary>
        /// 将DataTable数据导出到Excel
        /// </summary>
        /// <param name="data">DataTable数据</param>
        /// <param name="caption">标题</param>
        public static void ExportExcel(DataTable data, string caption)
        {
            string path = "";
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.DefaultExt = "xls";
            saveDialog.Filter = "Excel 97-2003 工作簿|*.xls|Excel 工作簿|*.xlsx";
            saveDialog.FileName = caption;
            saveDialog.Title = "导出到Excel文件";

            //被点了取消 
            if (saveDialog.ShowDialog() == DialogResult.Cancel) return ;

            // 保存路径
            path = saveDialog.FileName;
            // 文件标题
            caption = Path.GetFileNameWithoutExtension(saveDialog.FileName);
            //被点了取消 
            if (path.IndexOf(":") < 0) return;

            if (!CheckFiles(path))
            {
                MessageBox.Show("文件被占用，请关闭后重试！" + path);
                return;
            }

            IWorkbook workbook;
            if (saveDialog.FilterIndex == 1)
            {
                workbook = new HSSFWorkbook();
            }
            else
            {
                workbook = new XSSFWorkbook();
            }

            if (data.Rows.Count <= 10000)
            {
                ISheet sheet = workbook.CreateSheet(caption);
                IRow rowHead = sheet.CreateRow(0);

                //填写表头
                for (int i = 0; i < data.Columns.Count; i++)
                {
                    rowHead.CreateCell(i, CellType.String).SetCellValue(data.Columns[i].ColumnName.ToString());
                }
                //填写内容
                for (int i = 0; i < data.Rows.Count; i++)
                {
                    IRow row = sheet.CreateRow(i + 1);
                    for (int j = 0; j < data.Columns.Count; j++)
                    {
                        string ValueType = data.Rows[i][j].GetType().ToString();
                        string Value = data.Rows[i][j].ToString();
                        switch (ValueType)
                        {
                            case "System.String"://字符串类型
                                row.CreateCell(j).SetCellValue(Value);
                                break;
                            case "System.DateTime"://日期类型
                                System.DateTime dateV;
                                System.DateTime.TryParse(Value, out dateV);
                                row.CreateCell(j).SetCellValue(dateV);
                                break;
                            case "System.Boolean"://布尔型
                                bool boolV = false;
                                bool.TryParse(Value, out boolV);
                                row.CreateCell(j).SetCellValue(boolV);
                                break;
                            case "System.Int16"://整型
                            case "System.Int32":
                            case "System.Int64":
                            case "System.Byte":
                                int intV = 0;
                                int.TryParse(Value, out intV);
                                row.CreateCell(j).SetCellValue(intV);
                                break;
                            case "System.Decimal"://浮点型
                            case "System.Double":
                                double doubV = 0;
                                double.TryParse(Value, out doubV);
                                row.CreateCell(j).SetCellValue(doubV);
                                break;
                            case "System.DBNull"://空值处理
                                row.CreateCell(j).SetCellValue("");
                                break;
                            default:
                                row.CreateCell(j).SetCellValue("");
                                break;
                        }
                        //row.CreateCell(j, CellType.String).SetCellValue(data.Rows[i][j].ToString());
                    }
                }

                for (int i = 0; i < data.Columns.Count; i++)
                {
                    sheet.AutoSizeColumn(i);
                }
            }
            else
            {
                int rowidx = 0;
                int page = 0;

                while (rowidx < data.Rows.Count)
                {
                    ISheet sheet = workbook.CreateSheet(string.Format("{0}-{1}", page * 10000 + 1, page * 10000 + 10000));
                    IRow rowHead = sheet.CreateRow(0);

                    //填写表头
                    for (int i = 0; i < data.Columns.Count; i++)
                    {
                        rowHead.CreateCell(i, CellType.String).SetCellValue(data.Columns[i].ColumnName.ToString());
                    }
                    int rownum = 0;
                    //填写内容
                    for (int i = page * 10000; i < page * 10000 + 10000; i++)
                    {
                        if (i >= data.Rows.Count) break;
                        rownum++;
                        IRow row = sheet.CreateRow(rownum);
                        for (int j = 0; j < data.Columns.Count; j++)
                        {
                            string ValueType = data.Rows[i][j].GetType().ToString();
                            string Value = data.Rows[i][j].ToString();
                            switch (ValueType)
                            {
                                case "System.String"://字符串类型
                                    row.CreateCell(j).SetCellValue(Value);
                                    break;
                                case "System.DateTime"://日期类型
                                    System.DateTime dateV;
                                    System.DateTime.TryParse(Value, out dateV);
                                    row.CreateCell(j).SetCellValue(dateV);
                                    break;
                                case "System.Boolean"://布尔型
                                    bool boolV = false;
                                    bool.TryParse(Value, out boolV);
                                    row.CreateCell(j).SetCellValue(boolV);
                                    break;
                                case "System.Int16"://整型
                                case "System.Int32":
                                case "System.Int64":
                                case "System.Byte":
                                    int intV = 0;
                                    int.TryParse(Value, out intV);
                                    row.CreateCell(j).SetCellValue(intV);
                                    break;
                                case "System.Decimal"://浮点型
                                case "System.Double":
                                    double doubV = 0;
                                    double.TryParse(Value, out doubV);
                                    row.CreateCell(j).SetCellValue(doubV);
                                    break;
                                case "System.DBNull"://空值处理
                                    row.CreateCell(j).SetCellValue("");
                                    break;
                                default:
                                    row.CreateCell(j).SetCellValue("");
                                    break;
                            }
                            //row.CreateCell(j, CellType.String).SetCellValue(data.Rows[i][j].ToString());
                        }
                    }

                    for (int i = 0; i < data.Columns.Count; i++)
                    {
                        sheet.AutoSizeColumn(i);
                    }

                    page += 1;
                    rowidx = page * 10000 - 1;
                }
            }
            using (FileStream stream = File.OpenWrite(path))
            {
                workbook.Write(stream);
                stream.Close();
            }
            MessageBox.Show("数据导出成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            GC.Collect();
        }

        #region 检测文件被占用 
        [DllImport("kernel32.dll")]
        public static extern IntPtr _lopen(string lpPathName, int iReadWrite);
        [DllImport("kernel32.dll")]
        public static extern bool CloseHandle(IntPtr hObject);
        public const int OF_READWRITE = 2;
        public const int OF_SHARE_DENY_NONE = 0x40;
        public static readonly IntPtr HFILE_ERROR = new IntPtr(-1);
        /// <summary>
        /// 检测文件被占用 
        /// </summary>
        /// <param name="FileNames">要检测的文件路径</param>
        /// <returns></returns>
        public static bool CheckFiles(string FileNames)
        {
            if (!File.Exists(FileNames))
            {
                //文件不存在
                return true;
            }
            IntPtr vHandle = _lopen(FileNames, OF_READWRITE | OF_SHARE_DENY_NONE);
            if (vHandle == HFILE_ERROR)
            {
                //文件被占用
                return false;
            }
            //文件没被占用
            CloseHandle(vHandle);
            return true;
        }
        #endregion

        ///// <summary>
        ///// 根据Excel和Sheet返回DataTable
        ///// </summary>
        ///// <param name="filePath">Excel文件地址</param>
        ///// <param name="sheetIndex">Sheet索引</param>
        ///// <returns>DataTable</returns>
        //public static DataTable GetDataTable(string filePath, int sheetIndex)
        //{
        //    return GetDataSet(filePath, sheetIndex).Tables[0];
        //}

        ///// <summary>
        ///// 根据Excel返回DataSet
        ///// </summary>
        ///// <param name="filePath">Excel文件地址</param>
        ///// <param name="sheetIndex">Sheet索引，可选，默认返回所有Sheet</param>
        ///// <returns>DataSet</returns>
        //public static DataSet GetDataSet(string filePath, int? sheetIndex = null)
        //{
        //    DataSet ds = new DataSet();
        //    IWorkbook fileWorkbook;
        //    using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
        //    {
        //        if (filePath.Last() == 's')
        //        {
        //            try
        //            {
        //                fileWorkbook = new HSSFWorkbook(fs);
        //            }
        //            catch (Exception ex)
        //            {
        //                throw ex;
        //            }
        //        }
        //        else
        //        {
        //            try
        //            {
        //                fileWorkbook = new XSSFWorkbook(fs);
        //            }
        //            catch
        //            {
        //                fileWorkbook = new HSSFWorkbook(fs);
        //            }
        //        }
        //    }

        //    for (int i = 0; i < fileWorkbook.NumberOfSheets; i++)
        //    {
        //        if (sheetIndex != null && sheetIndex != i)
        //            continue;
        //        DataTable dt = new DataTable();
        //        ISheet sheet = fileWorkbook.GetSheetAt(i);

        //        //表头
        //        IRow header = sheet.GetRow(sheet.FirstRowNum);
        //        List<int> columns = new List<int>();
        //        for (int j = 0; j < header.LastCellNum; j++)
        //        {
        //            object obj = GetValueTypeForXLS(header.GetCell(j) as HSSFCell);
        //            if (obj == null || obj.ToString() == string.Empty)
        //            {
        //                dt.Columns.Add(new DataColumn("Columns" + j.ToString()));
        //            }
        //            else
        //                dt.Columns.Add(new DataColumn(obj.ToString()));
        //            columns.Add(j);
        //        }
        //        //数据
        //        IEnumerator rows = sheet.GetEnumerator();
        //        while (rows.MoveNext())
        //        {
        //            int j = sheet.FirstRowNum + 1;
        //            DataRow dr = dt.NewRow();
        //            bool hasValue = false;
        //            foreach (int K in columns)
        //            {
        //                dr[K] = GetValueTypeForXLS(sheet.GetRow(K).GetCell(K) as HSSFCell);
        //                if (dr[K] != null && dr[K].ToString() != string.Empty)
        //                {
        //                    hasValue = true;
        //                }
        //            }
        //            if (hasValue)
        //            {
        //                dt.Rows.Add(dr);
        //            }
        //            j++;
        //        }
        //        ds.Tables.Add(dt);
        //    }

        //    return ds;
        //}

        ///// <summary>
        ///// 根据DataTable导出Excel
        ///// </summary>
        ///// <param name="dt">DataTable</param>
        ///// <param name="file">保存地址</param>
        //public static void GetExcelByDataTable(DataTable dt, string file)
        //{
        //    DataSet ds = new DataSet();
        //    ds.Tables.Add(dt);
        //    GetExcelByDataSet(ds, file);
        //}

        ///// <summary>
        ///// 根据DataSet导出Excel
        ///// </summary>
        ///// <param name="ds">DataSet</param>
        ///// <param name="file">保存地址</param>
        //public static void GetExcelByDataSet(DataSet ds, string file)
        //{
        //    IWorkbook fileWorkbook = new HSSFWorkbook();
        //    int index = 0;
        //    foreach (DataTable dt in ds.Tables)
        //    {
        //        index++;
        //        ISheet sheet = fileWorkbook.CreateSheet("Sheet" + index);

        //        //表头
        //        IRow row = sheet.CreateRow(0);
        //        for (int i = 0; i < dt.Columns.Count; i++)
        //        {
        //            ICell cell = row.CreateCell(i);
        //            cell.SetCellValue(dt.Columns[i].ColumnName);
        //        }

        //        //数据
        //        for (int i = 0; i < dt.Rows.Count; i++)
        //        {
        //            IRow row1 = sheet.CreateRow(i + 1);
        //            for (int j = 0; j < dt.Columns.Count; j++)
        //            {
        //                ICell cell = row1.CreateCell(j);
        //                cell.SetCellValue(dt.Rows[i][j].ToString());
        //            }
        //        }
        //    }

        //    //转为字节数组
        //    MemoryStream stream = new MemoryStream();
        //    fileWorkbook.Write(stream);
        //    var buf = stream.ToArray();

        //    //保存为Excel文件
        //    using (FileStream fs = new FileStream(file, FileMode.Create, FileAccess.Write))
        //    {
        //        fs.Write(buf, 0, buf.Length);
        //        fs.Flush();
        //    }
        //}

        ///// <summary>
        ///// 根据单元格将内容返回为对应类型的数据
        ///// </summary>
        ///// <param name="cell">单元格</param>
        ///// <returns>数据</returns>
        //private static object GetValueTypeForXLS(HSSFCell cell)
        //{
        //    if (cell == null)
        //        return null;
        //    switch (cell.CellType)
        //    {
        //        case CellType.Blank: //BLANK:
        //            return null;
        //        case CellType.Boolean: //BOOLEAN:
        //            return cell.BooleanCellValue;
        //        case CellType.Numeric: //NUMERIC:
        //            return cell.NumericCellValue;
        //        case CellType.String: //STRING:
        //            return cell.StringCellValue;
        //        case CellType.Error: //ERROR:
        //            return cell.ErrorCellValue;
        //        case CellType.Formula: //FORMULA:
        //        default:
        //            return "=" + cell.CellFormula;
        //    }
        //}

        ///// <summary>
        ///// DataTable导出到Excel
        ///// </summary>
        ///// <param name="dt"></param>
        //public static void ExportToExcel(DataTable dt)
        //{
        //    ExportToExcelData(dt);
        //}

        ///// <summary>
        ///// 把DataGridView的数据(仅保留可见列)存入DataTable
        ///// </summary>
        ///// <param name="myDGV">DataGridView对象</param>
        ///// <returns></returns>
        //public static DataTable ToDataTable(this DataGridView myDGV)
        //{
        //    DataTable dt = new DataTable();
        //    for (int i = 0; i < myDGV.ColumnCount; i++)
        //    {
        //        if (myDGV.Columns[i].Visible)
        //        {
        //            dt.Columns.Add(myDGV.Columns[i].HeaderText);
        //        }
        //    }
        //    //写入数值
        //    for (int r = 0; r < myDGV.Rows.Count; r++)
        //    {
        //        List<object> values = new List<object>();
        //        for (int i = 0; i < myDGV.ColumnCount; i++)
        //        {
        //            if (myDGV.Columns[i].Visible)
        //            {
        //                values.Add(myDGV.Rows[r].Cells[i].Value);
        //            }
        //        }
        //        dt.Rows.Add(values.ToArray());
        //    }
        //    return dt;
        //}
        //#region 导出
        ///// <summary>
        ///// 数据导出
        ///// </summary>
        ///// <param name="data"></param>
        ///// <param name="sheetName"></param>
        //public static void ExportToExcelData(this DataTable data)
        //{
        //    ExportToExcel(data, "Sheet1");
        //}
        ///// <summary>
        ///// 数据导出
        ///// </summary>
        ///// <param name="data"></param>
        ///// <param name="sheetName"></param>
        //public static void ExportToExcel(this DataTable data, string sheetName)
        //{
        //    SaveFileDialog fileDialog = new SaveFileDialog();
        //    //fileDialog.Filter = "Excel(97-2003)|*.xls|Excel(2007-2013)|*.xlsx";
        //    fileDialog.Filter = "Excel|*.xls|Excel|*.xlsx";
        //    if (fileDialog.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
        //    {
        //        return;
        //    }
        //    IWorkbook workbook = new XSSFWorkbook();
        //    ISheet sheet = workbook.CreateSheet(sheetName);
        //    IRow rowHead = sheet.CreateRow(0);

        //    //填写表头
        //    for (int i = 0; i < data.Columns.Count; i++)
        //    {
        //        rowHead.CreateCell(i, CellType.String).SetCellValue(data.Columns[i].ColumnName.ToString());
        //    }
        //    //填写内容
        //    for (int i = 0; i < data.Rows.Count; i++)
        //    {
        //        IRow row = sheet.CreateRow(i + 1);
        //        for (int j = 0; j < data.Columns.Count; j++)
        //        {
        //            row.CreateCell(j, CellType.String).SetCellValue(data.Rows[i][j].ToString());
        //        }
        //    }

        //    for (int i = 0; i < data.Columns.Count; i++)
        //    {
        //        sheet.AutoSizeColumn(i);
        //    }

        //    using (FileStream stream = File.OpenWrite(fileDialog.FileName))
        //    {
        //        workbook.Write(stream);
        //        stream.Close();
        //    }
        //    MessageBox.Show("导出数据成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    GC.Collect();
        //}
        //#endregion

        //#region 导入
        ///// <summary>
        ///// 导入的文件名
        ///// </summary>
        ///// <param name="fileName"></param>
        ///// <returns></returns>
        //public static DataSet ExcelToDataSet(string fileName)
        //{
        //    return ExcelToDataSet(fileName, true);
        //}
        ///// <summary>
        ///// 返回dataset
        ///// </summary>
        ///// <param name="fileName"></param>
        ///// <param name="firstRowAsHeader"></param>
        ///// <returns></returns>
        //public static DataSet ExcelToDataSet(string fileName, bool firstRowAsHeader)
        //{
        //    int sheetCount = 0;
        //    return ExcelToDataSet(fileName, firstRowAsHeader, out sheetCount);
        //}
        ///// <summary>
        ///// 返回dataset
        ///// </summary>
        ///// <param name="fileName">文件名</param>
        ///// <param name="firstRowAsHeader">文件头</param>
        ///// <param name="sheetCount">内容</param>
        ///// <returns></returns>
        //public static DataSet ExcelToDataSet(string fileName, bool firstRowAsHeader, out int sheetCount)
        //{
        //    using (DataSet ds = new DataSet())
        //    {
        //        using (FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
        //        {
        //            IWorkbook workbook = WorkbookFactory.Create(fileStream);
        //            IFormulaEvaluator evaluator = WorkbookFactory.CreateFormulaEvaluator(workbook);

        //            sheetCount = workbook.NumberOfSheets;

        //            for (int i = 0; i < sheetCount; ++i)
        //            {
        //                ISheet sheet = workbook.GetSheetAt(i);
        //                DataTable dt = ExcelToDataTable(sheet, evaluator, firstRowAsHeader);
        //                ds.Tables.Add(dt);
        //            }
        //            return ds;
        //        }
        //    }
        //}
        ///// <summary>
        ///// 返回DataTable
        ///// </summary>
        ///// <param name="sheet"></param>
        ///// <param name="evaluator"></param>
        ///// <param name="firstRowAsHeader"></param>
        ///// <returns></returns>
        //private static DataTable ExcelToDataTable(ISheet sheet, IFormulaEvaluator evaluator, bool firstRowAsHeader)
        //{
        //    if (firstRowAsHeader)
        //    {
        //        return ExcelToDataTableFirstRowAsHeader(sheet, evaluator);
        //    }
        //    else
        //    {
        //        return ExcelToDataTable(sheet, evaluator);
        //    }
        //}
        //private static DataTable ExcelToDataTableFirstRowAsHeader(ISheet sheet, IFormulaEvaluator evaluator)
        //{
        //    try
        //    {
        //        using (DataTable dt = new DataTable())
        //        {
        //            IRow firstRow = sheet.GetRow(0);
        //            int cellCount = GetCellCount(sheet);

        //            for (int i = 0; i < cellCount; i++)
        //            {
        //                if (firstRow.GetCell(i) != null)
        //                {
        //                    dt.Columns.Add(firstRow.GetCell(i).StringCellValue ?? string.Format("F{0}", i + 1), typeof(string));
        //                }
        //                else
        //                {
        //                    dt.Columns.Add(string.Format("F{0}", i + 1), typeof(string));
        //                }
        //            }

        //            for (int i = 1; i <= sheet.LastRowNum; i++)
        //            {
        //                IRow row = sheet.GetRow(i);
        //                DataRow dr = dt.NewRow();
        //                FillDataRowByRow(row, evaluator, ref dr);
        //                dt.Rows.Add(dr);
        //            }

        //            dt.TableName = sheet.SheetName;
        //            return dt;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //        return null;
        //    }
        //}
        //private static DataTable ExcelToDataTable(ISheet sheet, IFormulaEvaluator evaluator)
        //{
        //    using (DataTable dt = new DataTable())
        //    {
        //        if (sheet.LastRowNum != 0)
        //        {
        //            int cellCount = GetCellCount(sheet);

        //            for (int i = 0; i < cellCount; i++)
        //            {
        //                dt.Columns.Add(string.Format("F{0}", i), typeof(string));
        //            }

        //            for (int i = 0; i < sheet.FirstRowNum; ++i)
        //            {
        //                DataRow dr = dt.NewRow();
        //                dt.Rows.Add(dr);
        //            }

        //            for (int i = sheet.FirstRowNum; i <= sheet.LastRowNum; i++)
        //            {
        //                IRow row = sheet.GetRow(i);
        //                DataRow dr = dt.NewRow();
        //                FillDataRowByRow(row, evaluator, ref dr);
        //                dt.Rows.Add(dr);
        //            }
        //        }

        //        dt.TableName = sheet.SheetName;
        //        return dt;
        //    }
        //}
        ///// <summary>
        ///// 填充数据
        ///// </summary>
        ///// <param name="row"></param>
        ///// <param name="evaluator"></param>
        ///// <param name="dr"></param>
        //private static void FillDataRowByRow(IRow row, IFormulaEvaluator evaluator, ref DataRow dr)
        //{
        //    if (row != null)
        //    {
        //        for (int j = 0; j < dr.Table.Columns.Count; j++)
        //        {
        //            ICell cell = row.GetCell(j);

        //            if (cell != null)
        //            {
        //                switch (cell.CellType)
        //                {
        //                    case CellType.Blank:
        //                        {
        //                            dr[j] = DBNull.Value;
        //                            break;
        //                        }
        //                    case CellType.Boolean:
        //                        {
        //                            dr[j] = cell.BooleanCellValue;
        //                            break;
        //                        }
        //                    case CellType.Numeric:
        //                        {
        //                            if (DateUtil.IsCellDateFormatted(cell))
        //                            {
        //                                dr[j] = cell.DateCellValue;
        //                            }
        //                            else
        //                            {
        //                                dr[j] = cell.NumericCellValue;
        //                            }
        //                            break;
        //                        }
        //                    case CellType.String:
        //                        {
        //                            dr[j] = cell.StringCellValue;
        //                            break;
        //                        }
        //                    case CellType.Error:
        //                        {
        //                            dr[j] = cell.ErrorCellValue;
        //                            break;
        //                        }
        //                    case CellType.Formula:
        //                        {
        //                            cell = evaluator.EvaluateInCell(cell) as HSSFCell;
        //                            dr[j] = cell.ToString();
        //                            break;
        //                        }
        //                    default:
        //                        throw new NotSupportedException(string.Format("Unsupported format type:{0}", cell.CellType));
        //                }
        //            }
        //        }
        //    }
        //}

        ///// <summary>
        ///// 获取单元格
        ///// </summary>
        ///// <param name="sheet"></param>
        ///// <returns></returns>
        //private static int GetCellCount(ISheet sheet)
        //{
        //    int firstRowNum = sheet.FirstRowNum;

        //    int cellCount = 0;

        //    for (int i = sheet.FirstRowNum; i <= sheet.LastRowNum; ++i)
        //    {
        //        IRow row = sheet.GetRow(i);

        //        if (row != null && row.LastCellNum > cellCount)
        //        {
        //            cellCount = row.LastCellNum;
        //        }
        //    }
        //    return cellCount;
        //}


        //#endregion
    }
}
