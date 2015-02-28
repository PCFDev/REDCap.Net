using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace REDCapClient
{
    public static class XmlExtensions
    {
        public static bool ElementIsEmpty(this XElement element)
        {
            bool result = false;

            result = string.IsNullOrEmpty(element.Value);

            return result;
        }

        public static string GetValue(this XElement element)
        {
            if (element.ElementIsEmpty())
                return string.Empty;
            else
                return element.Value.ToString();
        }
    }
}