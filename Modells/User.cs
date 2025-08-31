namespace UserManagementAPI.Modells
{
    public class User
    {
        private User(Guid id, string passwordHash, string email, string username, bool isConfirmed)
        {
            Id = id;
            PasswordHash = passwordHash;
            Email = email;
            Username = username;
            IsConfirmed = isConfirmed;
        }

        public Guid Id { get; set; }
        public bool IsConfirmed { get; set; } 
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public static User Create(Guid id, string passwordHash, string email, string username, bool isConfirmed)
        {
            return new User(id, passwordHash, email, username, isConfirmed);
        }
    }
}
