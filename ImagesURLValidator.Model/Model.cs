namespace ImagesURLValidator.Model
{
    public class InputModel
    {
        public InputModel() { }
        public InputModel(InputModel input)
        {
            this.URI_Part_Number = input.URI_Part_Number;
            this.URL_Text = input.URL_Text;
        }
        public string URI_Part_Number { get; set; }
        public string URL_Text { get; set; }

    }
    public class OutputModel : InputModel
    {
        public OutputModel() { }
        public OutputModel(InputModel input) : base(input) { }

        public string URL_Correct { get; set; }
    }
}
