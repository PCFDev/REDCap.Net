using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PCF.REDCap.i2b2Importer
{
    public class FileREDCapClient : REDCapClient
    {

        private static string _armFile = string.Empty;
        private static string _eventFile = string.Empty;
        private static string _exportFiledNamesFile = string.Empty;
        private static string _instrumentFile = string.Empty;
        private static string _instrumentEventMappingFile = string.Empty;
        private static string _metadataFile = string.Empty;
        private static string _userFile = string.Empty;


        public FileREDCapClient()
        {

            _eventFile = @"TestFiles\Fructose_Events.xml";
            _armFile = @"TestFiles\Fructose_Arms.xml";
            _exportFiledNamesFile = @"TestFiles\Fructose_ExportFieldNames.xml";
            _instrumentFile = @"TestFiles\Fructose_Forms.xml";
            _instrumentEventMappingFile = @"TestFiles\Fructose_Mapping.xml";
            _metadataFile = @"TestFiles\Fructose_Metadata.xml";
            _userFile = @"TestFiles\Fructose_Users.xml";            
        }

        protected override async Task<string> GetXml(string url)
        {
            var xml = await Task.Run<XElement>( () =>
            {
                if (url.Contains("content=arm"))
                {
                    return XElement.Load(_armFile);
                }
                else if (url.Contains("content=event"))
                {
                    return XElement.Load(_eventFile);
                }
                else if (url.Contains("content=instrument"))
                {
                    return XElement.Load(_instrumentFile);
                }
                else if (url.Contains("content=metadata"))
                {
                    return XElement.Load(_metadataFile);
                }
                else if (url.Contains("content=user"))
                {
                    return XElement.Load(_userFile);
                }
                else if (url.Contains("content=formEventMapping"))
                {
                    return XElement.Load(_instrumentEventMappingFile);
                }
                else if (url.Contains("content=exportFieldNames"))
                {
                    return XElement.Load(_exportFiledNamesFile);
                }

                return null;
            });

            return xml.ToString();
        }
    }
}
