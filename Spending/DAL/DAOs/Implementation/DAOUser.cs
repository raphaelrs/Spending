using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.DAOs.Implementation
{
    public class DAOUser : DAOGeneric<User>, IDAOUser
    {
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
