using NetClock.IdentityServer.Apps.Consent;

namespace NetClock.IdentityServer.Apps.Device
{
    public class DeviceAuthorizationViewModel : ConsentViewModel
    {
        public string UserCode { get; set; }
        public bool ConfirmUserCode { get; set; }
    }
}
