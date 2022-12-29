
using System;
using System.Collections.Generic;
using System.Linq;
using ASPRad.Models;
using ASPRad.Helpers;
public class Rbac
{
	private readonly AppDBContext DB;

	public Rbac(AppDBContext dbContext)
	{
		DB = dbContext;
	}
	private List<string> ExcludePages = new List<string>() { "auth", "account", "home","components_data" };

	public List<string> GetRolePages(int? roleId)
	{
		var records = DB.Permissions.Where(p => p.role_id == roleId).ToList();
		return records.Select(x => x.permission).ToList();
	}

	public List<string> GetRoleNames(int? roleId)
	{
		var records = DB.Roles.Where(p => p.role_id == roleId).ToList();
		return records.Select(x => x.role_name).ToList();
	}

	public bool GetPageAccess(int? roleId, string page, string action)
	{
		if (ExcludePages.Contains(page, StringComparer.OrdinalIgnoreCase))
		{
			return true;
		}

		if (action == "" || action.ToLower() == "index")
		{
			action = "list";
		}
		else if (action.ToLower() == "detail")
		{
			action = "view";
		}
		var pagePath = $"{page}/{action}".ToLower();
		List<string> userPages  = GetRolePages(roleId);
		return userPages.Contains(pagePath, StringComparer.OrdinalIgnoreCase);
	}
}
