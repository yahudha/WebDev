using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementApp.Models.DAL
{
    interface IUserRepository : IDisposable
    {
        IEnumerable<User> GetUsers();

        IEnumerable<Role> GetRoles();


        User GetUserByID(int Id);

        void InsertUser(User objUser);

        void UpdateUser(User objUser);

        void DeleteUser(int Id);

        void Save();
    }
}
