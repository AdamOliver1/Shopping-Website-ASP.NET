using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    [Serializable]
    public class UserModel
    {       
        public int Id { get; set; }
        [Required(ErrorMessage = "* Required field")]
        [MaxLength(50)]
        [TrimArrribute]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "* Required field")]
        [MaxLength(50)]
        [TrimArrribute]
        public string LastName { get; set; }
    
        [Required(ErrorMessage = "* Required field")]
        [Column(TypeName = "smalldatetime")]
        public DateTime BirthDate { get; set; }
      
        [Required(ErrorMessage = "* Required field")]
        [MaxLength(50)]
        [TrimArrribute]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "* Required field")]
        [MaxLength(50)]
        [TrimArrribute]
        public string UserName { get; set; }
        [Required(ErrorMessage = "* Required field")]
        [MaxLength(50)]
        [TrimArrribute]
        public string Password { get; set; }

        [NotMapped]
        [Compare("Password", ErrorMessage = "* Confirm password doesn't match, Type again !")]
        [Required(ErrorMessage = "Required field")]      
        public string ConfirmPassword { get; set; }

    }
}
