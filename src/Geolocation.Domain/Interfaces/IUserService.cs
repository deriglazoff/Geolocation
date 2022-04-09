using System;
using System.Collections.Generic;
using Geolocation.Domain.Models;

namespace Geolocation.Domain.Interfaces
{
    public interface IUserService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model, string ipAddress);
        AuthenticateResponse RefreshToken(string token, string ipAddress);
        bool RevokeToken(string token, string ipAddress);
        IEnumerable<IUser> GetAll();
        IUser GetById(Guid id);
    }
}