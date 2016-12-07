using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceLogic;
using ServiceLogic.UserEntity;

namespace UserServiceNodesReplication
{
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
}
