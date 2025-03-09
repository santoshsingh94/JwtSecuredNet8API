using JwtSecuredNet8API.Entity;
using JwtSecuredNet8API.Model;
using Microsoft.EntityFrameworkCore;

namespace JwtSecuredNet8API.Services
{
    public class OurHeroService : IOurHeroService
    {
        private readonly OurHeroDbContext _dbContext;
        public OurHeroService(OurHeroDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<OurHero>> GetAllHeros(bool? isActive)
        {
            if (isActive == null)
            {
                return await _dbContext.OurHeros.ToListAsync();
            }
            return await _dbContext.OurHeros.Where(obj => obj.isActive == isActive).ToListAsync();
        }

        public async Task<OurHero?> GetHerosByIDAsync(int id)
        {
            return await _dbContext.OurHeros.FirstOrDefaultAsync(hero => hero.Id == id);
        }

        public async Task<OurHero?> AddOurHero(AddUpdateOurHero obj)
        {
            var addHero = new OurHero()
            {
                FirstName = obj.FirstName,
                LastName = obj.LastName,
                isActive = obj.isActive,
            };

            _dbContext.OurHeros.Add(addHero);
            var result = await _dbContext.SaveChangesAsync();
            return result >= 0 ? addHero : null;
        }

        public async Task<OurHero?> UpdateOurHero(int id, AddUpdateOurHero obj)
        {
            var hero = await _dbContext.OurHeros.FirstOrDefaultAsync(index => index.Id == id);
            if (hero != null)
            {
                hero.FirstName = obj.FirstName;
                hero.LastName = obj.LastName;
                hero.isActive = obj.isActive;

                var result = await _dbContext.SaveChangesAsync();
                return result >= 0 ? hero : null;
            }
            return null;
        }

        public async Task<bool> DeleteHerosByID(int id)
        {
            var hero = await _dbContext.OurHeros.FirstOrDefaultAsync(index => index.Id == id);
            if (hero != null)
            {
                _dbContext.OurHeros.Remove(hero);
                var result = await _dbContext.SaveChangesAsync();
                return result >= 0;
            }
            return false;
        }
    }
}
