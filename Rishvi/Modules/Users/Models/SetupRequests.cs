using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rishvi.Modules.Users.Models
{
    public class AddNewUserRequest
    {
        public string Email;
        public string LinnworksUniqueIdentifier;
        public string AccountName;
    }

    public class UserConfigRequest : BaseRequest
    {

    }

    public class UpdateConfigRequest : BaseRequest
    {
        public string ConfigStatus;
        public List<UserStageConfigItem> ConfigItems = new List<UserStageConfigItem>();
    }
    public class ConfigDeleteRequest : BaseRequest
    {

    }

    public class UserAvailableServicesRequest : BaseRequest
    {

    }

    public class ExtendedPropertyMappingRequest : BaseRequest
    {

    }
}
