using System;
using System.IO;
using System.Data;
using System.Collections.Generic;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.Linq;
using System.ComponentModel;
using System.Text;
using XCodeFramework.Core.Extensions;

namespace XCodeFramework.Core.Helpers
{
    public class ExcelHelper
    {
        public ExcelHelper() { }

        public List<String> Errors { get; set; }

        public ExcelHelper(Stream stream)
        {
            _IWorkbook = CreateWorkbook(stream);
        }

        public ExcelHelper(string fileName)
        {
            using (FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                _IWorkbook = CreateWorkbook(fileStream);
            }
        }

        private IWorkbook _IWorkbook;

        private IWorkbook CreateWorkbook(Stream stream)
        {
            try
            {
                return new XSSFWorkbook(stream); //07
            }
            catch
            {
                return new HSSFWorkbook(stream); //03
            }

        }

        private DataTable ExportToDataTable(ISheet sheet)
        {
            DataTable dt = new DataTable();

            IRow headRow = sheet.GetRow(0);

            for (int i = headRow.FirstCellNum, len = headRow.LastCellNum; i < len; i++)
            {
                dt.Columns.Add(headRow.Cells[i].StringCellValue);
            }

            for (int i = (sheet.FirstRowNum + 1), len = sheet.LastRowNum; i < len; i++)
            {
                IRow tempRow = sheet.GetRow(i);
                DataRow dataRow = dt.NewRow();

                for (int r = 0, j = tempRow.FirstCellNum, len2 = tempRow.LastCellNum; j < len2; j++, r++)
                {

                    ICell cell = tempRow.GetCell(j);

                    if (cell != null)
                    {
                        switch (cell.CellType)
                        {
                            case CellType.String:
                                dataRow[r] = cell.StringCellValue;
                                break;
                            case CellType.Numeric:
                                dataRow[r] = cell.NumericCellValue;
                                break;
                            case CellType.Boolean:
                                dataRow[r] = cell.BooleanCellValue;
                                break;
                            default:
                                dataRow.SetField(r, "ERROR");
                                //dataRow[r] = "ERROR";
                                break;
                        }
                    }
                }
                dt.Rows.Add(dataRow);
            }
            return dt;
        }

        private IList<T> ExportToList<T>(ISheet sheet) where T : class, new()
        {
            var list = new List<T>();

            var headerRow = sheet.GetRow(0);
            var headerFields = headerRow.Cells.Select(it => it.StringCellValue).ToList();

            var lastRowNum = sheet.LastRowNum + 1;
            var firstRowNum = sheet.FirstRowNum + 1;
            for (int i = firstRowNum, len = lastRowNum; i < len; i++)
            {
                T t = new T();
                IRow row = sheet.GetRow(i);

                if (row.Cells.Count > 1)
                {
                    for (int j = 0, headerFieldsCount = headerFields.Count; j < headerFieldsCount; j++)
                    {
                        var cell = row.GetCell(j);
                        var propertyName = GetPropertyName<T>(headerFields[j]);

                        object cellValue = null;

                        if (cell != null)
                        {
                            switch (cell.CellType)
                            {
                                case CellType.String:
                                    var strValue = !string.IsNullOrEmpty(cell.StringCellValue) ? cell.StringCellValue.Trim() : "";
                                    cellValue = !string.IsNullOrEmpty(strValue) ? strValue : "";
                                    break;
                                case CellType.Numeric:
                                    cellValue = cell.NumericCellValue;
                                    break;
                                case CellType.Boolean:
                                    cellValue = cell.BooleanCellValue;
                                    break;
                                case CellType.Blank:
                                    cellValue = "";
                                    break;
                                default:
                                    cellValue = "ERROR";
                                    break;
                            }
                        }

                        try
                        {
                            typeof(T).GetProperty(propertyName).SetValue(t, cellValue?.ToString(), null);
                        }
                        catch (Exception ex)
                        {
                            if (Errors == null)
                            {
                                Errors = new List<string>();
                            }

                            Errors.Add(String.Format("Sheet:{0};Property:{1}:Error:{2}", sheet.SheetName, propertyName, ex.Message));
                        }
                    }
                    list.Add(t);
                }
            }

            return list;
        }

        private string GetPropertyName<T>(string fieldName)
        {
            fieldName = fieldName.Trim().ToLower();
            var properties = typeof(T).GetProperties();

            foreach (var property in properties)
            {
                var memberInfo = typeof(T).GetProperty(property.Name);
                var descriptionAttribute = memberInfo.GetCustomAttributes(typeof(DisplayNameAttribute), true).Cast<DisplayNameAttribute>().FirstOrDefault();

                if (descriptionAttribute != null)
                {
                    if (descriptionAttribute.DisplayName.ToLower().Trim() == fieldName)
                    {
                        return property.Name;
                    }
                }
                else if (property.Name.ToLower() == fieldName)
                {
                    return property.Name;
                }
            }

            return fieldName;
        }

        public string GetCellValue(int X, int Y)
        {
            ISheet sheet = _IWorkbook.GetSheetAt(0);

            IRow row = sheet.GetRow(X - 1);

            return row.GetCell(Y - 1).ToString();
        }

        public string[] GetCells(int X)
        {
            List<string> list = new List<string>();

            ISheet sheet = _IWorkbook.GetSheetAt(0);

            IRow row = sheet.GetRow(X - 1);

            for (int i = 0, len = row.LastCellNum; i < len; i++)
            {
                list.Add(row.GetCell(i).StringCellValue);
            }

            return list.ToArray();
        }

        public DataTable ExportExcelToDataTable()
        {
            return ExportToDataTable(_IWorkbook.GetSheetAt(0));
        }

        public DataTable ExportExcelToDataTable(int sheetIndex)
        {
            return ExportToDataTable(_IWorkbook.GetSheetAt(sheetIndex - 1));
        }

        public IList<T> ExcelToList<T>(int sheetIndex = 0) where T : class, new()
        {
            return ExportToList<T>(_IWorkbook.GetSheetAt(sheetIndex));
        }

        #region ExportToExcel

        public Stream ExportToExcel<T>(IEnumerable<T> data, String extension = "xlsx", string sheet = "Sheet 1")
        {
            var dataTable = data.ToDataTable();
            return ExportToExcel(dataTable, extension, sheet);
        }

        public Stream ExportToExcel(DataTable dt, String extension = "xlsx", string sheet = "Sheet 1")
        {
            IWorkbook workbook;

            if (extension == "xlsx")
            {
                workbook = new XSSFWorkbook();
            }
            else if (extension == "xls")
            {
                workbook = new HSSFWorkbook();
            }
            else
            {
                throw new Exception("This format is not supported");
            }

            ISheet sheet1 = workbook.CreateSheet(sheet);

            // make a header row
            IRow row1 = sheet1.CreateRow(0);

            for (int j = 0; j < dt.Columns.Count; j++)
            {

                ICell cell = row1.CreateCell(j);
                String columnName = dt.Columns[j].ToString();
                cell.SetCellValue(columnName);
            }

            // loops through data
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                IRow row = sheet1.CreateRow(i + 1);
                for (int j = 0; j < dt.Columns.Count; j++)
                {

                    ICell cell = row.CreateCell(j);
                    String columnName = dt.Columns[j].ToString();
                    cell.SetCellValue(dt.Rows[i][columnName].ToString());
                }
            }

            MemoryStream ms = new MemoryStream();
            using (MemoryStream tempStream = new MemoryStream())
            {
                workbook.Write(tempStream);
                var byteArray = tempStream.ToArray();
                ms.Write(byteArray, 0, byteArray.Length);
                ms.Seek(0, SeekOrigin.Begin);
            }

            return ms;
        }

        #endregion
    }
}
