using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace WordQuestAPI.Models 
{
    public class User : IdentityUser
    {
        public override string Id { get; set; }
        public override string UserName { get; set; }

        public override string PasswordHash { get; set; }

        public override string Email { get; set; }
        public override string PhoneNumber { get; set; }
    }
}