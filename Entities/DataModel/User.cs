namespace Entities.DataModel
{
    public class User : BaseModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public int FailCount { get; set; }
        public string Salt { get; set; }
    }
}
