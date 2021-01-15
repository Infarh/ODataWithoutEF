using System;

namespace Domain.Models
{
    public class School
    {
        public Guid Id { get; set; }
        public String Name { get; set; }

        public override string ToString() => $"[id:{Id}]{Name}";
    }
}
