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
using System.Linq;

namespace PCF.REDCap
{
    public class OdmXmlConverter : IOdmXmlConverter
    {
        /// <summary>Converts the parameterized REDCap study into an ODM model study.</summary>
        /// <param name="study">The PCF REDCap model study to convert.</param>
        /// <returns>An ODM study.</returns>
        public async Task<ODM> ConvertAsync(Study study)
        {
            var odm = new ODM();

            odm.ID = study.StudyName;

            // Study element
            odm.Study.Add(MapStudyObject(study));

            // AdminData element
            odm.AdminData.Add(MapAdminDataObject(study));

            // ReferenceData element
            //odm.ReferenceData.Add(MapReferenceDataObject(study));

            // ClinicalData element
            //odm.ClinicalData.Add(MapClinicalDataObject(study));

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

            adminElement.StudyOID = study.StudyName;

            foreach (var user in study.Users)
            {
                var loopUser = new ODMcomplexTypeDefinitionUser();
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

            //Creating a list of all distinct formdefs and itemgroupdefs, to be used for loading events
            List<ODMcomplexTypeDefinitionFormDef> forms = new List<ODMcomplexTypeDefinitionFormDef>();
            List<ODMcomplexTypeDefinitionItemGroupDef> groups = new List<ODMcomplexTypeDefinitionItemGroupDef>();
            List<ODMcomplexTypeDefinitionItemDef> itemDefs = new List<ODMcomplexTypeDefinitionItemDef>();
            var currentForms = study.Events.SelectMany(e => e.Instruments).Distinct();

            forms = GetForms(study);
            groups = GetItemGroups(study);
            itemDefs = GetItemDefs(study, groups);

            // Include Element (optional)
            // --not porting

            // Description Element (optional) -- no current mapping
            // StudyEventRef Element (0 to many)
            foreach (var eventItem in study.Events)//This is metadataversion children level
            {
                //var eventRefs = new List<ODMcomplexTypeDefinitionStudyEventRef>();
                var eventRef = new ODMcomplexTypeDefinitionStudyEventRef();
                var eventDef = new ODMcomplexTypeDefinitionStudyEventDef();

                eventDef.Name = eventItem.Arm.Name + ":" + eventItem.EventName;
                eventDef.OID = "SE." + eventItem.Arm.Name + ":" + eventItem.EventName;
                foreach (var formItem in eventItem.Instruments)
                {
                    var formRef = new ODMcomplexTypeDefinitionFormRef();
                    var groupRef = new ODMcomplexTypeDefinitionItemGroupRef();


                    //Finds form for this studyEventDef
                    var form = forms.First(e => e.OID == "FM." + formItem.InstrumentName);
                    var group = groups.First(e => e.OID == "IG." + formItem.InstrumentName);

                    formRef.FormOID = form.OID;
                    formRef.OrderNumber = (eventDef.FormRef.Count + 1).ToString();
                    //formRef.CollectionExceptionConditionOID -- no current mapping
                    formRef.Mandatory = YesOrNo.No;

                    groupRef.ItemGroupOID = group.OID;
                    groupRef.OrderNumber = "1";
                    groupRef.Mandatory = YesOrNo.No;

                    form.ItemGroupRef.Add(groupRef);
                    eventDef.FormRef.Add(formRef);
                }

                eventRef.StudyEventOID = eventDef.OID;
                eventRef.OrderNumber = (meta.Protocol.StudyEventRef.Count + 1).ToString();
                eventRef.Mandatory = YesOrNo.Yes;
                // studyRef.CollectionExceptionConditionOID -- no current mapping

                meta.CodeList = GetCodeList(study);
                meta.ItemDef = itemDefs;
                meta.ItemGroupDef = groups;
                meta.FormDef = forms;
                meta.Protocol.StudyEventRef.Add(eventRef);
                meta.StudyEventDef.Add(eventDef);
            }

            //always returning single meta due to lack of versioning in RedCap
            metas.Add(meta);

            // Alias Element (0 to many) -- no current mapping
            // End Protocol Element-------------------------------

            return metas;
        }

        private List<ODMcomplexTypeDefinitionFormDef> GetForms(Study study)
        {
            List<ODMcomplexTypeDefinitionFormDef> forms = new List<ODMcomplexTypeDefinitionFormDef>();
            var currentForms = study.Events.SelectMany(e => e.Instruments).Distinct();

            foreach (var item in currentForms)
            {
                ODMcomplexTypeDefinitionFormDef form = new ODMcomplexTypeDefinitionFormDef();
                form.OID = "FM." + item.InstrumentName;
                form.Name = item.InstrumentName;
                form.Repeating = YesOrNo.No;
                forms.Add(form);
            }

            return forms;
        }

        private List<ODMcomplexTypeDefinitionItemGroupDef> GetItemGroups(Study study)
        {
            List<ODMcomplexTypeDefinitionItemGroupDef> groups = new List<ODMcomplexTypeDefinitionItemGroupDef>();
            var currentForms = study.Events.SelectMany(e => e.Instruments).Distinct();

            foreach (var item in currentForms)
            {
                ODMcomplexTypeDefinitionItemGroupDef group = new ODMcomplexTypeDefinitionItemGroupDef();
                group.OID = "IG." + item.InstrumentName;
                group.Name = item.InstrumentName;
                group.Repeating = YesOrNo.No;
                groups.Add(group);
            }

            return groups;
        }

        private List<ODMcomplexTypeDefinitionItemDef> GetItemDefs(Study study, List<ODMcomplexTypeDefinitionItemGroupDef> groups)
        {
            List<ODMcomplexTypeDefinitionItemDef> itemDefs = new List<ODMcomplexTypeDefinitionItemDef>();
            var currentForms = study.Events.SelectMany(e => e.Instruments).Distinct();

            foreach (var item in study.Metadata)
            {
                var itemDef = new ODMcomplexTypeDefinitionItemDef();
                var itemDescription = new ODMcomplexTypeDefinitionDescription();
                var translatedText = new ODMcomplexTypeDefinitionTranslatedText();
                var codeLists = new List<ODMcomplexTypeDefinitionCodeList>();
                var codeListRef = new ODMcomplexTypeDefinitionCodeListRef();
                var itemRef = new ODMcomplexTypeDefinitionItemRef();
                var group = groups.First(e => e.Name == item.FormName);

                translatedText.lang = "en";
                translatedText.Value = item.FieldLabel;
                itemDescription.TranslatedText.Add(translatedText);

                itemDef.OID = "IT." + item.FieldName;
                itemDef.Name = item.FieldName;
                itemDef.DataType = GetItemType(item);
                itemDef.Comment = item.FieldNote;
                itemDef.Description = itemDescription;

                itemDef.CodeListRef = codeListRef;

                itemRef.ItemOID = itemDef.OID;
                itemRef.Mandatory = (item.IsRequired == true) ? YesOrNo.Yes : YesOrNo.No;

                itemRef.OrderNumber = (group.ItemRef.Count + 1).ToString();
                group.ItemRef.Add(itemRef);

                itemDefs.Add(itemDef);
            }

            return itemDefs;
        }

        private DataType GetItemType(Metadata studyItem)
        {
            var itemType = new DataType();

            if (studyItem.FieldType == "select"
                || studyItem.FieldType == "radio"
                || studyItem.FieldType == "textarea"
                || studyItem.FieldType == "yesno"
                || studyItem.FieldType == "slider"
                || studyItem.FieldType == "file"
                || studyItem.FieldType == "checkbox")
            {
                if (studyItem.TextValidation == "float")
                {
                    itemType = DataType.@float;
                }
                else if (studyItem.TextValidation == "integer")
                {
                    itemType = DataType.integer;
                }
                else
                {
                    itemType = DataType.text;
                }
            }
            else if (studyItem.FieldType == "calc")
            {
                itemType = DataType.@float;
            }
            else
            {
                itemType = DataType.text;
            }

            return itemType;
        }

        private List<ODMcomplexTypeDefinitionCodeList> GetCodeList(Study study)
        {
            List<ODMcomplexTypeDefinitionCodeList> codes = new List<ODMcomplexTypeDefinitionCodeList>();

            foreach (var item in study.Metadata)
            {
                var code = new ODMcomplexTypeDefinitionCodeList();

                if (item.FieldChoices.Count > 1 && item.FieldType != "calc" && item.FieldType != "slider")
                {
                    string codeListOID = item.FieldChoices.Count.ToString();

                    if (codeListOID != null)
                    {
                        codeListOID = "CL." + (codes.Count + 1);
                        code.OID = codeListOID;
                        code.Name = item.FieldName;
                        DataType itemType = GetItemType(item);
                        switch (itemType.ToString())
                        {
                            case "integer":
                                code.DataType = CLDataType.integer;
                                break;
                            case "@float":
                                code.DataType = CLDataType.@float;
                                break;
                            case "text":
                                code.DataType = CLDataType.text;
                                break;
                            case "@string":
                                code.DataType = CLDataType.@string;
                                break;
                            default:
                                code.DataType = CLDataType.text;
                                break;
                        }


                        foreach (var choice in item.FieldChoices)
                        {
                            var codeItem = new ODMcomplexTypeDefinitionCodeListItem();
                            var text = new ODMcomplexTypeDefinitionTranslatedText();

                            codeItem.CodedValue = choice.Key;
                            text.lang = "en";
                            text.Value = choice.Value;
                            codeItem.Decode.TranslatedText.Add(text);

                            code.Items.Add(codeItem);
                        }
                    }
                    codes.Add(code);
                }
            }
            return codes;
        }

        /*
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
        */
        #endregion
    }
}
