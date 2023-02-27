using Microsoft.AspNetCore.Mvc;

namespace JwstFeed.Controllers;

public class FeaturedController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
