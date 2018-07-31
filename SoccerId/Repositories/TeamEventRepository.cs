using SoccerId.Entities;
using SoccerId.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SoccerId.Repositories
{
    public class TeamEventRepository:BaseRepository<TeamEvent>
    {
        public override TeamEvent GetById(int id)
        {
            TeamEvent result = null;
            using (SoccerIdDbContext context = new SoccerIdDbContext())
            {
                result = context.TeamEvents.Where(t => t.Id == id).FirstOrDefault();
            }
            return result;
        }

        public override async Task<TeamEvent> GetByIdAsync(int id)
        {

            using (SoccerIdDbContext context = new SoccerIdDbContext())
            {
                return await Task<TeamEvent>.Factory.StartNew(() =>
                {
                    return context.TeamEvents.Where(t => t.Id == id).FirstOrDefault();
                });
            }

        }

        public override void Remove(int id)
        {
            using (SoccerIdDbContext context = new SoccerIdDbContext())
            {
                var tEvent = context.TeamEvents.FirstOrDefault(t => t.Id == id);
                context.Entry(tEvent).State = System.Data.Entity.EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public override async Task RemoveAsync(int id)
        {
            using (SoccerIdDbContext context = new SoccerIdDbContext())
            {
                var tEvent = context.TeamEvents.FirstOrDefault(t => t.Id == id);
                context.Entry(tEvent).State = System.Data.Entity.EntityState.Deleted;
                await context.SaveChangesAsync();
            }
        }
    }
}