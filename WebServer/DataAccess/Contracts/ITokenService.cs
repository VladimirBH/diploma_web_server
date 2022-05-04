﻿using WebServer.DataAccess.Implementations.Entities;

namespace WebServer.DataAccess.Contracts
{
    public interface ITokenService
    {
        string BuildToken(string key, string issuer, Users user);
        bool IsTokenValid(string key, string issuer, string token);
    }
}
