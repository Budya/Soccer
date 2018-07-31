using SoccerId.Entities;
using SoccerId.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SoccerId.Repositories
{
    public class VisitLogRepository : BaseRepository<VisitLog>
    {
        public override VisitLog GetById(int id)
        {
            VisitLog result = null;
            using (SoccerIdDbContext context = new SoccerIdDbContext())
            {
                result = context.Visits.Where(t => t.Id == id).FirstOrDefault();
            }
            return result;
        }

        public override async Task<VisitLog> GetByIdAsync(int id)
        {
            using (SoccerIdDbContext context = new SoccerIdDbContext())
            {
                return await Task<VisitLog>.Factory.StartNew(() =>
                {
                    return context.Visits.Where(t => t.Id == id).FirstOrDefault();
                });
            }
        }

        public override void Remove(int id)
        {
            using (SoccerIdDbContext context = new SoccerIdDbContext())
            {
                var visit = context.Visits.FirstOrDefault(t => t.Id == id);
                context.Entry(visit).State = System.Data.Entity.EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public override async Task RemoveAsync(int id)
        {
            using (SoccerIdDbContext context = new SoccerIdDbContext())
            {
                var visits = context.Visits.FirstOrDefault(t => t.Id == id);
                context.Entry(visits).State = System.Data.Entity.EntityState.Deleted;
                await context.SaveChangesAsync();
            }
        }
    }
}