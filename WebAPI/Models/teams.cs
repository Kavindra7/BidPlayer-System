using ASPRad.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ASPRad.Models{
	[Table("teams")]
	public class Teams : BaseRecord
	{
		[Key]
		public int id { get; set; } = default!;
		public string? team_name { get; set; } = default!;
		public string? max_players { get; set; } = default!;
		public decimal? max_price { get; set; } = default!;
		public string? status { get; set; } = default!;
		public int? owner_id { get; set; } = default!;
[NotMapped]
		public string users_full_name { get; set; } = default!;
	}
	public class TeamsAddDTO
	{
		public string? team_name { get; set; } = default!;
		public string? max_players { get; set; } = default!;
		public decimal? max_price { get; set; } = default!;
		public string? status { get; set; } = default!;
		public int? owner_id { get; set; } = default!;
	}
	public class TeamsEditDTO
	{
		public string? team_name { get; set; } = default!;
		public string? max_players { get; set; } = default!;
		public decimal? max_price { get; set; } = default!;
		public string? status { get; set; } = default!;
		public int? owner_id { get; set; } = default!;
	}
}
