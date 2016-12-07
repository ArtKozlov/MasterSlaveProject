using System;
using System.Collections.Generic;
using System.Linq;
using ServiceLogic.Validation;
using ServiceLogic.Interfaces;

namespace ServiceLogic
{
    public class UserService
    {
        private readonly IRepository<User> _userRepository;
        private Func<int, int> increment;

        public UserService() : this(new UserRepository(), s => ++s)
        {
        }

        public UserService(IRepository<User> repository) : this(repository, s => ++s)
        {
        }

        public UserService(Func<int, int> fun) : this(new UserRepository(), fun)
        {
        }

        public UserService(IRepository<User> repository, Func<int, int> inc)
        {
            _userRepository = repository;
            increment = inc;

            foreach (var item in _userRepository)
            {               
                item.Id = _userRepository.Last().Id == 0 ? increment(0) : increment(_userRepository.Last().Id);
            }
            
        }

        public User AddUser(User user)
        {
            if (ReferenceEquals(user, null))
                throw new ArgumentNullException();

            if (!Validator.UserIsValid(user))
                throw new InvalidUserException();
            try
            {
                user.Id = _userRepository.Last().Id == 0 ? increment(0) : increment(_userRepository.Last().Id);
            }
            catch(Exception)
            {
                user.Id = 1;
            }
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
            return _userRepository.Update(user);
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
