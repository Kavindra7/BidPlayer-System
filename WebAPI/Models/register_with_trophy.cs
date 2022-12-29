using ASPRad.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ASPRad.Models{
	[Table("register_with_trophy")]
	public class Register_With_Trophy : BaseRecord
	{
		[Key]
		public int id { get; set; } = default!;
		public int? trophy_id { get; set; } = default!;
		public int? player_id { get; set; } = default!;
[NotMapped]
		public string trophies_trophy_name { get; set; } = default!;
[NotMapped]
		public string users_full_name { get; set; } = default!;
	}
	public class RegisterWithTrophyAddDTO
	{
		[Required]
		public int? trophy_id { get; set; } = default!;
		public int? player_id { get; set; } = default!;
	}
	public class RegisterWithTrophyEditDTO
	{
		[RequiredIfEmpty]
		public int? trophy_id { get; set; } = default!;
	}
}
