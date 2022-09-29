using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TSUE.Models.Data;

namespace TSUE.Utils
{
    public class CustomHandlers
    {
        private BirdTsueDBContext _dbContext; //= new TranscriptAdminDbContext();

        public CustomHandlers(BirdTsueDBContext adminDbContext)
        {
            _dbContext = adminDbContext;
        }

        internal async Task<Task> InitializeUserClaims(TicketReceivedContext ctxt)
        {
            string role = "";
            string url = "";

            bool isAdmin = false;
            int officerId = 0;

            // Invoked after the remote ticket has been received.
            // Can be used to modify the Principal before it is passed to the Cookie scheme for sign-in.
            string username = ctxt.Principal.Claims.Where(x => x.Type.ToLower() == "Name".ToLower()).FirstOrDefault().Value;
            var user = _dbContext.ApplicationUsers.Where(x => x.Username == username && x.IsActive).FirstOrDefault();

            if (user != null)
            {
                var userRoleIds = _dbContext.ApplicationUserRoles
               .Where(x => x.ApplicationUserId == user.ApplicationUserId)
               .Select(x => x.RoleId).ToList();

                var roles = _dbContext.Roles.Where(x => userRoleIds.Contains(x.RoleId)).ToList();

                isAdmin = roles.Where(x => x.RoleName == UserRoles.Administrator.ToString()).FirstOrDefault() != null;

                if (roles.Count() == 1)
                {
                    role = roles.FirstOrDefault().RoleName;
                }
                


                if (isAdmin)
                {
                    SetUserSession(user.Username, user.ApplicationUserId, null, ctxt, _dbContext);

                    url = "/admin/index";
                }
                else
                {
                    url = "/account/unauthorized";
                }
                

                // var id_token_hint = await HttpContext.GetTokenAsync("id_token");

            }
            else
            {
                url = "/account/unauthorized";
            }

            if (ctxt.Principal.Identity is ClaimsIdentity identity)
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, role));
                identity.AddClaim(new Claim("PathURL", url));
                identity.AddClaim(new Claim("OfficerId", officerId.ToString()));
            }
            await Task.Yield();

            return Task.CompletedTask;
        }

        private void SetUserSession(string applicationUsername, int applicationUserId, int? officerOrAdminId, TicketReceivedContext ctxt, BirdTsueDBContext dbContext)
        {
            ctxt.HttpContext.Session.SetInt32(SessionValueKeys.applicationUserId, applicationUserId);

            ctxt.HttpContext.Session.SetString(SessionValueKeys.username, applicationUsername);
        }

    }

    public enum UserRoles
    {
        Applicant,
        TranscriptOfficer,
        Administrator,
        Support,
        Accounts

    }
}

