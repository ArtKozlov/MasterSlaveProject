using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Xml.Serialization;
using ServiceLogic.Interfaces;
using ServiceLogic.UserEntity;

namespace ServiceLogic.DumpToXml
{
    public class XmlDump : IDump
    {
        private string FilePath { get; }


        public XmlDump()
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            ConfigurationSectionCollection sections = config.Sections;
            DumpConfigSection settings = (DumpConfigSection)sections["dumpSettings"];
            string path = settings.GetPathItems[0].Path;
            FilePath = path;
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
