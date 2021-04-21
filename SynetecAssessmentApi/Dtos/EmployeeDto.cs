using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace SynetecAssessmentApi.Dtos
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class EmployeeDto
    {
        public int Id { get; set; }
        public string Fullname { get; set; }
        public string JobTitle { get; set; }
        public int Salary { get; set; }
        public DepartmentDto Department { get; set; }
    }
}
