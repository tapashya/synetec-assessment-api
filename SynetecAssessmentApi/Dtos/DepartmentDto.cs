using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace SynetecAssessmentApi.Dtos
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class DepartmentDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
