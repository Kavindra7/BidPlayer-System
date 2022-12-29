
namespace ASPRad.Controller{
	using System;
	using Microsoft.AspNetCore.Mvc;
	using System.Collections.Generic;
	using Microsoft.Extensions.Logging;
	using System.Linq;
    using Microsoft.AspNetCore.Http;
	using Microsoft.AspNetCore.Hosting;
	using ASPRad.Models;
	using ASPRad.Helpers;
	using Microsoft.EntityFrameworkCore;
	using System.Linq.Dynamic.Core;
    using Microsoft.Extensions.Configuration;
	using AutoMapper;
	
	using Microsoft.AspNetCore.Authorization;
	

	/// <summary>
	/// Controller for Register_With_Trophy api
	/// </summary>
	public class Register_With_TrophyController:BaseController
	{
		private readonly IConfiguration Config;
		private readonly EmailHelper Mailer;
		private readonly IHttpContextAccessor HttpAccessor;
		private readonly AppDBContext DB;
		private readonly IMapper Mapper;
		private readonly IWebHostEnvironment hostEnvironment;
		private readonly Rbac rbac;
		public Register_With_TrophyController(AppDBContext dbContext, IMapper mapper, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment environment, IConfiguration Configuration, EmailHelper mailer, Rbac _rbac) :base(dbContext, httpContextAccessor, environment, Configuration)
		{
			Config = Configuration;
			Mailer = mailer;
			HttpAccessor = httpContextAccessor;
			DB = dbContext;
			Mapper = mapper;
			hostEnvironment = environment;
			rbac = _rbac;
		}
		

		/// <summary>
		/// List  Register_With_Trophy records
		/// Support searching, filtering and ordering of table records
		/// </summary>
		/// <param name="fieldname">Filter records based on table field.</param>
		/// <param name="fieldvalue">Filter value.</param>
   		/// <returns>JSON Array of Register_With_Trophy records</returns>
		[HttpGet]
		public ActionResult Index(string? fieldname, string? fieldvalue, string? search, string? orderby, string ordertype = "desc", int page = 1, int limit = 10){
			try{
				var query = (from Register_With_Trophy in DB.Register_With_Trophy
				join Trophies in DB.Trophies on Register_With_Trophy.trophy_id equals Trophies.id
				join Users in DB.Users on Register_With_Trophy.player_id equals Users.id
					select new Register_With_Trophy{
						id = Register_With_Trophy.id,
						trophies_trophy_name = Trophies.trophy_name.ToString(),
						trophy_id = Register_With_Trophy.trophy_id,
						users_full_name = Users.full_name.ToString(),
						player_id = Register_With_Trophy.player_id
					}
				);
				if(orderby != null){
					bool asc = (ordertype.ToLower() == "asc" ? true : false);
					query = query.OrderByField(orderby, asc);
				}
				else{
					query = query.OrderByField("id", false);
				}
				var allowedRoles =  new List<String>{"admin", "player", "team owner"};
				var userRole = rbac.GetRoleNames(CurrentUser.user_role_id).FirstOrDefault();
				if(!allowedRoles.Contains(userRole, StringComparer.OrdinalIgnoreCase)){
				query = query.Where(p => p.player_id.Equals(CurrentUser.id));
				}
				if(fieldvalue != null)
				{
					// filter record by table field
					query = query.Where(fieldname + " == @0", fieldvalue);
				}
				int total_records = query.Count();
				int offset = ((page - 1) * limit);
				var records = query.Skip(offset).Take(limit).ToList();
				int record_count = records.Count;
				double t = total_records / limit;
				int total_page = (int)Math.Ceiling(t);
				var result = new { records, total_records, record_count, total_page};
				return Ok(result);
			}
			catch (Exception ex){
				return ServerError(ex);
			}
		}
		

		/// <summary>
		/// Select single Register_With_Trophy record by ID
		/// </summary>
		/// <param name="id">Table primary key.</param>
   		/// <returns>JSON Object of Register_With_Trophy record</returns>
		[HttpGet]
		[Route("api/register_with_trophy/view/{id}")]
		public ActionResult Detail(int id){
			try{
				var query = (from Register_With_Trophy in DB.Register_With_Trophy
				join Trophies in DB.Trophies on Register_With_Trophy.trophy_id equals Trophies.id
					select new Register_With_Trophy{
						id = Register_With_Trophy.id,
						trophies_trophy_name = Trophies.trophy_name.ToString(),
						trophy_id = Register_With_Trophy.trophy_id,
						player_id = Register_With_Trophy.player_id
					}
				);
				var allowedRoles =  new List<String>{"admin", "player", "team owner"};
				var userRole = rbac.GetRoleNames(CurrentUser.user_role_id).FirstOrDefault();
				if(!allowedRoles.Contains(userRole, StringComparer.OrdinalIgnoreCase)){
				query = query.Where(p => p.player_id.Equals(CurrentUser.id));
				}
				query = query.Where(p => p.id.Equals(id));
				var record = query.FirstOrDefault();
				if (record == null )
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
		/// Save form record to the  table
		/// </summary>
   		/// <returns>JSON Object of newly created Register_With_Trophy record</returns>
		[HttpPost]
		public ActionResult Add([FromBody] RegisterWithTrophyAddDTO postdata){
			try{
				postdata.player_id = CurrentUser.id;
				if (!ModelState.IsValid)
				{
					return BadRequest(ModelState);
				}
				var modeldata = Mapper.Map<Register_With_Trophy>(postdata);
				// save Register_With_Trophy record
				DB.Register_With_Trophy.Add(modeldata);
				var record = modeldata; //newly created record
				DB.SaveChanges();
				var recId = record.id; //newly created record id
				return Ok(record);
			}
			catch (Exception ex){
				return ServerError(ex);
			}
		}
		

		/// <summary>
		/// Get  Register_With_Trophy record for edit
		/// </summary>
		/// <param name="id">Select record by table primary key.</param>
   		/// <returns>JSON Object of Register_With_Trophy record</returns>
		[HttpGet]
		public ActionResult Edit(int id){
			try{
				var query = (
					from Register_With_Trophy in DB.Register_With_Trophy
					select new Register_With_Trophy{
						id = Register_With_Trophy.id,
						trophy_id = Register_With_Trophy.trophy_id
					}
				);
				var allowedRoles =  new List<String>{"admin", "player"};
				var userRole = rbac.GetRoleNames(CurrentUser.user_role_id).FirstOrDefault();
				if(!allowedRoles.Contains(userRole, StringComparer.OrdinalIgnoreCase)){
				query = query.Where(p => p.player_id.Equals(CurrentUser.id));
				}
				query = query.Where(p => p.id.Equals(id));
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
		/// Update  Register_With_Trophy record with form data
		/// </summary>
		/// <param name="id">Select record by table primary key.</param>
   		/// <returns>JSON Object of Register_With_Trophy record</returns>
		[HttpPost]
		public ActionResult Edit(int id, [FromBody] RegisterWithTrophyEditDTO postdata){
			try{
				if (!ModelState.IsValid)
				{
					return BadRequest(ModelState);
				}
				var query = (
					from Register_With_Trophy in DB.Register_With_Trophy
					select Register_With_Trophy
				);
				var allowedRoles =  new List<String>{"admin", "player"};
				var userRole = rbac.GetRoleNames(CurrentUser.user_role_id).FirstOrDefault();
				if(!allowedRoles.Contains(userRole, StringComparer.OrdinalIgnoreCase)){
				query = query.Where(p => p.player_id.Equals(CurrentUser.id));
				}
				query = query.Where(p => p.id.Equals(id));
				var record = query.FirstOrDefault();
				if (record == null)
				{
					return NotFound("No record found");
				}
				
				var modeldata = Mapper.Map(postdata, record);
				DB.Update(modeldata);
				DB.SaveChanges();
				return Ok(record);
			}
			catch (Exception ex){
				return ServerError(ex);
			}
		}
		

		/// <summary>
		/// Delete Register_With_Trophy record
		/// Support multi delete by separating record id by comma.
		/// </summary>
		/// <param name="id">Record ID.</param>
   		/// <returns>Deleted Records ID</returns>
		[HttpGet]
		public ActionResult Delete(string id){
			try{
				var query = DB.Register_With_Trophy.AsQueryable();
				List<string> arrId = id.Split(",").ToList();
				var allowedRoles =  new List<String>{"admin", "player"};
				var userRole = rbac.GetRoleNames(CurrentUser.user_role_id).FirstOrDefault();
				if(!allowedRoles.Contains(userRole, StringComparer.OrdinalIgnoreCase)){
				query = query.Where(p => p.player_id.Equals(CurrentUser.id));
				}
				query = query.Where(p => arrId.Contains(p.id.ToString()));
				DB.Register_With_Trophy.RemoveRange(query);
				DB.SaveChanges();
				return Ok(id);
			}
			catch (Exception ex){
				return ServerError(ex);
			}
		}
		
		
	}
}
