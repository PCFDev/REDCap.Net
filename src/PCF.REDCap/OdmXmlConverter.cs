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
using System.Diagnostics;

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

            foreach (var odmStudy in odm.Study)
            {
                odm.ClinicalData.Add(MapClinicalDataObject(study, odmStudy));
            }

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

            // MetaDataVersion Element (0 to many, always 1 for REDCap Study)
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

        /// <summary>Creates the MetaDataVersion object</summary>
        /// <param name="study"></param>
        /// <returns List of MetaDataVersion objects (always 1 object due to REDCap model)></returns>
        private List<ODMcomplexTypeDefinitionMetaDataVersion> MapMetadataVersion(Study study)
        {
            List<ODMcomplexTypeDefinitionMetaDataVersion> metas = new List<ODMcomplexTypeDefinitionMetaDataVersion>();
            ODMcomplexTypeDefinitionMetaDataVersion meta = new ODMcomplexTypeDefinitionMetaDataVersion();

            //Creating lists of formDefs, itemDefs, codeLists and itemGroupDefs to be loaded.
            List<ODMcomplexTypeDefinitionFormDef> forms = new List<ODMcomplexTypeDefinitionFormDef>();
            List<ODMcomplexTypeDefinitionItemGroupDef> groups = new List<ODMcomplexTypeDefinitionItemGroupDef>();
            List<ODMcomplexTypeDefinitionItemDef> itemDefs = new List<ODMcomplexTypeDefinitionItemDef>();
            List<ODMcomplexTypeDefinitionCodeList> codeLists = new List<ODMcomplexTypeDefinitionCodeList>();

            //Loading formDefs, itemGroupDefs,codeLists and itemDefs.
            //itemDefs also takes codeLists and groups to add necessary references.
            forms = GetForms(study);
            groups = GetItemGroups(study);
            codeLists = GetCodeList(study);
            itemDefs = GetItemDefs(study, groups, codeLists);

            // Include Element (optional)
            // --not porting

            // Description Element (optional) -- no current mapping
            // StudyEventRef Element (0 to many)
            foreach (var eventItem in study.Events)//This is metadataversion children level
            {
                var eventRef = new ODMcomplexTypeDefinitionStudyEventRef();
                var eventDef = new ODMcomplexTypeDefinitionStudyEventDef();

                eventDef.Name = eventItem.Arm.Name + ":" + eventItem.EventName;
                eventDef.OID = "SE." + eventItem.Arm.Name + ":" + eventItem.EventName;

                foreach (var formItem in eventItem.Instruments)
                {
                    var formRef = new ODMcomplexTypeDefinitionFormRef();
                    var groupRef = new ODMcomplexTypeDefinitionItemGroupRef();


                    //Finds form for this studyEventDef
                    var form = forms.FirstOrDefault(e => e.OID == "FM." + formItem.InstrumentName);
                    var group = groups.FirstOrDefault(e => e.OID == "IG." + formItem.InstrumentName);

                    if (form != null)
                    {
                        formRef.FormOID = form.OID;
                        formRef.OrderNumber = (eventDef.FormRef.Count + 1).ToString();
                        formRef.Mandatory = YesOrNo.No;
                        //formRef.CollectionExceptionConditionOID -- no current mapping
                    }

                    if (group != null)
                    {
                        groupRef.ItemGroupOID = group.OID;
                        groupRef.OrderNumber = "1";
                        groupRef.Mandatory = YesOrNo.No;
                    }

                    form.ItemGroupRef.Add(groupRef);
                    eventDef.FormRef.Add(formRef);
                }

                eventRef.StudyEventOID = eventDef.OID;
                eventRef.OrderNumber = (meta.Protocol.StudyEventRef.Count + 1).ToString();
                eventRef.Mandatory = YesOrNo.Yes;
                // studyRef.CollectionExceptionConditionOID -- no current mapping

                meta.CodeList = codeLists;
                meta.ItemDef = itemDefs;
                meta.ItemGroupDef = groups;
                meta.FormDef = forms;
                meta.Protocol.StudyEventRef.Add(eventRef);
                meta.StudyEventDef.Add(eventDef);
            }

            //always returning single meta due to lack of versioning in RedCap
            metas.Add(meta);

            // Alias Element (0 to many) -- no current mapping

            return metas;
        }


        /// <summary>
        /// Returns List of formDef objects from study
        /// </summary>
        /// <param name="study"></param>
        /// <returns></returns>
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


        /// <summary>
        /// Returns List of itemGroupDef objects from study
        /// </summary>
        /// <param name="study"></param>
        /// <returns></returns>
        private List<ODMcomplexTypeDefinitionItemGroupDef> GetItemGroups(Study study)
        {
            List<ODMcomplexTypeDefinitionItemGroupDef> groups = new List<ODMcomplexTypeDefinitionItemGroupDef>();
            var currentGroups = study.Events.SelectMany(e => e.Instruments).Distinct();

            foreach (var item in currentGroups)
            {
                ODMcomplexTypeDefinitionItemGroupDef group = new ODMcomplexTypeDefinitionItemGroupDef();
                group.OID = "IG." + item.InstrumentName;
                group.Name = item.InstrumentName;
                group.Repeating = YesOrNo.No;
                groups.Add(group);
            }

            return groups;
        }


        /// <summary>
        /// Returns List of itemDefs from study. 
        /// Adds itemRef to matching itemGroupDef in List groups.
        /// Loads codeListRef into itemDef using List codeLists.
        /// </summary>
        /// <param name="study"></param>
        /// <param name="groups"></param>
        /// <param name="codeLists"></param>
        /// <returns></returns>
        private List<ODMcomplexTypeDefinitionItemDef> GetItemDefs(Study study,
                                                List<ODMcomplexTypeDefinitionItemGroupDef> groups, List<ODMcomplexTypeDefinitionCodeList> codeLists)
        {
            List<ODMcomplexTypeDefinitionItemDef> itemDefs = new List<ODMcomplexTypeDefinitionItemDef>();

            foreach (var item in study.Metadata)
            {
                var itemDef = new ODMcomplexTypeDefinitionItemDef();
                var itemDescription = new ODMcomplexTypeDefinitionDescription();
                var translatedText = new ODMcomplexTypeDefinitionTranslatedText();
                var itemRef = new ODMcomplexTypeDefinitionItemRef();
                var group = groups.First(e => e.Name == item.FormName);

                //Loading codeListRef. If no codelist is generated (i.e. item has no list), codeListRef is set to null.
                var codeList = codeLists.FirstOrDefault(e => e.Name == item.FieldName);
                itemDef.CodeListRef.CodeListOID = (codeList != null) ? codeList.OID : null;

                translatedText.lang = "en";
                translatedText.Value = item.FieldLabel;
                itemDescription.TranslatedText.Add(translatedText);

                itemDef.OID = "IT." + item.FieldName;
                itemDef.Name = item.FieldName;
                itemDef.DataType = GetItemType(item);
                itemDef.Comment = item.FieldNote;
                itemDef.Description = itemDescription;

                itemRef.ItemOID = itemDef.OID;
                itemRef.Mandatory = (item.IsRequired == true) ? YesOrNo.Yes : YesOrNo.No;
                itemRef.OrderNumber = (group.ItemRef.Count + 1).ToString();

                group.ItemRef.Add(itemRef);
                itemDefs.Add(itemDef);
            }

            return itemDefs;
        }


        /// <summary>
        /// Returns DataType of itemDef based on studyItem
        /// </summary>
        /// <param name="studyItem"></param>
        /// <returns></returns>
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


        /// <summary>
        /// Returns List of codeList objects from study
        /// </summary>
        /// <param name="study"></param>
        /// <returns></returns>
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

        #endregion #region Clinical Data

        #region ClinicalData element construction

        private ODMcomplexTypeDefinitionClinicalData MapClinicalDataObject(Study study, ODMcomplexTypeDefinitionStudy odmStudy)
        {

            var clinicalData = new ODMcomplexTypeDefinitionClinicalData()
            {
                StudyOID = odmStudy.OID,
                MetaDataVersionOID = odmStudy.MetaDataVersion.First().OID
            };

            foreach (var record in study.Records)
            {

                var currentItem = new
                {
                    record = record,
                    studyEvent = study.Events.FirstOrDefault(e => e.UniqueEventName == record.EventName),
                    field = study.Metadata.FirstOrDefault(m => m.FieldName == record.Concept)
                };




                if (currentItem.field != null && currentItem.studyEvent != null)
                {


                    var currentSubjectData = clinicalData.SubjectData.FirstOrDefault(s => s.SubjectKey == currentItem.record.PatientId);

                    if (currentSubjectData == null)
                    {

                        currentSubjectData = new ODMcomplexTypeDefinitionSubjectData();

                        currentSubjectData.SubjectKey = currentItem.record.PatientId;

                        clinicalData.SubjectData.Add(currentSubjectData);
                    }



                    var studyEventOID = "SE." + currentItem.studyEvent.Arm.Name + ":" + currentItem.studyEvent.UniqueEventName;



                    var currentStudyEventData = currentSubjectData.StudyEventData.FirstOrDefault(e => e.StudyEventOID == studyEventOID);

                    if (currentStudyEventData == null)
                    {
                        currentStudyEventData = new ODMcomplexTypeDefinitionStudyEventData();
                        currentStudyEventData.StudyEventOID = studyEventOID;
                        currentSubjectData.StudyEventData.Add(currentStudyEventData);
                    }



                    var formOID = "FM." + currentItem.field.FormName;
                    var currentFormData = currentStudyEventData.FormData.FirstOrDefault(f => f.FormOID == formOID);


                    ODMcomplexTypeDefinitionItemGroupData currentItemGroupData = null;

                    if (currentFormData == null)
                    {
                        currentFormData = new ODMcomplexTypeDefinitionFormData();
                        currentFormData.FormOID = formOID;
                        currentStudyEventData.FormData.Add(currentFormData);


                        var itemGroupOID = "IG." + currentItem.field.FormName;
                        currentItemGroupData = new ODMcomplexTypeDefinitionItemGroupData();
                        currentItemGroupData.ItemGroupOID = itemGroupOID;
                        currentFormData.ItemGroupData.Add(currentItemGroupData);
                    }
                    else
                    {
                        currentItemGroupData = currentFormData.ItemGroupData.First();
                    }


                    var itemOID = "IT." + currentItem.record.Concept; // field.name;

                    //TODO: generate type specific items, so that metadata is not required to convert to i2b2 observations
                    //			switch (ODMUtil.getItem(odmStudy, itemOID).getDataType()) {
                    //			case FLOAT:
                    //				ODMcomplexTypeDefinitionItemDataFloat itemData = new ODMcomplexTypeDefinitionItemDataFloat();
                    //				currentItemGroupData.getItemDataStarGroup().add(itemData);
                    //			}

                    ODMcomplexTypeDefinitionItemData itemData = new ODMcomplexTypeDefinitionItemData();
                    itemData.ItemOID = itemOID;
                    itemData.Value = currentItem.record.ConceptValue;

                    currentItemGroupData.Items.Add(itemData); //.getItemDataGroup().add(itemData);


                }
                else
                {

                    Debug.WriteLine("Missing Data:" + currentItem.record.Concept + " - " + currentItem.record.EventName);

                }

            }

            return clinicalData;
        }

        #endregion

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

       
        */
       
    }
}
