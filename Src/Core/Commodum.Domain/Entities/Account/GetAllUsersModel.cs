using System;
using System.Collections.Generic;
using System.Text;

namespace Commodum.Domain.Entities.Account
{
    public class GetAllUsersModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }  
    }
}
