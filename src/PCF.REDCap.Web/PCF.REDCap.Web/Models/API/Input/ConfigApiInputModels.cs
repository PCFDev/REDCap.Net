using System;
using System.ComponentModel.DataAnnotations;
using PCF.REDCap.Web.Data.API.DB;
using PCF.REDCap.Web.Data.API.DTO;

namespace PCF.REDCap.Web.Models.API.Input.Config
{
    public class PatchInputModel
    {
        public bool? Enabled { get; set; }
        [StringLength(32, MinimumLength = 32)]
        public string Key { get; set; }
        [StringLength(255)]
        public string Name { get; set; }
        [StringLength(2048), Url]
        public string Url { get; set; }

        //TODO: Proper change tracking
        public UpdateConfig GetDTO(IConfig config)
        {
            return new UpdateConfig
            {
                Enabled = Enabled ?? config.Enabled,
                Key = Key ?? config.Key,
                Name = Name ?? config.Name,
                Url = Url ?? config.Url
            };
        }
    }

    public class PostInputModel
    {
        [Required]
        public bool Enabled { get; set; }
        [Required, StringLength(32, MinimumLength = 32)]
        public string Key { get; set; }
        [Required, StringLength(255)]
        public string Name { get; set; }
        [Required, StringLength(2048), Url]
        public string Url { get; set; }

        public AddConfig GetDTO()
        {
            return new AddConfig
            {
                Enabled = Enabled,
                Key = Key,
                Name = Name,
                Url = Url
            };
        }
    }

    public class PutInputModel
    {
        [Required]
        public bool Enabled { get; set; }
        [Required, StringLength(32, MinimumLength = 32)]
        public string Key { get; set; }
        [Required, StringLength(255)]
        public string Name { get; set; }
        [Required, StringLength(2048), Url]
        public string Url { get; set; }

        public UpdateConfig GetDTO()
        {
            throw new NotImplementedException();
        }
    }
}