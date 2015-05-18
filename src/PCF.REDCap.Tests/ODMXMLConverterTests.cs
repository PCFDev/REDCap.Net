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
        [TestMethod]
        public async Task MainTest()
        {
            var client = new FileREDCapClient();

            var study = new Study();

            study = await client.GetStudyAsync(new ProjectConfiguration() { Name = "Study 1", ApiKey = "Key", ApiUrl = "file://test" });

            var OdmXml = new OdmXmlConverter();

            var result = await OdmXml.ConvertAsync(study);

            Assert.IsNotNull(result);

        }


        [TestMethod]
        public async Task GetAllFormsFromStudy()
        {
            var client = new FileREDCapClient();

            var study = new Study();

            study = await client.GetStudyAsync(new ProjectConfiguration() { Name = "Study 1", ApiKey = "Key", ApiUrl = "file://test" });

            //Get all forms in the study
            var forms = study.Events.SelectMany(e => e.Instruments).Distinct();

            Assert.AreEqual(18, forms.Count());


        }
    }
}
