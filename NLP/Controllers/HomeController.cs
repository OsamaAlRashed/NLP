using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NLP.Enums;
using NLP.Models;
using NLP.Services;
using NLP.ViewModels;
using System.Diagnostics;
using X.PagedList;

namespace NLP.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MorphoLogicalService morphoLogicalService;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly AppDbContext context;

        public HomeController(ILogger<HomeController> logger, MorphoLogicalService morphoLogicalService, IWebHostEnvironment webHostEnvironment, AppDbContext context)
        {
            _logger = logger;
            this.morphoLogicalService = morphoLogicalService;
            this.webHostEnvironment = webHostEnvironment;
            this.context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        #region MorphoLogical
        public async Task<IActionResult> MorphoLogical()
        {
            return View(); 
        }

        public async Task<IActionResult> ExcuteMorphoLogical(string input)
        {
            var output = await morphoLogicalService.Excute(input);
            return Json(output);
        }
        #endregion

        #region Audio
        public async Task<IActionResult> AudioAnalyzer(int? page, int? type)
        {
            var data = await context.AudioFeatures.Where(x => !type.HasValue || (int)x.ForType == type).ToListAsync();

            int pageSize = 6;
            int pageNumber = (page ?? 1);

            ViewBag.AudioForTypes = Helpers.Helpers.EnumToList(typeof(AudioForType));
            ViewBag.CurrentType = type;

            return View(data.OrderByDescending(x => x.Id).ToPagedList(pageNumber, pageSize));
        }
        
        [HttpGet]
        public async Task<IActionResult> Microphone()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Microphone(AudioFeatureVM vm)
        {
            var audio = await context.AudioFeatures
                .Where(x => Math.Abs(x.RMS - vm.RMS) <= 0.1
                         && Math.Abs(x.ZCR - vm.ZCR) <= 10
                         && Math.Abs(x.Energy - vm.Energy) <= 3)
                .FirstOrDefaultAsync();

            dynamic result = audio == null ? new
            {   
                status = false
            } : new
            {
                status = true,
                data = audio.ForType.ToString()
            };

            return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> UploadAudio()
        {
            ViewBag.AudioForTypes = Helpers.Helpers.EnumToList(typeof(AudioForType));

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UploadAudio(AudioFileVM vm)
        {
            var path = Helpers.Helpers.TryUploadFile(vm.File, vm.ForType.ToString(), webHostEnvironment.WebRootPath);
            var commandResult = Helpers.Helpers.ExecuteCommandSync(@"meyda source\repos\NLP\NLP\wwwroot\" + path + " zcr rms energy");
            if (!string.IsNullOrEmpty(commandResult) && commandResult.Contains("Average zcr:"))
            {
                string zcr = commandResult.Split("Average zcr: ")[1].Split("\n")[0];
                string rms = commandResult.Split("Average rms: ")[1].Split("\n")[0];
                string energy = commandResult.Split("Average energy: ")[1].Split("\n")[0];

                double zcrValue = double.Parse(zcr);
                double rmsValue = double.Parse(rms);
                double energyValue = double.Parse(energy);

                context.AudioFeatures.Add(new AudioFeature
                {
                    Energy = energyValue,
                    RMS = rmsValue,
                    ZCR = zcrValue,
                    ForType = vm.ForType,
                    Path = path
                });

                await context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(AudioAnalyzer));
        }

        [HttpGet]
        public async Task<IActionResult> DeleteAudio(int id)
        {
            AudioFeature? audio = await context.AudioFeatures.Where(x => x.Id == id).SingleOrDefaultAsync();

            if (audio == null)
            {
                return View("Error");
            }

            context.AudioFeatures.Remove(audio);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(AudioAnalyzer));
        }

        #endregion
    }
}