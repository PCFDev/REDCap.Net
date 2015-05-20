using System;
using System.Collections.Generic;
using System.Linq;
using PCF.REDCap.Web.Data.API.DB;

namespace PCF.REDCap.Web.Models.API.View.Config
{
    #region Base Models

    public class BaseConfigViewModel : BaseViewModel
    {
        public BaseConfigViewModel(IConfig config)
        {
            Id = config.Id;
            Name = config.Name;
            Url = config.Uri.AbsoluteUri;
            Key = config.Key;
            Enabled = config.Enabled;
        }

        public bool Enabled { get; set; }
        public int Id { get; set; }
        public string Key { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
    }

    #endregion Base Models

    public class DeleteViewModel : BaseViewModel
    {
    }

    public class GetIdViewModel : BaseConfigViewModel
    {
        public GetIdViewModel(IConfig config)
            : base(config)
        {
        }
    }

    public class GetViewModel : BaseViewModel
    {
        public GetViewModel(IEnumerable<IConfig> configs)
        {
            Configs = configs.Select(_ => new Config(_)).ToList();
        }

        public IList<Config> Configs { get; set; }

        public class Config
        {
            public Config(IConfig config)
            {
                Id = config.Id;
                Name = config.Name;
                Url = config.Uri.AbsoluteUri;
                Key = config.Key;
                Enabled = config.Enabled;
            }

            public bool Enabled { get; set; }
            public int Id { get; set; }
            public string Key { get; set; }
            public string Name { get; set; }
            public string Url { get; set; }
        }
    }

    public class PatchViewModel : BaseConfigViewModel
    {
        public PatchViewModel(IConfig config)
            : base(config)
        {
        }
    }

    public class PostViewModel : BaseConfigViewModel
    {
        public PostViewModel(IConfig config)
            : base(config)
        {
        }
    }

    public class PutViewModel : BaseConfigViewModel
    {
        public PutViewModel(IConfig config)
            : base(config)
        {
        }
    }
}