using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCF.REDCap
{
    public class User
    {
        public User()
        {
            _forms = new Dictionary<string, int>();
        }

        private Dictionary<string, int> _forms;

        public string UserName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Expiration { get; set; }
        public string DataAccessGroup { get; set; }
        public int DataExport { get; set; }
        public Dictionary<string, int> Forms
        {
            get { return _forms; }
            set { _forms = value; }
        }
    }
}