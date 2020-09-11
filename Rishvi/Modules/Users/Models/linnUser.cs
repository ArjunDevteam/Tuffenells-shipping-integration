using System;

namespace Rishvi.Modules.Linn.Models
{
    public class linnUser
    {
        public Guid Id { get; set; }
        public string LinnToken { get; set; }
        public int MaxImagesPerStockItem { get; set; }
        public string FullName { get; set; }
        public string Company { get; set; }
        public string DatabaseName { get; set; }
        public string ProductName { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool SuperAdmin { get; set; }
        public int SessionUserId { get; set; }
        public string Device { get; set; }
        public string DeviceType { get; set; }
        public string Server { get; set; }
        public string PushServer { get; set; }
        //public Status Status { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string UserType { get; set; }
        public string Token { get; set; }
        public string EntityId { get; set; }
        public string GroupName { get; set; }
        public string Locality { get; set; }
        //public Properties Properties { get; set; }
        public string Md5Hash { get; set; }
        public bool IsAccountHolder { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public bool isMainUser { get; set; }
        public bool isTrial { get; set; }
        public int noOfUser { get; set; }
        public string type { get; set; }
        public DateTime subscribedOn { get; set; }
        public int DaysLeft { get; set; }
        public bool isSubscribed { get; set; }
    }

    public class Status
    {
        public string State { get; set; }
        public string Reason { get; set; }
        //public Parameters Parameters { get; set; }
    }

    public class Properties
    {
    }

    public class Parameters
    {
    }

    public class TokenModel
    {
        public string email { get; set; }
        public string maintoken { get; set; }
        public string sessiontoken { get; set; }
        public string server { get; set; }
    }
}
