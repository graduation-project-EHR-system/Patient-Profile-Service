using MedicalRecords.Service.Api.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

public class ApiKeyAuthAttribute : Attribute, IAsyncActionFilter
{
    private const string API_KEY_HEADER_NAME = "x-api-key";

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var config = context.HttpContext.RequestServices.GetRequiredService<IOptions<ApiKeySettings>>();
        var configuredApiKey = config.Value.ApiKey;

        if (!context.HttpContext.Request.Headers.TryGetValue(API_KEY_HEADER_NAME, out var extractedApiKey))
        {
            context.Result = new ContentResult()
            {
                StatusCode = 401,
                Content = "API Key is missing."
            };
            return;
        }

        if (!configuredApiKey.Equals(extractedApiKey))
        {
            context.Result = new ContentResult()
            {
                StatusCode = 403,
                Content = "Invalid API Key."
            };
            return;
        }

        await next(); 
    }
}
