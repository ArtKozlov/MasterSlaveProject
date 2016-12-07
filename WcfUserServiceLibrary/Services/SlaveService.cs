using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using ServiceLogic;
using ServiceLogic.UserEntity;
using UserServiceNodesReplication;
using WcfUserServiceLibrary.Infostructure;
using WcfUserServiceLibrary.Interfaces;

namespace WcfUserServiceLibrary.Services
{
    public class SlaveService : ISlaveService
    {

        public static List<Slave> slaves;
        static SlaveService()
        {
            slaves = new List<Slave>();
        }

        public static void Start(int[] ports)
        {
            for (int i = 0; i < ports.Length; i++)
            {
                CreateSlave(ports[i]);
            }
        }

        private static void CreateSlave(int port)
        {

            AppDomainSetup appDomainSetup = new AppDomainSetup
            {
                ApplicationBase = AppDomain.CurrentDomain.BaseDirectory,
                PrivateBinPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Slave")
            };

            AppDomain domain = AppDomain.CreateDomain($"Slave is listening the {port} port", null, appDomainSetup);


            Slave slave = (Slave)domain.CreateInstanceAndUnwrap("MasterSlaveReplication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", typeof(Slave).FullName, false, BindingFlags.Default, null, new object[] { port }, null, null);
            new Thread(() => slave.ListenMaster()).Start();
            slaves.Add(slave);
        }

        public IEnumerable<User> SearchByFirstAndLastName(UserDataContract userDC)
        {
            int slave = slaves.Count == 1 ? 0 : new Random().Next(0, slaves.Count - 1);

            return slaves[slave].SearchByFirstAndLastName(userDC.ToUser());
        }

        public IEnumerable<User> SearchByLastName(UserDataContract userDC)
        {
            int slave = slaves.Count == 1 ? 0 : new Random().Next(0, slaves.Count - 1);

            return slaves[slave].SearchByLastName(userDC.ToUser());
        }

        public IEnumerable<User> SearchByName(UserDataContract userDC)
        {
            int slave = slaves.Count == 1 ? 0 : new Random().Next(0, slaves.Count - 1);

            return slaves[slave].SearchByName(userDC.ToUser());
        }
    }
}