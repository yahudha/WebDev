using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeManagementPortal_v0._8.Models.DAL
{
    public class ModelRepository : IModelRepository
    {

        private EmployeeManagementAppEntities db = new EmployeeManagementAppEntities();

        public ModelRepository(EmployeeManagementAppEntities db)
        {
            this.db = db;
        }

        public void DeleteUser(int Id)
        {
            User objUser = db.Users.Find(Id);
            db.Users.Remove(objUser);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Role> GetRoles()
        {
            return db.Roles.ToList();
        }

        public User GetUserbyID(int Id)
        {
            User objUser = db.Users.Find(Id);
            return objUser;
        }

        public IEnumerable<User> GetUsers()
        {
            return db.Users.ToList();
        }

        public void InsertUser(User objUser)
        {
            db.Users.Add(objUser);
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}