using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain;

public class Activity
{
	[Key]
	[Column("id")]
	public string Id { get; set; } = Guid.NewGuid().ToString();

	[Column("title")]
	public required string Title { get; set; }

	[Column("date")]
	public DateTime Date { get; set; }

	[Column("description")]
	public required string Description { get; set; }

	[Column("category")]
	public required string Category { get; set; }

	[Column("is_cancelled")]
	public bool IsCancelled { get; set; }

	// Location Props
	[Column("city")]
	public required string City { get; set; }

	[Column("venue")]
	public required string Venue { get; set; }

	[Column("latitude")]
	public double Latitude { get; set; }

	[Column("longitude")]
	public double Longitude { get; set; }
}