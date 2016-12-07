using System;
using System.Collections.Generic;
using System.ServiceModel;
using ServiceLogic;
using ServiceLogic.UserEntity;

namespace WcfUserServiceLibrary.Interfaces
{
    [ServiceContract]
    public interface ISlaveService
    {
        [OperationContract]
        IEnumerable<User> SearchByName(UserDataContract user);

        [OperationContract]
        IEnumerable<User> SearchByLastName(UserDataContract user);

        [OperationContract]
        IEnumerable<User> SearchByFirstAndLastName(UserDataContract user);
    }
}
