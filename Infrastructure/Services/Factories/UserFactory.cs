using Core.Entities;
using Core.Interfaces;
using Core.Options;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.Factories
{
    public class UserFactory : IFactory<User, UserInitOptions>
    {
        private readonly IConfiguration _configuration;

        public UserFactory(IConfiguration configuration) 
        {
            _configuration = configuration;
        }

        public User Create(UserInitOptions userOptions)
        {
            return new User(
                userOptions.Name, 
                userOptions.Email, 
                _configuration["DefaultCatalog"] ?? throw new NullReferenceException()
            );
        }
    }
}
