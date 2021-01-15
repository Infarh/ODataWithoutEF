using System;

namespace Domain.Models
{
    public class Student
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public virtual School School { get; set; }

        public override string ToString() => $"[id:{Id}]{Name} - {School}";
    }
}
