using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCF.REDCap.OdmXml
{
    public class Converter
    {
        public async Task<ODMcomplexTypeDefinitionStudy> ConvertStudy(PCF.REDCap.Model.REDCapStudy rcStudy)
        {

            var study = new ODMcomplexTypeDefinitionStudy()
            {
                OID = rcStudy.StudyName,
                BasicDefinitions = new ODMcomplexTypeDefinitionBasicDefinitions()
                {
                    MeasurementUnit = new ODMcomplexTypeDefinitionMeasurementUnit[] { new ODMcomplexTypeDefinitionMeasurementUnit() { Name = "lbs." } }
                },
                GlobalVariables = new ODMcomplexTypeDefinitionGlobalVariables()
                {
                    ProtocolName = new ODMcomplexTypeDefinitionProtocolName() { Value = "" },
                    StudyName = new ODMcomplexTypeDefinitionStudyName() { Value = rcStudy.StudyName },
                    StudyDescription = new ODMcomplexTypeDefinitionStudyDescription() { Value = "" }
                },
                MetaDataVersion = new ODMcomplexTypeDefinitionMetaDataVersion[]
                {
                    new ODMcomplexTypeDefinitionMetaDataVersion()
                    {
                       FormDef = new ODMcomplexTypeDefinitionFormDef[]
                        {
                            new ODMcomplexTypeDefinitionFormDef()
                            {

                                ItemGroupRef = new ODMcomplexTypeDefinitionItemGroupRef[]
                                {
                                    new ODMcomplexTypeDefinitionItemGroupRef()
                                    {
                                        Mandatory = YesOrNo.Yes

                                    }
                                }
                            }
                        }
                    }
                }
            };



            var fieldGroup = new ODMcomplexTypeDefinitionItemGroupDef()
            {
                OID = "1",
                IsReferenceData = YesOrNo.No,
                ItemRef = new ODMcomplexTypeDefinitionItemRef[] { new ODMcomplexTypeDefinitionItemRef() { ItemOID = "Item1" } }

            };

            var field = new ODMcomplexTypeDefinitionItemDef()
            {

            };

            return await Task.FromResult(study);

        }

    }
}
