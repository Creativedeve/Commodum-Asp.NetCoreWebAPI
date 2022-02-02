using System;
using System.Collections.Generic;
using System.Text;

namespace Commodum.Domain.Entities.Account
{
    public class UserPasswordLink
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string PasswordToken { get; set; }
        public string UserPasswordLinkConfirmationId { get; set; }
        public string Email { get; set; }
    }
}
