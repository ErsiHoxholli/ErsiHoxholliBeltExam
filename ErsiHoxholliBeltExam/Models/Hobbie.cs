using System.ComponentModel.DataAnnotations;
namespace ErsiHoxholliBeltExam.Models;
#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations.Schema;

public class Hobbie
{
    [Key]
    public int HobbieId {get; set;}
    [Required]
    public string Name {get; set;}
    [Required]
    public string Description {get; set;}
    [Required]
    public int UserId {get; set;}
    public User? Creator {get; set;}
    public List<Enthusiast> Enthusiasts { get; set; } = new List<Enthusiast>();
    public DateTime CreatedAt {get; set;} = DateTime.Now;
    public DateTime UpdatedAt {get; set;} = DateTime.Now;


}