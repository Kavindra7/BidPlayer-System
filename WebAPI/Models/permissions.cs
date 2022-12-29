using ASPRad.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ASPRad.Models{
	[Table("permissions")]
	public class Permissions : BaseRecord
	{
		[Key]
		public int permission_id { get; set; } = default!;
		public string permission { get; set; } = default!;
		public int? role_id { get; set; } = default!;
	}
	public class PermissionsAddDTO
	{
		[Required]
		public string permission { get; set; } = default!;
		public int? role_id { get; set; } = default!;
	}
	public class PermissionsEditDTO
	{
		[RequiredIfEmpty]
		public string? permission { get; set; } = default!;
		public int? role_id { get; set; } = default!;
	}
}
