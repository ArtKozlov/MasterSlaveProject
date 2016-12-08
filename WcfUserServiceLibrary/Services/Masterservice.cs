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
            PortConfigSection settings = (PortConfigSection)sections["portSettings"];
            List<int> ports = new List<int>();
            List<string> ips = new List<string>();
            for (int i = 0; i < settings.ServiceNodesItems.Count; i++)
            {
                ips.Add(settings.ServiceNodesItems[i].Ip);
                ports.Add(settings.ServiceNodesItems[i].Port);
            }
            SlaveService.Start(ports, ips);
            master = new Master(ports, ips);
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