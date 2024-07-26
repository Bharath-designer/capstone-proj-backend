using Brokerless.DTOs.Property;
using Brokerless.Enums;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace Brokerless.Utilities
{
    public class ParentDtoModelBinder : IModelBinder
    {
        public async Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
                throw new ArgumentNullException(nameof(bindingContext));

            var request = bindingContext.HttpContext.Request;
            request.EnableBuffering();

            using (var reader = new StreamReader(request.Body))
            {
                var body = await reader.ReadToEndAsync();
                request.Body.Position = 0;
                await Console.Out.WriteLineAsync("---->"+body);
                var tempDto = JsonConvert.DeserializeObject<BasePropertyDTO>(body);
                BasePropertyDTO resultDto = null;

                switch (tempDto.PropertyType)
                {
                    case PropertyType.Product:
                        resultDto = JsonConvert.DeserializeObject<ProductDetailsDTO>(body);
                        break;
                    case PropertyType.Land:
                        resultDto = JsonConvert.DeserializeObject<LandDetailsDTO>(body);
                        break;
                    default:
                        bindingContext.Result = ModelBindingResult.Failed();
                        return;
                }

                bindingContext.Result = ModelBindingResult.Success(resultDto);
            }
        }
    }

}
