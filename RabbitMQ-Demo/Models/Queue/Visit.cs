using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Demo.Models.Queue
{
    [JsonObject(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class Visit
    {
        public string Visitor { get; }

        public DateTime VisitTime { get; }

        public Visit(string visitor)
        {
            Visitor = visitor;
            VisitTime = DateTime.UtcNow;
        }
    }
}