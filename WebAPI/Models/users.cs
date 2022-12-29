using ASPRad.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ASPRad.Models{
	[Table("users")]
	public class Users : BaseRecord
	{
		[Key]
		public int id { get; set; } = default!;
		public string? full_name { get; set; } = default!;
		public string username { get; set; } = default!;
		public string? email { get; set; } = default!;
		public string? password { get; set; } = default!;
		public int? age { get; set; } = default!;
		public string? gender { get; set; } = default!;
		public double? balance { get; set; } = default!;
		public int? user_role_id { get; set; } = default!;
[NotMapped]
		public string roles_role_name { get; set; } = default!;
	}
	public class UsersRegisterDTO
	{
		[RecordUnique("username", "users")]
		[Required]
		public string username { get; set; } = default!;
		[RecordUnique("email", "users")]
		[Required]
		[EmailAddress]
		public string? email { get; set; } = default!;
		[Required]
		public string? password { get; set; } = default!;
		[Compare("password")]
		[NotMapped]
		public string Confirm_Password { get; set; } = default!;
		public int? user_role_id { get; set; } = default!;
	}
	public class UsersAccounteditDTO
	{
		public string? full_name { get; set; } = default!;
		[RequiredIfEmpty]
		public string? username { get; set; } = default!;
		public int? age { get; set; } = default!;
		public string? gender { get; set; } = default!;
		public double? balance { get; set; } = default!;
		public int? user_role_id { get; set; } = default!;
	}
	public class UsersAddDTO
	{
		[RecordUnique("username", "users")]
		[Required]
		public string username { get; set; } = default!;
		[RecordUnique("email", "users")]
		[Required]
		[EmailAddress]
		public string? email { get; set; } = default!;
		[Required]
		public string? password { get; set; } = default!;
		[Compare("password")]
		[NotMapped]
		public string Confirm_Password { get; set; } = default!;
		public int? user_role_id { get; set; } = default!;
	}
	public class UsersEditDTO
	{
		public string? full_name { get; set; } = default!;
		[RequiredIfEmpty]
		public string? username { get; set; } = default!;
		public int? age { get; set; } = default!;
		public string? gender { get; set; } = default!;
		public double? balance { get; set; } = default!;
		public int? user_role_id { get; set; } = default!;
	}
}
