﻿using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using ITJakub.Web.Hub.Core.Communication;
using ITJakub.Web.Hub.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Authentication;
using Vokabular.MainService.DataContracts.Contracts;

namespace ITJakub.Web.Hub.Core.Managers
{
    public class AuthenticationManager
    {
        public const string AuthenticationTokenName = "AuthorizationToken";
        private readonly IHttpContextAccessor m_httpContextAccessor;
        private readonly CommunicationProvider m_communicationProvider;

        public AuthenticationManager(IHttpContextAccessor httpContextAccessor, CommunicationProvider communicationProvider)
        {
            m_httpContextAccessor = httpContextAccessor;
            m_communicationProvider = communicationProvider;
        }

        public async Task SignInAsync(LoginViewModel model)
        {
            using (var client = m_communicationProvider.GetMainServiceClient())
            {
                var signInResult = client.SignIn(new SignInContract
                {
                    Username = model.UserName,
                    Password = model.Password,
                });

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, model.UserName),
                    //new Claim("LastChanged", {Database Value})
                };

                var claimsIdentity = new ClaimsIdentity(
                    claims,
                    CookieAuthenticationDefaults.AuthenticationScheme);

                var authenticationProperties = new AuthenticationProperties();
                authenticationProperties.StoreTokens(new[]
                {
                    new AuthenticationToken
                    {
                        Name = AuthenticationTokenName,
                        Value = signInResult.CommunicationToken
                    }
                });

                await m_httpContextAccessor.HttpContext.Authentication.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity), authenticationProperties);
            }
            
            // var test = await m_httpContextAccessor.HttpContext.Authentication.GetTokenAsync(AuthenticationTokenName); // Getting token (in next request)
        }

        public async Task SignOutAsync()
        {
            using (var client = m_communicationProvider.GetMainServiceClient())
            {
                client.SignOut();

                await m_httpContextAccessor.HttpContext.Authentication.SignOutAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme);
            }
        }
    }
}
