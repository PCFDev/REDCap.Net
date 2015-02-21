using System.Collections.Generic;

namespace REDCapClient
{
    public class Metadata
    {
        private string _fieldName;
        private string _formName;
        private string _fieldType;
        private string _fieldLabel;
        private Dictionary<string,string> _fieldChoices;
        private string _filedCalculation;
        private string _fieldNote;
        private string _textValidation;
        private string _textValidationMin;
        private string _textValidationMax;
        private bool _isIdentifier;
        private string _branchingLogic;
        private bool _isRequired;
        private string _customAlignment;
        private string _questionNumber;
        private string _matrixGroupName;
        private bool _isMatrixRanking;

        public bool IsMatrixRanking
        {
            get { return _isMatrixRanking; }
            set { _isMatrixRanking = value; }
        }

        public string MatrixGroupName
        {
            get { return _matrixGroupName; }
            set { _matrixGroupName = value; }
        }

        public string QuestionNumber
        {
            get { return _questionNumber; }
            set { _questionNumber = value; }
        }

        public string CustomAlignment
        {
            get { return _customAlignment; }
            set { _customAlignment = value; }
        }

        public bool IsRequired
        {
            get { return _isRequired; }
            set { _isRequired = value; }
        }

        public string BranchingLogic
        {
            get { return _branchingLogic; }
            set { _branchingLogic = value; }
        }

        public bool IsIdentifier
        {
            get { return _isIdentifier; }
            set { _isIdentifier = value; }
        }

        public string TextValidationMax
        {
            get { return _textValidationMax; }
            set { _textValidationMax = value; }
        }

        public string TextValidationMin
        {
            get { return _textValidationMin; }
            set { _textValidationMin = value; }
        }

        public string TextValidation
        {
            get { return _textValidation; }
            set { _textValidation = value; }
        }

        public string FieldNote
        {
            get { return _fieldNote; }
            set { _fieldNote = value; }
        }

        public Dictionary<string,string> FieldSlider
        {
            get { return _filedSlider; }
            set { _filedSlider = value; }
        }

        public string FieldCalculation
        {
            get { return _filedCalculation; }
            set { _filedCalculation = value; }
        }

        public Dictionary<string,string> FieldChoices
        {
            get { return _fieldChoices; }
            set { _fieldChoices = value; }
        }

        public string FieldLabel
        {
            get { return _fieldLabel; }
            set { _fieldLabel = value; }
        }

        public string FieldType
        {
            get { return _fieldType; }
            set { _fieldType = value; }
        }

        public string FormName
        {
            get { return _formName; }
            set { _formName = value; }
        }

        public string FieldName
        {
            get { return _fieldName; }
            set { _fieldName = value; }
        }
    }
}