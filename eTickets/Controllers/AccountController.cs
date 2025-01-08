using eTickets.Data;
using eTickets.Data.Static;
using eTickets.Data.ViewModels;
using eTickets.DTO;
using eTickets.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Controllers
{

    public class AccountController : Controller
        {
            private readonly UserManager<ApplicationUser> _userManager;
            private readonly SignInManager<ApplicationUser> _signInManager;
            private readonly AppDbContext _context;

            public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, AppDbContext context)
            {
                _userManager = userManager;
                _signInManager = signInManager;
                _context = context;
            }

        public async Task<IActionResult> Users()
        {
            var users = await _context.Users.ToListAsync();
            return View(users);
        }


        public IActionResult Login()
            {
                return View(new LoginVM());
            }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (!ModelState.IsValid) return View(loginVM);

            var user = await _userManager.FindByEmailAsync(loginVM.EmailAddress);
            if (user != null)
            {
                var passwordCheck = await _userManager.CheckPasswordAsync(user, loginVM.Password);
                if (passwordCheck)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Movies");
                    }
                }
                TempData["Error"] = "Wrong credentials. Please, try again!";
                return View(loginVM);
            }

            TempData["Error"] = "Wrong credentials. Please, try again!";
            return View(loginVM);
        }

        public IActionResult Register() => View(new RegisterDTO());
        [HttpPost]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            if (!ModelState.IsValid) return View(registerDTO);

            var user = await _userManager.FindByEmailAsync(registerDTO.EmailAddress);
            if (user != null)
            {
                TempData["Error"] = "This email address is already in use";
                return View(registerDTO);
            }

            var newUser = new ApplicationUser()
            {
                FullName = registerDTO.FullName,
                Email = registerDTO.EmailAddress,
                UserName = registerDTO.EmailAddress
            };
            var newUserResponse = await _userManager.CreateAsync(newUser, registerDTO.Password);

            if (newUserResponse.Succeeded)
                await _userManager.AddToRoleAsync(newUser, UserRoles.User);

            return View("RegisterCompleted");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Movies");
        }

    }
}