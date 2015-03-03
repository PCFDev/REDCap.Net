using System;
using System.ComponentModel.DataAnnotations;

namespace REDCapWeb
{
    public class UserStudy
    {
        [Key]
        [Required]
        public int Id { get; set; }
        
        [Display(Name="Identifier Field Name")]
        public string KeyFieldName { get; set; }
        
        [Display(Name="Identifier Form Name")]
        public string KeyFormName { get; set; }
        
        [Display(Name="User Name")]
        public string UserName { get; set; }
        
        [Display(Name="Last Accessed")]
        public DateTime LastUpdated { get; set; }

        [Display(Name="Date Created")]
        public DateTime Created { get; set; }

        [Required]
        [Display(Name="Study API Key")]
        public string ApiKey { get; set; }

        [Required]
        [Display(Name="Study Name")]
        public string StudyName { get; set; }

        [Display(Name="REDCap URL")]
        public string RedCapUrl { get; set; }
    }
}