using System.Threading.Tasks;
using PCF.OdmXml;
using PCF.REDCap;

namespace REDCapExporter
{
    public class OdmXmlConverter
    {

        public async Task<ODM> Convert(Study study)
        {
            var odm = new ODM();

            odm.ID = study.StudyName;
            odm.AdminData.Add(MapAdminDataElement(study));

            return odm;
        }

        // AdminData element construction
        private ODMcomplexTypeDefinitionAdminData MapAdminDataElement(Study study)
        {
            // The User Element
            ODMcomplexTypeDefinitionAdminData adminElement = new ODMcomplexTypeDefinitionAdminData();
            ODMcomplexTypeDefinitionUser loopUser = new ODMcomplexTypeDefinitionUser();

            adminElement.StudyOID = study.StudyName;

            foreach (var user in study.Users)
            {
                loopUser.OID = user.UserName; // using UserName for the user identifier
                
                // switch on the data export value to fill in UserType
                // UserType: Sponsor | Investigator | Lab | Other
                switch (user.DataExport)
                {
                    case 0: // No Access
                        loopUser.UserType = UserType.Other;
                        break;
                    case 1: // Full Access
                        loopUser.UserType = UserType.Investigator;
                        break;
                    case 2: // De-Identified Access
                        loopUser.UserType = UserType.Lab;
                        break;
                    default:
                        break;
                }

                loopUser.LoginName.Value = user.UserName;
                loopUser.DisplayName.Value = user.FirstName;
                loopUser.FullName.Value = string.Format("{0} {1}", user.FirstName, user.LastName);
                loopUser.FirstName.Value = user.FirstName;
                loopUser.LastName.Value = user.LastName;
                // loopUser.Organization -- no current mapping
                // loopuser.Address -- no current mapping
                if(!string.IsNullOrEmpty(user.Email))
                {
                    ODMcomplexTypeDefinitionEmail email = new ODMcomplexTypeDefinitionEmail();
                    email.Value = user.Email;

                    loopUser.Email.Add(email);
                }
                // loopUser.Picture -- no current mapping
                // loopUser.Pager -- no current mapping
                // loopUser.Fax -- no current mapping
                // loopUser.Phone -- no current mapping
                // loopUser.LocationRef -- no current mapping
                // loopUser.Certificate -- no current mapping

                adminElement.User.Add(loopUser);
            }

            // The Location Element
            // -- no current mapping

            // The SignatureDef Element
            // -- no current mapping

            return adminElement;
        }
    }
}
