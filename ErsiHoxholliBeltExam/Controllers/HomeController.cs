using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ErsiHoxholliBeltExam.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ErsiHoxholliBeltExam.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private MyContext _context;

    public HomeController(ILogger<HomeController> logger,MyContext context )
    {
        _logger = logger;
         _context = context;
    }

    public IActionResult Index()
    {
        if (HttpContext.Session.GetInt32("UserId") == null)
        {   
            return RedirectToAction ("Register"); 
        }

        ViewBag.hobbies = _context.Hobbies.Include(e => e.Creator).ThenInclude(e => e.Enthusiasts).ToList();
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [HttpGet("Register")]
    public IActionResult Register(){
        if (HttpContext.Session.GetInt32("userId") == null)
        {
            return View();
        }
        return RedirectToAction("Index"); 
    }

    [HttpPost("Register")]
    public IActionResult Register(User user)
    {
        if (ModelState.IsValid)
        {
            if (_context.Users.Any(u => u.UserName == user.UserName))
            {
                ModelState.AddModelError("Username", "UserName is already in use!");

                return View("Register");
            }

            PasswordHasher<User> Hasher = new PasswordHasher<User>();
            user.Password = Hasher.HashPassword(user, user.Password);
            _context.Users.Add(user);
            _context.SaveChanges();
            User Userdb = _context.Users.FirstOrDefault(u => u.UserName == user.UserName);

            HttpContext.Session.SetInt32("UserId", Userdb.UserId);
            int IntVariable = (int)HttpContext.Session.GetInt32("UserId");

            return RedirectToAction("Index");
        }
        else
        {
            return View("Register");
        }
    }

    [HttpPost("Login")]
    public IActionResult Login(LoginUser user)
    {
        if (ModelState.IsValid)
        {
            var userInDb = _context.Users.FirstOrDefault(u => u.UserName == user.UserName);
            if (userInDb == null)
            {
                ModelState.AddModelError("Username", "Invalid UserName/Password");
                return View("Register");
            }

            var hasher = new PasswordHasher<LoginUser>();
            var result = hasher.VerifyHashedPassword(user, userInDb.Password, user.Password);

            if (result == 0)
            {
                ModelState.AddModelError("Password", "Invalid Password");
                return View("Register");
            }
            HttpContext.Session.SetInt32("UserId", userInDb.UserId);
            return RedirectToAction("Index");
        }
        return View("Register");
    }

    // [HttpGet("Logout")]
    // public IActionResult Logout()
    // {
    //     HttpContext.Session.Clear();
    //     return View("register");
    // }


        //Hobbies Part

    [HttpGet("HobbieAdd")]
    public IActionResult HobbieAdd()
    {
        return View();
    }

    [HttpPost("Hobbie/Create")]
    public IActionResult HobbieCreate(Hobbie marrNgaView){
        if (ModelState.IsValid)
        {   
            if (_context.Hobbies.Any(u => u.Name == marrNgaView.Name))
            {
                ModelState.AddModelError( "Name", "Name is already in use!");

                return View("HobbieAdd");
            }
            int id =(int) HttpContext.Session.GetInt32("UserId");
            marrNgaView.UserId = id;
            _context.Hobbies.Add(marrNgaView);
            _context.SaveChanges();
            return RedirectToAction ("Index");
        }
        return View("HobbieAdd");
    }

    [HttpGet("ShowHobbie/{id}")]
    public IActionResult ShowHobbie(int id)
    {  
        ViewBag.showHobbie = _context.Hobbies.Include(e=>e.Enthusiasts).ThenInclude(e=>e.Person).FirstOrDefault(e => e.HobbieId == id);
        return View();
    }

    [HttpGet("Edit/Hobbie/{id}")]
    public IActionResult EditHobbie(int id)
    {
        ViewBag.EditHobbie = id;
        Hobbie EditHobbie = _context.Hobbies.FirstOrDefault(i => i.HobbieId == id);
        return View(EditHobbie);
    }

    [HttpPost("Edited/Hobbie/{id}")]
    public IActionResult EditedHobbie(int id ,Hobbie marreNgaAdd)
    {
        Hobbie dukeEdituar = _context.Hobbies.FirstOrDefault(b => b.HobbieId == id);
        if (ModelState.IsValid)
        {   
            if (_context.Hobbies.Any(u => u.Name == marreNgaAdd.Name))
            {

                return RedirectToAction("EditHobbie", new {id=id});
            }
            dukeEdituar.Name = marreNgaAdd.Name;
            dukeEdituar.Description = marreNgaAdd.Description;
            dukeEdituar.UpdatedAt = DateTime.Now;
            _context.SaveChanges();
            return RedirectToAction("ShowHobbie" , new {id = id});  
        }
        else
        {
           return RedirectToAction("EditHobbie" , new {id = id});
        }
    }

    
    [HttpPost("CreateEnthusiast/{id}")]
    public IActionResult CreateEnthusiast(int id, string type){   


            int id2 =(int)HttpContext.Session.GetInt32("UserId");
            Enthusiast abc = new Enthusiast(){
                UserId = id2,
                HobbieId = id,
                Tipi = type
            };
            _context.Add(abc);
            _context.SaveChanges();
            return RedirectToAction("ShowHobbie" , new {id = id});


    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
