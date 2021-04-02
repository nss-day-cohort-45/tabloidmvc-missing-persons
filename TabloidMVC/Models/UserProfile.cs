using DocuSign.eSign.Model;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace TabloidMVC.Models
{
    public class UserProfile
    {
        public int Id { get; set; }
        [DisplayName("Image")]
        public string ImageLocation { get; set; }
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        [DisplayName("Call me as")]
        public string DisplayName { get; set; }
        public string Email { get; set; }
        [DisplayName("Date of Creation")]
        public DateTime CreateDateTime { get; set; }
        [DisplayName("User Type")]
        public int UserTypeId { get; set; }
        public UserType UserType { get; set; }
        [DisplayName("Full Name")]
        public string FullName
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }
    }
}