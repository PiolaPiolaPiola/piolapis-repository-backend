using System;

namespace PiolAPIS_Repository.Domain.Entities
{
    public class User : Base
    {
        public string Role { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{Name} {LastName}";
    }
}
