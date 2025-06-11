using MedicalRecords.Service.Api.Response;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace MedicalRecords.Service.Api.Middlewares
{
    public class TokenOrApiKeyAuthMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;

        public TokenOrApiKeyAuthMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var apiKey = context.Request.Headers["x-api-key"].FirstOrDefault();

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["JWT:SecurityKey"]);
            var tokenIsValid = false;

            if (!string.IsNullOrEmpty(token))
            {
                try
                {
                    tokenHandler.ValidateToken(token, new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = true,
                        ValidIssuer = _configuration["JWT:ValidIssuer"],
                        ValidateAudience = true,
                        ValidAudience = _configuration["JWT:ValidAudience"],
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero
                    }, out SecurityToken validatedToken);

                    tokenIsValid = true;
                }
                catch
                {
                    tokenIsValid = false;
                }
            }

            if (!tokenIsValid)
            {
                var validApiKey = _configuration["ApiKeySettings:apiKey"];

                if (string.IsNullOrEmpty(apiKey) || apiKey != validApiKey)
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsJsonAsync(new ApiResponse(401, "Unauthorized: Token or API Key is required."));
                    return;
                }
            }

            
            await _next(context);
        }
    }

}
