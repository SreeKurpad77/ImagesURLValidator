namespace ExcelReader.WorkbookProcessor.Base
{
    public class WorkBookConfig
    {
        public string Name { get; set; }
        public WorkSheetConfig[] workSheets { get; set; }
    }
    public class WorkSheetConfig
    {
        public string Name { get; set; }
        public string[] HeaderColumns { get; set; }
        public Wkrange wkrange { get; set; }
    }
    public class Wkrange
    {
        public string Start { get; set; }
        public string End { get; set; }
    }
}
