using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rishvi.Modules.Users.Models
{
    public class LabelLogs
    {
        public Guid Id { get; set; }
        public Guid GenerateLabelId { get; set; }
        public string Log { get; set; }

        public GeneratelabelLog GeneratelabelLog { get; set; }
    }
}
