﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DynamicBlog.Models;

namespace DynamicBlog.Controllers
{
    public class HomeController : Controller
    {
        protected override void HandleUnknownAction(string actionName)
        {
            View(actionName).ExecuteResult(ControllerContext);
        }

        public dynamic Blogs { get; set; }

        public HomeController()
        {
            Blogs = new Blogs();
        }

        public dynamic Index()
        {
            ViewBag.Blogs = Blogs.All();

            return View();
        }

        public dynamic Get(dynamic @params)
        {
            var blog = Blogs.Single(@params.id);

            if (blog == null) return HttpNotFound();

            ViewBag.Blog = blog;

            ViewBag.Comments = blog.Comments();

            return View();
        }

        [HttpPost]
        public dynamic New(dynamic form)
        {
            var blog = new Blog(form);

            if (!blog.IsValid())
            {
                ViewBag.Flash = blog.Message();
                return View();
            }

            Blogs.Save(blog);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public dynamic Comment(dynamic @params)
        {
            var blog = Blogs.Single(@params.id);

            blog.AddComment(@params.comment);

            return RedirectToAction("Get", new { id = @params.id });
        }

        public dynamic Edit(dynamic @params)
        {
            var blog = Blogs.Single(@params.id);

            if (blog == null) return HttpNotFound();

            ViewBag.Blog = blog;

            return View();
        }

        public dynamic Update(dynamic @params)
        {
            var blog = Blogs.Single(@params.id);

            blog.Title = @params.title;
            blog.Body = @params.body;

            if(!blog.IsValid())
            {
                ViewBag.Flash = blog.Message();
                ViewBag.Blog = blog;
                return View("edit");
            }

            Blogs.Save(blog);

            return RedirectToAction("get", new { id = @params.id });
        }
    }
}



