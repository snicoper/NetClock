using Microsoft.AspNetCore.Http;

namespace NetClock.WebApi.Validators
{
    public class ValidateParams : IValidateParams
    {
        public IValidateParams Equals<T>(T ida, T idb)
        {
            if (!ida.Equals(idb))
            {
                throw new BadHttpRequestException($"Los parámetros {ida} y {idb} no parece iguales");
            }

            return this;
        }
    }
}
