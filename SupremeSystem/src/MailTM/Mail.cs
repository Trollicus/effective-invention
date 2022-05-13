namespace SupremeSystem.MailTM;

public static class Mail
{
    private static readonly Random Random = new Random();

    private const string Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

    public static string Base64Encode(string plainText) =>
        Convert.ToBase64String(Encoding.UTF8.GetBytes(plainText));

    public static string RandomString(int length) =>
        new string(Enumerable.Repeat(Chars, length).Select(s => s[Random.Next(s.Length)]).ToArray());

    private static readonly string Up =
        $"{{\"address\": \"{RandomString(8)}@{GetDomain().Result}\", \"password\": \"{RandomString(4)}\"}}";


    private static Json.Root? DeserializeRoot(string json) => JsonSerializer.Deserialize<Json.Root>(json);

    private static Json.CreateMailJson? DeserializeMail(string json) => JsonSerializer.Deserialize<Json.CreateMailJson>(json);
    
    private static Json.Discord? DeserializeFingerPrint(string json) => JsonSerializer.Deserialize<Json.Discord>(json);

    private static async Task<string?> GetDomain()
    {
        var response = await PostAsync("https://api.mail.tm/domains?page=1", HttpMethod.Get);

        var deserialize = DeserializeRoot(await response.Content.ReadAsStringAsync());

        return deserialize?.HydraMember[0].domain;
    }

    public static async Task<string> CreateMail()
    {
        var response = await PostAsync("https://api.mail.tm/accounts", HttpMethod.Post, Up);

        var deserialize = DeserializeMail(await response.Content.ReadAsStringAsync());

        return deserialize!.address;
    }

    public static async Task<string> FingerPrint()
    {
        var fingerPrint = await PostAsync("https://discord.com/api/v9/experiments", HttpMethod.Get);

        var deserialize = DeserializeFingerPrint(await fingerPrint.Content.ReadAsStringAsync());

        return deserialize!.fingerPrint;
    }
}