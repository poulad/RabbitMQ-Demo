using System;

namespace Demo.Models.Queue
{
    [Serializable]
    public class Visit
    {
        public string Visitor { get; }

        public DateTime VisitTime { get; }

        public Visit(string name)
        {
            Visitor = name;
            VisitTime = DateTime.UtcNow;
        }
    }
}