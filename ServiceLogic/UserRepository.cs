using ServiceLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;

namespace ServiceLogic
{
    [Serializable]
    public class UserRepository : IRepository<User>
    {

        private List<User> _userRepository;

        public UserRepository()
        {
            _userRepository = new List<User>();
        }

        public UserRepository(List<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public User Add(User user)
        {
            if (ReferenceEquals(user, null))
                throw new ArgumentNullException();
            _userRepository.Add(user);
            return user;
        }

        public User GetById(int id)
        {
            return _userRepository.FirstOrDefault(u => u.Id == id);
        }

        public IEnumerable<User> GetByPredicate(Func<User, bool> predicate)
        {
            return _userRepository.Where(predicate);
        }

        public List<User> GetUsers()
        {
            return _userRepository;
        }

        public void Remove(User user)
        {
            if (ReferenceEquals(user, null))
                throw new ArgumentNullException();
            _userRepository.Remove(user);
        }



        public IEnumerable<User> SearchUsers(params Func<User, bool>[] predicates)
        {
            List<User> result = new List<User>();
            foreach (var predicate in predicates)
                result.AddRange(_userRepository.Where(predicate));
            return result;
        }

        public bool Update(User user)
        {
            if (ReferenceEquals(user, null))
                throw new ArgumentNullException();
            User updatedUser = GetById(user.Id);
            if (updatedUser == null)
            {
                return false;
            }

            updatedUser.FirstName = user.FirstName;
            updatedUser.LastName = user.LastName;
            updatedUser.DateBirth = user.DateBirth;
            return true;
        }

        public IEnumerator<User> GetEnumerator()
        {
            return _userRepository.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
