using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rishvi.Modules.Users.Models
{
    public class CreateManifestRequest : BaseRequest
    {
        public List<string> OrderId = new List<string>();
        public string OrderReference { get; set; }
    }

    public class PrintManifestRequest : BaseRequest
    {
        public string ManifestReference;
    }

}
