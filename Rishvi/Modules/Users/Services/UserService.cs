using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;
using Rishvi.Modules.AddNewRequest.Models.DTOs;
using Rishvi.Modules.Core;
using Rishvi.Modules.Core.Content;
using Rishvi.Modules.Core.Data;
using Rishvi.Modules.Core.Filters;
using Rishvi.Modules.Core.Helpers;
using Rishvi.Modules.Linn.Models;
using Rishvi.Modules.Users.Models;
using Rishvi.Modules.Users.Models.DTOs;
using Rishvi.Modules.Users.Validators;
using Spinx.Web.Modules.Core.Aws;
using Spire.Pdf;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace Rishvi.Modules.Users.Services
{
    public interface IUserService
    {
        AddNewUserResponse CreateNewUser(AddNewRequestDto dto);
        UserConfigResponse UserConfig(UserConfigRequest request);
        UpdateConfigResponse UpdateConfig(UpdateConfigRequest request);
        ConfigDeleteResponse ConfigDelete(ConfigDeleteRequest request);
        UserAvailableServicesResponse UserAvailableServices(UserAvailableServicesRequest request);
        Models.ExtendedPropertyMappingResponse ExtendedPropertyMapping(Models.ExtendedPropertyMappingRequest request);
        GenerateLabelResponse GenerateLabel(Models.GenerateLabelRequest request);
        CancelLabelResponse CancelLabel(CancelLabelRequest request);
        CreateManifestResponse CreateManifest(CreateManifestRequest request);
        PrintManifestResponse PrintManifest(PrintManifestRequest request);
        TokenModel Install(string token);
        Result InsertData(GenerateLabelFilterDto dto);
        UserAvailableServicesResponse UserAvailableServices(string AuthorizationToken);

        string test();
    }

    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<linnUser> _linnuserRepository;
        private readonly IRepository<GeneratelabelLog> _generateLabelRepository;
        private readonly IRepository<LabelLogs> _labelLogsRepository;
        private readonly IRepository<GenerateLabelCount> _generatelabelcountRepository;
        private readonly IMapper _mapper;
        public UserEditValidator UserEditValidator = new UserEditValidator();
        public UserDeleteValidator UserDeleteValidator = new UserDeleteValidator();
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IConfiguration _config;
        public UserService(IRepository<User> userRepository, IRepository<linnUser> linnuserRepository, IRepository<GeneratelabelLog> generateLabelRepository, IRepository<LabelLogs> labelLogsRepository, IRepository<GenerateLabelCount> generatelabelcountRepository, IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment environment, IConfiguration config)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _linnuserRepository = linnuserRepository;
            _generateLabelRepository = generateLabelRepository;
            _labelLogsRepository = labelLogsRepository;
            _generatelabelcountRepository = generatelabelcountRepository;
            _unitOfWork = unitOfWork;
            _hostingEnvironment = environment;
            _config = config;
        }

        public AddNewUserResponse CreateNewUser(AddNewRequestDto dto)
        {
            try
            {
                AuthorizationConfig newConfig = AuthorizationConfig.CreateNew(dto.Email, dto.LinnworksUniqueIdentifier, dto.AccountName, _hostingEnvironment.WebRootPath);
                return new AddNewUserResponse()
                {
                    AuthorizationToken = newConfig.AuthorizationToken.ToString()
                };
            }
            catch (Exception ex)
            {
                return new AddNewUserResponse("AddNewUser error : " + ex.Message);
            }
        }


        public UserConfigResponse UserConfig(UserConfigRequest request)
        {
            // authenticate the user first
            AuthorizationConfig auth = AuthorizationConfig.Load(request.AuthorizationToken);
            if (auth == null)
            {
                return new UserConfigResponse()
                {
                    IsError = true,
                    ErrorMessage = "Authorization failed for token " + request.AuthorizationToken
                };
            }

            // if config is not activated, i.e. in Wizard Stage - the user is going through the integration steps
            if (!auth.IsConfigActive)
            {
                if (auth.ConfigStatus == "")    // if new integration, lets assign a config stage to the integration process, in this case we will ask for Customer Details
                {
                    auth.ConfigStatus = "ContactStage";
                    auth.Save();
                }

                if (auth.ConfigStatus == "ContactStage")    // if config stage is ContactStage return the appropriate stage
                {
                    return new UserConfigResponse()
                    {
                        ConfigStage = ContactStage.GetContactStage,
                        ConfigStatus = "ContactStage"
                    };
                }

                if (auth.ConfigStatus == "ValuesStage")
                {
                    return new UserConfigResponse()
                    {
                        ConfigStage = ValuesStage.GetValuesStage,
                        ConfigStatus = "ValuesStage"
                    };
                }
                if (auth.ConfigStatus == "DescriptionStage")
                {
                    return new UserConfigResponse()
                    {
                        ConfigStage = DescriptionStage.GetDescriptionStage,
                        ConfigStatus = "DescriptionStage"
                    };
                }

                // finally if we don't have a specific handler for the stage name, throw an error
                return new UserConfigResponse("Config stage is not handled: " + auth.ConfigStatus);
            }
            else   // if the integration config is in comlpeted stage (i.e. this is an active config)
            {
                return new UserConfigResponse()
                {
                    ConfigStage = UserConfigStage.GetUserConfigStage(auth),
                    IsConfigActive = true,      // MUST SET THIS TO TRUE for the config to actuall be treated as completed               
                    ConfigStatus = "CONFIG"
                };
            }
        }

        public UpdateConfigResponse UpdateConfig(UpdateConfigRequest request)
        {
            try
            {
                // first authenticate the user and load it's config file

                AuthorizationConfig auth = AuthorizationConfig.Load(request.AuthorizationToken);
                if (auth == null)
                {
                    return new UpdateConfigResponse("Authorization failed for token " + request.AuthorizationToken);
                }


                // lets make sure the configstage known by linnworks is the same as what we have locally. This is important to avoid discrepency in the steps
                if (auth.ConfigStatus != request.ConfigStatus)
                {
                    return new UpdateConfigResponse("Current config stage is not what is sent in the Update");
                }


                // when current stage is ContactStage lets save details specific by the user into the configuration file locally
                if (auth.ConfigStatus == "ContactStage")
                {
                    auth.ContactName = request.ConfigItems.Find(s => s.ConfigItemId == "NAME").SelectedValue;
                    auth.CompanyName = request.ConfigItems.Find(s => s.ConfigItemId == "COMPANY").SelectedValue;
                    auth.AddressLine1 = request.ConfigItems.Find(s => s.ConfigItemId == "ADDRESS1").SelectedValue;
                    auth.AddressLine2 = request.ConfigItems.Find(s => s.ConfigItemId == "ADDRESS2").SelectedValue;
                    auth.AddressLine3 = request.ConfigItems.Find(s => s.ConfigItemId == "ADDRESS3").SelectedValue;
                    auth.City = request.ConfigItems.Find(s => s.ConfigItemId == "CITY").SelectedValue;
                    auth.County = request.ConfigItems.Find(s => s.ConfigItemId == "REGION").SelectedValue;
                    auth.CountryCode = request.ConfigItems.Find(s => s.ConfigItemId == "COUNTRY").SelectedValue;
                    auth.ContactPhoneNo = request.ConfigItems.Find(s => s.ConfigItemId == "TELEPHONE").SelectedValue;
                    auth.PostCode = request.ConfigItems.Find(s => s.ConfigItemId == "POSTCODE").SelectedValue;
                    auth.Username = request.ConfigItems.Find(s => s.ConfigItemId == "USERNAME").SelectedValue;
                    auth.Password = request.ConfigItems.Find(s => s.ConfigItemId == "PASSWORD").SelectedValue;
                    auth.AccountNumber = request.ConfigItems.Find(s => s.ConfigItemId == "ACCOUNTNUMBER").SelectedValue;
                    auth.ConfigStatus = "CONFIG";
                    auth.IsConfigActive = true;

                    // if user selected that the next stage should be ValuesStage, lets switch current state of the config to ValuesStage, alternativly we will route the config stage to DescriptionStage
                    if (request.ConfigItems.Find(s => s.ConfigItemId == "STAGE_SELECT").SelectedValue == "ValuesStage")
                    {
                        auth.ConfigStatus = "ValuesStage";
                    }
                    else
                    {
                        auth.ConfigStatus = "DescriptionStage";
                    }
                    auth.Save();
                    return new UpdateConfigResponse();
                }
                else if (auth.ConfigStatus == "ValuesStage")
                {
                    int intValue = 0;
                    if (int.TryParse(request.ConfigItems.Find(s => s.ConfigItemId == "INTVALUE").SelectedValue, out intValue))
                    {
                        if (intValue < 10)
                        {
                            return new UpdateConfigResponse("Some Int Value must be greater than 10. This is just some validation on the integration side;");
                        }
                    }
                    else
                    {
                        return new UpdateConfigResponse("Some Int Value is not an int. WTF? ");
                    }
                    auth.ConfigStatus = "DescriptionStage";
                    auth.Save();
                    return new UpdateConfigResponse();
                }
                else if (auth.ConfigStatus == "DescriptionStage")   // ON THE Final step we need to activate the config and set the status to CONFIG
                {
                    auth.IsConfigActive = true;
                    auth.ConfigStatus = "CONFIG";
                    auth.Save();
                    return new UpdateConfigResponse();
                }
                else if (auth.ConfigStatus == "CONFIG" || auth.IsConfigActive)  // if the config is active the user can only change config properties
                {
                    auth.IsConfigActive = true;
                    auth.ContactName = request.ConfigItems.Find(s => s.ConfigItemId == "NAME").SelectedValue;
                    auth.CompanyName = request.ConfigItems.Find(s => s.ConfigItemId == "COMPANY").SelectedValue;
                    auth.AddressLine1 = request.ConfigItems.Find(s => s.ConfigItemId == "ADDRESS1").SelectedValue;
                    auth.AddressLine2 = request.ConfigItems.Find(s => s.ConfigItemId == "ADDRESS2").SelectedValue;
                    auth.AddressLine3 = request.ConfigItems.Find(s => s.ConfigItemId == "ADDRESS3").SelectedValue;
                    auth.City = request.ConfigItems.Find(s => s.ConfigItemId == "CITY").SelectedValue;
                    auth.County = request.ConfigItems.Find(s => s.ConfigItemId == "REGION").SelectedValue;
                    auth.CountryCode = request.ConfigItems.Find(s => s.ConfigItemId == "COUNTRY").SelectedValue;
                    auth.ContactPhoneNo = request.ConfigItems.Find(s => s.ConfigItemId == "TELEPHONE").SelectedValue;
                    auth.PostCode = request.ConfigItems.Find(s => s.ConfigItemId == "POSTCODE").SelectedValue;
                    auth.Username = request.ConfigItems.Find(s => s.ConfigItemId == "USERNAME").SelectedValue;
                    auth.Password = request.ConfigItems.Find(s => s.ConfigItemId == "PASSWORD").SelectedValue;
                    auth.AccountNumber = request.ConfigItems.Find(s => s.ConfigItemId == "ACCOUNTNUMBER").SelectedValue;
                    auth.Save();
                    return new UpdateConfigResponse();
                }
                else
                {
                    return new UpdateConfigResponse(auth.ConfigStatus + " is not handled");
                }
            }
            catch (Exception ex)
            {
                return new UpdateConfigResponse("Unhandled exception saving user config: " + ex.Message);
            }
        }

        public ConfigDeleteResponse ConfigDelete(ConfigDeleteRequest request)
        {
            AuthorizationConfig auth = AuthorizationConfig.Load(request.AuthorizationToken);
            if (auth == null)
            {
                return new ConfigDeleteResponse("Authorization failed for token " + request.AuthorizationToken);
            }
            AuthorizationConfig.Delete(request.AuthorizationToken);
            return new ConfigDeleteResponse();
        }

        public UserAvailableServicesResponse UserAvailableServices(UserAvailableServicesRequest request)
        {
            AuthorizationConfig auth = AuthorizationConfig.Load(request.AuthorizationToken);
            if (auth == null)
            {
                return new UserAvailableServicesResponse("Authorization failed for token " + request.AuthorizationToken);
            }
            return new UserAvailableServicesResponse()
            {
                Services = ServicesDto.GetServices
            };
        }

        public Models.ExtendedPropertyMappingResponse ExtendedPropertyMapping(Models.ExtendedPropertyMappingRequest request)
        {
            AuthorizationConfig auth = AuthorizationConfig.Load(request.AuthorizationToken);
            if (auth == null)
            {
                return new Models.ExtendedPropertyMappingResponse("Authorization failed for token " + request.AuthorizationToken);
            }
            return new Models.ExtendedPropertyMappingResponse()
            {
                Items = new List<ExtendedPropertyMapping>() {
                    new Models.ExtendedPropertyMapping() {
                        PropertyName ="SafePlace1",
                        PropertyTitle="Safe Place note",
                        PropertyType = "ORDER",
                        PropertyDescription = "Safe place note for delivery"
                    },
                    new Models.ExtendedPropertyMapping() {
                        PropertyName ="ExtendedCover",
                        PropertyTitle="Extended Cover flag",
                        PropertyType = "ITEM",
                        PropertyDescription = "Identifies whether the item requires Extended Cover. Set to 1 if required."
                    }
                }
            };

        }


        public GenerateLabelResponse GenerateLabel(Models.GenerateLabelRequest request)
        {
            try
            {
                List<string> mlogs = new List<string>();
                mlogs.Add("Label request get from linnworkson " + DateTime.UtcNow.ToString("dd/MM/yyyy hh:mm:ss"));
                AuthorizationConfig auth = AuthorizationConfig.Load(request.AuthorizationToken);
                AuthorizationConfig.Log(request.OrderReference, request.AuthorizationToken, Newtonsoft.Json.JsonConvert.SerializeObject(request), "GenerateLabel");
                if (auth == null)
                {
                    mlogs.Add("Authorization failed for token " + request.AuthorizationToken);
                    return new GenerateLabelResponse("Authorization failed for token " + request.AuthorizationToken);
                }
                mlogs.Add("Request for customer - " + auth.ContactName + "," + auth.CompanyName);
                // load all the services we have (either for this user specifically or all services)
                List<CourierService> services = ServicesDto.GetServices;

                //linnworks will send the serviceId as defined in list of services, we will need to find the service by id 
                CourierService selectedService = services.Find(s => s.ServiceUniqueId == request.ServiceId);
                if (selectedService == null)
                {
                    mlogs.Add("Service Id " + request.ServiceId.ToString() + " is not available");
                    throw new Exception("Service Id " + request.ServiceId.ToString() + " is not available");
                }

                // get the service code
                string serviceCode = selectedService.ServiceCode;
                //and some other information, whatever we need
                string VendorCode = selectedService.ServiceGroup;
                mlogs.Add("Service Id " + request.ServiceId.ToString() + " is not available");

                //create response class, we will be adding packages to it
                GenerateLabelResponse response = new GenerateLabelResponse();


                /* If you need to do any validation of services or consignment data, do it before you generate labels and simply throw an error 
                 * on the whole request
                 */

                foreach (var package in request.Packages)   // we need to generate a label for each package in the consignment
                {
                    mlogs.Add("Customer Order Referencee - " + request.OrderReference);
                    mlogs.Add("Customer Name - " + request.Name);
                    mlogs.Add("Customer Company name - " + request.CompanyName);
                    mlogs.Add("Customer Address line 1 - " + request.AddressLine1);
                    mlogs.Add("Customer Address line 2 - " + request.AddressLine2);
                    mlogs.Add("Customer Address line 3 - " + request.AddressLine3);
                    mlogs.Add("Customer City - " + request.Town);
                    mlogs.Add("Customer State - " + request.Region);
                    mlogs.Add("Customer Country - " + request.CountryCode);
                    mlogs.Add("Customer PostCode - " + request.Postalcode);
                    // an order may have extended property bound to it, here we can pass any specific parameter we need
                    // in this specific example we will be taking SafePlace extended property of the order and outputting it on the label
                    string strnam1 = "1234567890";
                    string strnum1 = "1234567890";
                    string streetname = "";
                    var num = Regex.Match(request.AddressLine1 + request.AddressLine2 + request.AddressLine3, @"(\d+)(?!.*\d)").Value;
                    string streetnumber = num;
                    if (serviceCode != "V01PAK")
                    {
                        streetnumber = request.AddressLine2;
                        if (request.AddressLine2 == "")
                        {
                            var mycut = Regex.Split(request.AddressLine1, " ");
                            streetnumber = mycut[0];
                        }
                    }
                    if (streetnumber.Length > 0)
                    {
                        strnum1 = streetnumber.ToString();
                    }
                    string[] streetnamelist = Regex.Split("@#@" + request.AddressLine1 + "@##@" + request.AddressLine2 + "@###@" + request.AddressLine3, num);
                    if (streetnamelist.Length > 0)
                    {
                        string streetnamefull = streetnamelist[0];
                        if (streetnamefull.Contains("@###@"))
                        {
                            string[] mystrnm = Regex.Split(streetnamefull, "@###@");
                            streetname = mystrnm[1];
                            if (streetname == "")
                            {
                                string[] mystrnmthird = Regex.Split(mystrnm[0], "@##@");
                                streetname = mystrnmthird[1];
                                if (streetname == "")
                                {
                                    string[] mystrnmsecond = Regex.Split(mystrnmthird[0], "@#@");
                                    streetname = mystrnmsecond[1];
                                }
                            }
                        }
                        else if (streetnamefull.Contains("@##@"))
                        {
                            string[] mystrnm = Regex.Split(streetnamefull, "@##@");
                            streetname = mystrnm[1];
                            if (streetname == "")
                            {
                                string[] mystrnmsecond = Regex.Split(mystrnm[0], "@#@");
                                streetname = mystrnmsecond[1];
                            }
                        }
                        else if (streetnamefull.Contains("@#@"))
                        {
                            string[] mystrnm = Regex.Split(streetnamefull, "@#@");
                            streetname = mystrnm[1];

                        }
                        if (serviceCode != "V01PAK")
                        {
                            streetname = request.AddressLine1;
                            if (request.AddressLine2 == "")
                            {
                                var mycutr = Regex.Split(request.AddressLine1, " ");
                                streetname = mycutr[1];
                            }
                        }
                        if (streetname.Length > 0)
                        {
                            strnam1 = streetname.ToString();
                        }
                    }
                    if (request.OrderExtendedProperties.Count > 0)
                    {

                        var strname = request.OrderExtendedProperties.Where(p => p.Name == "StreetName").FirstOrDefault();
                        var strnumber = request.OrderExtendedProperties.Where(p => p.Name == "StreetNumber").FirstOrDefault();
                        if (strname != null)
                        {
                            streetname = strname.Value;
                            if (streetname.Length > 0)
                            {
                                strnam1 = streetname.ToString();
                            }


                        }
                        if (strnumber != null)
                        {
                            streetnumber = strnumber.Value;
                            if (streetnumber.Length > 0)
                            {
                                strnum1 = streetnumber.ToString();
                            }
                        }

                    }
                    mlogs.Add("Customer Street Name - " + streetname);
                    mlogs.Add("Customer Street Number - " + streetnumber);

                    var basictoken = AuthorizationConfig.BuildBasicAuthenticationString(_config.GetSection("DHLConfig").GetSection("AppUsername").Value, _config.GetSection("DHLConfig").GetSection("AppPassword").Value);
                    string path = AppDomain.CurrentDomain.BaseDirectory + "Format";
                    var otheraccount = request.ServiceConfigItems.Where(p => p.ConfigItemId == "AccountNumber").FirstOrDefault();
                    if (otheraccount != null)
                    {
                        if (otheraccount.SelectedValue != "")
                        {
                            auth.AccountNumber = otheraccount.SelectedValue;
                        }
                    }
                    var defaultweight = request.ServiceConfigItems.Where(p => p.ConfigItemId == "DefaultWeight").FirstOrDefault();
                    if (defaultweight != null)
                    {
                        if (defaultweight.SelectedValue != "")
                        {
                            if (package.PackageWeight == 0)
                            {
                                package.PackageWeight = Convert.ToDecimal(Convert.ToDecimal(defaultweight.SelectedValue) / 1000);
                            }
                            else
                            {
                                package.PackageWeight = Convert.ToDecimal(package.PackageWeight / 1000);
                            }
                        }
                        else
                        {
                            package.PackageWeight = Convert.ToDecimal(package.PackageWeight / 1000);
                        }
                    }
                    else
                    {
                        package.PackageWeight = Convert.ToDecimal(package.PackageWeight / 1000);
                    }
                    mlogs.Add("Service Account Number - " + auth.AccountNumber);
                    string compna = "";
                    string packpath = "";
                    if (streetname.Trim().ToLower() == "packstation")
                    {
                        packpath = "P";
                        compna = Regex.Match(request.AddressLine1, @"(\d+)(?!.*\d)").Value.ToString();
                        mlogs.Add("Service Packstartion Post Number - " + compna);
                    }
                    // string path = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Files\Format");
                    string postData = AwsS3.GetS3File(path + "/" + packpath + serviceCode + ".txt")
                        .Replace("{{user}}", auth.Username)
                .Replace("{{pass}}", auth.Password)
                .Replace("{{product}}", serviceCode)
                .Replace("{{accountNumber}}", auth.AccountNumber)
                .Replace("{{shipmentDate}}", DateTime.UtcNow.ToString("yyyy-MM-dd"))
                .Replace("{{returnShipmentReference}}", request.OrderReference)
                .Replace("{{recipientEmailAddress}}", request.Email)
                .Replace("{{weight}}", package.PackageWeight.ToString())
                .Replace("{{sname1}}", auth.ContactName)
                .Replace("{{sstreetName}}", auth.AddressLine1)
                .Replace("{{sstreetNumber}}", auth.AddressLine2)
                .Replace("{{saddressAddition}}", auth.AddressLine3)
                .Replace("{{sdispatchingInformation}}", auth.CompanyName)
                .Replace("{{szip}}", auth.PostCode)
                .Replace("{{scity}}", auth.City)
                .Replace("{{scountry}}", auth.CountryCode)
                .Replace("{{scountryISOCode}}", auth.CountryCode)
                .Replace("{{sstate}}", auth.County)
                .Replace("{{rname1}}", (request.Name + "," + request.CompanyName + "," + request.AddressLine1.Replace(strnam1, "").Replace(strnum1, "") + "," + request.AddressLine2.Replace(strnam1, "").Replace(strnum1, "") + "," + request.AddressLine3.Replace(strnam1, "").Replace(strnum1, "")).Replace(",,,,,", "").Replace(",,,,", "").Replace(",,,", "").Replace(",,", ""))
                 .Replace("{{rpostnum}}", compna)
                .Replace("{{rstreetName}}", streetname)
                .Replace("{{rstreetNumber}}", streetnumber)
                .Replace("{{raddressAddition}}", request.AddressLine3)
                .Replace("{{rdispatchingInformation}}", request.CompanyName)
                .Replace("{{rzip}}", request.Postalcode)
                .Replace("{{rcity}}", request.Town)
                .Replace("{{rcountry}}", request.CountryCode)
                .Replace("{{rcountryISOCode}}", request.CountryCode)
                .Replace("{{rstate}}", request.Region);

                    var pdfresp = DHLgetLabel("getLabel", "POST", basictoken, postData);
                    if (pdfresp.IsError == false)
                    {
                        mlogs.Add("Label response labelid - " + pdfresp.Labelid);
                        mlogs.Add("Label response Tracking Number - " + pdfresp.TrackingNumber);
                        if (response.LeadTrackingNumber == "")
                        { response.LeadTrackingNumber = pdfresp.TrackingNumber; }
                        response.Package.Add(new PackageResponse()
                        {
                            LabelHeight = 6,
                            LabelWidth = 4,
                            PNGLabelDataBase64 = pdfresp.PNGBase64,
                            SequenceNumber = package.SequenceNumber,
                            PDFBytesDocumentationBase64 = new string[] { },
                            TrackingNumber = pdfresp.TrackingNumber
                        });
                    }
                    else
                    {
                        mlogs.Add("Label response Error - " + pdfresp.Error);
                        response.IsError = true;
                        response.ErrorMessage = pdfresp.Error.Replace("The place is to this postal code is not known. The shipment is not leitcodierbar.", "");
                    }
                    mlogs.Add("Label response send to linnworks on " + DateTime.UtcNow.ToString("dd/MM/yyyy hh:mm:ss"));

                    try
                    {
                        var hhid = Guid.NewGuid();

                        GeneratelabelLog generatelabelLog = new GeneratelabelLog()
                        {
                            Id = hhid,
                            Token = request.AuthorizationToken,
                            Orderid = request.OrderId.ToString(),
                            Orderreference = request.OrderReference,
                            //Logs = mlogs,
                            Created = DateTime.UtcNow,
                            Linnrequest = Newtonsoft.Json.JsonConvert.SerializeObject(request),
                            Linnresponse = Newtonsoft.Json.JsonConvert.SerializeObject(response),
                            DHLrequest = postData,
                            DHLresponse = pdfresp.Labelurlbyts,
                            Iserror = pdfresp.IsError,
                            Error = pdfresp.Error,
                            Labelid = pdfresp.Labelid
                        };
                        _generateLabelRepository.Insert(generatelabelLog);
                        _unitOfWork.Commit();

                        foreach (var log in mlogs)
                        {
                            var l = new LabelLogs();
                            l.Id = Guid.NewGuid();
                            l.GenerateLabelId = generatelabelLog.Id;
                            l.Log = log;
                            _labelLogsRepository.Insert(l);
                            _unitOfWork.Commit();
                        }


                        GenerateLabelCount generatelabelcount = new GenerateLabelCount()
                        {
                            Id = hhid,
                            Token = request.AuthorizationToken,
                            Orderid = request.OrderId.ToString(),
                            Created = DateTime.UtcNow,
                            Iserror = pdfresp.IsError,
                            Labelid = pdfresp.Labelid,
                            Error = pdfresp.Error
                        };

                        _generatelabelcountRepository.Insert(generatelabelcount);
                        _unitOfWork.Commit();
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }
                return response;
            }
            catch (Exception ex)
            {
                return new Models.GenerateLabelResponse("Unhandled error " + ex.Message);
            }
        }

        static byte[] imageToByteArray(System.Drawing.Image imageIn)
        {
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            return ms.ToArray();
        }

        public linnUser Auth(string token)
        {

            var client = new RestClient("https://api.linnworks.net/api/Auth/AuthorizeByApplication");
            LinnworkAuthFilterDto filter = new LinnworkAuthFilterDto()
            {
                ApplicationId = _config.GetSection("LinnworkAppCredential").GetSection("ApplicationId").Value,
                ApplicationSecret = _config.GetSection("LinnworkAppCredential").GetSection("ApplicationSecret").Value,
                Token = token
            };
            RestRequest request = new RestRequest(Method.POST);
            //request.AddHeader("Authorization", token);
            //request.AddHeader("Content-Type", "application/json");
            //var filterJson = @"{
            //                        ""ApplicationId"": ""c72b1b12-c98c-4eb0-a841-ad95921fe1fb"",
            //                        ""ApplicationSecret"": ""d6b4cd7d-6412-468e-a60a-d721181c610e"",
            //                        ""Token"": ""f1b7ffe4-2ff5-39ab-3b13-5cdb61e46ac5""
            //                    }";
            var filterJson = JsonConvert.SerializeObject(filter);
            request.AddParameter("application/json", filterJson, ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {

                var linnworkAuthDto = JsonConvert.DeserializeObject<linnUser>(response.Content);
                return linnworkAuthDto;
            }
            return null;
        }

        public string test()
        {
            ImageHelper.PDFtoImage(AwsS3.GetS3File("PDF/dummy.pdf"), AwsS3.GetS3File("PDF/dummy.png")); ;
            var base64String = AwsS3.GetS3File("PDF/dummy.png");
            return base64String;
        }

        public DHLResponse DHLgetLabel(string url, string method, string token, string body)
        {
            try
            {
                // Create a request using a URL that can receive a post. 
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://cig.dhl.de/services/" + _config.GetSection("DHLConfig").GetSection("DHLStage").Value + "/soap");
                // Set the Method property of the request to POST.
                request.Method = "POST";
                // Create POST data and convert it to a byte array.
                string postData = body;
                byte[] byteArray = Encoding.UTF8.GetBytes(postData.Replace("&", "&amp;"));
                // Set the ContentType property of the WebRequest.
                request.ContentType = "text/xml;charset=utf-8";
                request.Headers.Add("SOAPAction", "urn:" + url);
                request.Headers.Add("soap_version", "2");
                request.Headers.Add("Authorization", "Basic " + token);
                // Set the ContentLength property of the WebRequest.
                request.ContentLength = byteArray.Length;
                //You must change the path to point to your .cer file location. 
                //string path = AppDomain.CurrentDomain.BaseDirectory + "Format";
                //X509Certificate Cert = X509Certificate.CreateFromCertFile(path + "/" + "trusted_root_ca_sha256_g2.crt");

                //// You must change the URL to point to your Web server.
                //request.ClientCertificates.Add(Cert);
                // Get the request stream.
                Stream dataStream = request.GetRequestStream();
                // Write the data to the request stream.
                dataStream.Write(byteArray, 0, byteArray.Length);
                // Close the Stream object.
                dataStream.Close();
                // Get the response.
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                // Get the stream containing content returned by the server.
                dataStream = response.GetResponseStream();
                // Open the stream using a StreamReader for easy access.
                StreamReader reader = new StreamReader(dataStream);
                // Read the content.
                string responseFromServer = reader.ReadToEnd();
                // Display the content.
                if (responseFromServer.Contains("<statusCode>0</statusCode>"))
                {
                    // using the method 
                    string[] strlist = Regex.Split(responseFromServer, @"<labelUrl>");
                    string[] strlist2 = Regex.Split(strlist[1], @"</labelUrl>");
                    string base64String = strlist2[0];
                    string[] strlisttrack = Regex.Split(responseFromServer, @"<shipmentNumber>");
                    string[] strlist2track = Regex.Split(strlisttrack[1], @"</shipmentNumber>");
                    string trackingnum = strlist2track[0];
                    WebRequest requestpdf = WebRequest.Create(base64String);
                    WebResponse responsepdf = requestpdf.GetResponse();
                    string originalFileName = Guid.NewGuid().ToString();
                    Stream streamWithFileBody = responsepdf.GetResponseStream();

                    AwsS3.UploadFileToS3(streamWithFileBody, "Files/PDF/" + originalFileName + ".pdf");

                    //string pathpdf = AppDomain.CurrentDomain.BaseDirectory + AppSettings.ConfigStoragePath;
                    //using (Stream output = File.OpenWrite(pathpdf + "/label/" + originalFileName + ".pdf"))
                    //{
                    //    streamWithFileBody.CopyTo(output);
                    //}
                    // cs_pdf_to_image.Pdf2Image.PrintQuality = 200;
                    ImageHelper.PDFtoImage("Files/PDF/" + originalFileName + ".pdf", "Files/PDF/Images/" + originalFileName + ".png");
                    byte[] byteImage = AwsS3.GetByteS3File("Files/PDF/Images/" + originalFileName + ".png");
                    base64String = Convert.ToBase64String(byteImage);

                    //cspdftoimage.Pdf2Image.Convert(pathpdf + "/label/" + originalFileName + ".pdf", pathpdf + "/label/" + originalFileName + ".png", 200, 200, 200);
                    //PerformImageResizeAndPutOnCanvas(pathpdf + "/label/", originalFileName + ".png", 1096, 2088, "/" + originalFileName + "resize.png");
                    //base64String = Convert.ToBase64String(File.ReadAllBytes(pathpdf + "/label/" + originalFileName + "resize.png"));
                    // string base64Stringbyts = Convert.ToBase64String(File.ReadAllBytes(pathpdf + "/label/" + originalFileName + ".pdf"));

                    reader.Close();
                    dataStream.Close();
                    response.Close();
                    return new DHLResponse() { Labelurl = base64String, Labelid = originalFileName, Labelurlbyts = responseFromServer, TrackingNumber = trackingnum, IsError = false };
                }
                else
                {
                    string base64String = "";
                    string[] strlist = Regex.Split(responseFromServer, @"<statusMessage>");
                    for (var k = 1; k < strlist.Length; k++)
                    {
                        string[] strlist2 = Regex.Split(strlist[k], @"</statusMessage>");
                        base64String += strlist2[0] + ",";
                    }
                    if (base64String == "")
                    {
                        string[] strlist1 = Regex.Split(responseFromServer, @"<statusText>");
                        for (var k = 1; k < strlist1.Length; k++)
                        {
                            string[] strlist21 = Regex.Split(strlist1[k], @"</statusText>");
                            base64String += strlist21[0] + ",";
                        }

                    }
                    HttpWebRequest request2 = (HttpWebRequest)WebRequest.Create("https://translate.yandex.net/api/v1.5/tr/translate?key=" + _config.GetSection("DHLConfig").GetSection("LangKey").Value + "&text=" + base64String + "&lang=en");

                    HttpWebResponse response2 = (HttpWebResponse)request2.GetResponse();
                    // Get the stream containing content returned by the server.

                    Stream dataStream2 = response2.GetResponseStream();
                    // Open the stream using a StreamReader for easy access.
                    StreamReader reader2 = new StreamReader(dataStream2);
                    // Read the content.
                    string responseFromServer2 = reader2.ReadToEnd();
                    string[] streetnamelist = Regex.Split(responseFromServer2, "<text>");
                    string[] streetnamelist2 = Regex.Split(streetnamelist[1], "</text>");
                    base64String = streetnamelist2[0];
                    return new DHLResponse() { Labelurl = null, Labelid = "", Labelurlbyts = responseFromServer, TrackingNumber = null, IsError = true, Error = base64String };
                }
            }
            catch (WebException e)
            {
                string text = "";
                using (WebResponse response = e.Response)
                {
                    HttpWebResponse httpResponse = (HttpWebResponse)response;
                    using (Stream data = response.GetResponseStream())
                    {
                        text = new StreamReader(data).ReadToEnd();

                    }
                }
                return new DHLResponse() { Labelurl = null, TrackingNumber = null, IsError = true, Error = text };
            }
        }

        public DHLResponse DHLgetManifest(string url, string method, string token, string body)
        {
            try
            {
                // Create a request using a URL that can receive a post. 
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://cig.dhl.de/services/" + _config.GetSection("DHLConfig").GetSection("DHLStage").Value + "/soap");
                // Set the Method property of the request to POST.
                request.Method = "POST";
                // Create POST data and convert it to a byte array.
                string postData = body;
                byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                // Set the ContentType property of the WebRequest.
                request.ContentType = "text/xml;charset=utf-8";
                request.Headers.Add("SOAPAction", "urn:" + url);
                request.Headers.Add("soap_version", "2");
                request.Headers.Add("Authorization", "Basic " + token);
                // Set the ContentLength property of the WebRequest.
                request.ContentLength = byteArray.Length;
                //You must change the path to point to your .cer file location. 
                //string path = AppDomain.CurrentDomain.BaseDirectory + "Format";
                //X509Certificate Cert = X509Certificate.CreateFromCertFile(path + "/" + "trusted_root_ca_sha256_g2.crt");

                //// You must change the URL to point to your Web server.
                //request.ClientCertificates.Add(Cert);
                // Get the request stream.
                Stream dataStream = request.GetRequestStream();
                // Write the data to the request stream.
                dataStream.Write(byteArray, 0, byteArray.Length);
                // Close the Stream object.
                dataStream.Close();
                // Get the response.
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                // Get the stream containing content returned by the server.
                dataStream = response.GetResponseStream();
                // Open the stream using a StreamReader for easy access.
                StreamReader reader = new StreamReader(dataStream);
                // Read the content.
                string responseFromServer = reader.ReadToEnd();
                // Display the content.
                if (responseFromServer.Contains("<statusCode>0</statusCode>"))
                {
                    // using the method 

                    reader.Close();
                    dataStream.Close();
                    response.Close();
                    return new DHLResponse() { Labelurl = responseFromServer, TrackingNumber = "", IsError = false };
                }
                else
                {
                    string base64String = "";
                    string[] strlist = Regex.Split(responseFromServer, @"<statusMessage>");
                    for (var k = 1; k < strlist.Length; k++)
                    {
                        string[] strlist2 = Regex.Split(strlist[k], @"</statusMessage>");
                        base64String += strlist2[0] + ",";
                    }
                    if (base64String == "")
                    {
                        string[] strlist1 = Regex.Split(responseFromServer, @"<statusText>");
                        for (var k = 1; k < strlist1.Length; k++)
                        {
                            string[] strlist21 = Regex.Split(strlist1[k], @"</statusText>");
                            base64String += strlist21[0] + ",";
                        }

                    }

                    HttpWebRequest request2 = (HttpWebRequest)WebRequest.Create("https://translate.yandex.net/api/v1.5/tr.json/translate?key=" + _config.GetSection("DHLConfig").GetSection("LangKey").Value + "&text=" + base64String + "&lang=en");
                    Stream dataStream2 = request2.GetRequestStream();
                    HttpWebResponse response2 = (HttpWebResponse)request2.GetResponse();
                    // Get the stream containing content returned by the server.

                    dataStream2 = response2.GetResponseStream();
                    // Open the stream using a StreamReader for easy access.
                    StreamReader reader2 = new StreamReader(dataStream2);
                    // Read the content.
                    string responseFromServer2 = reader2.ReadToEnd();
                    string[] streetnamelist = Regex.Split(responseFromServer2, "[");
                    string[] streetnamelist2 = Regex.Split(streetnamelist[1], "]");
                    base64String = streetnamelist2[0];
                    return new DHLResponse() { Labelurl = null, TrackingNumber = null, IsError = true, Error = base64String };
                }
            }
            catch (WebException e)
            {
                string text = "";
                using (WebResponse response = e.Response)
                {
                    HttpWebResponse httpResponse = (HttpWebResponse)response;
                    using (Stream data = response.GetResponseStream())
                    {
                        text = new StreamReader(data).ReadToEnd();

                    }
                }
                return new DHLResponse() { Labelurl = null, TrackingNumber = null, IsError = true, Error = text };
            }

        }
        public UserAvailableServicesResponse UserAvailableServices(string AuthorizationToken)
        {

            UserAvailableServicesResponse resp = new UserAvailableServicesResponse();
            AuthorizationConfig auth = AuthorizationConfig.Load(AuthorizationToken);
            if (auth == null)
            {
                resp = new UserAvailableServicesResponse("Authorization failed for token " + AuthorizationToken);
            }
            return new UserAvailableServicesResponse()
            {
                Services = ServicesDto.GetServices
            };
        }

        public TokenModel Install(string token)
        {
            var data = Auth(token);

            if (data != null)
            {
                var existingData = _linnuserRepository.GetById(data.Id);
                if (existingData == null)
                {
                    //insert data into database
                    _linnuserRepository.Insert(data);
                    _unitOfWork.Commit();
                }
                else
                {
                    //delete data into database
                    _linnuserRepository.Delete(data.Id);
                    _unitOfWork.Commit();

                    //insert data into database
                    _linnuserRepository.Insert(data);
                    _unitOfWork.Commit();
                }

            }

            return new TokenModel()
            {
                email = data.Email,
                server = data.Server
            };
        }

        public Result InsertData(Models.DTOs.GenerateLabelFilterDto dto)
        {
            var result = new Result();
            var filter = dto ?? new Models.DTOs.GenerateLabelFilterDto();
            List<GenerateLabelCount> allList = new List<GenerateLabelCount>();

            var query = _generateLabelRepository.Get();
            query = new GenerateLabelCountFilter(query, filter).FilteredQuery();

            foreach (var d in query)
            {
                var count = new GenerateLabelCount()
                {
                    Id = d.Id,
                    Token = d.Token,
                    Labelid = d.Labelid,
                    Orderid = d.Orderid,
                    Created = d.Created,
                    Iserror = d.Iserror,
                    Error = d.Error
                };

                _generatelabelcountRepository.Insert(count);
                _unitOfWork.Commit();
            }
            return result.SetSuccess("Log inserted");
        }
        public CancelLabelResponse CancelLabel(CancelLabelRequest requestdto)
        {
            AuthorizationConfig auth = AuthorizationConfig.Load(requestdto.AuthorizationToken);
            //var trackingnum = System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + AppSettings.ConfigStoragePath + "\\" + AuthorizationToken + "\\" + OrderReference + "-" + "tracking" + ".json");
            var trackingnum = AwsS3.GetS3File(requestdto.AuthorizationToken + "\\" + requestdto.OrderReference + "-" + "tracking" + ".json");
            if (auth == null)
            {
                return new CancelLabelResponse("Authorization failed for token " + requestdto.AuthorizationToken);
            }
            var basictoken = AuthorizationConfig.BuildBasicAuthenticationString(_config.GetSection("DHLConfig").GetSection("AppUsername").Value, _config.GetSection("DHLConfig").GetSection("AppPassword").Value);
            string postData = AwsS3.GetS3File("Format/Caclelabel.txt").Replace("{{user}}", auth.Username)
                    .Replace("{{pass}}", auth.Password).Replace("{{shippmentnumber}}", trackingnum);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://cig.dhl.de/services/" + _config.GetSection("DHLConfig").GetSection("DHLStage").Value + "/soap");
            // Set the Method property of the request to POST.
            request.Method = "POST";

            byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            // Set the ContentType property of the WebRequest.
            request.ContentType = "text/xml;charset=utf-8";
            request.Headers.Add("SOAPAction", "urn:" + "deleteShipmentOrder");
            request.Headers.Add("soap_version", "2");
            request.Headers.Add("Authorization", "Basic " + basictoken);
            // Set the ContentLength property of the WebRequest.
            request.ContentLength = byteArray.Length;
            //You must change the path to point to your .cer file location. 
            //string path = AppDomain.CurrentDomain.BaseDirectory + "Format";
            //X509Certificate Cert = X509Certificate.CreateFromCertFile(path + "/" + "trusted_root_ca_sha256_g2.crt");
            // System.IO.File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + AppSettings.ConfigStoragePath + "/" + "cancelreq" + ".txt", postData);

            AwsS3.UploadFileToS3(AuthorizationConfig.GenerateStreamFromString(postData), "Format/cancelreq.txt");
            //// You must change the URL to point to your Web server.
            //request.ClientCertificates.Add(Cert);
            // Get the request stream.
            Stream dataStream = request.GetRequestStream();
            // Write the data to the request stream.
            dataStream.Write(byteArray, 0, byteArray.Length);
            // Close the Stream object.
            dataStream.Close();
            // Get the response.
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            // Get the stream containing content returned by the server.
            dataStream = response.GetResponseStream();
            // Open the stream using a StreamReader for easy access.
            StreamReader reader = new StreamReader(dataStream);
            // Read the content.
            string responseFromServer = reader.ReadToEnd();
            // Display the content.
            //System.IO.File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + AppSettings.ConfigStoragePath + "/" + "cancelresp" + ".txt", postData);
            AwsS3.UploadFileToS3(AuthorizationConfig.GenerateStreamFromString(postData), "Format/cancelreq.txt");
            if (responseFromServer.Contains("<statusCode>0</statusCode>"))
            {

            }
            else
            {

            }

            // implement label cancelation routine here 
            // remember that request will 

            return new CancelLabelResponse();
        }

        public CreateManifestResponse CreateManifest(CreateManifestRequest requestdto)
        {
            AuthorizationConfig auth = AuthorizationConfig.Load(requestdto.AuthorizationToken);
            //var trackingnum = System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + AppSettings.ConfigStoragePath + "\\" + AuthorizationToken + "\\" + OrderReference + "-" + "tracking" + ".json");
            var trackingnum = AwsS3.GetS3File(requestdto.OrderReference + "-" + "tracking" + ".json");
            if (auth == null)
            {
                return new CreateManifestResponse("Authorization failed for token " + requestdto.AuthorizationToken);
            }
            var basictoken = AuthorizationConfig.BuildBasicAuthenticationString(auth.Username, auth.Password);
            string path = AppDomain.CurrentDomain.BaseDirectory + "Format";
            //here we can implement manifest submission, upload etc if needed
            //the request will contain all orderIds the customer is manifesting 
            // in this specific example we simply output a dummy manifest reference
            return new CreateManifestResponse()
            {
                ManifestReference = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 10).ToUpper()
            };
        }

        public PrintManifestResponse PrintManifest(PrintManifestRequest request)
        {
            AuthorizationConfig auth = AuthorizationConfig.Load(request.AuthorizationToken);
            if (auth == null)
            {
                return new Models.PrintManifestResponse("Authorization failed for token " + request.AuthorizationToken);
            }
            // if there is a specific End-Of-Day/Manifest documentation you can generate the PDF here
            // if you integration doesn't support manifest document or it is not needed simply throw an error via the response of course and state: This integration doesn't support End Of Day manifest documentation. 

            return new Models.PrintManifestResponse()
            {
                PDFbase64 = Convert.ToBase64String(null)
            };
        }

        public static void SaveJpeg(string path, System.Drawing.Image img)
        {
            var qualityParam = new System.Drawing.Imaging.EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 100L);
            var jpegCodec = GetEncoderInfo("image/jpeg");

            var encoderParams = new EncoderParameters(1);
            encoderParams.Param[0] = qualityParam;
            img.Save(path, jpegCodec, encoderParams);
        }
        public static void Save(string path, System.Drawing.Image img, ImageCodecInfo imageCodecInfo)
        {
            var qualityParam = new System.Drawing.Imaging.EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 100L);

            var encoderParams = new EncoderParameters(1);
            encoderParams.Param[0] = qualityParam;
            img.Save(path, imageCodecInfo, encoderParams);
        }
        public static ImageCodecInfo GetEncoderInfo(string mimeType)
        {
            return ImageCodecInfo.GetImageEncoders().FirstOrDefault(t => t.MimeType == mimeType);
        }
        public static System.Drawing.Image PutOnCanvas(System.Drawing.Image image, int width, int height, Color canvasColor)
        {
            var res = new Bitmap(width, height);
            using (var g = Graphics.FromImage(res))
            {
                g.Clear(canvasColor);
                var x = (width - image.Width) / 2;
                var y = (height - image.Height) / 2;
                g.DrawImageUnscaled(image, x, y, image.Width, image.Height);
            }

            return res;
        }
        public static System.Drawing.Image PutOnWhiteCanvas(System.Drawing.Image image, int width, int height)
        {
            return PutOnCanvas(image, width, height, Color.White);
        }
        public static System.Drawing.Image Resize(System.Drawing.Image image, int newWidth, int maxHeight, bool onlyResizeIfWider)
        {
            if (onlyResizeIfWider && image.Width <= newWidth) newWidth = image.Width;

            var newHeight = image.Height * newWidth / image.Width;
            if (newHeight > maxHeight)
            {
                // Resize with height instead  
                newWidth = image.Width * maxHeight / image.Height;
                newHeight = maxHeight;
            }

            var res = new Bitmap(newWidth, newHeight);

            using (var graphic = Graphics.FromImage(res))
            {
                graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphic.SmoothingMode = SmoothingMode.HighQuality;
                graphic.PixelOffsetMode = PixelOffsetMode.HighQuality;
                graphic.CompositingQuality = CompositingQuality.HighQuality;
                graphic.DrawImage(image, 0, 0, newWidth, newHeight);
            }

            return res;
        }

        public static System.Drawing.Image Crop(System.Drawing.Image img, Rectangle cropArea)
        {
            var bmpImage = new Bitmap(img);
            var bmpCrop = bmpImage.Clone(cropArea, bmpImage.PixelFormat);
            return bmpCrop;
        }

        public static System.Drawing.Image byteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            System.Drawing.Image returnImage = System.Drawing.Image.FromStream(ms);
            return returnImage;
        }

        //The actual converting function  
        public static string GetImage(object img)
        {
            return "data:image/jpg;base64," + Convert.ToBase64String((byte[])img);
        }

        public static void PerformImageResizeAndPutOnCanvas(string pFilePath, string pFileName, int pWidth, int pHeight, string pOutputFileName)
        {
            System.Drawing.Image imgBef;
            imgBef = System.Drawing.Image.FromFile(pFilePath + pFileName);

            System.Drawing.Image _imgR;
            _imgR = Crop(imgBef, new Rectangle(0, 0, pWidth, pHeight));

            //Save JPEG  
            SaveJpeg(pFilePath + pOutputFileName, _imgR);
        }

        private static string BuildBasicAuthenticationString(string username, string password)
        {
            var byteArray = Encoding.ASCII.GetBytes(string.Format("{0}:{1}", username, password));
            return Convert.ToBase64String(byteArray);
        }
    }
}
