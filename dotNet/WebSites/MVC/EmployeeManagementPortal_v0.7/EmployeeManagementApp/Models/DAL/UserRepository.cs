using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Web;

namespace EmployeeManagementApp.Models.DAL
{
    public class UserRepository : IUserRepository
    {
        private EmployeeManagementAppEntities db = new EmployeeManagementAppEntities();

        public UserRepository(EmployeeManagementAppEntities db)
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

        public User GetUserByID(int Id)
        {
            return db.Users.Find(Id);
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

        public void UpdateUser(User objUser)
        {
            db.Entry(objUser).State = EntityState.Modified;
        }



        //public void UpdateFieldsSave(T entity, params Expression<func<t, object="">>[] includeProperties)
        //{
        //    using (var context = new MyContext())
        //    {
        //        var dbEntry = context.Entry(entity);

        //        foreach (var includeProperty in includeProperties)
        //        {
        //            dbEntry.Property(includeProperty).IsModified = true;
        //        }

        //        context.SaveChanges();
        //    }
        //}
    }
}