using System;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;

namespace yujvidya
{
    public static class Extensions
    {
        public static IActionResult RedirectBasedOnResult(this Controller controller, IActionResult actionResult)
        {
            var result = actionResult as ObjectResult;

            if (result.StatusCode.IsSuccessStatusCode())
                return controller.RedirectToAction("Index");

            return controller.BadRequest(result.Value);
        }

        public static async Task<IActionResult> ToActionResult(this HttpResponseMessage response)
        {
            var jsonString = await response.Content.ReadAsStringAsync();
            return new ObjectResult(jsonString) { StatusCode = (int)response.StatusCode };
        }
    }
}
