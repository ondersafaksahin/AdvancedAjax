using Microsoft.AspNetCore.Mvc;

namespace AdvancedAjax.Controllers
{
    public class CountryController : Controller
    {
        private readonly AppDbContext _context;
        public CountryController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Country> countries;
            countries = _context.Countries.ToList();
            return View(countries);
        }

        [HttpGet]
        public IActionResult Create()
        {
            Country country = new Country();
            return View(country);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Create(Country country)
        {
            _context.Countries.Add(country);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Details(int Id)
        {
            Country country = GetCountry(Id);
            return View(country);
        }

        [HttpGet]
        public IActionResult Edit(int Id)
        {
            Country country = GetCountry(Id);
            return View(country);
        }


        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Edit(Country country)
        {
            _context.Attach(country);
            _context.Entry(country).State = EntityState.Modified;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }


        private Country GetCountry(int id) 
        {
            return _context.Countries.FirstOrDefault(x => x.Id == id);
        }


        [HttpGet]
        public IActionResult Delete(int Id)
        {
            Country country = GetCountry(Id);
            return View(country);
        }


        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Delete(Country country) 
        {
            _context.Attach(country);
            _context.Entry(country).State = EntityState.Deleted;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
