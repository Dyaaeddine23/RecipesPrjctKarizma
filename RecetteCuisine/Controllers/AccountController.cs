using RecetteCuisine;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;
using System.Security.Cryptography;
using System.Text;
using System.Web.UI.WebControls;
using System.Linq;
using System;

public class AccountController : Controller
{
    public static class PasswordHasher
    {
        public static string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        internal static bool VerifyPassword(string passwordHash, string password)
        {
            throw new NotImplementedException();
        }
    }
    RecetteCuisineContent _context;

    public AccountController(RecetteCuisineContent context)
    {
        _context = context;
    }
 


public ActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public ActionResult Login(Login model)
    {
        if (ModelState.IsValid)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == model.UserName);
            if (user != null && PasswordHasher.VerifyPassword(user.PasswordHash, model.Password))
            {
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Invalid login attempt.");
        }

        return View(model);
    }

}
