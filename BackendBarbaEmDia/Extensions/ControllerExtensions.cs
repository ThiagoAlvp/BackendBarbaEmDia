using BackendBarbaEmDia.Domain.Models.Responses;
using BackendBarbaEmDia.Models.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Collections;

namespace BackendBarbaEmDia.Extensions
{
    public static class ControllerExtensions
    {
        public static ActionResult<APIResponse<T>> TrataServiceResult<T>(this ControllerBase controller, ServiceResult<T> serviceResult) where T : class
        {
            if (serviceResult.ExceptionGenerated)
            {
                return controller.StatusCode(StatusCodes.Status500InternalServerError, (APIResponse<T>)serviceResult);
            }

            if (!serviceResult.Success)
            {
                return controller.BadRequest((APIResponse<T>)serviceResult);
            }

            if (serviceResult.Data is IEnumerable enumerable && !enumerable.Cast<object>().Any())
            {
                return controller.StatusCode(StatusCodes.Status204NoContent, (APIResponse<T>)serviceResult);
            }

            if (serviceResult.Data is null)
            {
                return controller.NotFound((APIResponse<T>)serviceResult);
            }

            return controller.Ok((APIResponse<T>)serviceResult);
        }
    }
}
