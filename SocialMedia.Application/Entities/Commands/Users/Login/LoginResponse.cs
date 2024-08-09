using SocialMedia.Application.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Application.Entities.Commands.Users.Login
{
    public class LoginResponse : Response
    {
        public string? JWT { get; set; }
    }
}
