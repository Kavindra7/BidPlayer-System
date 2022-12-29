
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
	/// Controller for Users api
	/// </summary>
	public class UsersController:BaseController
	{
		private readonly IConfiguration Config;
		private readonly EmailHelper Mailer;
		private readonly IHttpContextAccessor HttpAccessor;
		private readonly AppDBContext DB;
		private readonly IMapper Mapper;
		private readonly IWebHostEnvironment hostEnvironment;
		private readonly Rbac rbac;
		public UsersController(AppDBContext dbContext, IMapper mapper, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment environment, IConfiguration Configuration, EmailHelper mailer, Rbac _rbac) :base(dbContext, httpContextAccessor, environment, Configuration)
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
		/// List  Users records
		/// Support searching, filtering and ordering of table records
		/// </summary>
		/// <param name="fieldname">Filter records based on table field.</param>
		/// <param name="fieldvalue">Filter value.</param>
   		/// <returns>JSON Array of Users records</returns>
		[HttpGet]
		public async Task<IActionResult> Index(string? fieldname, string? fieldvalue, string? search, string? orderby, string ordertype = "desc", int page = 1, int limit = 10){
			try{
				var query = (from Users in DB.Users
				join Roles in DB.Roles on Users.user_role_id equals Roles.role_id
					select new Users{
						id = Users.id,
						full_name = Users.full_name,
						username = Users.username,
						email = Users.email,
						age = Users.age,
						gender = Users.gender,
						balance = Users.balance,
						roles_role_name = Roles.role_name.ToString(),
						user_role_id = Users.user_role_id
					}
				);
				if(search != null){
					query = query.Where(
						p => EF.Functions.Like(p.full_name, $"%{search}%") || 
						EF.Functions.Like(p.username, $"%{search}%") || 
						EF.Functions.Like(p.email, $"%{search}%") 
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
		/// Select single Users record by ID
		/// </summary>
		/// <param name="id">Table primary key.</param>
   		/// <returns>JSON Object of Users record</returns>
		[HttpGet]
		[Route("api/users/view/{id}")]
		public ActionResult Detail(int id){
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
   		/// <returns>JSON Object of newly created Users record</returns>
		[HttpPost]
		public ActionResult Add([FromBody] UsersAddDTO postdata){
			try{
				if (!ModelState.IsValid)
				{
					return BadRequest(ModelState);
				}
				var modeldata = Mapper.Map<Users>(postdata);
				string passwordText = postdata.password;
				modeldata.password = Hash.ComputeHash(passwordText);
				// save Users record
				DB.Users.Add(modeldata);
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
		/// Get  Users record for edit
		/// </summary>
		/// <param name="id">Select record by table primary key.</param>
   		/// <returns>JSON Object of Users record</returns>
		[HttpGet]
		public ActionResult Edit(int id){
			try{
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
		/// Update  Users record with form data
		/// </summary>
		/// <param name="id">Select record by table primary key.</param>
   		/// <returns>JSON Object of Users record</returns>
		[HttpPost]
		public ActionResult Edit(int id, [FromBody] UsersEditDTO postdata){
			try{
				if (!ModelState.IsValid)
				{
					return BadRequest(ModelState);
				}
				var query = (
					from Users in DB.Users
					select Users
				);
				if (DB.Users.Any(p => p.username.Equals(postdata.username) &&  !p.id.Equals(id))){
					return BadRequest(postdata.username + " " + " Already exist!");
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
		/// Delete Users record
		/// Support multi delete by separating record id by comma.
		/// </summary>
		/// <param name="id">Record ID.</param>
   		/// <returns>Deleted Records ID</returns>
		[HttpGet]
		public ActionResult Delete(string id){
			try{
				var query = DB.Users.AsQueryable();
				List<string> arrId = id.Split(",").ToList();
				query = query.Where(p => arrId.Contains(p.id.ToString()));
				DB.Users.RemoveRange(query);
				DB.SaveChanges();
				return Ok(id);
			}
			catch (Exception ex){
				return ServerError(ex);
			}
		}
		

		/// <summary>
		/// Export  Users records to different format such as: pdf, cvs, excel, html
		/// </summary>
   		/// <returns>PDF | CSV | Excel | HTML File</returns>
		[HttpGet]
		private async Task<IActionResult> ExportListRecords(List<Users> records)
		{
			string exportFormat = Request.Query["export"].ToString().ToLower();
			string viewName = "ExportList"; // html template for pdf
			try{
				var fileName = "UsersReport";
				if (exportFormat == "excel" || exportFormat == "csv")
				{
					List<DataHeader> Columns = new List<DataHeader>()
					{
					    new DataHeader { Header = "Id", Key = "id" },
					    new DataHeader { Header = "Full Name", Key = "full_name" },
					    new DataHeader { Header = "Username", Key = "username" },
					    new DataHeader { Header = "Email", Key = "email" },
					    new DataHeader { Header = "Age", Key = "age" },
					    new DataHeader { Header = "Gender", Key = "gender" },
					    new DataHeader { Header = "Balance", Key = "balance" },
					    new DataHeader { Header = "User Role", Key = "user_role_id" }
					};
					var dataTable = records.ToDataTable(Columns);
					dataTable.TableName = "Users"; // Excel worksheet title
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
