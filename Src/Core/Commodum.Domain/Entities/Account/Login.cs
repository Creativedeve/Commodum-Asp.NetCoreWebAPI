using System;
using System.Collections.Generic;
using System.Text;

namespace Commodum.Domain.Entities.Account
{
    public class Login
    {
        public string Token { get; set; }
        public int UserType { get; set; }
    }
}
