using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace TimeSheetManagementSystem.Models.Claims
{
    public class ClaimsTransformer : IClaimsTransformer
    {
        //http://benfoster.io/blog/customising-claims-transformation-in-aspnet-core-identity
        //You need to refer to the comments and discussions at the bottm to find out the code changes
        //for this TransformAsync method which I have placed here.
        //The purpose is to create a new Claim having the name, InstructorId
        public Task<ClaimsPrincipal> TransformAsync(ClaimsTransformationContext context)
        {
            ClaimsPrincipal principal = context.Principal;
            ((ClaimsIdentity)principal.Identity).AddClaim(new Claim("InstructorId", ""));
            return Task.FromResult(principal);
        }

    }
}
