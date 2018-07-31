using SoccerId.Entities;
using SoccerId.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SoccerId.Repositories
{
    public class PhotoRepository<TClass> : BaseRepository<TClass> where TClass : Photo
    {
        public override TClass GetById(int id)
        {
            TClass result = null;
            using (SoccerIdDbContext context = new SoccerIdDbContext())
            {
                result = context.Set<TClass>().Where(t => t.Id == id).FirstOrDefault();
            }
            return result;
        }

        public override async Task<TClass> GetByIdAsync(int id)
        {

            using (SoccerIdDbContext context = new SoccerIdDbContext())
            {
                return await Task<TClass>.Factory.StartNew(() =>
                {
                    return context.Set<TClass>().Where(t => t.Id == id).FirstOrDefault();
                });
            }

        }

        public override void Remove(int id)
        {
            using (SoccerIdDbContext context = new SoccerIdDbContext())
            {
                var photo = context.Set<Photo>().FirstOrDefault(t => t.Id == id);
                context.Entry(photo).State = System.Data.Entity.EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public override async Task RemoveAsync(int id)
        {
            using (SoccerIdDbContext context = new SoccerIdDbContext())
            {
                var photo = context.Set<Photo>().FirstOrDefault(t => t.Id == id);
                context.Entry(photo).State = System.Data.Entity.EntityState.Deleted;
                await context.SaveChangesAsync();
            }
        }
    }
    public class TeamPhotoRepository : PhotoRepository<TeamPhoto> { }
    public class LeaguePhotoRepository : PhotoRepository<LeaguePhoto> { }
    public class UserPhotoRepository : PhotoRepository<UserPhoto> { }
    public class TeamLogoRepository : PhotoRepository<TeamLogo> { }
    public class LeagueLogoRepository : PhotoRepository<LeagueLogo> { }




}