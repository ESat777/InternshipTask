using System.Diagnostics;
using InternshipTask.API;
using InternshipTask.Models;
using Microsoft.AspNetCore.Mvc;
using InternshipTask.Settings;

namespace InternshipTask.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            List<Post> posts = await TestAPI.GetPostsAsync(Constants.GET_ALL_POSTS);
            return View(posts);
        }
        // GET: Post/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await TestAPI.GetPostAsync(Constants.GET_POST + id);
                
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // GET: Post/Create
        public IActionResult Create()
        {
            return View();
        }

        //POST: Post/Create
        //To protect from overposting attacks, enable the specific properties you want to bind to.
        //For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

       [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,Id,Title,Body")] Post post)
        {
            if (ModelState.IsValid)
            {
                var des = await TestAPI.SendPostAsync(post);
                return Json(des);
            }
            return View(post);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}