namespace SupremeSystem.Features;

public class Join
{
    public static async Task JoinServer(string invite, string authorization)
    {
        var request = await PostAsync($"https://discord.com/api/v9/invites/{invite}", HttpMethod.Post, "{}", new []
        {
            new RequestHeadersEx("X-Super-Properties", XSuperProperties),
            new RequestHeadersEx("User-Agent", UserAgent),
            new RequestHeadersEx("Referer", "https://discord.com/channels/@me")
        });
        
        var taskResultResponse = JsonSerializer.Deserialize<JError>(await request.Content.ReadAsStringAsync());
        
        if (request.StatusCode != HttpStatusCode.NoContent)
        {
#pragma warning disable CS8602
            throw new Exception($"{taskResultResponse.Code} : {taskResultResponse.Message}");
#pragma warning restore CS8602
        }
        
        Console.WriteLine(await request.Content.ReadAsStringAsync());
    }
}