using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FunctionChallenge.Models;
using MyServices;
using System.Text.Json;

namespace FunctionChallenge.Controllers
{
    public class HomeController : Controller
           
    {
        private readonly ViewToStringConverter viewToStringConverter;

        public HomeController(ViewToStringConverter viewToStringConverter)
        {
            this.viewToStringConverter = viewToStringConverter;
        }

        [HttpGet]
        public IActionResult Main()
        {
            return View(new FunctionViewModel() {
            a=5, b=5, c=16, step=1, from=-10, to=10
            });
        }

        [HttpPost]
        public IActionResult Function(FunctionViewModel functionView)
        {
            if (functionView.to <= functionView.from)
            {
                ModelState.AddModelError(nameof(functionView.to), "Value of 'to' must be greater then 'From'");
            }

            if (functionView.step >= (functionView.to - functionView.from))
            {
                ModelState.AddModelError(nameof(functionView.step), "Value of 'step' must be greater, then difference of 'from' and 'to'");
            }

            if (ModelState.IsValid)
            {
                var points = innerFunction(functionView.a, functionView.b, functionView.c,
                    functionView.step, functionView.from, functionView.to);
                functionView.points = JsonSerializer.Serialize(points);
            }

            return View("Main", functionView);
        }

        [HttpPost]
        public IActionResult FunctionAjax(FunctionViewModel functionView)
        {
            if (functionView.to <= functionView.from)
            {
                ModelState.AddModelError(nameof(functionView.to), "Value of 'to' must be greater then 'From'");
            }
            if (functionView.step>=(functionView.to-functionView.from))
            {
                ModelState.AddModelError(nameof(functionView.step), "Value of 'step' must be greater, then difference of 'from' and 'to'");
            }

            if (ModelState.IsValid)
            {
                var points = innerFunction(functionView.a, functionView.b, functionView.c,
                    functionView.step, functionView.from, functionView.to);
                return Json(points);
            }
            return View("Main", functionView);
        }

        private  IEnumerable<Point> innerFunction(double a, double b, double c, double step,
            double from, double to)
        {
            List<Point> points = new List<Point>();
            for (double x = from; x <= to; x+=step)
            {
                double y = a * (Math.Pow(x, 2)) + (b * x) + c;
                Point point = new Point(x, y);
                points.Add(point);
            }
            return points;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
