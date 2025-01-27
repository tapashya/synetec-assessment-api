﻿using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace SynetecAssessmentApi.Dtos
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class BonusPoolCalculatorResultDto
    {
        public int Amount { get; set; }
        public EmployeeDto Employee { get; set; }
    }
}
