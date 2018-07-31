using SoccerId.Entities;
using SoccerId.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SoccerId.Repositories
{
    public class UserArchiveTeamRepository : BaseRepository<UserArchiveTeam>
    {
        public UserArchiveTeam GetById(int teamId, int userId)
        {
            UserArchiveTeam result = null;
            using (SoccerIdDbContext context = new SoccerIdDbContext())
            {
                result = context.UsersArchiveTeams.Where(t => t.ArchiveTeam_Id == teamId && t.User_Id == userId).FirstOrDefault();
            }
            return result;
        }

        public async Task<UserArchiveTeam> GetByIdAsync(int teamId, int userId)
        {

            using (SoccerIdDbContext context = new SoccerIdDbContext())
            {
                return await Task<UserArchiveTeam>.Factory.StartNew(() =>
                {
                    return context.UsersArchiveTeams.Where(t => t.ArchiveTeam_Id == teamId && t.User_Id == userId).FirstOrDefault();
                });
            }

        }

        public void Remove(int teamId, int userId)
        {
            using (SoccerIdDbContext context = new SoccerIdDbContext())
            {
                var userArchTeam = context.UsersArchiveTeams.FirstOrDefault(t => t.ArchiveTeam_Id == teamId && t.User_Id == userId);
                context.Entry(userArchTeam).State = System.Data.Entity.EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public async Task RemoveAsync(int teamId, int userId)
        {
            using (SoccerIdDbContext context = new SoccerIdDbContext())
            {
                var userArchTeam = context.UsersArchiveTeams.FirstOrDefault(t => t.ArchiveTeam_Id == teamId && t.User_Id == userId);
                context.Entry(userArchTeam).State = System.Data.Entity.EntityState.Deleted;
                await context.SaveChangesAsync();
            }
        }
    }
}