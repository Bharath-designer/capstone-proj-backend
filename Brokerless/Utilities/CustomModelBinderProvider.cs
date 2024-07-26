using Brokerless.DTOs.Property;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Brokerless.Utilities
{
    public class CustomModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context.Metadata.ModelType == typeof(BasePropertyDTO))
            {
                return new ParentDtoModelBinder();
            }
            return null;
        }
    }
}
