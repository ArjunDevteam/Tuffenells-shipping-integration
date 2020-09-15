using Microsoft.Extensions.Configuration;
using Spinx.Web.Modules.Core.Aws;
using System;
using System.IO;
using System.Text;

namespace Rishvi.Modules.Users.Models
{
    public class AuthorizationConfig
    {
        public string Email;
        public string LinnworksUniqueIdentifier;
        public DateTime IntegratedDateTime = DateTime.UtcNow;
        public Guid AuthorizationToken;
        public string AccountName;
        public Boolean IsConfigActive = false;
        public string ConfigStatus = "";

        public string ContactName = "";
        public string AddressLine1 = "";
        public string AddressLine2 = "";
        public string AddressLine3 = "";
        public string City = "";
        public string CompanyName = "";
        public string ContactPhoneNo = "";
        public string CountryCode = "GB";
        public string County = "";
        public string PostCode = "";
        public string Username = "";
        public string Password = "";
        public string AccountNumber = "";

        public static string AppUsername = "DHLDEnew_1";
        public static string AppPassword = "WxR0oK2Ga7QABd33LgGppLt4zwjjBQ";
        public static string DHLStage = "production";
        public static string LangKey = "trnsl.1.1.20200123T043541Z.b7d75565cb7be549.8deb73d59b920971dd950410b5404aeb1f847ad1";

        public string serverPath = string.Empty;

        public static AuthorizationConfig Load(string AuthorizationToken)
        {
            if (AwsS3.S3FileIsExists("Files/" + AuthorizationToken + ".json"))
            {
                string json = AwsS3.GetS3File("Files/" + AuthorizationToken + ".json");
                AuthorizationConfig output = Newtonsoft.Json.JsonConvert.DeserializeObject<AuthorizationConfig>(json);
                return output;
            }
            else
            {
                return null;
            }
        }

        public static void Delete(string AuthorizationToken)
        {
            if (AwsS3.S3FileIsExists("Files/" + AuthorizationToken + ".json"))
            {
                AwsS3.DeleteImageToAws("Files/" + AuthorizationToken + ".json");
            }
        }

        public static AuthorizationConfig CreateNew(string email, string LinnworksUniqueIdentifier, string accountName, string serverPath)
        {
            AuthorizationConfig output = new AuthorizationConfig();
            output.AuthorizationToken = Guid.NewGuid();
            output.Email = email;
            output.LinnworksUniqueIdentifier = LinnworksUniqueIdentifier;
            output.AccountName = accountName;
            output.serverPath = serverPath;
            output.Save();
            return output;
        }

        public void Save()
        {
            string jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(this);
            Stream stream = GenerateStreamFromString(jsonData);

            AwsS3.UploadFileToS3(stream, "Files/" + this.AuthorizationToken.ToString() + ".json");
        }

        public static void Log(string ordnum, string token, string data, string type)
        {
            string jsonData = data;
            Stream stream = GenerateStreamFromString(jsonData);


            AwsS3.UploadFileToS3(stream, "Logs/" + token + ".json");
        }

        public static Stream GenerateStreamFromString(string s)
        {
            var stream = new MemoryStream();
            System.IO.StreamWriter sw = new System.IO.StreamWriter(stream);
            sw.Write(s);
            sw.Flush();
            stream.Position = 0;
            return stream;
        }

        public static string BuildBasicAuthenticationString(string username, string password)
        {
            var byteArray = Encoding.ASCII.GetBytes(string.Format("{0}:{1}", username, password));
            return Convert.ToBase64String(byteArray);
        }

    }
}
