using System.ComponentModel.DataAnnotations;
namespace ErsiHoxholliBeltExam.Models;
#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations.Schema;

public class Enthusiast
{
    public int EnthusiastId { get; set; }
    public int UserId { get; set; }
    public int HobbieId { get; set; }
    public string Tipi { get; set; }
    public User? Person { get; set; }
    public Hobbie? Hobbie { get; set; }
}