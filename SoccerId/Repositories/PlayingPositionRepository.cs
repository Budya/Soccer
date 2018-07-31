using SoccerId.Entities;
using SoccerId.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SoccerId.Repositories
{
    public class PlayingPositionRepository : BaseRepository<PlayingPosition>
    {
        public override PlayingPosition GetById(int id)
        {
            PlayingPosition result = null;
            using (SoccerIdDbContext context = new SoccerIdDbContext())
            {
                result = context.PlayingPositions.Where(t => t.Id == id).FirstOrDefault();
            }
            return result;
        }

        public override async Task<PlayingPosition> GetByIdAsync(int id)
        {

            using (SoccerIdDbContext context = new SoccerIdDbContext())
            {
                return await Task<PlayingPosition>.Factory.StartNew(() =>
                {
                    return context.PlayingPositions.Where(t => t.Id == id).FirstOrDefault();
                });
            }

        }

        public override void Remove(int id)
        {
            using (SoccerIdDbContext context = new SoccerIdDbContext())
            {
                var playingPosition = context.PlayingPositions.FirstOrDefault(t => t.Id == id);
                context.Entry(playingPosition).State = System.Data.Entity.EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public override async Task RemoveAsync(int id)
        {
            using (SoccerIdDbContext context = new SoccerIdDbContext())
            {
                var playingPosition = context.PlayingPositions.FirstOrDefault(t => t.Id == id);
                context.Entry(playingPosition).State = System.Data.Entity.EntityState.Deleted;
                await context.SaveChangesAsync();
            }
        }
    }
}