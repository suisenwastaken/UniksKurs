using System;
using l4Razor.Model;
using Microsoft.EntityFrameworkCore;

namespace l4Razor.Data
{
    public class NewDbContext : DbContext
    {

        public NewDbContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Role userRole = new Role {Id = 1, Name = "User"};
            Role adminRole = new Role {Id = 2, Name = "Admin"};
            User admin = new User {Id = 1, UserName = "admin", FirstName ="Админ", Password = "AGAgAQkbX4P9dtmy44icZV8e+KctLNsp55VoGJsxl0PfeJjLRERvtEMxZ+plohHlOw==", RoleId = adminRole.Id, PictureUrl = "https://api.dicebear.com/6.x/big-smile/svg?seed=Miss%20kitty&scale=110&radius=50&accessories=sunglasses&accessoriesProbability=100&eyes=starstruck&hair=wavyBob&hairColor=71472d&mouth=kawaii&skinColor=efcc9f&backgroundColor=c0aede"};

            Institute IMO = new Institute {Id = 1, InstituteName = "ИМО"};
            Institute ICMIIT = new Institute {Id = 2, InstituteName = "ИВМиИТ"};
            Institute IF = new Institute {Id = 3, InstituteName = "ИФ"};
            Institute HI = new Institute {Id = 4, InstituteName = "ХИ"};

            CollectiveTheme Theater = new CollectiveTheme {Id = 1, ThemeName = "Театр"};
            CollectiveTheme Vocal = new CollectiveTheme {Id = 2, ThemeName = "Вокал"};
            CollectiveTheme Dance = new CollectiveTheme {Id = 3, ThemeName = "Танцы"};
            CollectiveTheme Instrumental = new CollectiveTheme {Id = 4, ThemeName = "Инструментал"};

            Collective Fenty = new Collective {Id = 1, CollectiveName = "Фенти",InstituteOfCollectiveId = IMO.Id, ThemeOfCollectiveId = Dance.Id, CollevtiveDescription = "Ну мы любим потанцевать", CollectiveLeaderContuct = "@pipipupu"};
            Collective Club = new Collective {Id = 2, CollectiveName = "Полторы Комнаты", InstituteOfCollectiveId = ICMIIT.Id, ThemeOfCollectiveId = Theater.Id, CollevtiveDescription = "Ну мы любим поиграть", CollectiveLeaderContuct = "@suisenwastaken"};

            Tag SV = new Tag {Id = 1, TagName = "СВ"};
            Tag DP = new Tag {Id = 2, TagName = "ДП"};

            modelBuilder.Entity<Institute>().HasData(IMO, ICMIIT, IF, HI);
            modelBuilder.Entity<CollectiveTheme>().HasData(Theater, Vocal, Dance, Instrumental);
            modelBuilder.Entity<Role>().HasData(adminRole, userRole);
            modelBuilder.Entity<User>().HasData(admin);
            modelBuilder.Entity<Collective>().HasData(Fenty, Club);
            modelBuilder.Entity<Tag>().HasData(SV,DP);
        }
        
        public DbSet<ContuctUsMessage> MessageList { get; set; }
        
        public DbSet<New> News { get; set; }
        public DbSet<Tag> TagsList { get; set; }
        
        public DbSet<User> UserList { get; set; }
        public DbSet<Role> RoleList { get; set; }
        
        public DbSet<CollectiveTheme> CollectiveThemeList { get; set; }
        public DbSet<Institute> InstituteList { get; set; }
        public DbSet<Collective> CollectiveList { get; set; }

    }
}