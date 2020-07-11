using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementPortal_v0._8.Models.DAL
{
    interface IModelRepository : IDisposable
    {
        IEnumerable<User> GetUsers();

        IEnumerable<Role> GetRoles();

        User GetUserbyID(int Id);

        void InsertUser(User objUser);

        void DeleteUser(int Id);

        void Save();
    }
}
