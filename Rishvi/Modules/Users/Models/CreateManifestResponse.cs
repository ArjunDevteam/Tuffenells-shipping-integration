using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rishvi.Modules.Users.Models
{
    public class CreateManifestResponse :BaseResponse
    {
        public CreateManifestResponse(string error) : base(error) { }
        public CreateManifestResponse() : base() { }

        public string ManifestReference;
    }

    public class PrintManifestResponse : BaseResponse
    {
        public string PDFbase64;

        public PrintManifestResponse(string error) : base(error) { }
        public PrintManifestResponse() : base() { }
    }
}
