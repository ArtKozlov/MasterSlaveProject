using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLogic.Interfaces
{
    public interface IRepository<User>
    {
        User Add(User user);
        void Remove(User user);
        User GetById(int id);
        IEnumerable<User> GetByPredicate(Func<User, bool> predicate);
        IEnumerable<User> SearchUsers(params Func<User, bool>[] predicates);
        bool UpdateUser(User user);
        List<User> GetUsers();
    }
}
