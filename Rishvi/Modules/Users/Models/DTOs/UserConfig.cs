using Rishvi.Modules.Core.Filters;
using System;
using System.Collections.Generic;

namespace Rishvi.Modules.UserConfig.Models.DTOs
{
	public class UserConfigDto 
	{
        
    }
    public class UserStageConfig
    {
        public string ConfigStage { get; set; }
        public List<UserStageConfigItem> Items = new List<UserStageConfigItem>();
    }


    public class UserStageConfigItem
    {
        public string ConfigItemId { get; set; }
        public string SelectedValue { get; set; }
    }
}