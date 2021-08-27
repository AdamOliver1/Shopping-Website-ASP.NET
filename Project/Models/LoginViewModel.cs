using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Required field")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Required field")]
        public string Password { get; set; }
    }
}
