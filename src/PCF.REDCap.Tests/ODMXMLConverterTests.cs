using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PCF.REDCap.i2b2Importer;
using System.Threading.Tasks;
using System.Linq;

namespace PCF.REDCap.Tests
{
    /// <summary>
    /// Summary description for ODMXMLConverterTests
    /// </summary>
    [TestClass]
    public class ODMXMLConverterTests
    {

        private OdmXmlConverter _converter = new OdmXmlConverter();

        private static async Task<Study> GetStudy()
        {
            var client = new FileREDCapClient();

            var study = new Study();

            study = await client.GetStudyAsync(new ProjectConfiguration() { Name = "Study 1", ApiKey = "Key", ApiUrl = "file://test" });

            return study;
        }


        [TestMethod]
        public async Task MainTest()
        {
            var study = await GetStudy();

            var result = await this._converter.ConvertAsync(study);

            Assert.IsNotNull(result);

        }



        [TestMethod]
        public async Task Convert_Forms_To_ItemGroupDefs()
        {
            var study = await GetStudy();

            var result = await this._converter.ConvertAsync(study);

            Assert.IsNotNull(result);

            var redCapForms = study.Events.SelectMany(e => e.Instruments).Distinct().OrderBy(f => f.InstrumentName);
            var odmItemGroupDefs = result.Study.First().MetaDataVersion.First().ItemGroupDef.OrderBy(i => i.Name);

            Assert.AreEqual(redCapForms.Count(), odmItemGroupDefs.Count());

            for (int i = 0; i < redCapForms.Count(); i++)
            {
                Assert.AreEqual(redCapForms.ElementAt(i).InstrumentName, odmItemGroupDefs.ElementAt(i).Name);

            }

        }

        [TestMethod]
        public async Task Create_CodeLists_From_Study_Metadata()
        {
            var study = await GetStudy();

            var lists = study.Metadata.Where(m => m.FieldType == "radio" || m.FieldType == "checkbox").OrderBy(f => f.FieldName);

            var result = await this._converter.ConvertAsync(study);

            var codeLists = result.Study.First().MetaDataVersion.First().CodeList.OrderBy(c => c.Name);

            Assert.AreEqual(lists.Count(), codeLists.Count(), "Number of fields with choices in REDCap differ from the number in ODM");

            for (int i = 0; i < lists.Count(); i++)
            {
                var codeList = codeLists.ElementAt(i);

                var field = lists.ElementAt(i);

                Assert.AreEqual(field.FieldName, codeList.Name, "Field name does not match the code list name");

                Assert.AreEqual(field.FieldChoices.Count, codeList.Items.Count, "The number of choices does not match the number of CodeListItems");

                foreach (var item in field.FieldChoices)
                {
                    Assert.IsTrue(codeList.Items
                        .OfType<OdmXml.ODMcomplexTypeDefinitionCodeListItem>()
                        .Count(c => c.CodedValue == item.Key && c.Decode.TranslatedText.First().Value == item.Value) == 1, 
                        item.Key + " - " + item.Value + " was not found in the list of CodeListItems");
                }

            }

        }
    }
}
