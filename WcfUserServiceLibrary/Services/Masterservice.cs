using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using ServiceLogic.UserEntity;
using UserServiceNodesReplication;
using WcfUserServiceLibrary.Infostructure;
using WcfUserServiceLibrary.Interfaces;

namespace WcfUserServiceLibrary.Services
{
    public class MasterService : IMasterService
    {
        private static readonly Master master;
        static MasterService()
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            ConfigurationSectionCollection sections = config.Sections;
            PortConfigSection settings = (PortConfigSection)sections["PortSettings"];
            int[] ports = settings.ServiceNodesItems.ToArray();
            SlaveService.Start(ports);
            master = new Master(ports);
        }

        public void Add(UserDataContract userDC) => master.Add(userDC.ToUser());

        public void Delete(UserDataContract userDC) => master.Delete(userDC.ToUser());

        public User GetUserById(int id) 
            => master.GetUserById(id);

        public IEnumerable<User> GetUsers() 
            => master.GetUsers();

        public IEnumerable<User> SearchByLastAndFirstName(UserDataContract userDC) 
            => master.SearchByFirstAndLastName(userDC.ToUser());


        public IEnumerable<User> SearchByLastName(UserDataContract userDC)
            => master.SearchByLastName(userDC.ToUser());


        public IEnumerable<User> SearchByName(UserDataContract userDC) 
            => master.SearchByName(userDC.ToUser());

        public bool Update(UserDataContract userDC)
            => master.Update(userDC.ToUser());

    }
}