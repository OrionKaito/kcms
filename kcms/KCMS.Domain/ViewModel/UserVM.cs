using System.Collections.Generic;

namespace KCMS.Domain.ViewModel
{
    public class UserInsertModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
    }

    public class UserListViewModel
    {
        public IEnumerable<User.User> Results { get; set; }
        public int TotalPages { get; set; }
    }

    public class UserUpdateModel : UserInsertModel
    {
        public long Id { get; set; }
    }

    public class UserLoginModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
