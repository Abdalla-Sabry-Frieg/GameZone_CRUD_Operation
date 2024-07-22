using Microsoft.AspNetCore.Mvc.Rendering;

namespace GameZone.Services
{
    public class DevicesServices : IDevicesServices
    {
        private readonly ApplicationDbContext _context;

        public DevicesServices(ApplicationDbContext context)
        {
            _context = context;
        }
        public IEnumerable<SelectListItem> GetSelectList()
        {
            return _context
                .Devices.Select(d => new SelectListItem { Value = d.id.ToString(), Text = d.Name })
                .OrderBy(d => d.Text) // to display in site as a alphabitic
                .AsNoTracking()
                .ToList();
        }
    }
}
