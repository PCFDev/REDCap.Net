using System;

namespace PCF.REDCap.Web.Data.API.DB
{
    public interface IConfig
    {
        bool Enabled { get; set; }
        int Id { get; }
        string Key { get; set; }
        string Name { get; set; }
        Uri Uri { get; }
        string Url { get; set; }
    }
}
