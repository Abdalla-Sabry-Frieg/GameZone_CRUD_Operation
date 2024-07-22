using GameZone.ViewModels;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GameZone.Controllers
{
    public class GamesController : Controller
    {
       
        private readonly ICategoriesService _CategoriesService;
        private readonly IDevicesServices _DevicesService;
		private readonly IGamesServices _gamesServices;


		public GamesController( ICategoriesService categoriesService, IDevicesServices devicesService, IGamesServices gamesServices)
		{
			
			_CategoriesService = categoriesService;
			_DevicesService = devicesService;
			_gamesServices = gamesServices;
		}

        public IActionResult Index()
        {
            var games = _gamesServices.GetAllGames();
            return View(games);
        }

        public IActionResult Details(int id)
        {
            var game = _gamesServices.GetGameById(id);
            if (game is null) 
                return NotFound();

            return View(game);
        }



        [HttpGet]
        public IActionResult Create() //Add view 
        {

            CreateGameFromViewModel viewModel = new()
            {
                Categories = _CategoriesService.GetSelectList(),

                Devices = _DevicesService.GetSelectList()
			};
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // if token is wrong will stop the request
        public async Task<IActionResult> Create(CreateGameFromViewModel model)
        {
            // intialization the data from database 

			//1- Servre side validation

			if (!ModelState.IsValid)
            {
				model.Categories = _CategoriesService.GetSelectList();

				model.Devices = _DevicesService.GetSelectList();

				return View(model);
            }

            //save game to database 
            //save cover to database 

            await _gamesServices.Create(model);

            return RedirectToAction(nameof(Index)); // action , controller // when bug in server the view will take the page to spasific page with name and controller
        }


        // To add the update controller 


        [HttpGet]
        public IActionResult Edit(int id) 
        {
            var game = _gamesServices.GetGameById(id);
            if (game is null)
                return NotFound();

            EditGameFormViewModel editGameFormViewModel = new()
            {
                id = id,
                Name=game.Name,
                Description=game.Description,
                CategoryId=game.CategoryId, 
                SelectedDevices=game.Devices.Select(d=>d.DeviceId).ToList(),
                Categories=_CategoriesService.GetSelectList(),
                Devices=_DevicesService.GetSelectList(),
                currentCover=game.Cover,
            };
            return View(editGameFormViewModel); 
        }

		[HttpPost]
		[ValidateAntiForgeryToken] // if token is wrong will stop the request
		public async Task<IActionResult> Edit(EditGameFormViewModel model)
		{
			// intialization the data from database 


			//1- Servre side validation

			if (!ModelState.IsValid)
			{
				model.Categories = _CategoriesService.GetSelectList();

				model.Devices = _DevicesService.GetSelectList(); 

				return View(model);
			}

            //save game to database 
            //save cover to database 

            var game = await _gamesServices.Update(model);

            if (game is null)
                return BadRequest();

			return RedirectToAction(nameof(Index)); // action , controller // when bug in server the view will take the page to spasific page with name and controller
		}

        [HttpDelete]
        public IActionResult Delete (int id)
        {
            var isDeleted = _gamesServices.Delete(id);


            return isDeleted ? Ok() : BadRequest();
        }
	}
}
