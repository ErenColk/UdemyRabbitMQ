﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UdemyRabbitMQWeb.ExcelCreate.Models;
using UdemyRabbitMQWeb.ExcelCreate.Services;

namespace UdemyRabbitMQWeb.ExcelCreate.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RabbitMQPublisher _rabbitMQPublisher;

        public ProductController(AppDbContext context, UserManager<IdentityUser> userManager,RabbitMQPublisher rabbitMQPublisher)
        {
            _context = context;
            _userManager = userManager;
            _rabbitMQPublisher = rabbitMQPublisher;
        }
        public IActionResult Index()
        {
            return View();
        }


        public async Task<IActionResult> CreateProductExcel()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name); // Giriş yapan kullanıcıyı buluyoruz.

            var fileName = $"product-excel-{Guid.NewGuid().ToString().Substring(1, 10)}";

            UserFile userFile = new()
            {
                UserId = user.Id,
                FileName = fileName,
                FileStatus = FileStatus.Creating
            };

            TempData["StartCreatingExcel"] = true;
            await _context.UserFiles.AddAsync(userFile);
            await _context.SaveChangesAsync();

            _rabbitMQPublisher.Publish(new Shared.CreateExcelMessage()
            {
                FileId = userFile.Id

            });

            return RedirectToAction(nameof(Files));


        }


        public async Task<IActionResult> Files()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);



            return View(await _context.UserFiles.Where(x=>x.UserId ==user.Id).OrderByDescending(x=>x.CreatedDate).ToListAsync());
        }




    }
}
