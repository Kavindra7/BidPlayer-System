
namespace ASPRad.Controller{
	using System;
	using Microsoft.AspNetCore.Mvc;
	
	using System.Linq;
    using Microsoft.AspNetCore.Http;
	using Microsoft.AspNetCore.Hosting;
	using System.Linq.Dynamic.Core;
	using ASPRad.Models;
	using ASPRad.Helpers;
    using Microsoft.Extensions.Configuration;
	using Microsoft.EntityFrameworkCore;
	
	public class HomeController:BaseController{
		private readonly IConfiguration Config;
		private readonly AppDBContext DB;
		private readonly IWebHostEnvironment hostEnvironment;
		public HomeController(AppDBContext dbContext, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment environment, IConfiguration Configuration) :base(dbContext, httpContextAccessor, environment, Configuration)
		{
			DB = dbContext;
			Config = Configuration;
			hostEnvironment = environment;
		}

		public ActionResult index(){
			return View();
		}
		
	}
	
}
