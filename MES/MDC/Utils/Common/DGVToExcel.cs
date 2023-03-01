using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Text;
using System.Diagnostics;
using System.IO;
using Microsoft.Office.Interop.Excel;

namespace Utils
{
    public class DGVToExcel
    {
        private const int OLDOFFICEVESION = -4143;
        private const int NEWOFFICEVESION = 56;
        /// <summary>
        /// DataGridView导出Excel
        /// </summary>
        /// <param name="strCaption">Excel文件中的标题</param>
        /// <param name="myDGV">DataGridView 控件</param>
        /// <returns>0:成功;1:DataGridView中无记录;2:Excel无法启动;100:Cancel;9999:异常错误</returns>
        public static int ExportExcel(string strCaption, DataGridView myDGV, SaveFileDialog saveFileDialog)
        {
            saveFileDialog.Filter = "Execl files (*.xls)|*.xls";
            saveFileDialog.FilterIndex = 0;
            saveFileDialog.RestoreDirectory = true;
            //saveFileDialog.CreatePrompt = true;
            saveFileDialog.Title = "Export Excel File";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (saveFileDialog.FileName == "")
                {
                    MessageBox.Show("请输入保存文件名！");
                    saveFileDialog.ShowDialog();
                }
                // 列索引，行索引，总列数，总行数
                int ColIndex = 0, RowIndex = 0;
                int ColCount = myDGV.ColumnCount, RowCount = myDGV.RowCount;
 
                if (myDGV.RowCount == 0)
                {
                    return 1;
                }
 
                // 创建Excel对象
                Microsoft.Office.Interop.Excel.Application xlApp = new ApplicationClass();
                if (xlApp == null)
                {
                    return 2;
                }
                try
                {
                    // 创建Excel工作薄
                    Workbook xlBook = xlApp.Workbooks.Add(true);
                    Worksheet xlSheet = (Worksheet)xlBook.Worksheets[1];
                    ////Get excel Version
                    string Version = xlApp.Version;
                    //保存excel文件的格式
                    int FormatNum;
                    if (Convert.ToDouble(Version) < 12)
                    {
                        //使用Excel 97-2003
                        FormatNum = OLDOFFICEVESION;
                    }
                    else
                    {
                        //使用 excel 2007或更新
                        FormatNum = NEWOFFICEVESION;
                    }
                    // 设置标题
                    //标题所占的单元格数与DataGridView中的列数相同
                    Range range = xlSheet.get_Range(xlApp.Cells[1, 1], xlApp.Cells[1, ColCount]); 
                    range.MergeCells = true;
                    xlApp.ActiveCell.FormulaR1C1 = strCaption;
                    xlApp.ActiveCell.Font.Size = 20;
                    xlApp.ActiveCell.Font.Bold = true;
                    xlApp.ActiveCell.HorizontalAlignment = Constants.xlCenter;
                    // 创建缓存数据
                    object[,] objData = new object[RowCount + 1, ColCount];
                    //获取列标题
                    foreach (DataGridViewColumn col in myDGV.Columns)
                    {
                        objData[RowIndex, ColIndex++] = col.HeaderText;
                    }
                    // 获取数据
                    for (RowIndex = 1; RowIndex < RowCount; RowIndex++)
                    {
                        for (ColIndex = 0; ColIndex < ColCount; ColIndex++)
                        {
                            //这里就是验证DataGridView单元格中的类型,如果是string或是DataTime类型,则在放入缓存时在该内容前加入" ";
                            if (myDGV[ColIndex, RowIndex - 1].ValueType == typeof(string)
                                || myDGV[ColIndex, RowIndex - 1].ValueType == typeof(DateTime))
                            {
                                objData[RowIndex, ColIndex] = "";
                                if (myDGV[ColIndex, RowIndex - 1].Value != null)
                                {
                                    objData[RowIndex, ColIndex] = "" + myDGV[ColIndex, RowIndex - 1].Value.ToString();
                                }
                            }
                            else
                            {
                                objData[RowIndex, ColIndex] = myDGV[ColIndex, RowIndex - 1].Value;
                            }
                        }
                        System.Windows.Forms.Application.DoEvents();
                    }
                    // 写入Excel
                    range = xlSheet.get_Range(xlApp.Cells[2, 1], xlApp.Cells[RowCount+1, ColCount]);
                    range.Value2 = objData;
 
                    xlBook.Saved = true;
                    xlBook.SaveAs(saveFileDialog.FileName, FormatNum);
                }
                catch (Exception err)
                {
                    MessageBox.Show("Err:" + err.Message);
                    return 9999;
                }
                finally
                {
                    xlApp.Quit();
                    GC.Collect(); //强制回收
                }
                return 0;
            }
            return 100;
        }
        /// <summary>
        /// DataTable导出到Excel
        /// </summary>
        /// <param name="fileName">默认的文件名</param>
        /// <param name="dataTable">数据源,一个DataTable数据表</param>
        /// <param name="titleRowCount">标题占据的行数，为0则表示无标题</param>
        public static void ExportExcel(string fileName, DataGridView dataGridView, int titleRowCount)
        {
            string saveFileName = "";
            string savePath = "";
            //bool fileSaved = false;
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.DefaultExt = "xls";
            saveDialog.Filter = "Excel文件 (*.xls)|*.xls";
            saveDialog.FileName = fileName;
            saveDialog.Title = "导出到Excel文件";
            saveDialog.ShowDialog();

            savePath = saveDialog.FileName;
            saveFileName = Path.GetFileNameWithoutExtension(saveDialog.FileName);
            if (savePath.IndexOf(":") < 0) return; //被点了取消 
            Microsoft.Office.Interop.Excel.Application xlApp;
            try
            {
                //xlApp = new Microsoft.Office.Interop.Excel.Application();
                // 创建Excel对象
                xlApp = new ApplicationClass();
            }
            catch (Exception)
            {
                MessageBox.Show("无法创建Excel对象，可能您的机子未安装Excel");
                return;
            }
            finally
            {
            }
            // 创建Excel工作薄
            //Workbook workbook = xlApp.Workbooks.Add(true);
            //Worksheet worksheet = (Worksheet)workbook.Worksheets[1];
            Microsoft.Office.Interop.Excel.Workbooks workbooks = xlApp.Workbooks;
            Microsoft.Office.Interop.Excel.Workbook workbook = workbooks.Add(Microsoft.Office.Interop.Excel.XlWBATemplate.xlWBATWorksheet);
            Microsoft.Office.Interop.Excel.Worksheet worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets[1];//取得sheet1
            ////Get excel Version
            string Version = xlApp.Version;
            //保存excel文件的格式
            int FormatNum;
            if (Convert.ToDouble(Version) < 12)
            {
                //使用Excel 97-2003
                FormatNum = OLDOFFICEVESION;
            }
            else
            {
                //使用 excel 2007或更新
                FormatNum = NEWOFFICEVESION;
            }
            //写Title
            if (titleRowCount != 0)
                MergeCells(worksheet, 1, 1, titleRowCount, dataGridView.Columns.Count, saveFileName);
            //写入列标题
            for (int i = 0; i <= dataGridView.Columns.Count - 1; i++)
            {
                worksheet.Cells[titleRowCount + 1, i + 1] = dataGridView.Columns[i].HeaderText;
            }
            //写入数值
            for (int r = 0; r <= dataGridView.Rows.Count - 1; r++)
            {
                for (int i = 0; i <= dataGridView.Columns.Count - 1; i++)
                {
                    worksheet.Cells[r + titleRowCount + 2, i + 1] = dataGridView[i, r].Value.ToString(); ;
                }
                System.Windows.Forms.Application.DoEvents();
            }
            worksheet.Columns.EntireColumn.AutoFit();//列宽自适应

            // 添加边框
            Microsoft.Office.Interop.Excel.Range range = worksheet.get_Range(worksheet.Cells[1, 1], worksheet.Cells[dataGridView.Rows.Count + titleRowCount + 1, dataGridView.Columns.Count]);
            range.Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            
            //if (Microsoft.Office.Interop.cmbxType.Text != "Notification")
            //{
            // Excel.Range rg = worksheet.get_Range(worksheet.Cells[2, 2], worksheet.Cells[ds.Tables[0].Rows.Count + 1, 2]);
            // rg.NumberFormat = "00000000";
            //}
            if (savePath != "")
            {
                try
                {
                   
                    workbook.Saved = true;
                    workbook.SaveAs(savePath, FormatNum);
                    //workbook.SaveCopyAs(savePath, FormatNum);

                    //fileSaved = true;
                }
                catch (Exception ex)
                {
                    //fileSaved = false;
                    MessageBox.Show("导出文件时出错,文件可能正被打开！n" + ex.Message);
                }
            }
            //else
            //{
            // fileSaved = false;
            //}
            xlApp.Quit();
            GC.Collect();//强行销毁 
            // if (fileSaved && System.IO.File.Exists(saveFileName)) System.Diagnostics.Process.Start(saveFileName); //打开EXCEL
            MessageBox.Show(saveFileName + "导出到Excel成功", "提示", MessageBoxButtons.OK);
        }
        /// <summary>
        /// 导出到Excel
        /// </summary>
        /// <param name="fileName">默认文件名</param>
        /// <param name="listView">数据源，一个页面上的ListView控件</param>
        /// <param name="titleRowCount">标题占据的行数，为0表示无标题</param>
        public static void ExportExcel(string fileName, System.Windows.Forms.ListView listView, int titleRowCount)
        {
            string saveFileName = "";
            //bool fileSaved = false;
            SaveFileDialog saveDialog = new SaveFileDialog
            {
                DefaultExt = "xls",
                Filter = "Excel文件|*.xls",
                FileName = fileName
            };
            saveDialog.ShowDialog();
            saveFileName = saveDialog.FileName;
            if (saveFileName.IndexOf(":") < 0) return; //被点了取消 
            Microsoft.Office.Interop.Excel.Application xlApp;
            try
            {
                xlApp = new Microsoft.Office.Interop.Excel.Application();
            }
            catch (Exception)
            {
                MessageBox.Show("无法创建Excel对象，可能您的机子未安装Excel");
                return;
            }
            finally
            {
            }
            Microsoft.Office.Interop.Excel.Workbooks workbooks = xlApp.Workbooks;
            Microsoft.Office.Interop.Excel.Workbook workbook = workbooks.Add(Microsoft.Office.Interop.Excel.XlWBATemplate.xlWBATWorksheet);
            Microsoft.Office.Interop.Excel.Worksheet worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets[1];//取得sheet1
            //写Title
            if (titleRowCount != 0)
                MergeCells(worksheet, 1, 1, titleRowCount, listView.Columns.Count, listView.Tag.ToString());
            //写入列标题
            for (int i = 0; i <= listView.Columns.Count - 1; i++)
            {
                worksheet.Cells[titleRowCount + 1, i + 1] = listView.Columns[i].Text;
            }
            //写入数值
            for (int r = 0; r <= listView.Items.Count - 1; r++)
            {
                for (int i = 0; i <= listView.Columns.Count - 1; i++)
                {
                    worksheet.Cells[r + titleRowCount + 2, i + 1] = listView.Items[r].SubItems[i].Text;
                }
                System.Windows.Forms.Application.DoEvents();
            }
            worksheet.Columns.EntireColumn.AutoFit();//列宽自适应
            //if (Microsoft.Office.Interop.cmbxType.Text != "Notification")
            //{
            // Excel.Range rg = worksheet.get_Range(worksheet.Cells[2, 2], worksheet.Cells[ds.Tables[0].Rows.Count + 1, 2]);
            // rg.NumberFormat = "00000000";
            //}
            if (saveFileName != "")
            {
                try
                {
                    workbook.Saved = true;
                    workbook.SaveCopyAs(saveFileName);
                    //fileSaved = true;
                }
                catch (Exception ex)
                {
                    //fileSaved = false;
                    MessageBox.Show("导出文件时出错,文件可能正被打开！n" + ex.Message);
                }
            }
            //else
            //{
            // fileSaved = false;
            //}
            xlApp.Quit();
            GC.Collect();//强行销毁 
            // if (fileSaved && System.IO.File.Exists(saveFileName)) System.Diagnostics.Process.Start(saveFileName); //打开EXCEL
            MessageBox.Show(fileName + "导出到Excel成功", "提示", MessageBoxButtons.OK);
        }
        /// <summary>
        /// DataTable导出到Excel
        /// </summary>
        /// <param name="fileName">默认的文件名</param>
        /// <param name="dataTable">数据源,一个DataTable数据表</param>
        /// <param name="titleRowCount">标题占据的行数，为0则表示无标题</param>
        public static void ExportExcel(string fileName, System.Data.DataTable dataTable, int titleRowCount)
        {
            string saveFileName = "";
            //bool fileSaved = false;
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.DefaultExt = "xls";
            saveDialog.Filter = "Excel文件|*.xls";
            saveDialog.FileName = fileName;
            saveDialog.ShowDialog();
            saveFileName = saveDialog.FileName;
            if (saveFileName.IndexOf(":") < 0) return; //被点了取消 
            Microsoft.Office.Interop.Excel.Application xlApp;
            try
            {
                xlApp = new Microsoft.Office.Interop.Excel.Application();
            }
            catch (Exception)
            {
                MessageBox.Show("无法创建Excel对象，可能您的机子未安装Excel");
                return;
            }
            finally
            {
            }
            Microsoft.Office.Interop.Excel.Workbooks workbooks = xlApp.Workbooks;
            Microsoft.Office.Interop.Excel.Workbook workbook = workbooks.Add(Microsoft.Office.Interop.Excel.XlWBATemplate.xlWBATWorksheet);
            Microsoft.Office.Interop.Excel.Worksheet worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets[1];//取得sheet1
            //写Title
            if (titleRowCount != 0)
                MergeCells(worksheet, 1, 1, titleRowCount, dataTable.Columns.Count, dataTable.TableName);
            //写入列标题
            for (int i = 0; i <= dataTable.Columns.Count - 1; i++)
            {
                worksheet.Cells[titleRowCount + 1, i + 1] = dataTable.Columns[i].ColumnName;
            }
            //写入数值
            for (int r = 0; r <= dataTable.Rows.Count - 1; r++)
            {
                for (int i = 0; i <= dataTable.Columns.Count - 1; i++)
                {
                    worksheet.Cells[r + titleRowCount + 2, i + 1] = dataTable.Rows[r][i].ToString();
                }
                System.Windows.Forms.Application.DoEvents();
            }
            worksheet.Columns.EntireColumn.AutoFit();//列宽自适应
            //if (Microsoft.Office.Interop.cmbxType.Text != "Notification")
            //{
            // Excel.Range rg = worksheet.get_Range(worksheet.Cells[2, 2], worksheet.Cells[ds.Tables[0].Rows.Count + 1, 2]);
            // rg.NumberFormat = "00000000";
            //}
            if (saveFileName != "")
            {
                try
                {
                    workbook.Saved = true;
                    workbook.SaveCopyAs(saveFileName);
                    //fileSaved = true;
                }
                catch (Exception ex)
                {
                    //fileSaved = false;
                    MessageBox.Show("导出文件时出错,文件可能正被打开！n" + ex.Message);
                }
            }
            //else
            //{
            // fileSaved = false;
            //}
            xlApp.Quit();
            GC.Collect();//强行销毁 
            // if (fileSaved && System.IO.File.Exists(saveFileName)) System.Diagnostics.Process.Start(saveFileName); //打开EXCEL
            MessageBox.Show(fileName + "导出到Excel成功", "提示", MessageBoxButtons.OK);
        }
        /// <summary> 
        /// 合并单元格，并赋值，对指定WorkSheet操作 
        /// </summary> 
        /// <param name="sheetIndex">WorkSheet索引</param> 
        /// <param name="beginRowIndex">开始行索引</param> 
        /// <param name="beginColumnIndex">开始列索引</param> 
        /// <param name="endRowIndex">结束行索引</param> 
        /// <param name="endColumnIndex">结束列索引</param> 
        /// <param name="text">合并后Range的值</param> 
        public static void MergeCells(Microsoft.Office.Interop.Excel.Worksheet workSheet, int beginRowIndex, int beginColumnIndex, int endRowIndex, int endColumnIndex, string text)
        {
            Microsoft.Office.Interop.Excel.Range range = workSheet.get_Range(workSheet.Cells[beginRowIndex, beginColumnIndex], workSheet.Cells[endRowIndex, endColumnIndex]);
            range.ClearContents(); //先把Range内容清除，合并才不会出错 
            range.MergeCells = true;
            range.Value2 = text;
            range.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            range.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
        } 

    }
}
