﻿using UserManagementAPI.Modells;

namespace UserManagementAPI.Entities
{
    public class UserEntity
    {
        public UserEntity(Guid id, string passwordHash, string email, string username)
        {
            Id = id;
            PasswordHash = passwordHash;
            Email = email;
            Username = username;
        }

        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool isConfirmed { get; set; } = false;
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
    }
}
