using ServiceLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using ServiceLogic.UserEntity;

namespace UserServiceNodesReplication
{
        public enum Operation
        {
            Add,
            Delete,
            Update
        }

    public class Master
    {
        private readonly UserService _userService;
        private readonly List<int> _ports;
        private readonly List<string> _ips;

        public Master(List<int> ports, List<string> ips)
        {
            _ips = ips;
            _ports = ports;
            _userService = new UserService();
        }

        public void Add(User user)
        {
            _userService.AddUser(user);

            SendMessage(new Message(user, Operation.Add));

        }

        public bool Update(User user)
        {
            bool result = _userService.UpdateUser(user);

            if (result)
            {
                SendMessage(new Message(user, Operation.Update));
            }

            return result;
        }

        public void Delete(User user)
        {
            _userService.RemoveUser(user);

            SendMessage(new Message(user, Operation.Delete));

        }

        public User GetUserById(int id) => _userService.GetUserById(id);

        public IEnumerable<User> SearchByName(User user)
            => _userService.SearchUsers(u => u.FirstName == user.FirstName);

        public IEnumerable<User> SearchByLastName(User user)
            => _userService.SearchUsers(u => u.LastName == user.LastName);

        public IEnumerable<User> SearchByFirstAndLastName(User user)
            => _userService.SearchUsers(u => u.LastName == user.LastName && u.FirstName == user.FirstName);

        public IEnumerable<User> GetUsers()
            => _userService.GetAllUsers();

        private void SendMessage(Message message)
        {
            BinaryFormatter formatter = new BinaryFormatter();

            for(int i = 0; i < _ports.Count; i++)
            {
                
                using (TcpClient client = new TcpClient(_ips[i], _ports[i]))
                {
                    using (NetworkStream stream = client.GetStream())
                    {
                        formatter.Serialize(stream, message);
                    }
                }
            }
        }

    }
}
