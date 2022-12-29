
namespace ASPRad.Controller{
	using System;
	using Microsoft.AspNetCore.Mvc;
	using System.Collections.Generic;
	using Microsoft.Extensions.Logging;
	using System.Linq;
    using System.IO;
	using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
	using Microsoft.AspNetCore.Hosting;
	using ASPRad.Models;
	using ASPRad.Helpers;
	using Microsoft.EntityFrameworkCore;
	using System.Linq.Dynamic.Core;
    using Microsoft.Extensions.Configuration;
	using AutoMapper;
	using WkWrap.Core;
    using System.Text;
	using Microsoft.AspNetCore.Authorization;
	

	/// <summary>
	/// Controller for Trophies api
	/// </summary>
	public class TrophiesController:BaseController
	{
		private readonly IConfiguration Config;
		private readonly EmailHelper Mailer;
		private readonly IHttpContextAccessor HttpAccessor;
		private readonly AppDBContext DB;
		private readonly IMapper Mapper;
		private readonly IWebHostEnvironment hostEnvironment;
		private readonly Rbac rbac;
		public TrophiesController(AppDBContext dbContext, IMapper mapper, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment environment, IConfiguration Configuration, EmailHelper mailer, Rbac _rbac) :base(dbContext, httpContextAccessor, environment, Configuration)
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
		/// List  Trophies records
		/// Support searching, filtering and ordering of table records
		/// </summary>
		/// <param name="fieldname">Filter records based on table field.</param>
		/// <param name="fieldvalue">Filter value.</param>
   		/// <returns>JSON Array of Trophies records</returns>
		[HttpGet]
		public async Task<IActionResult> Index(string? fieldname, string? fieldvalue, string? search, string? orderby, string ordertype = "desc", int page = 1, int limit = 10){
			try{
				var query = (from Trophies in DB.Trophies
				join Teams in DB.Teams on Trophies.team_1 equals Teams.id
					select new Trophies{
						trophy_name = Trophies.trophy_name,
						team_1 = Trophies.team_1,
						teams_team_name = Teams.team_name.ToString(),
						team_2 = Trophies.team_2,
						id = Trophies.id,
						prize = Trophies.prize
					}
				);
				if(search != null){
					query = query.Where(
						p => EF.Functions.Like(p.trophy_name, $"%{search}%") 
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
				// export page records
				if (Request.Query.ContainsKey("export")){
					var exportRecords = query.ToList();
					return await ExportListRecords(exportRecords);
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
		/// Select single Trophies record by ID
		/// </summary>
		/// <param name="id">Table primary key.</param>
   		/// <returns>JSON Object of Trophies record</returns>
		[HttpGet]
		[Route("api/trophies/view/{id}")]
		public ActionResult Detail(int id){
			try{
				var query = (from Trophies in DB.Trophies
					select new Trophies{
						trophy_name = Trophies.trophy_name,
						team_1 = Trophies.team_1,
						team_2 = Trophies.team_2,
						id = Trophies.id,
						prize = Trophies.prize
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
   		/// <returns>JSON Object of newly created Trophies record</returns>
		[HttpPost]
		public ActionResult Add([FromBody] TrophiesAddDTO postdata){
			try{
				if (!ModelState.IsValid)
				{
					return BadRequest(ModelState);
				}
				var modeldata = Mapper.Map<Trophies>(postdata);
				// save Trophies record
				DB.Trophies.Add(modeldata);
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
		/// Get  Trophies record for edit
		/// </summary>
		/// <param name="id">Select record by table primary key.</param>
   		/// <returns>JSON Object of Trophies record</returns>
		[HttpGet]
		public ActionResult Edit(int id){
			try{
				var query = (
					from Trophies in DB.Trophies
					select new Trophies{
						trophy_name = Trophies.trophy_name,
						team_1 = Trophies.team_1,
						team_2 = Trophies.team_2,
						id = Trophies.id,
						prize = Trophies.prize
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
		/// Update  Trophies record with form data
		/// </summary>
		/// <param name="id">Select record by table primary key.</param>
   		/// <returns>JSON Object of Trophies record</returns>
		[HttpPost]
		public ActionResult Edit(int id, [FromBody] TrophiesEditDTO postdata){
			try{
				if (!ModelState.IsValid)
				{
					return BadRequest(ModelState);
				}
				var query = (
					from Trophies in DB.Trophies
					select Trophies
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
		/// Delete Trophies record
		/// Support multi delete by separating record id by comma.
		/// </summary>
		/// <param name="id">Record ID.</param>
   		/// <returns>Deleted Records ID</returns>
		[HttpGet]
		public ActionResult Delete(string id){
			try{
				var query = DB.Trophies.AsQueryable();
				List<string> arrId = id.Split(",").ToList();
				query = query.Where(p => arrId.Contains(p.id.ToString()));
				DB.Trophies.RemoveRange(query);
				DB.SaveChanges();
				return Ok(id);
			}
			catch (Exception ex){
				return ServerError(ex);
			}
		}
		

		/// <summary>
		/// Export  Trophies records to different format such as: pdf, cvs, excel, html
		/// </summary>
   		/// <returns>PDF | CSV | Excel | HTML File</returns>
		[HttpGet]
		private async Task<IActionResult> ExportListRecords(List<Trophies> records)
		{
			string exportFormat = Request.Query["export"].ToString().ToLower();
			string viewName = "ExportList"; // html template for pdf
			try{
				var fileName = "TrophiesReport";
				if (exportFormat == "excel" || exportFormat == "csv")
				{
					List<DataHeader> Columns = new List<DataHeader>()
					{
					    new DataHeader { Header = "Trophy Name", Key = "trophy_name" },
					    new DataHeader { Header = "Team 1", Key = "team_1" },
					    new DataHeader { Header = "Team 2", Key = "team_2" },
					    new DataHeader { Header = "Id", Key = "id" },
					    new DataHeader { Header = "Prize", Key = "prize" }
					};
					var dataTable = records.ToDataTable(Columns);
					dataTable.TableName = "Trophies"; // Excel worksheet title
					return ExportToExcel(dataTable, fileName, exportFormat);
				}
				else if (exportFormat == "pdf")
				{
					var htmlContent = await this.RenderViewAsync(viewName, records, true);
					var wkhtmltoExePath = Config["WKHTML_EXE_PATH"];
					var wkhtmltopdf = new FileInfo(wkhtmltoExePath);
					var converter = new HtmlToPdfConverter(wkhtmltopdf);
					var settings = new ConversionSettings(pageSize: PageSize.A3, orientation: PageOrientation.Portrait, enableExternalLinks: true, enableImages: true);
					var pdfBytes = converter.ConvertToPdf(htmlContent, Encoding.UTF8, settings);
					return File(pdfBytes, "application/pdf", $"{fileName}.pdf");
				}
				else if (exportFormat == "print")
				{
					return View(viewName, records);
				}
				return BadRequest("Export Format not Supported");
			}
			catch (Exception ex){
				return ServerError(ex);
			}
		}
		
	}
}
