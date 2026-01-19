using Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebApp.Controllers.Base
{
    public class AppBaseWebController : Controller
    {
        public string UserId
        {

            get
            {
                try
                {
                    return User.Claims.First(c => c.Type.Contains("nameidentifier")).Value;
                }
                catch
                {
                    // redirect to login. 
                    throw new Exception("");
                }
            }
        }
        public string Name
        {

            get
            {
                try
                {
                    return User.Claims.First(c => c.Type.Contains("Name")).Value;
                }
                catch
                {
                    // redirect to login. 
                    throw new Exception("");
                }
            }
        }
        public string EmailAddress
        {
            get
            {
                try
                {
                    return User.Claims.First(c => c.Type.Contains("emailaddress")).Value;

                }
                catch
                {
                    // redirect to login. 
                    throw new Exception("");
                }
            }
        }

        public string Role
        {
            get
            {
                try
                {
                    return User.Claims.First(c => c.Type.Contains("role")).Value;

                }
                catch
                {
                    // redirect to login. 
                    throw new Exception("");
                }
            }
        }

    }
}
