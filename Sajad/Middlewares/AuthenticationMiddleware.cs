using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Sajad.Middlewares
{

    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;

        // Dependency Injection
        public AuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            //Reading the AuthHeader which is signed with JWT
            string authHeader = context.Request.Query["authToken"];

            if (authHeader != null)
            {
                //Reading the JWT middle part           
                int startPoint = authHeader.IndexOf(".") + 1;
                int endPoint = authHeader.LastIndexOf(".");

                var tokenString = authHeader
                    .Substring(startPoint, endPoint - startPoint).Split(".");
                var token = tokenString[0].ToString(CultureInfo.InvariantCulture);

                var credentialString = Encoding.UTF8
                    .GetString(ConvertFromBase64String(token));

                // Splitting the data from Jwt
                var credentials = credentialString.Split(new char[] { ':', ',' });

                // Trim this Username and UserRole.
                var userRule = credentials[1].Replace("\"", "");
                var userName = credentials[3].Replace("\"", "");
                var id = credentials[5].Replace("\"", "");

                // Identity Principal
                var claims = new[]
                {
                    new Claim("name", userName),
                    new Claim(ClaimTypes.Role, userName),
                    new Claim("Id", id),
                };
                var identity = new ClaimsIdentity(claims, "basic");
                context.User = new ClaimsPrincipal(identity);
            }
            //Pass to the next middleware
            await _next(context);
        }

        private static byte[] ConvertFromBase64String(string input)
        {
            if (String.IsNullOrWhiteSpace(input)) return null;
            try
            {
                string working = input.Replace('-', '+').Replace('_', '/'); ;
                while (working.Length % 4 != 0)
                {
                    working += '=';
                }
                return Convert.FromBase64String(working);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
