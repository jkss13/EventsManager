using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventsManager.Models
{

    public enum Profile
    {
        Comum = 1,
        Produtor = 2
    }

    public class User
    {
        public Profile ProfileType { get; set; }
        public string Name { get; set; }
        public long CPF { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }


        public User(Profile profileType, string name, long cpf, string email, string password)
        {
            ProfileType = profileType;
            Name = name;
            CPF = cpf;
            Email = email;
            Password = password;
        }
        
        
    }

}