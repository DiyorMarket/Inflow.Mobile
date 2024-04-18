using System;
using System.Collections.Generic;
using System.Text;

namespace Inflow.Mobile.Models
{
    public class AuthenticationResponse
    {
        public string Token { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string UserPhone { get; set; }
    }
}
