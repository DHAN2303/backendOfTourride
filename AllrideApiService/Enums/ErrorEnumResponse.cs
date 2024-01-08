namespace AllrideApiService.Response
{
    public enum ErrorEnumResponse // AuthenticationErrorsEnum
    {
        NameNull = 10, // Register
        NameMinCharacter = 11,  // Register
        NameMaxCharacter = 12,  // Register
        NameEmpty = 13,

        LastNameNull = 20,   // Register
        LastNameMinCharacter = 21,  // Register
        LastNameMaxCharacter = 22,  // Register
        LastNameEmpty = 23,

        EmailNull= 30,     // Login - Register
        InvalidEmailAdressFormat = 31,
        EmailWasUsed = 32,         // *** EmailWasUsed (Register)
        EmailMaxLength = 33,       // 
        EmailEmpty = 34,
        EmailFormatException = 35, // Login - Register
        EmailIsNotRegistered = 36,

        BirthDateNull = 40,   // Register
        BirthDateEmpty = 41,   // Register
        BirthDateFormatException = 42, // Register

        PhoneNull = 50,    // Register
        PhoneFormatException = 51, // Register
        PhoneWasUsed = 52,        // Register 
        PhoneMaxLength = 53,
        PhoneEmpty = 54,    // Register

        PasswordNull = 60,   // Login - Register
        PasswordMinCharacter = 61,  // Login - Register
        PasswordFormatException = 62, // Login - Register
        PasswordMaxCharacter = 63,  // Login - Register
        PasswordMustContainOneOrMoreSpecialCharacters = 66,
        PasswordEmpty = 68, // Login - Register
        PasswordInvalid = 69, // Login

        PasswordConfirmNull= 70,     //
        PasswordConfirmMinCharacter = 71,
        PasswordConfirmFormatException = 72,
        PasswordConfirmMaxCharacter = 73,
        PasswordConfirmMismatch = 74,
        PasswordConfirmEmpty = 75,

        // Gender
        GenderIsFailed = 80,

        // User
        UserLoginFailed = 100,
        NoRegisteredUsersInDb = 101,
        EmailNotRegistered = 102, // Kullanıcı kayıtlı değil -- Login
        UserVehicleTypeIsNullOrEmpty = 103,
        NoUserIdInUserDetailTable = 104,
        UnsupportedVehicleType = 105,

        // password
        PasswordTrue = 201,

        // NEWS
        NewsIdNullOrEmpty = 400,
        NewsTitleNullOrEmpty = 401,
        NewsIsNotRegister = 402,
        NewsActionTypeIsFail = 403,
        NewsIdNotString = 404,
        ActionTypeIsNull = 405,
        ActionTypeIsEmpty= 406,
        UserNewsReactionNotRegister = 407,
        CouldntUpdateActionType =408,

        RegisterSuccessful = 500,

        UserIsFound = 501,

        // FetchUsersRoute
        FetchRouteDetailNotEmpty = 600,
        FetchRouteDetailNotNull = 601,
        FetchRouteDetailNotInteger = 602,
        RouteDetailDoesntRegisterDb = 603,
        BackendDidntAutoMapper = 604,

        // Route
        RecommendedTypeIsNotInt = 700,
        NoRecommendedRouteAdded = 701,
        NotGetRoutesDetailInDb = 702,
        RecommendedTypeLessThanZero = 703,
        NoRouteSavedInDb = 704,
        // Social Media

        UserHasNoFollowers = 800,

        // Group
        NotGroupCreated=900,
        GroupIdIsNotInt = 901,
        ThereIsNoSuchGroupInDb = 902,
        SearchedUserNotFoundinGroup = 903,
        UserIdCannotBeNullOrEmptyInGroup = 904,
        GroupDntRegisterInDB = 955,

        // Club
        NotClubCreated = 950,
        CLubIdIsNotInt = 951,
        ThereIsNoSuchClubInDb = 952,
        SearchedUserNotFoundinClub = 953,
        UserIdCannotBeNullOrEmptyInClub = 954,
        ClubDntRegisterInDB = 955,

        //
        LoginError = 1000, // Unknow Error. Email(geçerli bir formatta) fakat sistemde kayıtlı değil veya şifre hatalı

        FileUploadError = 600,

        HereApiRequestFail = 1000,
        HereApiDidNotReturnAValidRoute = 1001,
        CouldNotCreateRouteUrl = 1002,
        NoResponseReturnedFromHereApi = 1003,

        WeatherServiceResponseError = 2000,
        WeatherServiceJsonNotDeserialize = 2001,
        NoWeatherDataSavedInDb = 2002,
        WeatherRequestedDateHasPassed = 2003,

        UnknowError = 3000,

        NullData = 4000,

        UserIdNotFound = 5000,

        MappingFailed = 6000,

        TokenIsInValid = 7000,

        FailedToCreateToken = 7001,

        // Weather 

        NoPermissionToAccessWeatherService = 8000,
        BadRequest=4004,
        LimitExpired=4005,// "Your usage limit for the Here Route service has expired.";
        ApiServiceFail = 4006,

        // Touride Package 
        NoTouridePackagePricingInDB = 2050,




    }
}
