using NetClock.IdentityServer.Apps.Consent;

namespace NetClock.IdentityServer.Apps.Device
{
    public class DeviceAuthorizationInputModel : ConsentInputModel
    {
        public string UserCode { get; set; }
    }
}
