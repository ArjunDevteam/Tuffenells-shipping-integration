using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rishvi.Modules.Users.Models
{
    public static class ValuesStage
    {
        public static ConfigStage GetValuesStage
        {
            get
            {
                return new ConfigStage()
                {
                    WizardStepDescription = "Some values you can enter on stage 1",
                    WizardStepTitle = "Stage 1 selected",
                    ConfigItems = new List<ConfigItem>() {
                        new ConfigItem() {
                                ConfigItemId = "STRINGVALUE",
                                Description="Some string value",
                                GroupName="",
                                MustBeSpecified = true,
                                Name="String value",
                                ReadOnly= false,
                                SelectedValue="Some default value",
                                SortOrder=1,
                                ValueType = ConfigValueType.STRING
                            },
                        new ConfigItem() {
                                ConfigItemId = "DOUBLE",
                                Description="Some double value",
                                GroupName="",
                                MustBeSpecified = true,
                                Name="Some double value",
                                ReadOnly= false,
                                SelectedValue="1.99",
                                SortOrder=2,
                                ValueType = ConfigValueType.DOUBLE
                            },
                        new ConfigItem() {
                                ConfigItemId = "INTVALUE",
                                Description="Some int value",
                                GroupName="",
                                MustBeSpecified = true,
                                Name="Some int value",
                                ReadOnly= false,
                                SelectedValue="1",
                                SortOrder=3,
                                ValueType = ConfigValueType.INT
                            },
                        new ConfigItem() {
                                ConfigItemId = "LISTVALUE",
                                Description="List value",
                                GroupName="",
                                MustBeSpecified = true,
                                Name="Some values",
                                ReadOnly= false,
                                SelectedValue="1",
                                SortOrder=4,
                                ValueType = ConfigValueType.LIST,
                                ListValues = new List<ConfigItemListItem>()
                                {
                                     new ConfigItemListItem() {
                                        Display = "One",
                                        Value = "1"
                                    },
                                    new ConfigItemListItem() {
                                        Display = "Two",
                                        Value = "2"
                                    },
                                }
                            },
                            new ConfigItem() {
                                ConfigItemId = "BOOLEANVALUE",
                                Description="True or false",
                                GroupName="",
                                MustBeSpecified = true,
                                Name="True or false",
                                ReadOnly= false,
                                SelectedValue="",
                                SortOrder=5,
                                ValueType = ConfigValueType.BOOLEAN
                            },
                    }
                };
            }
        }
    }
}
