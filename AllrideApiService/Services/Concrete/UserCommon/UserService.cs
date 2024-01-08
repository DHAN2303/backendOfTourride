using AllrideApiCore.Dtos;
using AllrideApiCore.Entities.Users;
using AllrideApiRepository;
using AllrideApiRepository.Repositories.Abstract;
using AllrideApiService.Configuration;
using AllrideApiService.Configuration.Extensions;
using AllrideApiService.Configuration.Validator;
using AllrideApiService.Response;
using AllrideApiService.Services.Abstract.Users;
using AutoMapper;
using DTO.Insert;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using System.Globalization;

namespace AllrideApiService.Services.Concrete.UserCommon
{
    public class UserService : IUserService
    {
        private const string V = "User Register Log Error: ";
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<LoginService> _logger;
        protected readonly AllrideApiDbContext _context;

        public UserService(IUserRepository userRepository, IMapper mapper, ILogger<LoginService> logger, AllrideApiDbContext context)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _logger = logger;
            _context = context;
        }

        public CustomResponse<NoContentDto> Add(CreateUserDto userDto)
        {
            List<ErrorEnumResponse> registerEnumRespList = new List<ErrorEnumResponse>();
            //BaseResponse commandResponse = new();

            // KULLANICIDAN GELEN DOĞUM TARİHİNİ COVERT ETME
            try
            {
                DateTime userDate = ConvertDateTime(userDto.DateOfBirth);
                Debug.WriteLine(" Convert to DateOfBirth: " + userDate.ToString());
                var validator = new CreateUserValidation();
                var response = validator.Validate(userDto).ThrowIfException();
                if (userDto.Gender != "0" && userDto.Gender != "1" && userDto.Gender != "2")
                {
                    bool isConvert = int.TryParse("80", out int errorCode);
                    if (isConvert)
                    {
                        if (response.Status != true)
                        {
                            response.ErrorEnums.Add(ErrorEnumResponse.GenderIsFailed);
                        }
                        else
                        {
                            registerEnumRespList.Add(ErrorEnumResponse.GenderIsFailed);
                            return CustomResponse<NoContentDto>.Fail(registerEnumRespList, false);
                        }

                    }
                }
                if (response.Status == false)
                {
                    return response;
                }
                UserEntity user = _mapper.Map<UserEntity>(userDto);
                bool isExistUserPhone = _userRepository.IsExistUserPhone(userDto.Phone);
                bool isExistUserEmail = _userRepository.IsExistUserEmail(user);
                if (isExistUserEmail || isExistUserPhone)
                {
                    if (isExistUserPhone)
                    {
                        registerEnumRespList.Add(ErrorEnumResponse.PhoneWasUsed);
                    }

                    registerEnumRespList.Add(ErrorEnumResponse.EmailWasUsed);
                    return CustomResponse<NoContentDto>.Fail(registerEnumRespList, false);
                }

                UserDetail userDetail = new();
                userDetail.DateOfBirth = userDate;
                userDetail = _mapper.Map<UserDetail>(userDto);
                user.UserDetail = userDetail;
                user.ActiveUser = true;  // Active User alanı kullanıcının hesabı silinmişse 0 aktifse 1 anlamına gelir
                var result = _userRepository.Add(user);
                _userRepository.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError(V + ex.Message + "   Stack Trace: " + ex.StackTrace);

            }

            // Kullanıcı kayıt edildikten sonra bu alan çalışacak
            if (userDto.Phone.IsNullOrEmpty() == false)
            {
                SmsApi(_logger);//userDto.Phone
            }
            return CustomResponse<NoContentDto>.Success(true);


        }

        public CustomResponse<NoContentDto> UpdateUserVehicleType(string VehicleType, int UserId)
        {
            // NOT BURADA USER DETAİL I DA BAŞTAN DÖNMEM GEREKEBİLİR
            try
            {
                List<ErrorEnumResponse> registerEnumRespList = new();
                if (VehicleType.IsNullOrEmpty())
                {
                    registerEnumRespList.Add(ErrorEnumResponse.UserVehicleTypeIsNullOrEmpty);
                    return CustomResponse<NoContentDto>.Fail(registerEnumRespList, false);

                }
                var validation = ValidationVehicleType.VehicleTypeValidation(VehicleType);
                if (validation.Status == false)
                {
                    return validation;
                }

                var result = _userRepository.Update(VehicleType, UserId);
                if (result == false)
                {
                    registerEnumRespList.Add(ErrorEnumResponse.NoUserIdInUserDetailTable);
                    return CustomResponse<NoContentDto>.Fail(registerEnumRespList, false);
                }

                _userRepository.SaveChanges();

            }
            catch (Exception ex)
            {
                _logger.LogError(" USER VEHİCLE TYPE REGİSTER SERVICE ERROR  " + ex.Message);
            }

            return CustomResponse<NoContentDto>.Success(VehicleType, true);

        }

        public static DateTime ConvertDateTime(string date)
        {
            DateTime userDate;
            CultureInfo provider = CultureInfo.InvariantCulture;
            bool isSuccess = DateTime.TryParseExact(date, new string[] { "MM/dd/yyyy", "MM-dd-yyyy", "MM.dd.yyyy" }, provider, DateTimeStyles.None, out userDate);
            if (isSuccess)
            {
                userDate = DateTime.ParseExact(date, "dd.MM.yyyy", provider);
            }
             
            return userDate;
        }
       

        public static void SmsApi(ILogger<LoginService> _logger) // telefon numarası olacak parametrede string Phone
        {
            try
            {
                SMSApi.Api.IClient client = new SMSApi.Api.ClientOAuth("m4hDI8aOxEo0u7WB34yMuIRK4kIlgNS3lMRDdlwD");

                var smsApi = new SMSApi.Api.SMSFactory(client);
                Random rnd = new();
                int activationCode = rnd.Next(0, 9999);
                _logger.LogInformation(""+activationCode);
                //var result =
                //    smsApi.ActionSend()
                //        .SetText("TGLabs selam nabüünüz ayanlar, korkmayın bu bir dolandırıcı mesajı değil." +
                //        "Bu bir SMS test mesajıdır. Başlık da TEST değil TOURIDE yazısı görmek istiyorsanız bu servis için para ödemek zorundasınız. Hadi kolay gelsin cigerlerim... Doğrulama Kodu: " + activationCode) 
                //        .SetTo(new[] { "+905412773857", "+905549525596","+905448871314","+905546520553", "+905330429833","+905375620229" }) // //Phone
                //        .SetSender("Test") //Sender name
                //        .Execute();   
                var result =
                     smsApi.ActionSend()
                         .SetText("Merhaba Abdullah Bey Sms Servisi Tamamdır. Büşra Betül DİNLER. Kolay gelsin :)").SetTo(new[] { "+905369313398" }) // //Phone  , "+905549525596","+905448871314","+905546520553", "+905330429833","+905375620229" 
                         .SetSender("Test") //Sender name
                         .Execute();

                _logger.LogInformation("Send: " + result.Count);

                string[] ids = new string[result.Count];

                for (int i = 0, l = 0; i < result.List.Count; i++)
                {
                    if (!result.List[i].isError())
                    {
                        if (!result.List[i].isFinal())
                        {
                            ids[l] = result.List[i].ID;
                            l++;
                        }
                    }
                }

                result =
                    smsApi.ActionGet()
                        .Ids(ids)
                        .Execute();

                foreach (var status in result.List)
                {
                    _logger.LogInformation("ID: " + status.ID + " Number: " + status.Number + " Points:" + status.Points + " Status:" + status.Status + " IDx: " + status.IDx);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message + " " + e.InnerException);
                /**
                 * Error codes (list available in smsapi docs). Example:
                 * 101 	Invalid authorization info
                 * 102 	Invalid username or password
                 * 103 	Insufficient credits on Your account
                 * 104 	No such template
                 * 105 	Wrong IP address (for IP filter turned on)
                 * 110	Action not allowed for your account
                 */

            }
            

        }

        public CustomResponse<Object> GetOnlineUsers(int type, int id, int userId) //0:group || 2:club || 1:person
        {
            List<ErrorEnumResponse> registerEnumRespList = new();
            try
            {
                if(type == 0)
                {
                    var data = from ou in _context.online_users
                               join gu in _context.group_member
                               on ou.userId equals gu.user_id
                               where gu.group_id == id
                               select ou;
                    return GetOnlineUsersReturn(data);
                }
                else if (type == 2)
                {
                    var data = from ou in _context.online_users
                               join cu in _context.club_member
                               on ou.userId equals cu.user_id
                               where cu.club_id == id
                               select ou;
                    return GetOnlineUsersReturn(data);
                }
                else
                {
                    var data = from ou in _context.online_users
                               join pu in _context.social_media_follows
                               on ou.userId equals pu.follower_id
                               where pu.followed_id == userId
                               where ou.userId == pu.follower_id
                               select ou;
                    return GetOnlineUsersReturn(data);
                }

            }
            catch (Exception e)
            {
                _logger.LogError("There was an error pulling online users: " + e.Message + " " + e.InnerException);
                registerEnumRespList.Add(ErrorEnumResponse.BadRequest);
                return CustomResponse<Object>.Fail(registerEnumRespList, false);
            }

        }

        private CustomResponse<Object> GetOnlineUsersReturn(object data)
        {
            List<ErrorEnumResponse> registerEnumRespList = new();
            if (data != null)
            {
                return CustomResponse<Object>.Success(data, true);
            }
            else
            {
                registerEnumRespList.Add(ErrorEnumResponse.NullData);
                return CustomResponse<Object>.Fail(registerEnumRespList, false);
            }
        }


        //public CustomResponse<Object> GetUsersForInvite()
        //{
        //    var response = 
        //}

        //public static CustomResponse<NoContentDto> VehicleTypeValidation(string vehicleType)
        //{
        //    List<string> vehiclerList = new List<string> { "car", "truck"};
        //    List<ErrorEnumResponse> errorEnumResponses = new();
        //    if (!vehiclerList.Contains(vehicleType))
        //    {
        //        errorEnumResponses.Add(ErrorEnumResponse.UnsupportedVehicleType);
        //        return CustomResponse<NoContentDto>.Fail(errorEnumResponses, false);
        //    }
        //    return CustomResponse<NoContentDto>.Success(vehicleType, true);
        //}
    }
}
// Merhaba Touride'a hoş geldiniz.Lütfen şifreyi kimseyle paylaşmayın." +"Hesabınızı aktif etmek için lütfen kodu giriniz.Kod:
