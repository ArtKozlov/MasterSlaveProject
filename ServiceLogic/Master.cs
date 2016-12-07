using ServiceLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MastersSlaveProvider
{
        public enum Operation
        {
            Add,
            Delete,
            Update
        }

        [Serializable]
        public class Message
        {
            public User User { get; }
            public Operation Operation { get; }

            public Message(User user, Operation operation)
            {
                User = user;
                Operation = operation;
            }

        }

    public class Master
    {
        //private readonly UserService userService;
        //private readonly int port;

        //public Master()
        //{
        //    port = 33;
        //    userService = new UserService();
        //}

        //public User Add(User user)
        //{
        //    var result = userService.AddUser(user);

        //    if (result != null)
        //    {
        //        SendMessage(new Message(user, Operation.Add));
        //    }

        //    return result;
        //}

        //public bool Update(User user)
        //{
        //    bool result = userService.UpdateUser(user);

        //    if (result)
        //    {
        //        SendMessage(new Message(user, Operation.Update));
        //    }

        //    return result;
        //}

        //public void Delete(User user)
        //{
        //    userService.RemoveUser(user);

        //    SendMessage(new Message(user, Operation.Delete));

        //}

        //public User GetUserById(int id) => userService.GetUserById(id);

        //public IList<User> SearchByName(User user) => userService.SearchUsers(user.FirstName);

        //public IList<User> SearchByLastName(User user) => userService.SearchByLastName(user);

        //public IList<User> SearchByLastAndFirstName(User user) => userService.SearchByLastAndFirstName(user);

        //public IList<User> GetUsers() => userService.GetUsers();

        //private void SendMessage(Message message)
        //{
        //    var bf = new BinaryFormatter();
        //    // Create a TcpClient.
        //    // Note, for this client to work you need to have a TcpServer 
        //    // connected to the same address as specified by the server, port
        //    // combination.
        //    using (TcpClient client = new TcpClient("127.0.0.1", port))
        //    {
        //        using (NetworkStream stream = client.GetStream())
        //        {
        //            bf.Serialize(stream, message);
        //        }

        //    }
        //}
    }
}
