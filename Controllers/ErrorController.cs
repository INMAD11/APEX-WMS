using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace APEX_WMS.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/{statusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            switch (statusCode)
            {
                case 404:
                    ViewBag.ErrorMessage = "Sorry, the resource you requested could not be found";
                    break;
                case 500:
                    ViewBag.ErrorMessage = "Sorry, an internal server error has occurred";
                    break;
                default:
                    ViewBag.ErrorMessage = $"Sorry, an error occurred with status code {statusCode}";
                    break;
            }
            return View("NotFound");
        }
    }
}
