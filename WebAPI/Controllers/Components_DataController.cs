
namespace ASPRad.Controller{
	using System;
	using Microsoft.AspNetCore.Mvc;
	using System.Collections.Generic;
	using System.Linq;
    using Microsoft.AspNetCore.Http;
	using Microsoft.AspNetCore.Hosting;
	using System.Linq.Dynamic.Core;
	using ASPRad.Models;
	using ASPRad.Helpers;
    using Microsoft.Extensions.Configuration;
	using Microsoft.EntityFrameworkCore;
	

	/// <summary>
	/// Components Data Controller
	/// </summary>
	using Microsoft.AspNetCore.Authorization;
    [AllowAnonymous]
	public class Components_Data:BaseController
	{
		private readonly IConfiguration Config;
		private readonly AppDBContext DB;
		private readonly IWebHostEnvironment hostEnvironment;
		public Components_Data(AppDBContext dbContext, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment environment, IConfiguration Configuration) :base(dbContext, httpContextAccessor, environment, Configuration)
		{
			DB = dbContext;
			Config = Configuration;
			hostEnvironment = environment;
		}
	

	/// <summary>
	/// Get player_id_option_list records
	/// </summary>
	/// <returns>Array of option labels and value object</returns>
	[HttpGet]
	public ActionResult player_id_option_list()
	{
		string sqlText = "SELECT  DISTINCT id AS value,full_name AS label FROM users" ;
		var queryParams = new List<QueryParam>();
		var records =  DB.RawSqlQuery(sqlText, queryParams,
			record => new {
				
				
				value = record["value"],
				label = record["label"]
			}
		);
		return Ok(records) ;
	}
	

	/// <summary>
	/// Get thophy_id_option_list records
	/// </summary>
	/// <returns>Array of option labels and value object</returns>
	[HttpGet]
	public ActionResult thophy_id_option_list()
	{
		string sqlText = "SELECT  DISTINCT id AS value,trophy_name AS label FROM trophies" ;
		var queryParams = new List<QueryParam>();
		var records =  DB.RawSqlQuery(sqlText, queryParams,
			record => new {
				
				
				value = record["value"],
				label = record["label"]
			}
		);
		return Ok(records) ;
	}
	

	/// <summary>
	/// Get player_name_option_list records
	/// </summary>
	/// <returns>Array of option labels and value object</returns>
	[HttpGet]
	public ActionResult player_name_option_list()
	{
		string sqlText = "SELECT  DISTINCT id AS value,full_name AS label FROM users WHERE user_role_id = 8" ;
		var queryParams = new List<QueryParam>();
		var records =  DB.RawSqlQuery(sqlText, queryParams,
			record => new {
				
				
				value = record["value"],
				label = record["label"]
			}
		);
		return Ok(records) ;
	}
	

	/// <summary>
	/// Get role_id_option_list records
	/// </summary>
	/// <returns>Array of option labels and value object</returns>
	[HttpGet]
	public ActionResult role_id_option_list()
	{
		string sqlText = "SELECT role_id as value, role_name as label FROM roles" ;
		var queryParams = new List<QueryParam>();
		var records =  DB.RawSqlQuery(sqlText, queryParams,
			record => new {
				
				
				value = record["value"],
				label = record["label"]
			}
		);
		return Ok(records) ;
	}
	

	/// <summary>
	/// Get owner_id_option_list records
	/// </summary>
	/// <returns>Array of option labels and value object</returns>
	[HttpGet]
	public ActionResult owner_id_option_list()
	{
		string sqlText = "SELECT  DISTINCT id AS value,full_name AS label FROM users WHERE user_role_id = '9'" ;
		var queryParams = new List<QueryParam>();
		var records =  DB.RawSqlQuery(sqlText, queryParams,
			record => new {
				
				
				value = record["value"],
				label = record["label"]
			}
		);
		return Ok(records) ;
	}
	

	/// <summary>
	/// Get team_1_option_list records
	/// </summary>
	/// <returns>Array of option labels and value object</returns>
	[HttpGet]
	public ActionResult team_1_option_list()
	{
		string sqlText = "SELECT  DISTINCT id AS value,team_name AS label FROM teams" ;
		var queryParams = new List<QueryParam>();
		var records =  DB.RawSqlQuery(sqlText, queryParams,
			record => new {
				
				
				value = record["value"],
				label = record["label"]
			}
		);
		return Ok(records) ;
	}
	

	/// <summary>
	/// check if field value already exist in a Users table
	/// </summary>
	/// <param name="id">value to check if exists in a table records.</param>
	/// <returns>True or False</returns>
	[HttpGet]
	public ActionResult users_username_exist(string id = ""){
		bool exist = DB.Users.Any(p => p.username == id);
		if(exist == true){
			return Ok("true");
		}
		return Ok("false") ;
	}
	

	/// <summary>
	/// check if field value already exist in a Users table
	/// </summary>
	/// <param name="id">value to check if exists in a table records.</param>
	/// <returns>True or False</returns>
	[HttpGet]
	public ActionResult users_email_exist(string id = ""){
		bool exist = DB.Users.Any(p => p.email == id);
		if(exist == true){
			return Ok("true");
		}
		return Ok("false") ;
	}
	

	/// <summary>
	/// Get getcount_users record
	/// </summary>
	/// <returns>Single value</returns>
	[HttpGet]
	public ActionResult  getcount_users(){
		string sqlText = "SELECT COUNT(*) AS num FROM users" ;
		var queryParams = new List<QueryParam>();
		var value =  DB.RawSqlQueryScalar(sqlText, queryParams);
		return Ok(value) ;
	}
	

	/// <summary>
	/// Get getcount_teams record
	/// </summary>
	/// <returns>Single value</returns>
	[HttpGet]
	public ActionResult  getcount_teams(){
		string sqlText = "SELECT COUNT(*) AS num FROM teams" ;
		var queryParams = new List<QueryParam>();
		var value =  DB.RawSqlQueryScalar(sqlText, queryParams);
		return Ok(value) ;
	}
	

	/// <summary>
	/// Get getcount_trophies record
	/// </summary>
	/// <returns>Single value</returns>
	[HttpGet]
	public ActionResult  getcount_trophies(){
		string sqlText = "SELECT COUNT(*) AS num FROM trophies" ;
		var queryParams = new List<QueryParam>();
		var value =  DB.RawSqlQueryScalar(sqlText, queryParams);
		return Ok(value) ;
	}
	

	/// <summary>
	/// Get linechart_biddingprices record
	/// </summary>
	/// <returns>Array of label value object</returns>
	[HttpGet]
	public ActionResult  linechart_biddingprices(){
		try{
			string sqlText = "SELECT  bid_players.id, bid_players.bid_price FROM bid_players" ;
		var queryParams = new List<QueryParam>();
			var records =  DB.RawSqlQuery(sqlText, queryParams,
				record => new {
					bid_price = record["bid_price"]
				}
			);
			var labels = records.Select(x => x.bid_price).ToList();
			var datasets = new List<object>();
			var dataset1 = new {
				label = "LKR",
				data = records.Select(x => x.bid_price).ToList(),
				backgroundColor =  Utils.RandomColor(), 
				borderColor = Utils.RandomColor(), 
				borderWidth = "",
			};
			datasets.Add(dataset1);
			var chartData = new {
				labels,
				datasets,
			};
			return Ok(chartData) ;
		}
		catch (Exception ex){
			return ServerError(ex);
		}
	}
	}
}
