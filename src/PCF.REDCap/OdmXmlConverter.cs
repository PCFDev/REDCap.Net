//---------------------------------------
//  PCF REDCap to ODM
//
//  REDCap model v1.0
//  ODM model v1.3.2
//----------------------------------------
using System.Collections.Generic;
using System.Threading.Tasks;
using PCF.OdmXml;
using PCF.REDCap;
using System;

namespace PCF.REDCap
{
    public class OdmXmlConverter
    {
        /// <summary>Converts the parameterized REDCap study into an ODM model study.</summary>
        /// <param name="study">The PCF REDCap model study to convert.</param>
        /// <returns>An ODM study.</returns>
        public async Task<ODM> Convert(Study study)
        {
            var odm = new ODM();

            odm.ID = study.StudyName;

            // Study element
            odm.Study.Add(MapStudyObject(study));

            // AdminData element
            odm.AdminData.Add(MapAdminDataObject(study));

            // ReferenceData element
            odm.ReferenceData.Add(MapReferenceDataObject(study));

            // ClinicalData element
            odm.ClinicalData.Add(MapClinicalDataObject(study));

            // Association element
            // -- no current mapping

            // ds:Signature element
            // -- no current mapping

            return odm;
        }

        #region ADMINDATA element construction
        /// <summary>Creates the ODM AdminData object and flushes out its User object(s).</summary>
        /// <param name="study">The PCF REDCap model study to convert.</param>
        /// <returns>An ODM AdminData object.</returns>
        private ODMcomplexTypeDefinitionAdminData MapAdminDataObject(Study study)
        {
            // User Element (0 to many)
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
                if (!string.IsNullOrEmpty(user.Email))
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

            // Location Element (0 to many)
            // -- no current mapping

            // SignatureDef Element (0 to many)
            // -- no current mapping

            return adminElement;
        }
        #endregion

        #region STUDY element construction
        /// <summary>Creates the ODM Study object and flushes out its GlobalVariables, BasicDefinitions, and MetaDataVersion objects.</summary>
        /// <param name="study">The PCF REDCap model study to convert.</param>
        /// <returns>An ODM Study object.</returns>
        private ODMcomplexTypeDefinitionStudy MapStudyObject(Study study)
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

        /// <summary>Creates the GlobalVariables object.</summary>
        /// <param name="study">The PCF REDCap model study to convert.</param>
        /// <returns>An ODM GlobalVariables object.</returns>
        private ODMcomplexTypeDefinitionGlobalVariables MapGlobalVariables(Study study)
        {
            ODMcomplexTypeDefinitionGlobalVariables globes = new ODMcomplexTypeDefinitionGlobalVariables();

            globes.StudyName.Value = study.StudyName;
            // globes.StudyDescription -- no current mapping
            // globes.ProtocolName -- no current mapping

            return globes;
        }

        /// <summary></summary>
        /// <param name="study"></param>
        /// <returns></returns>
        private List<ODMcomplexTypeDefinitionMetaDataVersion> MapMetadataVersion(Study study)
        {
            List<ODMcomplexTypeDefinitionMetaDataVersion> metas = new List<ODMcomplexTypeDefinitionMetaDataVersion>();
            ODMcomplexTypeDefinitionMetaDataVersion meta = new ODMcomplexTypeDefinitionMetaDataVersion();

            // Include Element (optional)
            // --not porting

            // Description Element (optional) -- no current mapping
            // StudyEventRef Element (0 to many)
            foreach (var eventItem in study.Events)
            {
                var studyRef = new ODMcomplexTypeDefinitionStudyEventRef();
                var studyDef = new ODMcomplexTypeDefinitionStudyEventDef();

                studyDef.Name = eventItem.Arm.Name + ":" + eventItem.EventName;
                studyDef.OID = "SE." + eventItem.Arm.Name + ":" + eventItem.EventName;
                foreach (var formItem in eventItem.Instruments)
                {
                    var formRef = new ODMcomplexTypeDefinitionFormRef();

                    formRef.FormOID = "FM." + formItem.InstrumentName;
                    formRef.OrderNumber = (studyDef.FormRef.Count + 1).ToString();
                    //formRef.CollectionExceptionConditionOID -- no current mapping
                    formRef.Mandatory = YesOrNo.No;
                    studyDef.FormRef.Add(formRef);
                }

                studyRef.StudyEventOID = studyDef.OID;
                studyRef.OrderNumber = (meta.Protocol.StudyEventRef.Count + 1).ToString();
                // studyRef.Mandatory -- no current mapping
                // studyRef.CollectionExceptionConditionOID -- no current mapping

                meta.Protocol.StudyEventRef.Add(studyRef);
                meta.StudyEventDef.Add(studyDef);
            }

            //always returning single meta due to lack of versioning in RedCap
            metas.Add(meta);

            // Alias Element (0 to many) -- no current mapping
            // End Protocol Element-------------------------------

            return metas;
        }

        //Stubs. Not Complete!
        private ODMcomplexTypeDefinitionReferenceData MapReferenceDataObject(Study study)
        {
            ODMcomplexTypeDefinitionStudy odmStudy = new ODMcomplexTypeDefinitionStudy();

            // GlobalVariables Element (required)
            odmStudy.GlobalVariables = MapGlobalVariables(study);

            // BasicDefinitions Element (optional)
            // -- not porting, if even available

            // MetaDataVersion Element (0 to many)
            odmStudy.MetaDataVersion.AddRange(MapMetadataVersion(study));

            //return odmStudy;
            throw new NotImplementedException();
        }

        //Stubs. Not Complete!
        private ODMcomplexTypeDefinitionClinicalData MapClinicalDataObject(Study study)
        {
            ODMcomplexTypeDefinitionStudy odmStudy = new ODMcomplexTypeDefinitionStudy();

            // GlobalVariables Element (required)
            odmStudy.GlobalVariables = MapGlobalVariables(study);

            // BasicDefinitions Element (optional)
            // -- not porting, if even available

            // MetaDataVersion Element (0 to many)
            odmStudy.MetaDataVersion.AddRange(MapMetadataVersion(study));

            throw new NotImplementedException();
        }
        #endregion
    }
}
