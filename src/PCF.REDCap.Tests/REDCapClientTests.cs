using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using PCF.REDCap.i2b2Importer;
using System.Linq;

namespace PCF.REDCap.Tests
{
    [TestClass]
    public class REDCapClientTests
    {

        private static async Task<Study> GetStudy()
        {
            var client = new FileREDCapClient();

            var study = new Study();

            study = await client.GetStudyAsync(new ProjectConfiguration() { Name = "Study 1", ApiKey = "Key", ApiUrl = "file://test" });

            return study;
        }

        [TestMethod]
        public async Task GetStudy_Full_Model_Test()
        {
            Study study = await GetStudy();

            Assert.AreEqual("Study 1", study.StudyName);

            //Arms
            Assert.AreEqual(1, study.Arms.Count);
            Assert.AreEqual("Arm 1", study.Arms["1"]);

            //Events
            Assert.AreEqual(8, study.Events.Count());
            Assert.AreEqual("1", study.Events.First().ArmNumber);
            Assert.IsNotNull(study.Events.First().Arm);
            Assert.AreEqual("Arm 1", study.Events.First().Arm.Name);

            //Event Instruments
            Assert.IsNotNull(study.Events.First().Instruments);
            Assert.AreEqual(3, study.Events.First().Instruments.Count());
            Assert.AreEqual("demographics", study.Events.First().Instruments.First().InstrumentName);
            Assert.AreEqual("medical_history", study.Events.First().Instruments.ElementAt(1).InstrumentName);
            Assert.AreEqual("study_design", study.Events.First().Instruments.ElementAt(2).InstrumentName);


            //Metadata 
            Assert.AreEqual(974, study.Metadata.Count());
            Assert.AreEqual("ppi", study.Metadata.First().FieldName);
            Assert.AreEqual("text", study.Metadata.First().FieldType);

            var age = study.Metadata.FirstOrDefault(m => m.FieldName == "demo_age");
            Assert.IsNotNull(age);
            Assert.AreEqual("calc", age.FieldType);

            var gender = study.Metadata.FirstOrDefault(m => m.FieldName == "demo_gender");
            Assert.IsNotNull(gender);
            Assert.AreEqual("radio", gender.FieldType);
            Assert.AreEqual(2, gender.FieldChoices.Count);
            Assert.AreEqual("M", gender.FieldChoices["1"]);
            //Only one because it is a radio?
            Assert.AreEqual(1, gender.ExportFieldNames.Count);

            var race = study.Metadata.FirstOrDefault(m => m.FieldName == "demo_race");
            Assert.IsNotNull(race);
            Assert.AreEqual("checkbox", race.FieldType);
            Assert.AreEqual(6, race.FieldChoices.Count);
            Assert.AreEqual("Asian", race.FieldChoices["2"]);
            //Multiple fields because it is a checkbox
            Assert.AreEqual(6, race.ExportFieldNames.Count);

            //Users
            Assert.AreEqual(10, study.Users.Count());
            Assert.IsTrue(study.Users.Any(u => u.UserName == "kids_kenneyk"));

        }


        [TestMethod]
        public async Task REDCap_Instruments_Loaded()
        {
            var study = await GetStudy();

            //Get all forms in the study
            var forms = study.Events.SelectMany(e => e.Instruments).Distinct();

            Assert.AreEqual(18, forms.Count());


        }

    }
}
