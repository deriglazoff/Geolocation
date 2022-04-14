using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Geolocation.Authentication
{
    public struct User
    {
        public User(string base64) : this()
        {
            SetProperty(base64);
        }

        public User(IReadOnlyList<string> credentials) : this()
        {
            SetProperty(credentials);
        }
        public User(IReadOnlyList<Claim> claims) : this()
        {
            SetProperty(claims);
        }

        public string Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }

        public Claim[] GetClaims()
        {
            return new Claim[]
            {
                new(ClaimTypes.NameIdentifier, Id),
                new(ClaimTypes.Name, Username),
                new(ClaimTypes.Email, Email),
            };
        }
        public Claim[] Base64String()
        {
            return new Claim[]
            {
                new(ClaimTypes.NameIdentifier, Id),
                new(ClaimTypes.Name, Username),
                new(ClaimTypes.Email, Email),
            };
        }
        private void SetProperty(string base64)
        {
            var credentialBytes = Convert.FromBase64String(base64);
            var credentialsString = Encoding.UTF8.GetString(credentialBytes);

            var credentials = credentialsString.Split(new[] { ':' });
            SetProperty(credentials);
        }

        private void SetProperty(IReadOnlyList<string> credentials)
        {
            Username = credentials[0];
            //skip credentials[1] there password
            Id = credentials[2];
            Email = credentials[3];
        }

        private void SetProperty(IReadOnlyList<Claim> claims)
        {
            Username = claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
            Id = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            Email = claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
        }
    }


}