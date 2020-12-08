namespace NetClock.WebApi.Validators
{
    public interface IValidateParams
    {
        IValidateParams Equals<T>(T ida, T idb);
    }
}
