using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ExcelReader.WorkbookProcessor.Base
{
    public abstract class WorkbookProcessorBase
    {
        private WorkSheetConfig config = null;
        protected WorkbookProcessorBase() { }

        protected WorkbookProcessorBase(string sheetName)
        {
            WorkBookConfig bookConfig = WorksheetSettingsHelper.GetWorkBookSettings();
            config = bookConfig.workSheets.Where(w => w.Name == sheetName).FirstOrDefault();
        }

        public virtual List<object> ProcessWorkbookData(Stream stream)
        {
            using(SpreadsheetDocument doc = SpreadsheetDocument.Open(stream, false))
            {
                Dictionary<int, string> columnHeaders = new Dictionary<int, string>();
                WorkbookPart workbookPart = doc.WorkbookPart;

                int count = doc.WorkbookPart.Workbook.Sheets.Count();
                Sheet mysheet = GetSheet(doc, count);

                Worksheet worksheet = ((WorksheetPart)workbookPart.GetPartById(mysheet.Id)).Worksheet;  

                var stringTable = workbookPart.GetPartsOfType<SharedStringTablePart>().FirstOrDefault();

                SheetData sheetData = worksheet.Elements<SheetData>().First();
                var rowIndex = GetRowHeaderIndex(sheetData, stringTable, config);

                var rowCount = sheetData?.Elements<Row>()?.Count();
                var headerRow = sheetData.Elements<Row>()?.Where(r => r.RowIndex == rowIndex).FirstOrDefault();
                BuildHeaderRow(columnHeaders, stringTable, headerRow, config);

                var colStartIndex = columnHeaders.First().Key;
                var colEndIndex = columnHeaders.Last().Key;
                var totalColumns = colEndIndex - colStartIndex;

                var dataRows = sheetData.Elements<Row>().Where(r => r.RowIndex > headerRow.RowIndex).ToList();
                var remainingRowCount = dataRows.Count();

                var list = BuildObjectList(columnHeaders, dataRows, stringTable);
                return list;
            }
        }

        public virtual UInt32Value GetRowHeaderIndex(SheetData sheetData, SharedStringTablePart stringTable, WorkSheetConfig config)
        {
            return 0;
        }

        public virtual List<object> BuildObjectList(Dictionary<int, string> columnHeaders, List<Row> dataRows, SharedStringTablePart stringTable)
        {
            List<object> list = new List<object>();
            foreach (var row in dataRows)
            {
                var obj = GetObject();
                int colIndex = 1;
                foreach(Cell c in row.Elements<Cell>())
                {
                    string text = string.Empty;
                    text = ExtractCellValue(stringTable, c, text);
                    SetObjectProperty(text, columnHeaders, colIndex, obj);
                    colIndex++;
                }
                list.Add(obj);
            }
            return list;
        }

        public virtual object GetObject()
        {
            return new object();
        }

        private Sheet GetSheet(SpreadsheetDocument doc, int count)
        {
            Sheet matchingSheet = null;
            for (int i = 0; i < count; i++)
            {
                matchingSheet = (Sheet)doc.WorkbookPart.Workbook.Sheets.ChildElements.GetItem(i);
                if (matchingSheet.Name == config.Name)
                    break;
            }
            return matchingSheet;
        }

        public virtual Dictionary<int, string> BuildHeaderRow(Dictionary<int, string> columnHeaders, SharedStringTablePart stringTable, Row r, WorkSheetConfig config)
        {
            int i = 1;
            foreach(Cell c in r.Elements<Cell>())
            {
                string text = string.Empty;
                text = ExtractCellValue(stringTable, c, text, true);
                var valueIsRowHeader = IsRowHeader(text);
                if(valueIsRowHeader)
                {
                    columnHeaders.Add(i, text);
                    if (text == config.wkrange.End)
                        break;
                }
                i++;
            }
            return columnHeaders;
        }

        public virtual void SetObjectProperty(string text, Dictionary<int, string> columnHeaders, int colIndex, object obj)
        {
            return;
        }



        public virtual bool IsRowHeader(string text)
        {
            return true;
        }

        public virtual string ExtractCellValue(SharedStringTablePart stringTable, Cell c, string text, bool headerCell = false)
        {
            if (c.DataType == "s")
            {
                if (c.CellValue != null)
                    text = stringTable.SharedStringTable.ElementAt(int.Parse(c.CellValue.Text)).InnerText;
            }
            else
                text = c.CellValue?.Text;
            return text;
        }


    }
}
