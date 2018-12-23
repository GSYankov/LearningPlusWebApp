using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Security.Claims;
using LearningPlus.Models;
using AutoMapper;
using LearningPlus.Web.ViewModels;
using LearningPlus.Web.ViewModels.Users;

namespace Eventures.Controllers
{
    public class UsersController : Controller
    {
        private UserManager<LearningPlusUser> userManager;
        private SignInManager<LearningPlusUser> signInManager;
        private readonly IMapper mapper;

        public UsersController(UserManager<LearningPlusUser> userMgr,
                SignInManager<LearningPlusUser> signinMgr, IMapper mapper)
        {
            userManager = userMgr;
            signInManager = signinMgr;
            this.mapper = mapper;
        }

        public IActionResult Login()
        {

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(DoLoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                LearningPlusUser user = await userManager.FindByNameAsync(model.Username);
                if (user != null)
                {
                    await signInManager.SignOutAsync();
                    Microsoft.AspNetCore.Identity.SignInResult result = await signInManager
                        .PasswordSignInAsync(user, model.Password, model.RememberMe, false);
                    if (result.Succeeded)
                    {
                        return Redirect(returnUrl ?? "/");
                    }
                }

                ModelState.AddModelError(nameof(DoLoginViewModel.Username), "Invalid user or password");
            }
            return View(model);
        }

        public IActionResult RegisterStudent()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterStudent(RegisterStudentViewModel model)
        {
            //TODO: Set Username to be only in latin chars

            //TODO: To implemrnt return URL

            var IsParentUserNameTacken = await this.userManager.FindByNameAsync(model.ParentUserName);
            if (IsParentUserNameTacken != null)
            {
                ModelState.AddModelError(nameof(RegisterStudentViewModel.ParentUserName), "Потребителското име е вече заето.");
            }

            var IsChaildUserNameTacken = await this.userManager.FindByNameAsync(model.ParentUserName);
            if (IsChaildUserNameTacken != null)
            {
                ModelState.AddModelError(nameof(RegisterStudentViewModel.ChildUserName), "Потребителското име е вече заето.");
            }

            if (ModelState.IsValid)
            {
                var child = new LearningPlusUser
                {
                    FirstName = model.ChildFirstName,
                    LastName = model.ChildLastName,
                    PhoneNumber = model.ChildPhoneNumber,
                    Email = model.ChildEmail,
                    UserName = model.ChildUserName
                };

                await this.userManager.CreateAsync(child, model.ChaildPassword);
                if (this.User.IsInRole("Teacher"))
                {
                    await this.userManager.AddToRoleAsync(child, "Child");
                }


                var parent = new LearningPlusUser
                {
                    FirstName = model.ParentFirstName,
                    LastName = model.ParentLastName,
                    PhoneNumber = model.ParentPhoneNumber,
                    Email = model.ParentEmail,
                    UserName = model.ParentUserName
                };

                parent.Children.Add(child);

                await this.userManager.CreateAsync(parent, model.ParentPassword);
                if (this.User.IsInRole("Teacher"))
                {
                    await this.userManager.AddToRoleAsync(parent, "Parent");
                    return Redirect("/");
                }

                return RedirectToAction("Login", "Users");
            }

            return this.View(model);
        }

        public IActionResult Register()
        {

            return View();
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public IActionResult FacebookLogin(string returnUrl)
        {
            string redirectUrl = Url.Action("FacebookResponse", "Users", new { ReturnUrl = returnUrl });
            var properties = signInManager
                .ConfigureExternalAuthenticationProperties("Facebook", redirectUrl);
            return new ChallengeResult("Facebook", properties);
        }

        [AllowAnonymous]
        public async Task<IActionResult> FacebookResponse(string returnUrl = "/")
        {
            ExternalLoginInfo info = await signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return RedirectToAction(nameof(Login));
            }
            var result = await signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, false);
            if (result.Succeeded)
            {
                return Redirect(returnUrl);
            }
            else
            {
                LearningPlusUser user = new LearningPlusUser
                {
                    Email = info.Principal.FindFirst(ClaimTypes.Email).Value,
                    UserName =
                        info.Principal.FindFirst(ClaimTypes.Email).Value
                };
                IdentityResult identResult = await userManager.CreateAsync(user);
                if (identResult.Succeeded)
                {
                    identResult = await userManager.AddLoginAsync(user, info);
                    if (identResult.Succeeded)
                    {
                        await signInManager.SignInAsync(user, false);
                        return Redirect(returnUrl);
                    }
                }
                return RedirectToAction(nameof(Login));
            }
        }

    }
}
