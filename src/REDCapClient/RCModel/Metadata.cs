using System.Collections.Generic;

namespace REDCapClient
{
    public class Metadata
    {
        public bool IsMatrixRanking { get; set; }
        public string MatrixGroupName { get; set; }
        public string QuestionNumber { get; set; }
        public string CustomAlignment { get; set; }
        public bool IsRequired { get; set; }
        public string BranchingLogic { get; set; }
        public bool IsIdentifier { get; set; }
        public string TextValidationMax { get; set; }
        public string TextValidationMin { get; set; }
        public string TextValidation { get; set; }
        public string FieldNote { get; set; }
        public string FieldCalculation { get; set; }
        public Dictionary<string, string> FieldChoices { get; set; }
        public string FieldLabel { get; set; } // Key
        public string FieldType { get; set; }
        public string FormName { get; set; }
        public string FieldName { get; set; }

        public override string ToString()
        {
            return this.FieldName;
        }
    }
}