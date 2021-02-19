
using System.Collections.Generic;
using EFCoreAPIDemo.Models;


namespace EFCoreAPIDemo.Repositories
{
    public interface IUser
    {
        User Get(int id);
        User Get(string email);
        List<User> GetAll();       
        int Create(User user);
        int Update(User user);
        void Delete(int id);
    }
}
