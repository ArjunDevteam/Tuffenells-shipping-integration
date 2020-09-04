using Microsoft.Extensions.Configuration;
using System;
using System.IO;

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

        public string AddressLine1 = "";
        public string AddressLine2 = "";
        public string AddressLine3 = "";
        public string City = "";
        public string ContactName = "";
        public string ContactPhoneNo = "";
        public string CountryCode = "GB";
        public string County = "";
        public string Postcode = "";
        public string Username = "";
        public string Password = "";

        public static AuthorizationConfig Load(string AuthorizationToken)
        {
            if (System.IO.File.Exists(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Files\Authorization", AuthorizationToken.ToString() + ".json")))
            {
                string json = System.IO.File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Files\Authorization", AuthorizationToken.ToString() + ".json"));
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
            if (System.IO.File.Exists(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Files\Authorization", AuthorizationToken.ToString() + ".json")))
            {
                System.IO.File.Delete(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Files\Authorization", AuthorizationToken.ToString() + ".json"));
            }
        }

        public static AuthorizationConfig CreateNew(string email, string LinnworksUniqueIdentifier, string accountName)
        {
            AuthorizationConfig output = new AuthorizationConfig();
            output.AuthorizationToken = Guid.NewGuid();
            output.Email = email;
            output.LinnworksUniqueIdentifier = LinnworksUniqueIdentifier;
            output.AccountName = accountName;
            output.Save();
            return output;
        }

        public void Save()
        {
            string jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(this);
            System.IO.StreamWriter sw = new System.IO.StreamWriter(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Files\Authorization", this.AuthorizationToken.ToString() + ".json"));
            sw.Write(jsonData);
            sw.Close();
        }

        public static void Log(string ordnum, string token, string data, string type)
        {
            string jsonData = data;
            string path = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Files\Logs", token + ".json");
            System.IO.StreamWriter sw = new System.IO.StreamWriter(token +  ".json");
            sw.Write(jsonData);
            sw.Close();
        }

    }
}
