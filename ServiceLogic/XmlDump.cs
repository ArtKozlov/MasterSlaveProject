using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceLogic.Interfaces;
using System.Xml.Serialization;
using System.IO;

namespace ServiceLogic
{
    public class XmlDump: IDump
    {
        private string pathToFile;

        public XmlDump()
        {
            pathToFile = "C:\\Users\\Default\\Documents";
        }

        public IEnumerable<User> GetDump()
        {
            var formatter = new XmlSerializer(typeof(List<User>));
            IEnumerable<User> users;

            using (FileStream fileStream = new FileStream(pathToFile, FileMode.OpenOrCreate))
            {
                users = (IEnumerable<User>)formatter.Deserialize(fileStream);
            }

            return users;
        }

        public void Dump(IEnumerable<User> users, string path = null)
        {
            path = path ?? pathToFile;
            var formatter = new XmlSerializer(typeof(List<User>));

            using (FileStream fileStream = new FileStream(path, FileMode.OpenOrCreate))
            {
                formatter.Serialize(fileStream, users);
            }
        }
    }
}
