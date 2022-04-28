using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NLP.Enums;
using NLP.Models;
using X.PagedList;

namespace NLP.Controllers
{
    public class DictionaryController : Controller
    {
        private readonly AppDbContext context;

        public DictionaryController(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<IActionResult> Index(int? page, int? type)
        {
            var words = await context.Words.Where(x => !type.HasValue || (int)x.Type == type).ToListAsync();
            int pageSize = 6;
            int pageNumber = (page ?? 1);

            ViewBag.WordTypes = Helpers.Helpers.EnumToList(typeof(WordType));
            ViewBag.CurrentType = type;

            return View(words.OrderByDescending(x => x.Id).ToPagedList(pageNumber, pageSize));
        }

        [HttpGet]
        public async Task<IActionResult> Add(string msg, string newWord)
        {
            Word word = new();
            ViewBag.WordTypes = Helpers.Helpers.EnumToList(typeof(WordType));

            if (!string.IsNullOrEmpty(newWord))
            {
                word.Text = newWord;
            }

            if (!string.IsNullOrEmpty(msg))
            {
                ViewBag.Message = msg;
            }

            return View(word);
        }

        [HttpPost]
        public async Task<IActionResult> Add(Word word)
        {
            if (ModelState.IsValid)
            {
                context.Words.Add(word);
                await context.SaveChangesAsync();

                return RedirectToAction("Add", new { msg = "تمت الإضافة بنجاح." });
            }

            ViewBag.WordTypes = Helpers.Helpers.EnumToList(typeof(WordType));
            return View(word);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            Word? word = await context.Words.Where(x => x.Id == id).SingleOrDefaultAsync();

            if (word == null)
            {
                return View("Error");
            }

            context.Words.Remove(word);
            await context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
