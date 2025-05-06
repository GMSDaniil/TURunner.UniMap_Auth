﻿using UserManagementAPI.Modells;

namespace UserManagementAPI.Services
{
    public interface IJwtProvider
    {
        string GenerateToken(User user);
    }
}