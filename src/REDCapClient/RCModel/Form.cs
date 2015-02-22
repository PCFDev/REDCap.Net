namespace REDCapClient
{
    public class Form
    {
        public string FormName { get; set; }
        public string FormLabel { get; set; } // Key
        
        public override string ToString()
        {
            return this.FormName;
        }
    }
}