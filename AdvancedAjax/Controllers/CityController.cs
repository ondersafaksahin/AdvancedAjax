using Microsoft.AspNetCore.Mvc;

namespace AdvancedAjax.Controllers
{
    public class CityController : Controller
    {
        private readonly AppDbContext _context;

        public CityController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<City> cities;
            cities = _context.Cities.ToList();
            return View(cities);
        }

        [HttpGet]
        public IActionResult Create()
        {
            City city = new City();
            ViewBag.Countries = GetCountries();
            return View(city);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Create(City city)
        {
            _context.Cities.Add(city);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Details(int Id)
        {
            City city = _context.Cities.Where(x => x.Id == Id).FirstOrDefault();
            return View(city);
        }

        [HttpGet]
        public IActionResult Edit(int Id)
        {
            City city = _context.Cities.Where(x => x.Id == Id).FirstOrDefault();
            ViewBag.Countries = GetCountries();
            return View(city);
        }


        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Edit(City city)
        {
            _context.Attach(city);
            _context.Entry(city).State = EntityState.Modified;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Delete(int Id)
        {
            City city = _context.Cities.Where(x => x.Id == Id).FirstOrDefault();
            return View(city);
        }


        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Delete(Country city)
        {
            _context.Attach(city);
            _context.Entry(city).State = EntityState.Deleted;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private List<SelectListItem> GetCountries()
        {
            var lstCountries = new List<SelectListItem>();
            List<Country> countries = _context.Countries.ToList();

            lstCountries = countries.Select(ct => new SelectListItem()
            {
                Value = ct.Id.ToString(),
                Text = ct.Name
            }).ToList();

            var defItem = new SelectListItem()
            {
                Value="",
                Text="----Select Country----"
            };

            lstCountries.Insert(0, defItem);
            return lstCountries;
        }
    }
}
