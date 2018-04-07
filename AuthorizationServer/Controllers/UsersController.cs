using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthorizationServer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AuthorizationServer.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private SignInManager<ApplicationUser> _signManager;
        private UserManager<ApplicationUser> _userManager;

        public UsersController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signManager)
        {
            _userManager = userManager;
            _signManager = signManager;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]RegisterUserRequest model)
        {
            var response = new UserResponse();

            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Username };

                try
                {
                    response.Result = await _userManager.CreateAsync(user, model.Password);
                    response.Succeeded = response.Result.Succeeded;
                    
                    if(!response.Result.Succeeded)
                    {
                        var errors = new List<string>();
                        foreach (var error in response.Result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                            errors.Add(error.Description);
                        }
                        response.Errors = errors;
                    }
                }
                catch (Exception ex)
                {
                    var errors = new List<string>() { ex.Message };
                    response.Errors = errors;
                }
            }
            else
            {
                var errors = new List<string>();

                foreach (var modelErrors in ModelState)
                {
                    string propertyName = modelErrors.Key;
                    errors.Add($"Not valid {propertyName}.");
                }
                response.Errors = errors;

                return BadRequest(response);
            }

            return Ok(response);
        }

        #region Login and Logout - not used at the moment

        //[HttpPost("login")]
        //public async Task<IActionResult> Login([FromBody]LoginRequest model)
        //{
        //    var response = new LoginResponse();

        //    if (ModelState.IsValid)
        //    {
        //        var result = await _signManager.PasswordSignInAsync(model.Username,
        //           model.Password, model.RememberMe, false);

        //        if (result.Succeeded)
        //        {
        //            response.IsSuccess = true;
        //            if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
        //            {
        //                response.ReturnUrl = model.ReturnUrl;
        //            }

        //            return Ok(response);                                       
        //        }
        //    }
        //    response.Error = "Invalid login attempt";
        //    return Ok(response);
        //}

        //[HttpPost]
        //public async Task<IActionResult> Logout()
        //{
        //    await _signManager.SignOutAsync();
        //    return Ok();
        //}

        #endregion

        // PUT api/<controller>/5/test
        [HttpPut("{id}/{username}")]
        public async Task<IActionResult> PutAsync(string id, string username, [FromBody]UserCustom model)
        {
            var response = new UserResponse();

            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(id);

                if (user == null || user.UserName != username)
                {
                    return BadRequest();
                }

                user.Email = model.Email;

                if (!String.IsNullOrEmpty(model.PhoneNumber))
                {
                    user.PhoneNumber = model.PhoneNumber;
                }

                response.Result = await _userManager.UpdateAsync(user);
                response.Succeeded = response.Result.Succeeded;

                return Ok(response);
            }
            else
            {
                var errors = new List<string>();

                foreach (var modelErrors in ModelState)
                {
                    string propertyName = modelErrors.Key;
                    errors.Add($"Not valid {propertyName}.");
                }
                response.Errors = errors;

                return BadRequest(response);
            }
        }

        // DELETE api/<controller>/5/test
        [HttpDelete("{id}/{username}")]
        public async Task<IActionResult> DeleteAsync(string id, string username)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null || user.UserName != username)
            {                
                return BadRequest();
            }
            var response = new UserResponse();

            response.Result = await _userManager.DeleteAsync(user);
            response.Succeeded = response.Result.Succeeded;

            return Ok(response);
        }
    }
}
