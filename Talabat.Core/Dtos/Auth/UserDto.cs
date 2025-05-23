using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Dtos.Auth
{
    public class UserDto
    {
        [Required(ErrorMessage = "Email is Required")]
        public string Email { get; set; }
        public string DisplayName  { get; set; }
        public string Token { get; set; }
    }
}
