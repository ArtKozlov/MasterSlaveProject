using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceLogic.UserEntity;

namespace ServiceLogic.Interfaces
{
    public interface IDump
    {
        void Dump(IEnumerable<User> users, string path);
        IEnumerable<User> GetDump();
    }
}
