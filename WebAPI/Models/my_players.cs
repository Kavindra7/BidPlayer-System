using ASPRad.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ASPRad.Models{
	[Table("my_players")]
	public class My_Players : BaseRecord
	{
		[Key]
		public int id { get; set; } = default!;
		public int? player_name { get; set; } = default!;
		public decimal? bid_price { get; set; } = default!;
		public string? status { get; set; } = default!;
		public int? bidder_id { get; set; } = default!;
[NotMapped]
		public string users_username { get; set; } = default!;
	}
	public class MyPlayersAddDTO
	{
		public int? player_name { get; set; } = default!;
		public decimal? bid_price { get; set; } = default!;
		public string? status { get; set; } = default!;
		public int? bidder_id { get; set; } = default!;
	}
	public class MyPlayersEditDTO
	{
		public int? player_name { get; set; } = default!;
		public decimal? bid_price { get; set; } = default!;
		public string? status { get; set; } = default!;
	}
}
