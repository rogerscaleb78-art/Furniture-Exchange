namespace API.Models
{
    /// <summary>
    /// A user account (accounts table).
    /// </summary>
    public class Account
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        /// <summary>Stored as plain text. See "Possible next steps" in the README.</summary>
        public string Password { get; set; }

        /// <summary>Whether the account has admin rights.</summary>
        public bool Admin { get; set; }
    }
}
