using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Geolocation.Domain.Interfaces;
using Geolocation.Domain.Models;

namespace Geolocation.Infrastructure.Entities
{
    public class User : IUser
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }

        [JsonIgnore]
        public string Password { get; set; }

        [JsonIgnore]
        public List<RefreshToken> RefreshTokens { get; set; }
    }
}