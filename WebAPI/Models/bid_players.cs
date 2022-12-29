using ASPRad.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ASPRad.Models{
	[Table("bid_players")]
	public class Bid_Players : BaseRecord
	{
		[Key]
		public int id { get; set; } = default!;
		public int? owner_id { get; set; } = default!;
		public int? player_id { get; set; } = default!;
		public int? thophy_id { get; set; } = default!;
		public decimal? bid_price { get; set; } = default!;

        public DateTime? created_at { get; set; } = DateTime.Now;
        public string? status { get; set; } = default!;
[NotMapped]
		public string trophies_trophy_name { get; set; } = default!;
	}
	public class BidPlayersAddDTO
	{
		public int? owner_id { get; set; } = default!;
		public int? player_id { get; set; } = default!;
		public int? thophy_id { get; set; } = default!;
		public decimal? bid_price { get; set; } = default!;
        public DateTime? created_at { get; set; } = DateTime.Now;
        public string? status { get; set; } = default!;
	}
	public class BidPlayersEditDTO
	{
	}
}
