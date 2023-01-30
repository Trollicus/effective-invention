namespace SupremeSystem.Captcha;

public static class Settings
{
    public const string CaptchaToken = ""; //Your Anti-Captcha or 2Captcha token. 

    public const string WebsiteToken = "4c672d35-0701-42b2-88c3-78380b0db560";

    public const string UserAgent =
        "Dalvik/2.1.0 (Linux; U; Android 8.0.0; Moto Z2 Play Build/OPSS27.76-12-28-15)";
    
    public static readonly string XSuperProperties =
        $"{{\"os\":\"Windows\",\"browser\":\"Firefox\",\"device\":\"\",\"system_locale\":\"en-US\",\"browser_user_agent\":\"{UserAgent}\",\"browser_version\":\"108.0\",\"os_version\":\"10\",\"referrer\":\"\",\"referring_domain\":\"\",\"referrer_current\":\"\",\"referring_domain_current\":\"\",\"release_channel\":\"stable\",\"client_build_number\":\"{ ReturnBuildNumber().Result}\",\"client_event_source\":null}}";
    
    public enum CaptchaType // Yet to be added more, feel free to contribute.
    {
        AntiCaptcha,
        TwoCaptcha,
    }

    public static Task<string?> RunCaptcha(CaptchaType type)
    {
        return type switch
        {
            CaptchaType.AntiCaptcha => GetSolution(),
            //CaptchaType.TwoCaptcha => GetSolution(),
            _ => Task.FromResult<string?>(null)
        };
    }
}