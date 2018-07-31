using SoccerId.Entities;
using SoccerId.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SoccerId.Repositories
{
    public class LeagueRepository : BaseRepository<League>
    {
        public LeagueRepository()
        {

        }       

        public override void Remove(int id)
        {
            using (SoccerIdDbContext context = new SoccerIdDbContext())
            {
                var league = context.Leagues.FirstOrDefault(t => t.Id == id);
                context.Entry(league).State = System.Data.Entity.EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public override async Task RemoveAsync(int id)
        {
            using (SoccerIdDbContext context = new SoccerIdDbContext())
            {
                var league = context.Leagues.FirstOrDefault(t => t.Id == id);
                context.Entry(league).State = System.Data.Entity.EntityState.Deleted;
                await context.SaveChangesAsync();
            }
        }      

        public override League GetById(int id)
        {
            League result = null;
            using (SoccerIdDbContext context = new SoccerIdDbContext())
            {
                result = context.Leagues.Where(t => t.Id == id).FirstOrDefault();
            }
            return result;
        }

        public override async Task<League> GetByIdAsync(int id)
        {
            League result = null;
            using (SoccerIdDbContext context = new SoccerIdDbContext())
            {
                result = await context.Leagues.Where(t => t.Id == id).FirstOrDefaultAsync();
            }
            return result;
        }

       
    }
}