using FluentValidation;
using FluentValidation.Results;
using Newtonsoft.Json;

public class ValidationMiddleware<T> where T : class
{
    private readonly RequestDelegate _next;
    private readonly IValidator<T> _validator;

    public ValidationMiddleware(RequestDelegate next, IValidator<T> validator)
    {
        _next = next;
        _validator = validator;
    }

    public async Task Invoke(HttpContext context)
    {
        if (context.Request.Method == "POST" || context.Request.Method == "PUT")
        {
            string requestBody;
            using (var reader = new StreamReader(context.Request.Body))
            {
                requestBody = await reader.ReadToEndAsync();
            }

            var model = JsonConvert.DeserializeObject<T>(requestBody);

            var validationResult = await _validator.ValidateAsync(model);

            if (!validationResult.IsValid)
            {
                var errors = GetErrorMessages(validationResult.Errors);
                var errorsJson = JsonConvert.SerializeObject(errors);

                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(errorsJson);
                return;
            }
        }

        await _next.Invoke(context);
    }

    private List<ValidationError> GetErrorMessages(List<ValidationFailure> validationErrors)
    {
        return validationErrors
            .Select(error => new ValidationError
            {
                PropertyName = error.PropertyName,
                ErrorMessage = error.ErrorMessage
            })
            .ToList();
    }
}

public class ValidationError
{
    public string PropertyName { get; set; }
    public string ErrorMessage { get; set; }
}
