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

        EmailNull = 30,     // Login - Register
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
        PasswordMaxCharacter = 62,  // Login - Register
        PasswordMustContain1OrMoreCapitalLetters = 63,
        PasswordMustContain1OrMoreCapitalLowerCase = 64,
        PasswordMustContain1OrMoreContainsSpecialChracter = 65,
        PasswordMustContainOneOrMoreSpecialCharacters = 66,
        PasswordMustContainOneOrMoreSpecialMustContainSpaces = 67,
        PasswordEmpty = 68, // Login - Register
        PasswordInvalid = 69, // Login

        PasswordConfirmNull = 70,     //
        PasswordConfirmMinCharacter = 71,
        PasswordConfirmFormatException = 72,
        PasswordConfirmMaxCharacter = 73,
        PasswordConfirmMismatch = 74,
        PasswordConfirmEmpty = 75,
        // ThereIsAPasswordConfirmMismatch = 74,

        // Gender
        GenderIsFailed = 80,

        // User
        UserLoginFailed = 100,
        NoRegisteredUsersInDb = 101,
        EmailNotRegistered = 102, // Kullanıcı kayıtlı değil -- Login
        UserVehicleTypeIsNullOrEmpty = 103,
        NoUserIdInUserDetailTable = 104,
        UnsupportedVehicleType = 105,
        FailedToSendMail = 106,
        ActivationCodeCouldNotBeGenerated = 107,
        FailedToActivationCode = 108,
        UserIdCannotBeEqualTo0AndLessThanZero = 109,
        UserFollowersIsNull = 110,

        // password
        PasswordTrue = 201,

        // Search Service
        ThereIsNoData = 250,


        // Chat
        UserHasNoFriendsMessage = 300,
        GroupMessageListNull = 301,
        ClubMessageListNull = 302,

        // NEWS
        NewsIdNullOrEmpty = 400,
        NewsTitleNullOrEmpty = 401,
        NewsIsNotRegister = 402,
        NewsActionTypeIsFail = 403,
        NewsIdNotString = 404,
        ActionTypeIsNull = 405,
        ActionTypeIsEmpty = 406,
        UserNewsReactionNotRegister = 407,
        CouldntUpdateActionType = 408,
        RegisteredNoNews = 409,

        RegisterSuccessful = 500,

        UserIsFound = 501,

        // Acitivity
        ActivityDontRegisterDB = 550,

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
        NoTransportModeRegisteredInDatabase = 705,

        // Route Planner
        TaskIsNotDeletedInTable = 735,
        TaskIsNotAttachedToTheRoutePlan = 736,
        TaskIdIsNull = 737,
        TaskIdIsEmpty = 738,
        TaskIdCannotBeEqualTo0AndLessThanZero = 739,
        UserDoesntHaveRoutePlanner = 740,
        UserNotRegisteredInRoutePlan = 741,
        RoutePlannerIdIsEmpty = 742,
        RoutePlannerIdIsNull = 743,
        FriendsIdNull = 744,
        FriendsIdEmpty = 745,
        ThisGroupUserCannotCreateRoutePlan = 746,
        AddFriendsTasksRoutePlannerDtoIsNull = 747,
        UserIdIsNotInteger = 748,
        UsersInListNotAddedToRoutePlanning = 749,

        RoutePlannerTitleIsNull = 750,
        RoutePlannerTitleIsEmpty = 751,
        RoutePlannerTitleMaxLength = 752,
        RoutePlannerTitleMinLength = 753,

        RouteNameIsNull = 754,
        RouteNameIsEmpty = 755,
        RouteNameMaxLength = 756,
        RouteNameMinLength = 757,
        RoutePlannerNotRegisterDB = 758,
        UsersIsNotFoundInFollowers = 759,
        AddedUsersNoDb = 760,
        UserFailedToAddToRoutePlanning = 761,
        ColorCodeIsNull = 762,
        ColorCodeIsNotEmpty = 763,
        ColorCodeIsNotHexadecimal = 764,
        StartDateIsNotNull = 765,
        StartDateIsNotEmpty = 766,
        EndDateIsNotNull = 767,
        EndDateIsNotEmpty = 768,
        RoutePlannerDoesntCreated = 769,
        FriendsDoesntAddedInRoutePlanner = 770,
        RoutePlannerIsNull = 771,
        RoutePlannerNotesMaxLengthError = 772,
        RoutePlannerNotesMinLengthError = 773,
        RoutePlannerFriendsIdTasksNull = 774,
        RoutePlannerFriendsIdTasksEmpty = 775,
        FailedToAssignTaskToUserAndCount = 776,
        RouteIdIsNull = 777,
        RouteIdIsEmpty = 778,
        RouteIdCannotBeEqualTo0AndLessThanZero = 779,
        IfThereIsAGroupIdItShouldNotBeAClubId = 780,
        IfThereIsAClubIdItShouldNotBeAGroupId = 781,
        PlanRouteGroupIdNotFound = 782,
        PlanRouteClubIdNotFound = 783, 
        FailedToConvertAlertTimeDataTimespan = 784,
        AlertTimeIsNull = 785,
        AlertTimeIsEmpty = 786,
        AlertTimeFormatIsNotCorrect = 787,
        RoutePlannerIdCannotBeEqualTo0AndLessThanZero = 789,
        TasksIsNull = 790,
        TasksIsEmpty = 791,
        TasksIsMaxLengthError = 792,
        TasksIsMinLengthError = 793,
        TasksDontAdded = 794,
        RoutePlannerDoesntDeleted = 795,
        UsersIsNotFoundUserTable = 796,
        UsersIsNotFoundSocialMediaFollowTable = 796,
        ThisUserHasAlreadyBeenAddedToThisRoute = 797,
        InboundUsersAlreadyAddedToTheRoute = 798,
        FriendsNotAddedInTableForTasks = 799,

        // Social Media

        UserHasNoFollowers = 800,

        // Group
        NotGroupCreated = 900,
        GroupIdCannotBeEqualTo0AndLessThanZero = 901,
        ThereIsNoSuchGroupInDb = 902,
        SearchedUserNotFoundinGroup = 903,
        UserIdCannotBeNullOrEmptyInGroup = 904,
        GroupDntRegisterInDB = 905,
        GroupHasNotImage = 906,
        UserIsNotInAGroup = 907,
        FailedToUpdateGroupBackgroundCoverPhoto = 908,
        FailedToUpdateGroupProfilePhoto = 909,
        TheGroupsCurrentBackgroundCoverIsEmpty = 910,
        UserHasNoAuthorityToChangeProfilePhotoGroup = 911,
        UserHasNoAuthorityToChangeBackgroundCoverGroup = 912,
        FailedToUpdateGroupName = 913,
        UserHasNoAuthorityToChangeGroupName = 914,
        TheGroupNameIsNull = 915,
        GroupMemberIdIsNotInt = 916,
        ThereAreNoMembersInTheGroup = 917,
        GroupIdIsNotInt = 918,
        GroupIdIsNotEmpty = 919,
        GroupIdIsNotNull = 920,

        // Club
        NotClubCreated = 950,
        CLubIdIsNotInt = 951,
        ThereIsNoSuchClubInDb = 952,
        SearchedUserNotFoundinClub = 953,
        UserIdCannotBeNullOrEmptyInClub = 954,
        ClubDntRegisterInDB = 955,
        FailedToUpdateClubBackgroundCoverPhoto = 956,
        UserIsNotInAClub = 957,
        FailedToUpdateClubProfilePhoto = 958,
        TheClubCurrentBackgroundCoverIsEmpty = 959,
        TheClubCurrenProfilePhotoIsEmpty = 960,
        UserHasNoAuthorityToChangeProfilePhotoClub = 961,
        UserHasNoAuthorityToChangeBackgroundCoverPhotoClub = 962,
        FailedToUpdateClubName = 963,
        UserHasNoAuthorityToChangeClubName = 964,
        TheClubNameIsNull = 965,
        ClubMemberIdIsNotInt = 966,
        GroupMemberIsNull = 967,
        UserHasNoAuthorityToDeleteUserName = 968,
        ThereIsNoPostSharedInTheClub = 969,
        ClubMemberIsNull = 970,
        ClubIdCannotBeLessThan0 = 971,
        ClubAdminIsNull = 972,
        ClubIdCannotBeEqualTo0AndLessThanZero = 973,

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
        BadRequest = 4004,
        LimitExpired = 4005,// "Your usage limit for the Here Route service has expired.";
        ApiServiceFail = 4006,

        // Touride Package 
        NoTouridePackagePricingInDB = 2050,

    }
}
