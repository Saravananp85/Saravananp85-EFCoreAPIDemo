using EFCoreAPIDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreAPIDemo.Repositories
{
    public class UserRepositories : IUser
    {
        private DemoContext _demoDBContext;
        public UserRepositories(DemoContext demoContext)
        {
            _demoDBContext = demoContext;
        }

        public int Create(User user)
        {
            if(_demoDBContext.Users.Any(a => a.EmailId.ToLower() == user.EmailId.ToLower()))
            {
                return -1;
            }

            user.IsActive = true;
            user.CreatedDate = DateTime.Now;
            _demoDBContext.Users.Add(user);
            _demoDBContext.SaveChanges();
            return user.Id;
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<User> GetAll()
        {
            return _demoDBContext.Users.ToList();
        }

        public User Get(int id)
        {
            return _demoDBContext.Users.Find(id);
        }

        public User Get(string email)
        {
            return _demoDBContext.Users.FirstOrDefault(a => a.EmailId.ToLower() == email.ToLower());
        }

        public int Update(User user)
        {
            if (_demoDBContext.Users.Any(a => a.EmailId.ToLower() == user.EmailId.ToLower() && a.Id != user.Id ))
            {
                return -1;
            }           

            user.ModifiedDate = DateTime.Now;
            _demoDBContext.Users.Update(user);
            _demoDBContext.SaveChanges();
            return user.Id;
        }
    }
}
