using ASPRad.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ASPRad.Models{
	[Table("trophies")]
	public class Trophies : BaseRecord
	{
		public string? trophy_name { get; set; } = default!;
		public int? team_1 { get; set; } = default!;
		public int? team_2 { get; set; } = default!;
		[Key]
		public int id { get; set; } = default!;
		public double? prize { get; set; } = default!;
[NotMapped]
		public string teams_team_name { get; set; } = default!;
	}
	public class TrophiesAddDTO
	{
		public string? trophy_name { get; set; } = default!;
		public int? team_1 { get; set; } = default!;
		public int? team_2 { get; set; } = default!;
		public double? prize { get; set; } = default!;
	}
	public class TrophiesEditDTO
	{
		public string? trophy_name { get; set; } = default!;
		public int? team_1 { get; set; } = default!;
		public int? team_2 { get; set; } = default!;
		public double? prize { get; set; } = default!;
	}
}
