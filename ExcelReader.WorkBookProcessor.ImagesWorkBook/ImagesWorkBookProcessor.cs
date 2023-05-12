using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using ExcelReader.WorkbookProcessor.Base;
using ImagesURLValidator.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ExcelReader.WorkBookProcessor.ImagesWorkBook
{
    public class ImagesWorkBookProcessor : WorkbookProcessorBase
    {
        public ImagesWorkBookProcessor() : base("Sheet1") { }

        public override void SetObjectProperty(string text, Dictionary<int, string> columnHeaders, int colIndex, object obj)
        {
            if(columnHeaders.ContainsKey(colIndex))
            {
                var inputModel = obj as InputModel;

                string columnHeader = columnHeaders[colIndex];
                switch(columnHeader.ToUpper())
                {
                    case "URI_PART_NUMBER":
                        inputModel.URI_Part_Number = text; break;
                    case "URL_TEXT":
                        inputModel.URL_Text = text; break;        
                }

            }
        }

        public override bool IsRowHeader(string text)
        {
            bool result = false;
            switch(text.ToUpper())
            {
                case "URI_PART_NUMBER":
                case "URL_TEXT":
                    result = true;
                    break;
                default: result = false;
                    break;
            }
            return result;
        }

        public override object GetObject()
        {
            return new InputModel();
        }

        public override DocumentFormat.OpenXml.UInt32Value GetRowHeaderIndex(SheetData sheetData, SharedStringTablePart stringTable, WorkSheetConfig config)
        {
            foreach(Row r in sheetData.Elements<Row>())
            {
                foreach(Cell c in r.Elements<Cell>())
                {
                    var value = ExtractCellValue(stringTable, c, c.CellValue?.Text, true);
                    if (config.HeaderColumns.Contains(value))
                        return r.RowIndex;
                }
            }
            return 0;
        }

        public override List<object> BuildObjectList(Dictionary<int, string> columnHeaders, List<Row> dataRows, SharedStringTablePart stringTable)
        {
            var list = base.BuildObjectList(columnHeaders, dataRows, stringTable);
            list.RemoveAll(r => String.IsNullOrEmpty((r as InputModel).URL_Text));
            return list;
        }
    }
}
