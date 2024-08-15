using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Razor;

namespace Presentation.Shared.Customizations;

public class FeatureViewLocationExpander : IViewLocationExpander
{
    #region Implementation of IViewLocationExpander

    public void PopulateValues(ViewLocationExpanderContext context)
    {
        //throw new NotImplementedException();
    }

    public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        if (viewLocations == null)
        {
            throw new ArgumentNullException(nameof(viewLocations));
        }

        var controllerActionDescriptor = context.ActionContext.ActionDescriptor as ControllerActionDescriptor;
        if (controllerActionDescriptor == null)
        {
            throw new NullReferenceException("ControllerActionDescriptor cannot be null.");
        }

        var featureName = controllerActionDescriptor.Properties["feature"] as string;
        foreach (var location in viewLocations)
        {
            yield return location.Replace("{3}", featureName);
        }
    }

    #endregion
}