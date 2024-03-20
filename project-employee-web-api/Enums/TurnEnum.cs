using System.Text.Json.Serialization;

namespace project_employee_web_api.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]

    public enum TurnEnum
    {
        Morning,
        Afternoon,
        Evening
    }
}
