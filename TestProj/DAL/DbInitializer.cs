using System;
using System.Data.Entity;
using TestProj.Models;

namespace TestProj.DAL
{
    public class DbInitializer : DropCreateDatabaseIfModelChanges<EFContext>
    //public class DbInitializer : DropCreateDatabaseAlways<EFContext>
    {
        protected override void Seed(EFContext db)
        {
            GenerateApplicationUsers(db);

            var random = new Random();
            var companiesCount = random.Next(30, 50);
            for (int i = 0; i < companiesCount; i++)
                db.Companies.Add(new Company { Name = "Company" + random.Next(100, 200), Country = "Country" + random.Next(100, 200) });

            var endUsersCount = random.Next(5, 50);
            for (int i = 0; i < endUsersCount; i++)
                db.EndUsers.Add(new EndUser { Login = "EndUser" + random.Next(100, 200), Position = "Position" + random.Next(100, 200), DateOfBirth = new DateTime(random.Next(1950, 2000), random.Next(1, 12), random.Next(1, 28)) });

            var adminUsersCount = random.Next(5, 50);
            for (int i = 0; i < adminUsersCount; i++)
                db.AdminUsers.Add(new AdminUser { Login = "AdminUser" + random.Next(100, 200), Position = "Position" + random.Next(100, 200), DateOfBirth = new DateTime(random.Next(1950, 2000), random.Next(1, 12), random.Next(1, 28)) });

            
            //GenerateCompanies(db, random);
            //GenerateUsers<EndUser>(db.EndUsers, random);
            //GenerateUsers<AdminUser>(db.AdminUsers, random);


            base.Seed(db);
        }

        void GenerateUsers<T>(DbSet<T> db, Random random) where T : User, new()
        {
            var usersCount = random.Next(5, 50);
            for (int i = 0; i < usersCount; i++)
                db.Add(new T
                {
                    Login = "User" + random.Next(100, 500),
                    Position = "Position" + random.Next(100, 200),
                    DateOfBirth = new DateTime(random.Next(1950, 2000), random.Next(1, 12), random.Next(1, 28))
                });
        }
        void GenerateCompanies(EFContext db, Random random)
        {
            var companiesCount = random.Next(30, 50);
            for (int i = 0; i < companiesCount; i++)
                db.Companies.Add(new Company { Name = "Company" + random.Next(100, 200), Country = "Country" + random.Next(100, 200) });
        }
        void GenerateApplicationUsers(EFContext db)
        {
            db.Roles.Add(new Role { Id = 1, Name = "admin" });
            db.Roles.Add(new Role { Id = 2, Name = "user" });
            db.ApplicationUsers.Add(new ApplicationUser
            {
                Email = "admin@admin.com",
                Password = "123456",
                RoleId = 1
            });
            db.ApplicationUsers.Add(new ApplicationUser
            {
                Email = "user@user.com",
                Password = "123456",
                RoleId = 2
            });
        }
    }
}