using Agency.MVC.Models;
using Agency.MVC.ViewModels.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Agency.MVC.Controllers
{
	public class AccountController : Controller
	{
		private readonly UserManager<AppUser> userManager;
		private readonly SignInManager<AppUser> signInManager;
		private readonly RoleManager<IdentityRole> roleManager;

		public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
			this.userManager = userManager;
			this.signInManager = signInManager;
			this.roleManager = roleManager;
		}
        public IActionResult Register()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Register(RegisterVM vm)
		{
			if(!ModelState.IsValid)
			{
				return View();
			}
			AppUser user = new AppUser()
			{
				Name = vm.Name,
				Surname = vm.Surname,
				Email = vm.Email,
				UserName = vm.Username
			};

			var result = await userManager.CreateAsync(user, vm.Password);
			if(!result.Succeeded)
			{
				foreach (var item in result.Errors)
				{
					ModelState.AddModelError("", item.Description);
				}
			}
			return RedirectToAction(nameof(Login));
		}
		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		public async  Task<IActionResult> Login(LoginVM vm)
		{
			var user = await userManager.FindByEmailAsync(vm.UsernameOrEmail);
			if (user == null)
			{
				user = await userManager.FindByNameAsync(vm.UsernameOrEmail);
				if (user == null) throw new Exception("Username or Password incorrect");
			}
			var result =  await signInManager.CheckPasswordSignInAsync(user, vm.Password, false);
			if (!result.Succeeded) throw new Exception("USername or password incorrect.");

			await signInManager.SignInAsync(user, false);
			return View();
			
		}
	}
}
