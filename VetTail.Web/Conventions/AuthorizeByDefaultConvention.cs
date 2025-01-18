using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace VetTail.Web.Conventions;

public sealed class AuthorizeByDefaultConvention : IActionModelConvention
{
    public void Apply(ActionModel action)
    {
        if (
            action.Attributes.Any(attribute => attribute.GetType().Equals(typeof(AuthorizeAttribute))) ||
            action.Attributes.Any(attribute => attribute.GetType().Equals(typeof(AllowAnonymousAttribute)))
        ) return;

        action.Filters.Add(new AuthorizeFilter());
    }
}
