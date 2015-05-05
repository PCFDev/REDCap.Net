namespace REDCapClient
{
    public class Instrument
    {
        public string InstrumentName { get; set; }
        public string InstrumentLabel { get; set; } // Key

        public override string ToString()
        {
            return this.InstrumentName;
        }
    }
}