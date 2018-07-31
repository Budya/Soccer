using SoccerId.Entities;
using SoccerId.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SoccerId.Repositories
{
    public class EventTypeRepository : BaseRepository<EventType>
    {
        public override EventType GetById(int id)
        {
            EventType result = null;
            using (SoccerIdDbContext context = new SoccerIdDbContext())
            {
                result = context.EventTypes.Where(t => t.Id == id).FirstOrDefault();
            }
            return result;
        }

        public override async Task<EventType> GetByIdAsync(int id)
        {

            using (SoccerIdDbContext context = new SoccerIdDbContext())
            {
                return await Task<EventType>.Factory.StartNew(() =>
                {
                    return context.EventTypes.Where(t => t.Id == id).FirstOrDefault();
                });
            }

        }

        public override void Remove(int id)
        {
            using (SoccerIdDbContext context = new SoccerIdDbContext())
            {
                var eventType = context.EventTypes.FirstOrDefault(t => t.Id == id);
                context.Entry(eventType).State = System.Data.Entity.EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public override async Task RemoveAsync(int id)
        {
            using (SoccerIdDbContext context = new SoccerIdDbContext())
            {
                var eventType = context.EventTypes.FirstOrDefault(t => t.Id == id);
                context.Entry(eventType).State = System.Data.Entity.EntityState.Deleted;
                await context.SaveChangesAsync();
            }
        }
    }
}