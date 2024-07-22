using GameZone.Models;
using GameZone.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace GameZone.Services
{
    public class GamesServices : IGamesServices
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment; // to add the photo 
        private readonly string _imagePath;
        public GamesServices(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _imagePath = $"{_webHostEnvironment.WebRootPath}{ImageLocation.ImagePath}";  // location i need image saved
        }

        public IEnumerable<Game> GetAllGames()
        {
            var games = _context.Games.Include(g => g.Category)
                .Include(g => g.Devices)
                .ThenInclude(d => d.Device).
                AsNoTracking().ToList(); // (g=>g.Category) => to send my data to view to access 
            return games;
        }

        public Game? GetGameById(int id)
        {
            return _context.Games.Include(g => g.Category)
                .Include(g => g.Devices)
                .ThenInclude(d => d.Device).
                AsNoTracking().SingleOrDefault(g => g.id == id); // (g=>g.Category) => to send my data to view to access 

        }


        public async Task Create(CreateGameFromViewModel model)
        {
            // to get the path for img 
            var coverName = await SaveCover(model.Cover);
            // stream.Dispose(); // save cover in server

            Game game = new() // because interface for this method have no return
            {
                Name = model.Name,
                Description = model.Description,
                CategoryId = model.CategoryId,
                Cover = coverName,
                Devices = model.SelectedDevices.Select(d => new GameDevice { DeviceId = d }).ToList(), //projection to change the the type of selectedDevices to list of domain model to GameDevice 
            };

            _context.Add(game);
            _context.SaveChanges();

        }

		public async Task<Game?> Update(EditGameFormViewModel model)
		{
			// select the game from database to check the update 
			var game = _context.Games
                .Include(g => g.Devices)
                .SingleOrDefault(g => g.id == model.id);   
            
         
            if (game is null)
                return null;

            var hasNewCover = model.Cover is not null;    // is mean the user update the model cover 
            var oldCover = game.Cover;

            game.Name = model.Name;
            game.Description = model.Description;
            game.CategoryId = model.CategoryId;
            game.Devices = model.SelectedDevices.Select(d => new GameDevice { DeviceId = d }).ToList();

            if(hasNewCover)
            {
                game.Cover = await SaveCover(model.Cover!);
            }

            var effectedRows = _context.SaveChanges();

            if(effectedRows > 0)
            {
                if(hasNewCover)
                {
                    var cover = Path.Combine(_imagePath, oldCover);
                    File.Delete(cover);
                }
                return game;
            }
            else
            {
				var cover = Path.Combine(_imagePath, game.Cover); // new cover
				File.Delete(cover);

				return null;
            }
		}


		public bool Delete(int id)
		{
            var isDeleted = false;

            var game = _context.Games.Find(id);
           
            if (game is null)
                return false;

            _context.Games.Remove(game);

            var effectedRows = _context.SaveChanges();

            if (effectedRows > 0) 
            {
                isDeleted = true;

                var cover = Path.Combine(_imagePath,game.Cover);
                File.Delete(cover);

            }


            return isDeleted;
		}


		private async Task<string> SaveCover(IFormFile cover)
        {
			// to get the path for img 
			var coverName = $"{Guid.NewGuid()}{Path.GetExtension(cover.FileName)}";

			var path = Path.Combine(_imagePath, coverName);// المكان الي هيخزن جواه الصوره و اسم الصوره

			using var stream = File.Create(path);
			await cover.CopyToAsync(stream);

            return coverName;
		}

	
	}





}
