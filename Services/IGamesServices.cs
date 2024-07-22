using GameZone.Models;
using GameZone.ViewModels;

namespace GameZone.Services
{
    public interface IGamesServices
    {
        IEnumerable<Game> GetAllGames();

        Game? GetGameById(int id);

        Task Create(CreateGameFromViewModel model); // not retuen ang thing

        Task<Game?> Update(EditGameFormViewModel model); // return game after edited

        bool Delete (int id);
    }
}
