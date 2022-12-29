
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
	/// Controller for Teams api
	/// </summary>
	public class TeamsController:BaseController
	{
		private readonly IConfiguration Config;
		private readonly EmailHelper Mailer;
		private readonly IHttpContextAccessor HttpAccessor;
		private readonly AppDBContext DB;
		private readonly IMapper Mapper;
		private readonly IWebHostEnvironment hostEnvironment;
		private readonly Rbac rbac;
		public TeamsController(AppDBContext dbContext, IMapper mapper, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment environment, IConfiguration Configuration, EmailHelper mailer, Rbac _rbac) :base(dbContext, httpContextAccessor, environment, Configuration)
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
		/// List  Teams records
		/// Support searching, filtering and ordering of table records
		/// </summary>
		/// <param name="fieldname">Filter records based on table field.</param>
		/// <param name="fieldvalue">Filter value.</param>
   		/// <returns>JSON Array of Teams records</returns>
		[HttpGet]
		public ActionResult Index(string? fieldname, string? fieldvalue, string? search, string? orderby, string ordertype = "desc", int page = 1, int limit = 10){
			try{
				var query = (from Teams in DB.Teams
				join Users in DB.Users on Teams.owner_id equals Users.id
					select new Teams{
						id = Teams.id,
						team_name = Teams.team_name,
						max_players = Teams.max_players,
						max_price = Teams.max_price,
						status = Teams.status,
						users_full_name = Users.full_name.ToString(),
						owner_id = Teams.owner_id
					}
				);
				if(search != null){
					query = query.Where(
						p => EF.Functions.Like(p.team_name, $"%{search}%") || 
						EF.Functions.Like(p.max_players, $"%{search}%") || 
						EF.Functions.Like(p.status, $"%{search}%") 
					);
				}
				if(orderby != null){
					bool asc = (ordertype.ToLower() == "asc" ? true : false);
					query = query.OrderByField(orderby, asc);
				}
				else{
					query = query.OrderByField("id", false);
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
		/// Select single Teams record by ID
		/// </summary>
		/// <param name="id">Table primary key.</param>
   		/// <returns>JSON Object of Teams record</returns>
		[HttpGet]
		[Route("api/teams/view/{id}")]
		public ActionResult Detail(int id){
			try{
				var query = (from Teams in DB.Teams
				join Users in DB.Users on Teams.owner_id equals Users.id
					select new Teams{
						id = Teams.id,
						team_name = Teams.team_name,
						max_players = Teams.max_players,
						max_price = Teams.max_price,
						status = Teams.status,
						users_full_name = Users.full_name.ToString(),
						owner_id = Teams.owner_id
					}
				);
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
   		/// <returns>JSON Object of newly created Teams record</returns>
		[HttpPost]
		public ActionResult Add([FromBody] TeamsAddDTO postdata){
			try{
				if (!ModelState.IsValid)
				{
					return BadRequest(ModelState);
				}
				var modeldata = Mapper.Map<Teams>(postdata);
				// save Teams record
				DB.Teams.Add(modeldata);
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
		/// Get  Teams record for edit
		/// </summary>
		/// <param name="id">Select record by table primary key.</param>
   		/// <returns>JSON Object of Teams record</returns>
		[HttpGet]
		public ActionResult Edit(int id){
			try{
				var query = (
					from Teams in DB.Teams
					select new Teams{
						id = Teams.id,
						team_name = Teams.team_name,
						max_players = Teams.max_players,
						max_price = Teams.max_price,
						status = Teams.status,
						owner_id = Teams.owner_id
					}
				);
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
		/// Update  Teams record with form data
		/// </summary>
		/// <param name="id">Select record by table primary key.</param>
   		/// <returns>JSON Object of Teams record</returns>
		[HttpPost]
		public ActionResult Edit(int id, [FromBody] TeamsEditDTO postdata){
			try{
				if (!ModelState.IsValid)
				{
					return BadRequest(ModelState);
				}
				var query = (
					from Teams in DB.Teams
					select Teams
				);
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
		/// Delete Teams record
		/// Support multi delete by separating record id by comma.
		/// </summary>
		/// <param name="id">Record ID.</param>
   		/// <returns>Deleted Records ID</returns>
		[HttpGet]
		public ActionResult Delete(string id){
			try{
				var query = DB.Teams.AsQueryable();
				List<string> arrId = id.Split(",").ToList();
				query = query.Where(p => arrId.Contains(p.id.ToString()));
				DB.Teams.RemoveRange(query);
				DB.SaveChanges();
				return Ok(id);
			}
			catch (Exception ex){
				return ServerError(ex);
			}
		}
		
		
	}
}
