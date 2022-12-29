
namespace ASPRad.Controller{
	using System;
	using Microsoft.AspNetCore.Mvc;
	using System.Collections.Generic;
	using Microsoft.Extensions.Logging;
	using System.Linq;
	using System.Linq.Dynamic.Core;
    using Microsoft.AspNetCore.Http;
	using Microsoft.AspNetCore.Hosting;
	using ASPRad.Models;
	using AutoMapper;
    using Microsoft.Extensions.Configuration;
	

	/// <summary>
	/// User Account Controller
	/// </summary>
	public class AccountController:BaseController{
		private readonly IConfiguration Config;
		private readonly EmailHelper Mailer;
		private readonly AppDBContext DB;
		private IQueryable<Users> Query;
		private readonly IMapper Mapper;
		private readonly IWebHostEnvironment hostEnvironment;
		private readonly Rbac rbac;
		public AccountController(AppDBContext dbContext, IMapper mapper, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment environment, IConfiguration Configuration, EmailHelper mailer, Rbac _rbac) :base(dbContext, httpContextAccessor, environment, Configuration)
		{
			Config = Configuration;
			Mailer = mailer;
			DB = dbContext;
			Query = DB.Users;
			Mapper = mapper;
			hostEnvironment = environment;
			rbac = _rbac;
		}
		

		/// <summary>
		/// Route to view  user account detail
		/// </summary>
   		/// <returns>JSON Object of Users record</returns>
		[HttpGet]
		public ActionResult Index(){
			var id = CurrentUser.id;
			try{
				var query = (from Users in DB.Users
					select new Users{
						id = Users.id,
						full_name = Users.full_name,
						username = Users.username,
						email = Users.email,
						age = Users.age,
						gender = Users.gender,
						balance = Users.balance,
						user_role_id = Users.user_role_id
					}
				);
				query = query.Where(p => p.id.Equals(id)); // filter by current user id
				var record = query.FirstOrDefault();
				if (record == null)
				{
					return NotFound("No record found");
				}
				
				return Ok(record);
			}
			catch (Exception ex){
				return ServerError(ex);
			}
		}
		

		/// <summary>
		/// Get  user account record for edit
		/// </summary>
		/// <param name="id">Select record by table primary key.</param>
   		/// <returns>JSON Object of Users record</returns>
		[HttpGet]
		public ActionResult Edit(){
			try{
				var id = CurrentUser.id;
				var query = (
					from Users in DB.Users
					select new Users{
						id = Users.id,
						full_name = Users.full_name,
						username = Users.username,
						age = Users.age,
						gender = Users.gender,
						balance = Users.balance,
						user_role_id = Users.user_role_id
					}
				);
				query = query.Where(p => p.id.Equals(id));  // filter by current user id
				var record = query.FirstOrDefault();
				if (record == null)
				{
					return NotFound("No record found");
				}
				
				return Ok(record);
			}
			catch (Exception ex){
				return ServerError(ex);
			}
		}
		

		/// <summary>
		/// Update  user account record with form data
		/// </summary>
		/// <param name="id">Select record by table primary key.</param>
   		/// <returns>JSON Object of Users record</returns>
		[HttpPost]
		public ActionResult Edit([FromBody] UsersAccounteditDTO postdata){
			var id = CurrentUser.id;
			try{
				if (!ModelState.IsValid)
				{
					return BadRequest(ModelState);
				}
				var query = (from Users in DB.Users
					select Users
				);
				query = query.Where(p => p.id.Equals(id));  // filter by current user id
				var record = query.FirstOrDefault();
				if (record == null)
				{
					return NotFound("No record found");
				}
				
				// map and update current record with form data
				var modeldata = Mapper.Map(postdata, record);
				DB.Update(modeldata);
				DB.SaveChanges();
				return Ok(record);
			}
			catch (Exception ex){
				return ServerError(ex);
			}
		}
		public ActionResult CurrentUserData()
		{
			try
			{
				var userPages = new List<string>();
				var roleNames = new List<string>();
				if(CurrentUser != null){
					var userRole = CurrentUser.user_role_id;
					userPages = rbac.GetRolePages(userRole);
					roleNames = rbac.GetRoleNames(userRole);
					CurrentUser.password = "";
					var data = new {
						user = CurrentUser,
						pages = userPages,
						roles = roleNames
					};
					return Ok(data);
				}
				return NotFound("User not found");
			}
			catch (Exception ex)
			{
				return ServerError(ex);
			}
		}
		

		/// <summary>
		/// Change user account password
		/// </summary>
		/// <param name="modeldata">Request form data.</param>
   		/// <returns>JSON Object of Users record</returns>
		[HttpPost]
		public ActionResult ChangePassword([FromBody] ChangePassword modeldata){
			try
			{
				if (!ModelState.IsValid)
				{
					return BadRequest(ModelState);
				}
				Query = Query.Where(p => p.id.Equals(CurrentUser.id));
				var user = Query.FirstOrDefault();
				if(user != null ){
					if(!Hash.Verify(user.password, modeldata.OldPassword)){
						return BadRequest("Current password is incorrect");
					}
					user.password = Hash.ComputeHash(modeldata.NewPassword);;
					DB.Update(user);
					DB.SaveChanges();
					return Ok();
				}
				else{
					return NotFound("No record found");
				}
			}
			catch(Exception ex)
			{
				return ServerError(ex);
			}
		}
	}
}
