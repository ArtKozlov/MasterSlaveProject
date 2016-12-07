using System;
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
