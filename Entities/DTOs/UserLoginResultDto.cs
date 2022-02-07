using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities;
using Core.Utilities.Security.JWT;

namespace Entities.DTOs
{
    public class UserLoginResultDto : IDto
    {
        public AccessToken AccessToken { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
       
    }
}
