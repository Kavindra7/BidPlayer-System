
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
	using Newtonsoft.Json;


    using Microsoft.AspNetCore.Authorization;
	using System.Dynamic;
	using DocumentFormat.OpenXml.Office2010.Excel;


	/// <summary>
	/// Controller for Bid_Players api
	/// </summary>
	public class Bid_PlayersController:BaseController
	{
		public readonly int ExpireTime = 20; //Set Bid Expire Time <--
		private readonly IConfiguration Config;
		private readonly EmailHelper Mailer;
		private readonly IHttpContextAccessor HttpAccessor;
		private readonly AppDBContext DB;
		private readonly IMapper Mapper;
		private readonly IWebHostEnvironment hostEnvironment;
		private readonly Rbac rbac;
		public Bid_PlayersController(AppDBContext dbContext, IMapper mapper, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment environment, IConfiguration Configuration, EmailHelper mailer, Rbac _rbac) :base(dbContext, httpContextAccessor, environment, Configuration)
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
		/// List  Bid_Players records
		/// Support searching, filtering and ordering of table records
		/// </summary>
		/// <param name="fieldname">Filter records based on table field.</param>
		/// <param name="fieldvalue">Filter value.</param>
   		/// <returns>JSON Array of Bid_Players records</returns>
		[HttpGet]
		public ActionResult Index(string? fieldname, string? fieldvalue, string? search, string? orderby, string ordertype = "desc", int page = 1, int limit = 10){
			try{
				var query = (from Bid_Players in DB.Bid_Players
				join Trophies in DB.Trophies on Bid_Players.thophy_id equals Trophies.id
					select new Bid_Players{
						id = Bid_Players.id,
						owner_id = Bid_Players.owner_id,
						player_id = Bid_Players.player_id,
						trophies_trophy_name = Trophies.trophy_name.ToString(),
						thophy_id = Bid_Players.thophy_id,
						bid_price = Bid_Players.bid_price,
						created_at = Bid_Players.created_at,
                        status = Bid_Players.status
					}
				);
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
				if (query != null)
				{
     //               for(int i = 0; i < query.Count(); i++)
					//{

					//}
                    foreach (var ch in query)
                    {
						DateTime? exp = ch.created_at;
                        DateTime? expireDate = exp.Value.AddMinutes(ExpireTime);
                        if (DateTime.Now > expireDate && ch.status == "Pending")
						{
							CloseBid(ch.id);
                        }
                    }
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
		/// Select single Bid_Players record by ID
		/// </summary>
		/// <param name="id">Table primary key.</param>
   		/// <returns>JSON Object of Bid_Players record</returns>
		[HttpGet]
		[Route("api/bid_players/view/{id}")]
		public ActionResult Detail(int id){
			try{
				var query = (from Bid_Players in DB.Bid_Players
				join Trophies in DB.Trophies on Bid_Players.thophy_id equals Trophies.id
					select new Bid_Players{
						id = Bid_Players.id,
						owner_id = Bid_Players.owner_id,
						player_id = Bid_Players.player_id,
						trophies_trophy_name = Trophies.trophy_name.ToString(),
						thophy_id = Bid_Players.thophy_id,
						bid_price = Bid_Players.bid_price,
                        created_at = Bid_Players.created_at,
                        status = Bid_Players.status
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

        //Close Bid Status
        public ActionResult CloseBid(int id)
        {
            try
            {
                //if (!ModelState.IsValid)
                //{
                //    Console.WriteLine("dd " + id);
                //    return BadRequest(ModelState);
                //}
                var query = (
                    from Bid_Players in DB.Bid_Players
                    select Bid_Players
                );
                //var allowedRoles = new List<String> { "admin" };
                //var userRole = rbac.GetRoleNames(CurrentUser.user_role_id).FirstOrDefault();
                //if (!allowedRoles.Contains(userRole, StringComparer.OrdinalIgnoreCase))
                //{
                //    query = query.Where(p => p.owner_id.Equals(CurrentUser.id));
                //}
                query = query.Where(p => p.id.Equals(id));
                var record = query.FirstOrDefault();
                if (record == null)
                {
                    return NotFound("No record found");
                }
                record.status = "Sold";
                DB.Update(record);
                DB.SaveChanges();
				AddToTeamOwner(record.player_id,record.owner_id,record.bid_price);
                return Ok(record);
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }

        //Add Sold Player That Owner Team
        public ActionResult AddToTeamOwner(int? player,int? bidder, decimal? price)
        {
            try
            {
                dynamic postdata = new ExpandoObject();
                postdata.player_name = player;
                postdata.bidder_id = bidder;
                postdata.bid_price = price;
                postdata.status = "Won";

                string jsonData = JsonConvert.SerializeObject(postdata);
				Console.WriteLine("Log: "+ jsonData);
                var modeldata = Mapper.Map<My_Players>(postdata);
                // save Bid_Players record
                DB.My_Players.Add(modeldata);
                var record = modeldata; //newly created record
                DB.SaveChanges();
                var recId = record.id; //newly created record id
                return Ok(record);
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }


        /// <summary>
        /// Save form record to the  table
        /// </summary>
        /// <returns>JSON Object of newly created Bid_Players record</returns>
        [HttpPost]
		public ActionResult Add([FromBody] BidPlayersAddDTO postdata){
			try{
				postdata.owner_id = CurrentUser.id;
				if (!ModelState.IsValid)
				{
					return BadRequest(ModelState);
				}
				var modeldata = Mapper.Map<Bid_Players>(postdata);
				// save Bid_Players record
				DB.Bid_Players.Add(modeldata);
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
		/// Get  Bid_Players record for edit
		/// </summary>
		/// <param name="id">Select record by table primary key.</param>
   		/// <returns>JSON Object of Bid_Players record</returns>
		[HttpGet]
		public ActionResult Edit(int id){
			try{
				var query = (
					from Bid_Players in DB.Bid_Players
					select new Bid_Players{
						id = Bid_Players.id
					}
				);
				var allowedRoles =  new List<String>{"admin"};
				var userRole = rbac.GetRoleNames(CurrentUser.user_role_id).FirstOrDefault();
				if(!allowedRoles.Contains(userRole, StringComparer.OrdinalIgnoreCase)){
				query = query.Where(p => p.owner_id.Equals(CurrentUser.id));
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
		/// Update  Bid_Players record with form data
		/// </summary>
		/// <param name="id">Select record by table primary key.</param>
   		/// <returns>JSON Object of Bid_Players record</returns>
		[HttpPost]
		public ActionResult Edit(int id, [FromBody] BidPlayersEditDTO postdata){
			try{
				if (!ModelState.IsValid)
				{
					return BadRequest(ModelState);
				}
				var query = (
					from Bid_Players in DB.Bid_Players
					select Bid_Players
				);
				var allowedRoles =  new List<String>{"admin"};
				var userRole = rbac.GetRoleNames(CurrentUser.user_role_id).FirstOrDefault();
				if(!allowedRoles.Contains(userRole, StringComparer.OrdinalIgnoreCase)){
				query = query.Where(p => p.owner_id.Equals(CurrentUser.id));
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
		/// Delete Bid_Players record
		/// Support multi delete by separating record id by comma.
		/// </summary>
		/// <param name="id">Record ID.</param>
   		/// <returns>Deleted Records ID</returns>
		[HttpGet]
		public ActionResult Delete(string id){
			try{
				var query = DB.Bid_Players.AsQueryable();
				List<string> arrId = id.Split(",").ToList();
				var allowedRoles =  new List<String>{"admin"};
				var userRole = rbac.GetRoleNames(CurrentUser.user_role_id).FirstOrDefault();
				if(!allowedRoles.Contains(userRole, StringComparer.OrdinalIgnoreCase)){
				query = query.Where(p => p.owner_id.Equals(CurrentUser.id));
				}
				query = query.Where(p => arrId.Contains(p.id.ToString()));
				DB.Bid_Players.RemoveRange(query);
				DB.SaveChanges();
				return Ok(id);
			}
			catch (Exception ex){
				return ServerError(ex);
			}
		}
		
		
	}
}
