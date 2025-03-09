using JwtSecuredNet8API.Model;

namespace JwtSecuredNet8API.Services
{
    public interface IOurHeroService
    {
        Task<List<OurHero>> GetAllHeros(bool? isActive);

        Task<OurHero?> GetHerosByIDAsync(int id);

        Task<OurHero> AddOurHero(AddUpdateOurHero obj);

        Task<OurHero?> UpdateOurHero(int id, AddUpdateOurHero obj);

        Task<bool> DeleteHerosByID(int id);
    }
}
