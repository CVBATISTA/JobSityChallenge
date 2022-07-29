using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobSityNETChallenge.Application.ViewModels
{
    public class RegisterUserViewModel
    {
        public string Email { get; set; }
        public string Username { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string Password {get; set; }

        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
