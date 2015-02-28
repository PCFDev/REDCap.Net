﻿using System.Collections.Generic;

namespace REDCapClient
{
    public class Metadata
    {
        public Metadata()
        {
            this._fieldChoices = new Dictionary<string, string>();
            this._exportFieldNames = new Dictionary<string, string>();
        }

        private Dictionary<string, string> _fieldChoices;
        private Dictionary<string, string> _exportFieldNames;

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
        public string FieldLabel { get; set; } // Key
        public string FieldType { get; set; }
        public string FormName { get; set; }
        public string FieldName { get; set; }

        public virtual Dictionary<string, string> FieldChoices
        {
            get { return this._fieldChoices; }
            set { this._fieldChoices = value; }
        }

        public virtual Dictionary<string, string> ExportFieldNames
        {
            get { return this._exportFieldNames; }
            set { this._exportFieldNames = value; }
        }

        public override string ToString()
        {
            return this.FieldName;
        }
    }
}