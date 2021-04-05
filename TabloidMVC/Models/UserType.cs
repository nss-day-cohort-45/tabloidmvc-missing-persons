using System.ComponentModel.DataAnnotations;

namespace TabloidMVC.Models
{
    public class UserType
    {
        public int Id { get; set; }
        [Display (Name = "User Type")]
        public string Name { get; set; }
    }
}