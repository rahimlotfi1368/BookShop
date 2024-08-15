using System.Reflection;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace Presentation.Shared.Customizations;

public class FeatureConvention : IControllerModelConvention
{
    #region Implementation of IControllerModelConvention

    public void Apply(ControllerModel controller)
    {
        var featureName = GetFeatureName(controller.ControllerType);
        controller.Properties.Add("feature", featureName);
    }

    private string GetFeatureName(TypeInfo controllerType)
    {
        var tokens = controllerType.FullName?.Split('.');
        if (!tokens.Any(t => t == "Features"))
        {
            return "";
        }

        var featureName = tokens
            .SkipWhile(t => !t.Equals("features", StringComparison.CurrentCultureIgnoreCase))
            .Skip(1)
            .Take(1)
            .FirstOrDefault();

        return featureName;
    }

    #endregion
}