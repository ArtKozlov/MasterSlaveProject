using System;
using System.Collections.Generic;

namespace ServiceLogic.Interfaces
{
    public interface IUserService
    {
        User AddUser(User user);

        void RemoveUser(User user);

        User GetUserById(int id);

        IEnumerable<User> GetUserByPredicate(Func<User, bool> predicate);

        IEnumerable<User> SearchUsers(params Func<User, bool>[] predicates);

        bool UpdateUser(User user);

        IEnumerable<User> GetAllUsers();

        void DumpToXml(IDump dumper, string path);

        IEnumerable<User> GetXmlDump();
    }
}
