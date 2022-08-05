var response = await PostAsync("https://discord.com/api/v9/auth/register", HttpMethod.Post,
    $"{{\"fingerprint\": \"{FingerPrint().Result}\", \"email\": \"{CreateMail().Result}\", \"password\": \"{RandomString(16)}\", \"username\": \"DoubtYou\", \"consent\": \"true\", \"date_of_birth\": \"2001-12-06\", \"captcha_key\": \"{RunCaptcha(CaptchaType.AntiCaptcha).Result}\", \"invite\": \"\", \"gift_code_sku_id\":\"null\", \"promotional_email_opt_in\": false}}",
    new[]
    {
        new IHttpHandler.RequestHeadersEx("X-Fingerprint", FingerPrint().Result),
        new IHttpHandler.RequestHeadersEx("X-Super-Properties",
            Base64Encode(
                "{\"os\":\"Windows\",\"browser\":\"Firefox\",\"device\":\"\",\"system_locale\":\"en-US\",\"browser_user_agent\":\"Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:103.0) Gecko/20100101 Firefox/103.0\",\"browser_version\":\"103.0\",\"os_version\":\"10\",\"referrer\":\"\",\"referring_domain\":\"\",\"search_engine\":\"bin\",\"referrer_current\":\"\",\"referring_domain_current\":\"\",\"release_channel\":\"stable\",\"client_build_number\":\"140242\",\"client_event_source\":null}")),
        new IHttpHandler.RequestHeadersEx("Referer", "https://discord.com/register"),
        new IHttpHandler.RequestHeadersEx("Origin", "https://discord.com"),
        new IHttpHandler.RequestHeadersEx("User-Agent", UserAgent),
        new IHttpHandler.RequestHeadersEx("Host", "discord.com"),
        new IHttpHandler.RequestHeadersEx("X-Discord-Locale", "en-US"),
        new IHttpHandler.RequestHeadersEx("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:103.0) Gecko/20100101 Firefox/103.0")
    });

Console.WriteLine(await response.Content.ReadAsStringAsync());
Console.ReadLine();
