﻿using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GameZone.Services
{
    public class CategoriesService : ICategoriesService
    {
        private readonly ApplicationDbContext _context;

        public CategoriesService(ApplicationDbContext context)
        {
            _context = context;
        }


        public IEnumerable<SelectListItem> GetSelectList()
        {
            return _context
                .Categories.Select(c => new SelectListItem { Value = c.id.ToString(), Text = c.Name })
                .OrderBy(c => c.Text) // to display in site as a alphabitic
                .AsNoTracking()
                .ToList();
        }
    }
}
