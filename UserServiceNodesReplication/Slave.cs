using ServiceLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using ServiceLogic.UserEntity;

namespace UserServiceNodesReplication
{
    public class Slave : MarshalByRefObject
    {
        private readonly int _port;
        private readonly string _ip;
        private readonly UserService _userService;
        public Slave(int port, string ip)
        {
            _ip = ip;
            _port = port;
            _userService = new UserService();
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

        public void ListenMaster()
        {
            TcpListener server = null;
            try
            {
                IPAddress locAddr = IPAddress.Parse(_ip);

                server = new TcpListener(locAddr, _port);
                
                server.Start();
                

                BinaryFormatter formatter = new BinaryFormatter();
                
                while (true)
                {
                    TcpClient client = server.AcceptTcpClient();

                    using (NetworkStream stream = client.GetStream())
                    {
                        Message message = (Message)formatter.Deserialize(stream);
                        MakeAction(message);
                    }
                    
                    client.Close();
                }
            }
            catch (SocketException e)
            {

            }
            finally
            {
                server.Stop();
                
            }
        }

        private void Add(User user)
            => _userService.AddUser(user);

        private void Update(User user) 
            => _userService.UpdateUser(user);

        private void Delete(User user) 
            => _userService.RemoveUser(user);

        private void MakeAction(Message message)
        {
            switch (message.Operation)
            {
                case Operation.Add:
                    Add(message.User);
                    break;

                case Operation.Delete:
                    Delete(message.User);
                    break;

                case Operation.Update:
                    Update(message.User);
                    break;

                default:
                    break;
            }
        }
    }
}
