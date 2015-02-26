namespace REDCapClient
{
    public class Form
    {
        public string FormName { get; set; }
        public string FormLabel { get; set; } // Key
        public string EventId { get; set; } // Foreign Key to Event

        public virtual Event Event { get; set; }
        
        public override string ToString()
        {
            return this.FormName;
        }
    }
}