using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceLogic.Interfaces;

namespace ServiceLogic
{
    public class UserService
    {
        private readonly IRepository<User> _userRepository;

        public UserService()
        {
            _userRepository = new UserRepository();
        }

        public UserService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }


        public User AddUser(User user)
        {
            if (ReferenceEquals(user, null))
                throw new ArgumentNullException();
            return _userRepository.Add(user);
        }

        public void RemoveUser(User user)
        {
            if (ReferenceEquals(user, null))
                throw new ArgumentNullException();
            _userRepository.Remove(user);
        }

        public User GetUserById(int id)
        {
            if (ReferenceEquals(id, null))
                throw new ArgumentNullException();
            if (_userRepository.GetById(id) == null)
                throw new ArgumentOutOfRangeException();
            return _userRepository.GetById(id);
        }

        public IEnumerable<User> GetUserByPredicate(Func<User, bool> predicate)
        {
            if (ReferenceEquals(predicate, null))
                throw new ArgumentNullException();
            return _userRepository.GetByPredicate(predicate);
        }
        public IEnumerable<User> SearchUsers(params Func<User, bool>[] predicates)
        {
            if (ReferenceEquals(predicates, null))
                throw new ArgumentNullException();
            return _userRepository.SearchUsers(predicates);
        }

        public bool UpdateUser(User user)
        {
            if (ReferenceEquals(user, null))
                throw new ArgumentNullException();
            return _userRepository.UpdateUser(user);
        }
        public IEnumerable<User> GetAllUsers()
        {
            return _userRepository.GetUsers();
        }

        public void DumpToXml(IDump dumper, string path)
        {

            if (ReferenceEquals(dumper, null))
                dumper = new XmlDump();
            if(ReferenceEquals(path, null))
                dumper.Dump(_userRepository.GetUsers(), null);
            dumper.Dump(_userRepository.GetUsers(), path);
        }
        public IEnumerable<User> GetXmlDump()
        {
            XmlDump dumper = new XmlDump();
            return dumper.GetDump();
        }
    }
}
