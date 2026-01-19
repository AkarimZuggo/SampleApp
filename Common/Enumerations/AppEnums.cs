using System.Text.Json.Serialization;

namespace Common.Constant
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum EnumObjectType : int
    {
        Employee = 1,
        Customer = 2
    }
    //Days
    public enum EnumDays : int
    {
        Monday = 1,
        Tuesday = 2,
        Wednesday = 3,
        Thursday = 4,
        Friday = 5,
        Saturday = 6
    }
}
