namespace REDCapClient.RCModel
{
    public class Form
    {
        private string _formName;
        private string _formLabel;

        public string FormLabel
        {
            get { return _formLabel; }
            set { _formLabel = value; }
        }

        public string FormName
        {
            get { return _formName; }
            set { _formName = value; }
        }

        public override string ToString()
        {
            return this.FormName;
        }
    }
}