﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FunctionChallenge.Models;
using System.Text.Json;

namespace FunctionChallenge.Controllers
{
    public class HomeController : Controller       
    {
        private readonly ChartDBContext chartDB;

        public HomeController(ChartDBContext chartDB)
        {
            this.chartDB = chartDB;
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
                var points = innerFunction(functionView.a, functionView.b, functionView.c, functionView.step, functionView.from, functionView.to);
                AddDataToDB(functionView, points);
                functionView.points = JsonSerializer.Serialize(points);
            }

            return View("Main", functionView);
        }

        [HttpGet]
        public IActionResult ReactMain()
        {
            return View();
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
                AddDataToDB(functionView, points);
                return Json(points);
            }
            return null;
        }

        private  IEnumerable<Point> innerFunction(int a, int b, int c, int step,
            int from, int to)
        {
            List<Point> points = new List<Point>();
            for (int x = from; x <= to; x+=step)
            {
                int y = a * ((int)Math.Pow(x, 2)) + (b * x) + c;
                Point point = new Point(x, y);
                points.Add(point);
            }
            return points;
        }
        private void AddDataToDB(FunctionViewModel functionView, IEnumerable<Point> points)
        {
            UserData userData = new UserData
            {
                a = functionView.a,
                b = functionView.b,
                c = functionView.c,
                Step = functionView.step,
                RangeFrom = functionView.from,
                RangeTo = functionView.to
            };
            chartDB.UserDatas.Add(userData);
            chartDB.SaveChanges();

            foreach (var point in points)
            {
                chartDB.Add(point.GetDBModel(userData));
            }
            chartDB.SaveChanges();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
