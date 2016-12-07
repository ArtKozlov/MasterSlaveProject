using System;
using System.Collections.Generic;
using ServiceLogic.Interfaces;
using System.Xml.Serialization;
using System.IO;
using System.Configuration;
using ServiceLogic.UserEntity;

namespace ServiceLogic
{
    public class XmlDump : ConfigurationSection, IDump
    {
        private string FilePath { get; }


        public XmlDump()
        {
            // FilePath = ConfigurationManager.AppSettings["FilePath"];
            FilePath = @"C:\Users\Default\Documents\dump.xml";
        }
        public IEnumerable<User> GetDump()
        {
            var formatter = new XmlSerializer(typeof(List<User>));
            IEnumerable<User> users;

            using (FileStream fileStream = new FileStream(FilePath, FileMode.OpenOrCreate))
            {
                users = (IEnumerable<User>)formatter.Deserialize(fileStream);
            }

            return users;
        }

        public void Dump(IEnumerable<User> users, string path = null)
        {
            path = path ?? FilePath;
            var formatter = new XmlSerializer(typeof(List<User>));

            using (FileStream fileStream = new FileStream(path, FileMode.OpenOrCreate))
            {
                formatter.Serialize(fileStream, users);
            }
        }
    }
}
