using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Repository.Models;

public partial class FootballPlayer
{
    [Key]
    [Required]
    public string FootballPlayerId { get; set; } = null!;
    [Required]
    [MinLength(6)]
    [BeginWithCapitalLetterAttribute]
    public string FullName { get; set; } = null!;
    [Required]
    public string Achievements { get; set; } = null!;
    [Required]
    public DateTime? Birthday { get; set; }
    [Required]
    public string PlayerExperiences { get; set; } = null!;
    [Required]
    public string Nomination { get; set; } = null!;
    [Required]
    public string? FootballClubId { get; set; }
    [JsonIgnore]
    public virtual FootballClub? FootballClub { get; set; }
}
