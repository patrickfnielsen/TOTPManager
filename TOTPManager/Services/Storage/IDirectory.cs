using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TOTPManager.Services.Storage
{
    public interface IDirectory
    {
        void CreateDirectory(string path);
    }
}
