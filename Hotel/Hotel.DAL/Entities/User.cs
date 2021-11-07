using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Hotel.DAL.Entities
{
    public class User
    {
        [Key]
        public string Login { get; set; }
        public string PasswordHash { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}