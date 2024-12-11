using System;
using System.Collections.Generic;
using System.Text;

namespace Swack.Logic.Models
{
    public class User
    {
        public User(string username, string profileUrl)
        {
            Username = username;
            ProfileUrl = profileUrl;
        }

        public string Username { get; set; }

        public string ProfileUrl { get; set; }
    }
}
