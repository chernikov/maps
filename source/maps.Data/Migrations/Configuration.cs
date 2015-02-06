namespace maps.Data.Migrations
{
    using maps.Data.Entities;
    using maps.Data.Logging;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure.Interception;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MapsContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            DbInterception.Add(new LoggingInterceptor());
        }

        protected override void Seed(MapsContext context)
        {
            var cities = new List<City>()
            {
                new City()
                {
                    Name = "Івано-Франківськ",
                    CenterLat = 48.9227,
                    CenterLng = 24.7104,
                    Zoom = 14
                }
            };
            cities.ForEach(s => context.Cities.AddOrUpdate(p => p.Name, s));
            context.SaveChanges();

            var roles = new List<Role>() {
                new Role()
                {
                    Code = "Admin",
                    Name = "Админ"
                },
                new Role()
                {
                    Code = "Moderator",
                    Name = "Модератор"
                }
            };
            roles.ForEach(s => context.Roles.AddOrUpdate(p => p.Name, s));
            context.SaveChanges();

            var users = new List<User>() {
                new User()
                {
                    AddedDate = DateTime.Now,
                    LastVisitDate = DateTime.Now,
                    AvatarPath = "",
                    CityID = context.Cities.Single().ID,
                    Email = "admin@maps.if.ua",
                    FirstName = "Admin",
                    LastName = "Admin",
                    Password = "admin",
                    Login = "admin",
                }
            };

            users.ForEach(s => context.Users.AddOrUpdate(p => p.Login, s));
            context.SaveChanges();
            AddOrUpdateUserRole(context, "admin", "Admin");
        }

        private void AddOrUpdateUserRole(MapsContext context, string login, string roleCode)
        {
            var user = context.Users.SingleOrDefault(c => c.Login == login);
            var inst = user.UserRoles.SingleOrDefault(i => i.Role.Code == roleCode);
            if (inst == null)
            {
                user.UserRoles = new HashSet<UserRole>();
                user.UserRoles.Add(new UserRole()
                {
                    Role = context.Roles.Single(r => r.Code == roleCode),
                    User = user
             
                });
                context.SaveChanges();
            }
                  
        }
    }
}
