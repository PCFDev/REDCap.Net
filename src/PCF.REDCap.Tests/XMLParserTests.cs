using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace PCF.REDCap.Tests
{
    [TestClass]
    public class XMLParserTests
    {
        private XMLParser _parser = new XMLParser();

        [TestMethod]
        public void HydrateArms_Test()
        {
            var xml = @"<arms>
                        <item>
                            <arm_num><![CDATA[1]]></arm_num>
                            <name><![CDATA[Arm 1]]></name>
                            </item>
                        </arms>";


            var result = this._parser.HydrateArms(xml);

            Assert.AreEqual(1, result.Count);

        }


        [TestMethod]
        public void HydrateMetadataFields_3_FieldTypes_Test()
        {
            var xml = @"<item>
                            <field_name><![CDATA[demo_smoke]]></field_name>
                            <form_name><![CDATA[medical_history]]></form_name>
                            <section_header><![CDATA[Smoking History]]></section_header>
                            <field_type><![CDATA[radio]]></field_type>
                            <field_label><![CDATA[Do you smoke?]]></field_label>
                            <select_choices_or_calculations><![CDATA[1, Never smoked | 2, Do not currently smoke | 3, Currently smoke]]></select_choices_or_calculations>
                            <field_note></field_note>
                            <text_validation_type_or_show_slider_number></text_validation_type_or_show_slider_number>
                            <text_validation_min></text_validation_min>
                            <text_validation_max></text_validation_max>
                            <identifier></identifier>
                            <branching_logic></branching_logic>
                            <required_field><![CDATA[Y]]></required_field>
                            <custom_alignment></custom_alignment>
                            <question_number></question_number>
                            <matrix_group_name></matrix_group_name>
                            <matrix_ranking></matrix_ranking>
                          </item>";


            var result = this._parser.HydrateMetadataFields(xml);

            Assert.AreEqual(3, result.FieldChoices.Count);
            Assert.AreEqual("Never smoked", result.FieldChoices["1"]);
            Assert.AreEqual("Do not currently smoke", result.FieldChoices["2"]);
            Assert.AreEqual("Currently smoke", result.FieldChoices["3"]);

            Assert.IsTrue(result.IsRequired, "Field should be marked as required");

        }


        [TestMethod]
        public void HydrateRecords_Test()
        {

            var xml = @"<records>
                           <item>
                               <record> 2 </record>
                                <redcap_event_name> study_screening_arm_1 </redcap_event_name>
                              <field_name>labs_complete </field_name>
                               <value><![CDATA[2]]></value>
                           </item>
                           <item>
                               <record> 2 </record>
                                <redcap_event_name> study_screening_arm_1 </redcap_event_name>
                              <field_name>lab_date </field_name>
                               <value><![CDATA[2014 - 09 - 05]]></value>
                           </item>
                           <item>
                               <record> 2 </record>
                                <redcap_event_name> study_screening_arm_1 </redcap_event_name>
                                <field_name>lab_timepoint </field_name>
                                <value><![CDATA[1]]></value>
                           </item>
                           <item>
                               <record> 2 </record>
                                <redcap_event_name> study_screening_arm_1 </redcap_event_name>
                              <field_name>lab_fasting </field_name>
                               <value><![CDATA[1]]></value>
                           </item>
                           <item>
                               <record> 2 </record>
                                <redcap_event_name>study_screening_arm_1 </redcap_event_name>
                                <field_name> lab_albumin </field_name>
                                <value><![CDATA[3.5]]></value>
                           </item>
                       </records>";

           
            var result = this._parser.HydrateRecords(xml);

            Assert.AreEqual(5, result.Count());


        }
    }
}
