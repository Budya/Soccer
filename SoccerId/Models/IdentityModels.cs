using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using SoccerId.Entities;

namespace SoccerId.Models
{
    public class SoccerIdDbContext : IdentityDbContext<User, AppRole,
    int, AppUserLogin, AppUserRole, AppUserClaim>
    {
        public SoccerIdDbContext() : base("SoccerIdDb") { }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultSchema("SoccerLeagues");
            modelBuilder.Entity<User>().ToTable("Users");
            //.Property(p => p.UserName)
            //.HasColumnName("FirstName");            
            //modelBuilder.Entity<User>().Ignore(c => c.AccessFailedCount)
            //                            .Ignore(c => c.LockoutEnabled)
            //                            .Ignore(c => c.LockoutEndDateUtc)
            //                            .Ignore(c => c.SecurityStamp)
            //                            .Ignore(c => c.EmailConfirmed)
            //                            .Ignore(c => c.PhoneNumberConfirmed)
            //                            .Ignore(c => c.TwoFactorEnabled);                                   



            modelBuilder.Entity<UserArchiveTeam>().ToTable("UsersArchiveTeams");
            modelBuilder.Entity<TeamLogo>().ToTable("TeamLogos");
            modelBuilder.Entity<LeagueLogo>().ToTable("LeagueLogos");
            modelBuilder.Entity<TeamPhoto>().ToTable("TeamPhoto");
            modelBuilder.Entity<LeaguePhoto>().ToTable("LeaguePhoto");
            modelBuilder.Entity<UserPhoto>().ToTable("UserPhoto");
            modelBuilder.Entity<AppRole>().ToTable("Roles");
            modelBuilder.Entity<AppUserRole>().ToTable("UserRoles");
            modelBuilder.Entity<AppUserClaim>().ToTable("UserClaims");
            //.Ignore(c => c.ClaimType)
            // .Ignore(c => c.ClaimValue);

            modelBuilder.Entity<AppUserLogin>().ToTable("UserLogins");


            modelBuilder.Entity<Chat>()
                        .HasMany<User>(a => a._Participants)
                        .WithMany(u => u._Chats)
                        .Map(au =>
                        {
                            au.MapLeftKey("User_Id");
                            au.MapRightKey("Chat_Id");
                            au.ToTable("UsersChats");
                        });

            modelBuilder.Entity<PrivateMessage>()
                        .HasMany<User>(a => a._Recipients)
                        .WithMany(u => u._PrivateMessages)
                        .Map(au =>
                        {
                            au.MapLeftKey("User_Id");
                            au.MapRightKey("PrivateMessage_Id");
                            au.ToTable("UsersPrivateMessages");
                        });


        }

        public static SoccerIdDbContext Create()
        {
            return new SoccerIdDbContext();
        }

        public DbSet<League> Leagues { get; set; }
        public DbSet<ArchiveTeam> ArchiveTeams { get; set; }
        public DbSet<UserArchiveTeam> UsersArchiveTeams { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<PlayingPosition> PlayingPositions { get; set; }
        public DbSet<TeamPhoto> TeamPhoto { get; set; }
        public DbSet<LeaguePhoto> LeaguePhoto { get; set; }
        public DbSet<UserPhoto> UserPhoto { get; set; }
        public DbSet<PrivateMessage> PrivateMessages { get; set; }
        public DbSet<ChatMessage> ChatMessages { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<VisitLog> Visits { get; set; }
        public DbSet<EventType> EventTypes { get; set; }
        public DbSet<EventPlace> EventPlaces { get; set; }
        public DbSet<TeamEvent> TeamEvents { get; set; }
        public DbSet<AgreedPlayersList> AgreedPlayersLists { get; set; }

        public System.Data.Entity.DbSet<SoccerId.Models.DeleteModel> DeleteModels { get; set; }
    }

    public class AppUserRole : IdentityUserRole<int> { }
    public class AppUserClaim : IdentityUserClaim<int> { }
    public class AppUserLogin : IdentityUserLogin<int> { }

    public class AppRole : IdentityRole<int, AppUserRole>
    {
        public AppRole() { }
        public AppRole(string name) { Name = name; }
    }

    public class AppUserStore : UserStore<User, AppRole, int,
        AppUserLogin, AppUserRole, AppUserClaim>
    {
        public AppUserStore(SoccerIdDbContext context)
            : base(context)
        {
        }
    }


    public class UserManager : UserManager<User, int>
    {

        public UserManager(AppUserStore store) :
            base(store)
        {

        }
        public static UserManager Create(IdentityFactoryOptions<UserManager> options,
                                                        IOwinContext context)
        {
            SoccerIdDbContext db = context.Get<SoccerIdDbContext>();
            UserManager manager = new UserManager(new AppUserStore(context.Get<SoccerIdDbContext>()));
            manager.UserValidator = new UserValidator<User, int>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };
            return manager;
        }

        public override Task<User> FindByIdAsync(int userId)
        {
            return base.FindByIdAsync(userId);
        }

        //internal Task<User> FindByIdAsync(string id)
        //{
        //    throw new NotImplementedException();
        //}
    }

    public class RoleManager : RoleManager<AppRole, int>
    {
        public RoleManager(RoleStore<AppRole, int, AppUserRole> store)
                    : base(store)
        { }
        public static RoleManager Create(IdentityFactoryOptions<RoleManager> options,
                                                IOwinContext context)
        {
            return new RoleManager(new
                     RoleStore<AppRole, int, AppUserRole>(context.Get<SoccerIdDbContext>()));
        }
    }

    public class SignInManager : SignInManager<User, int>
    {
        public SignInManager(UserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(User user)
        {
            return user.GenerateUserIdentityAsync((UserManager)UserManager);
        }

        public static SignInManager Create(IdentityFactoryOptions<SignInManager> options, IOwinContext context)
        {
            return new SignInManager(context.GetUserManager<UserManager>(), context.Authentication);
        }
    }

    public class SoccerIdDbInitializer : DropCreateDatabaseAlways<SoccerIdDbContext>
    {
        protected override void Seed(SoccerIdDbContext context)
        {
            IdentityResult res = new IdentityResult();
            context.Configuration.LazyLoadingEnabled = false;
            Random rnd = new Random();

            List<EventType> etList = new List<EventType>
            {
                new EventType{Name = "Чемпионат"},
                new EventType{Name = "Товарищеский матч"},
                new EventType{Name = "Тренировка"},
                new EventType{Name = "Кубок"},

            };
            context.EventTypes.AddRange(etList);

            List<EventPlace> epList = new List<EventPlace>
            {
                new EventPlace{Name = "СОК \"Олимпийский\"", Address = "ул.Сурганова 2а"},
                new EventPlace{Name = "БГАТУ", Address = "просп.Независимости 99"},
                new EventPlace{Name = "СК \"Университетский\"", Address = "ул.Октябрьская 8а"},
                new EventPlace{Name = "ФК Минск", Address = "ул.Mаяковского 127/3"}

            };

            context.EventPlaces.AddRange(epList);



            var teamList = new List<Team>
            {
                new Team{TeamName = "Газпромнефть"},
                new Team{TeamName = "Урожайная"},
                new Team{TeamName = "Сябар-Бертон Ли"},
                new Team{TeamName = "Блуграна"},
                new Team{TeamName = "Сандерленд"},
                new Team{TeamName = "КовчегСервис"},
                new Team{TeamName = "Виктория"},
                new Team{TeamName = "Дублин"},
                new Team{TeamName = "АРТ-старс"},
                new Team{TeamName = "Мозилла Фаерфокс"},
                new Team{TeamName = "РентМоторс"},
                new Team{TeamName = "Орел"},
                new Team{TeamName = "Маланка"},
                new Team{TeamName = "Фаворит"},
                new Team{TeamName = "Серебрянка"},
                new Team{TeamName = "Звезда ВВС"}
            };
            context.Teams.AddRange(teamList);

            var userManager = new UserManager(new AppUserStore(context));
            var roleManager = new RoleManager<AppRole, int>(new RoleStore<AppRole, int, AppUserRole>(context));

            var adminRole = new AppRole { Name = "admin" };
            var leagueManagerRole = new AppRole { Name = "leagueManager" };
            var teamManagerRole = new AppRole { Name = "teamManager" };
            var playerRole = new AppRole { Name = "player" };

            roleManager.Create(adminRole);
            roleManager.Create(leagueManagerRole);
            roleManager.Create(teamManagerRole);
            roleManager.Create(playerRole);
            string adminPassword = "P@ssword1";
            string leaguePassword = "P@ssword2";
            string teamPassword = "P@ssword3";
            string playerPassword = "P@ssword4";

            User admin = new User { UserName = "admin@mail.com", Email = "admin@mail.com" };
            res = userManager.Create(admin, adminPassword);
            if (res.Succeeded)
                userManager.AddToRole(admin.Id, adminRole.Name);

            User league = new User { UserName = "league@mail.com", Email = "league@mail.com" };
            res = userManager.Create(league, leaguePassword);
            if (res.Succeeded)
                userManager.AddToRole(league.Id, leagueManagerRole.Name);

            User team = new User { UserName = "team@mail.com", Email = "team@mail.com" };
            res = userManager.Create(team, teamPassword);
            if (res.Succeeded)
                userManager.AddToRole(team.Id, teamManagerRole.Name);

            User player = new User { UserName = "player1@mail.com", Email = "player1@mail.com" };
            res = userManager.Create(player, playerPassword);
            if (res.Succeeded)
                userManager.AddToRole(player.Id, playerRole.Name);

            //try
            //{
            //     res = userManager.Create(admin, adminPassword);
            //}

            //catch (DbEntityValidationException ex)
            //{
            //    foreach (var entityValidationErrors in ex.EntityValidationErrors)
            //    {
            //        foreach (var validationError in entityValidationErrors.ValidationErrors)
            //        {
            //            Debug.WriteLine("User Man Create--------------------------------------------Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
            //        }
            //    }
            //}

            //try
            //{
            //    if (res.Succeeded)
            //    {
            //        userManager.AddToRole(admin.Id, adminRole.Name);
            //    }
            //}

            //catch (DbEntityValidationException ex)
            //{
            //    foreach (var entityValidationErrors in ex.EntityValidationErrors)
            //    {
            //        foreach (var validationError in entityValidationErrors.ValidationErrors)
            //        {
            //            Debug.WriteLine("Add roles--------------------------------------------Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
            //        }
            //    }
            //}

            //// /////////////////////////////////////////////////////////////////////////////////////////////////////////

            //var positionList = new List<PlayingPosition> {
            //    new PlayingPosition { PositionFirstName = "Либеро", PositionLastName = "Свободный защитник" , Abbreviation="СЗ"},
            //    new PlayingPosition { PositionFirstName = "Центральный защитник", PositionLastName = "" , Abbreviation="ЦЗ"},
            //    new PlayingPosition { PositionFirstName = "Левый защитник", PositionLastName = "" , Abbreviation="ЛЗ"},
            //    new PlayingPosition { PositionFirstName = "Правый защитник", PositionLastName = "" , Abbreviation="ПЗ"},
            //    new PlayingPosition { PositionFirstName = "Левый латераль", PositionLastName = "Крайний левый защитник" , Abbreviation="КЛЗ"},
            //    new PlayingPosition { PositionFirstName = "Правый латераль", PositionLastName = "Крайний правый защитник" , Abbreviation="КПЗ"},
            //    new PlayingPosition { PositionFirstName = "Левый полузащитник", PositionLastName = "" , Abbreviation="ЛПЗ"},
            //    new PlayingPosition { PositionFirstName = "Правый Полузащитник", PositionLastName = "ППЗ" },
            //    new PlayingPosition { PositionFirstName = "Цетральный полузащитник", PositionLastName = "" , Abbreviation="ЦПЗ"},
            //    new PlayingPosition { PositionFirstName = "Опорный полузащитник", PositionLastName = "" , Abbreviation="ОПЗ"},
            //    new PlayingPosition { PositionFirstName = "Левый вингер", PositionLastName = "Левый атакующий полузащитник" , Abbreviation="ЛАПЗ"},
            //    new PlayingPosition { PositionFirstName = "Правый вингер", PositionLastName = "Правый атакующий полузащитник" , Abbreviation="ПАПЗ"},
            //    new PlayingPosition { PositionFirstName = "Форвард", PositionLastName = "Нападающий" , Abbreviation="НП"},
            //    new PlayingPosition { PositionFirstName = "Оттянутый форвард", PositionLastName = "Оттянутый нападающий" , Abbreviation="ОНП"},
            //    new PlayingPosition { PositionFirstName = "Атакующий полузащитник", PositionLastName = "" , Abbreviation="АПЗ"},
            //    new PlayingPosition { PositionFirstName = "Голкипер", PositionLastName = "Вратарь" , Abbreviation="ВРТ"}
            //};
            //context.PlayingPositions.AddRange(positionList);
            //context.SaveChanges();
            //context.PlayingPositions.Load();
            //var queryPos = from pos in context.PlayingPositions
            //               select pos;
            //List<PlayingPosition> positions = queryPos.ToList();
            //var leaguesList = new List<League> {
            //    new League{ Name = "Высшая лига"},
            //    new League{ Name = "Первая лига"},
            //    new League{ Name = "Вторая лига"}
            //};
            ////P@ssword3
            //context.Leagues.AddRange(leaguesList);
            //context.SaveChanges();

            //context.Leagues.Load();
            //var leagues = from league in context.Leagues
            //              select league;

            //leaguesList = leagues.ToList();



            ////Thread.Sleep(5000);
            //int teamCounter = 0;
            //int teamTemp = 0;
            //int playerCounter = 0;
            //int playerTemp = 0;
            //IdentityResult result = new IdentityResult();
            //foreach (var league in leaguesList)
            //{

            //    for (int i = teamCounter; i < teamCounter + 10; i++)
            //    {
            //        positions = queryPos.ToList();
            //        Team newTeam = new Team { TeamName = "Team_" + i, _CurrentLeague = league };
            //        ArchiveTeam newArchTeam = new ArchiveTeam { TeamName = newTeam.TeamName };
            //        context.Teams.Add(newTeam);
            //        context.ArchiveTeams.Add(newArchTeam);
            //        try
            //        {
            //            context.SaveChanges();
            //        }
            //        catch (DbEntityValidationException ex)
            //        {
            //            Debug.WriteLine(" Source>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>" + ex.Source);
            //            foreach (var entityValidationErrors in ex.EntityValidationErrors)
            //            {
            //                Debug.WriteLine("Error >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>" + entityValidationErrors.ToString());
            //                foreach (var validationError in entityValidationErrors.ValidationErrors)
            //                {
            //                    Debug.WriteLine("Save changes after archiveTeam added--------------------------------------------Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
            //                }
            //            }
            //        }
            //        int flag = 0;
            //        for (int j = playerCounter; j < playerCounter + 15; j++)
            //        {
            //            DateTime bd = new DateTime(rnd.Next(1980, 2002), rnd.Next(1, 12), rnd.Next(1, 28));
            //            User newUser = new User
            //            {
            //                UserName = "testUser?@mail.com".Replace("?", j.ToString()),
            //                Email = "testUser?@mail.com".Replace("?", j.ToString()),
            //                FirstName = "User_" + j + "_Name",
            //                LastName = "User_" + j + "_LastName",
            //                Birthday = bd
            //            };

            //            try
            //            {
            //                int id = 0;
            //                for (int k = 0; k < 2; k++)
            //                {
            //                    id = rnd.Next(1, 15);
            //                    PlayingPosition position = positions[id];
            //                    position._Players.Add(newUser);
            //                    newUser._PlayingPositions.Add(position);
            //                }
            //                result = userManager.Create(newUser, adminPassword);

            //            }
            //            catch (DbEntityValidationException ex)
            //            {
            //                foreach (var entityValidationErrors in ex.EntityValidationErrors)
            //                {
            //                    foreach (var validationError in entityValidationErrors.ValidationErrors)
            //                    {
            //                        Debug.WriteLine("User Man Create--------------------------------------------Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
            //                    }
            //                }
            //            }


            //            try
            //            {
            //                if (result.Succeeded)
            //                {

            //                    if (flag == 0)
            //                    {
            //                        userManager.AddToRole(newUser.Id, playerRole.Name);
            //                        userManager.AddToRole(newUser.Id, teamManagerRole.Name);
            //                        newTeam._TeamManager = newUser;
            //                    }
            //                    else
            //                    {
            //                        userManager.AddToRole(newUser.Id, playerRole.Name);
            //                    }
            //                }

            //            }
            //            catch (DbEntityValidationException ex)
            //            {
            //                foreach (var entityValidationErrors in ex.EntityValidationErrors)
            //                {
            //                    foreach (var validationError in entityValidationErrors.ValidationErrors)
            //                    {
            //                        Debug.WriteLine("Add roles--------------------------------------------Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
            //                    }
            //                }
            //            }
            //            flag++;
            //            newUser._CurrentTeam = newTeam;
            //            newTeam._Players.Add(newUser);



            //            int addedId = context.Entry(newArchTeam).Entity.Id;
            //            UserArchiveTeam newUserArchTeam = new UserArchiveTeam { _ArchiveTeam = newArchTeam, _User = newUser, User_Id = newUser.Id, ArchiveTeam_Id = addedId };
            //            newArchTeam.UserArchiveTeam.Add(newUserArchTeam);
            //            context.UsersArchiveTeams.Add(newUserArchTeam);

            //            try
            //            {
            //                context.SaveChanges();
            //            }
            //            catch (DbEntityValidationException ex)
            //            {
            //                Debug.WriteLine(" Source>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>" + ex.Source);
            //                foreach (var entityValidationErrors in ex.EntityValidationErrors)
            //                {
            //                    Debug.WriteLine("Error >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>" + entityValidationErrors.ToString());
            //                    foreach (var validationError in entityValidationErrors.ValidationErrors)
            //                    {
            //                        Debug.WriteLine("Finale Save changes--------------------------------------------Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
            //                    }
            //                }
            //            }
            //            playerTemp = j;

            //            Debug.WriteLine($">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>User_{j} added");


            //        }
            //        teamTemp = i;
            //        playerCounter = playerTemp + 1;
            //        Debug.WriteLine($">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>Team_{i} added");

            //        //Thread.Sleep(500);
            //    }
            //    teamCounter = teamTemp + 1;
            //}





            // //// ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            //User testUser = new User { UserName = "TestUser", Email = "testUser@mail.com" };
            //Team testTeam = new Team { TeamName = "TestTeam", _CurrentLeague = leagues.First() };



            //try
            //{
            //    result = userManager.Create(testUser, adminPassword);


            //}
            //catch (DbEntityValidationException ex)
            //{
            //    foreach (var entityValidationErrors in ex.EntityValidationErrors)
            //    {
            //        foreach (var validationError in entityValidationErrors.ValidationErrors)
            //        {
            //            Debug.WriteLine("User Man Create--------------------------------------------Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
            //        }
            //    }
            //}


            //try
            //{
            //    if (result.Succeeded)
            //    {
            //        userManager.AddToRole(testUser.Id, playerRole.Name);
            //    }

            //}
            //catch (DbEntityValidationException ex)
            //{
            //    foreach (var entityValidationErrors in ex.EntityValidationErrors)
            //    {
            //        foreach (var validationError in entityValidationErrors.ValidationErrors)
            //        {
            //            Debug.WriteLine("Add roles--------------------------------------------Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
            //        }
            //    }
            //}

            //testUser._CurrentTeam = testTeam;
            //testTeam._Players.Add(testUser);
            //testTeam._TeamManager = testUser;
            //ArchiveTeam testArchTeam = new ArchiveTeam { TeamName = testTeam.TeamName };
            //context.ArchiveTeams.Add(testArchTeam);
            //context.SaveChanges();
            //int addedId = context.Entry(testArchTeam).Entity.Id;
            //UserArchiveTeam testUserArchTeam = new UserArchiveTeam { _ArchiveTeam = testArchTeam, _User = testUser, User_Id = testUser.Id, ArchiveTeam_Id = addedId };
            //testArchTeam.UserArchiveTeam.Add(testUserArchTeam);
            //context.UsersArchiveTeams.Add(testUserArchTeam);


            //try
            //{
            //    context.SaveChanges();
            //}
            //catch (DbEntityValidationException ex)
            //{
            //    Debug.WriteLine(" Source>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>" + ex.Source);
            //    foreach (var entityValidationErrors in ex.EntityValidationErrors)
            //    {
            //        Debug.WriteLine("Error >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>" + entityValidationErrors.ToString());
            //        foreach (var validationError in entityValidationErrors.ValidationErrors)
            //        {
            //            Debug.WriteLine("Save changes--------------------------------------------Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
            //        }
            //    }
            //}



            //context.SaveChanges();
            base.Seed(context);
        }
    }
}