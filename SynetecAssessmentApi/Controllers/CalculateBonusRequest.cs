using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace SynetecAssessmentApi.Dtos
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class CalculateBonusRequest
    {
        public int TotalBonusPoolAmount { get; set; }
        public int SelectedEmployeeId { get; set; }
    }
}
