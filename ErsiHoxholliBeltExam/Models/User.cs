#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ErsiHoxholliBeltExam.Models;

public class User
{
[Key]
public int UserId {get; set;}

[Required]
[MinLength (2, ErrorMessage ="First Name must be 2 Characters minimum")]
public string FirstName {get; set;}

[Required]
[MinLength (2, ErrorMessage ="First Name must be 2 Characters minimum")]
public string LastName {get; set;}


[Required]
public string UserName {get; set;}

[Required]
[DataType(DataType.Password)]
[MinLength(8, ErrorMessage =" Password must be at least 8 Characters")]
public string Password {get; set;}

public DateTime CreatedAt {get; set;} = DateTime.Now;
public DateTime UpdatedAt {get; set;} = DateTime.Now;

 public List<Hobbie> CreatedHobbies { get; set; } = new List<Hobbie>(); 
 public List<Enthusiast> Enthusiasts {get; set;} = new List<Enthusiast>();

[NotMapped]
[DataType(DataType.Password)]
[Compare("Password")]
public string Confirm {get; set;}
    
}

public class LoginUser 
{
[Required]
public string UserName {get; set;}

[Required]
[DataType(DataType.Password)]
[MinLength(8, ErrorMessage =" Password must be at least 8 Characters")]
public string Password {get; set;}
}