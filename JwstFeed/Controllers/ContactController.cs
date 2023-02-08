using Microsoft.AspNetCore.Mvc;

namespace JwstFeed.Controllers;

public class ContactController : Controller
{
    public IActionResult Index()
    {
        return View("ShowContactDetails");
    }

    public IActionResult ShowContactDetails()
    {
        return View();
    }
}