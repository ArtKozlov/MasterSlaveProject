using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using ServiceLogic;
using ServiceLogic.UserEntity;

namespace WcfUserServiceLibrary.Interfaces
{
    [ServiceContract]
    public interface IMasterService
    {
      
        [OperationContract]
        void Add(UserDataContract user);

        [OperationContract]
        bool Update(UserDataContract user);

        [OperationContract]
        void Delete(UserDataContract user);

        [OperationContract]
        User GetUserById(int id);

        [OperationContract]
        IEnumerable<User> SearchByName(UserDataContract user);

        [OperationContract]
        IEnumerable<User> SearchByLastName(UserDataContract user);

        [OperationContract]
        IEnumerable<User> SearchByLastAndFirstName(UserDataContract user);

        [OperationContract]
        IEnumerable<User> GetUsers();
        
    }

    [DataContract]
    public struct VisaDataContract
    {
        [DataMember]
        public string Country { get; set; }

        [DataMember]
        public DateTime Start { get; set; }

        [DataMember]
        public DateTime End { get; set; }

    }

    [DataContract]
    public class UserDataContract
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string LastName { get; set; }

        [DataMember]
        public DateTime DateOfBirth { get; set; }

        [DataMember]
        public VisaDataContract[] UserVisa { get; set; }

        [DataMember]
        public Gender UserGender { get; set; }
    }


}
