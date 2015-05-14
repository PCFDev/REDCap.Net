using System.Collections.Generic;
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

            // Study element
            odm.Study.Add(MapStudyElement(study));

            // AdminData element
            odm.AdminData.Add(MapAdminDataElement(study));

            // ReferenceData element

            // ClinicalData element

            // Association element
            // -- no current mapping

            // ds:Signature element
            // -- no current mapping

            return odm;
        }

        // AdminData element construction
        private ODMcomplexTypeDefinitionAdminData MapAdminDataElement(Study study)
        {
            // The User Element (0 to many)
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

            // The Location Element (0 to many)
            // -- no current mapping

            // The SignatureDef Element (0 to many)
            // -- no current mapping

            return adminElement;
        }

        // Study element construction
        private ODMcomplexTypeDefinitionStudy MapStudyElement(Study study)
        {
            ODMcomplexTypeDefinitionStudy odmStudy = new ODMcomplexTypeDefinitionStudy();

            // GlobalVariables Element (required)
            odmStudy.GlobalVariables = MapGlobalVariables(study);

            // BasicDefinitions Element (optional)
            // -- not porting, if even available

            // MetaDataVersion Element (0 to many)
            odmStudy.MetaDataVersion.AddRange(MapMetadataVersion(study));

            return odmStudy;
        }

        private ODMcomplexTypeDefinitionGlobalVariables MapGlobalVariables(Study study)
        {
            ODMcomplexTypeDefinitionGlobalVariables globes = new ODMcomplexTypeDefinitionGlobalVariables();

            globes.StudyName.Value = study.StudyName;
            // globes.StudyDescription -- no current mapping
            // globes.ProtocolName -- no current mapping

            return globes;
        }

        private List<ODMcomplexTypeDefinitionMetaDataVersion> MapMetadataVersion(Study study)
        {
            List<ODMcomplexTypeDefinitionMetaDataVersion> metas = new List<ODMcomplexTypeDefinitionMetaDataVersion>();

            foreach (var item in study.Metadata)
            {

            }

            return metas;
        }
    }
}
