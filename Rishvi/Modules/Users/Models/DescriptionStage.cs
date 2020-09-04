using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rishvi.Modules.Users.Models
{
    public static class DescriptionStage
    {
        public static ConfigStage GetDescriptionStage
        {
            get
            {
                return new ConfigStage()
                {
                    WizardStepDescription = "Here you can insert a link to a registration for form example <a href='http://www.gmail.com?token=[{token}]'>Register Here</a> where you can replace the token with pass through token.",
                    WizardStepTitle = "Very flexible description and instructions",
                    ConfigItems = new List<ConfigItem>() {
                            new ConfigItem() {
                                ConfigItemId = "BOOLEANVALUE",
                                Description="Some question?",
                                GroupName="",
                                MustBeSpecified = true,
                                Name="Some question",
                                ReadOnly= false,
                                SelectedValue="",
                                SortOrder=1,
                                ValueType = ConfigValueType.BOOLEAN
                            }
                    }
                };
            }
        }
    }
}
