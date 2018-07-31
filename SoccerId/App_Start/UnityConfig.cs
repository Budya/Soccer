using SoccerId.Entities;
using SoccerId.Models;
using SoccerId.Repositories;
using System;
using System.Data.Entity;
using Unity;

namespace SoccerId
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public static class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container =
          new Lazy<IUnityContainer>(() =>
          {
              var container = new UnityContainer();
              RegisterTypes(container);
              return container;
          });

        /// <summary>
        /// Configured Unity Container.
        /// </summary>
        public static IUnityContainer Container => container.Value;
        #endregion

        /// <summary>
        /// Registers the type mappings with the Unity container.
        /// </summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>
        /// There is no need to register concrete types such as controllers or
        /// API controllers (unless you want to change the defaults), as Unity
        /// allows resolving a concrete type even if it was not previously
        /// registered.
        /// </remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below.
            // Make sure to add a Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            // TODO: Register your type's mappings here.
            container.RegisterType<DbContext, SoccerIdDbContext>();          
            
            container.RegisterType<BaseRepository<Team>, TeamRepository>();
            container.RegisterType<BaseRepository<User>, UserRepository>();
            container.RegisterType<BaseRepository<League>, LeagueRepository>();
            container.RegisterType<BaseRepository<VisitLog>, VisitLogRepository>();
            container.RegisterType<BaseRepository<UserArchiveTeam>, UserArchiveTeamRepository>();
            container.RegisterType<BaseRepository<ArchiveTeam>, ArchiveTeamRepository>();
            container.RegisterType<BaseRepository<TeamEvent>, TeamEventRepository>();
            container.RegisterType<BaseRepository<PlayingPosition>, PlayingPositionRepository>();
            container.RegisterType<BaseRepository<UserPhoto>, UserPhotoRepository>();
            container.RegisterType<BaseRepository<TeamPhoto>, TeamPhotoRepository>();
            container.RegisterType<BaseRepository<LeaguePhoto>, LeaguePhotoRepository>();
            container.RegisterType<BaseRepository<LeagueLogo>, LeagueLogoRepository>();
            container.RegisterType<BaseRepository<TeamLogo>, TeamLogoRepository>();
            container.RegisterType<BaseRepository<ChatMessage>, ChatMessageRepository>();
            container.RegisterType<BaseRepository<PrivateMessage>, PrivateMessageRepository>();
            container.RegisterType<BaseRepository<Chat>, ChatRepository>();
            container.RegisterType<BaseRepository<EventType>, EventTypeRepository>();
            container.RegisterType<BaseRepository<EventPlace>, EventPlaceRepository>();
            container.RegisterType<BaseRepository<AgreedPlayersList>, AgreedPlayersListRepository>();


        }
    }
}