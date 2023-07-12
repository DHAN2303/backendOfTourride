namespace AllrideApiCore.Dtos.Here
{
    public class HereNearByRootobject
    {
        public HereNearByItem[] items { get; set; }
    }

    public class HereNearByItem
    {
        public string title { get; set; }
        public string id { get; set; }
        public string language { get; set; }
        public string resultType { get; set; }
        public HereNearByAddress address { get; set; }
        public HereNearByPosition position { get; set; }
        public HereNearByAccess[] access { get; set; }
        public int distance { get; set; }
        public HereNearByCategory[] categories { get; set; }
        public HereNearByReference[] references { get; set; }
        public HereNearByFoodtype[] foodTypes { get; set; }
        public HereNearByContact[] contacts { get; set; }
        public HereNearByOpeninghour[] openingHours { get; set; }
    }

    public class HereNearByAddress
    {
        public string label { get; set; }
        public string countryCode { get; set; }
        public string countryName { get; set; }
        public string stateCode { get; set; }
        public string state { get; set; }
        public string county { get; set; }
        public string city { get; set; }
        public string street { get; set; }
        public string postalCode { get; set; }
        public string houseNumber { get; set; }
    }

    public class HereNearByPosition
    {
        public float lat { get; set; }
        public float lng { get; set; }
    }

    public class HereNearByAccess
    {
        public float lat { get; set; }
        public float lng { get; set; }
    }

    public class HereNearByCategory
    {
        public string id { get; set; }
        public string name { get; set; }
        public bool primary { get; set; }
    }

    public class HereNearByReference
    {
        public HereNearBySupplier supplier { get; set; }
        public string id { get; set; }
    }

    public class HereNearBySupplier
    {
        public string id { get; set; }
    }

    public class HereNearByFoodtype
    {
        public string id { get; set; }
        public string name { get; set; }
        public bool primary { get; set; }
    }

    public class HereNearByContact
    {
        public HereNearByPhone[] phone { get; set; }
    }

    public class HereNearByPhone
    {
        public string value { get; set; }
    }

    public class HereNearByOpeninghour
    {
        public HereNearByCategory1[] categories { get; set; }
        public string[] text { get; set; }
        public bool isOpen { get; set; }
        public HereNearByStructured[] structured { get; set; }
    }

    public class HereNearByCategory1
    {
        public string id { get; set; }
    }

    public class HereNearByStructured
    {
        public string start { get; set; }
        public string duration { get; set; }
        public string recurrence { get; set; }
    }


}
