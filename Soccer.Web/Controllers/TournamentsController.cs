﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Soccer.Web.Data;
using Soccer.Web.Helpers;
using Soccer.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Soccer.Web.Controllers
{
    public class TournamentsController : Controller
    {
        private readonly DataContext _context;
        private readonly IConverterHelper _converterHelper;
        private readonly IImageHelper _imageHelper;

        public TournamentsController(
            DataContext context,
            IConverterHelper converterHelper,
            IImageHelper imageHelper)
        {
            _context = context;
            _converterHelper = converterHelper;
            _imageHelper = imageHelper;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context
                .Tournaments
                .Include(t => t.Groups)
                .OrderBy(t => t.StartDate)
                .ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TournamentViewModel model)
        {
            if (ModelState.IsValid)
            {
                string path = string.Empty;

                if (model.LogoFile != null)
                {
                    path = await _imageHelper.UploadImageAsync(model.LogoFile, "Tournaments");
                }

                var tournament = _converterHelper.ToTournamentEntity(model, path, true);
                _context.Add(tournament);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }
    }
}
