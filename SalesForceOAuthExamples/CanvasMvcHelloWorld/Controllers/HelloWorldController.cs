using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CanvasMvcHelloWorld.Models;

namespace CanvasMvcHelloWorld.Controllers
{
    public class HelloWorldController : Controller
    {
        //
        // GET: /HelloWorld/
        public ActionResult Index()
        {
            var postedSignedRequest = Request.Form["signed_request"];
            var model = new HelloWorldModel(postedSignedRequest);

            return View(model);
        }
	}
}