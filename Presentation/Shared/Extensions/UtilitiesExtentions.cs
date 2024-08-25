using FluentValidation.Results;
using Presentation.Shared.Application.Dtos.Response;
using Presentation.Shared.Application.Resorces;

namespace Presentation.Shared.Extensions;

public static class UtilitiesExtentions
{
    public static string ComputeSha256Hash(this string rawData)
    {
        using (var sha256Hash = System.Security.Cryptography.SHA256.Create())
        {
            byte[] bytes = sha256Hash.ComputeHash(System.Text.Encoding.UTF8.GetBytes(rawData));
            var builder = new System.Text.StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            return builder.ToString();
        }
    }

    public static T ExtractValidationerrors<T>(this ValidationResult result) where T :Response, new()
    {
        T response = new T();
        response.Message = Messages.FailedValidation;
        response.Success = false;
        foreach (var error in result.Errors)
        {
            if (response.ValidationErrors.ContainsKey(error.PropertyName) == false)
            {
                var errorList = new List<string>();
                errorList.Add(error.ErrorMessage);
                response.ValidationErrors.Add(error.PropertyName, errorList);
            }
            else
            {
                response.ValidationErrors[error.PropertyName].Add(error.ErrorMessage);
            }
        }

        return response;
    }
}