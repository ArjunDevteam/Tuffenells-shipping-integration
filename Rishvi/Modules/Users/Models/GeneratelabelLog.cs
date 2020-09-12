using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rishvi.Modules.Users.Models
{
    public class GeneratelabelLog
    {
        public Guid Id { get; set; }
        public string Token { get; set; }
        public string Orderid { get; set; }
        public string Orderreference { get; set; }
        //public List<string> Logs { get; set; }
        public string Logs { get; set; }
        public DateTime Created { get; set; }
        public string Labelid { get; set; }
        public bool Iserror { get; set; }
        public string Error { get; set; }
        public string Linnrequest { get; set; }
        public string Linnresponse { get; set; }
        public string DHLrequest { get; set; }
        public string DHLresponse { get; set; }
    }
}
