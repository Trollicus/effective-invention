namespace SupremeSystem.Discord;

public class JError
{
    [JsonPropertyName("message")]
    public string? Message { get; set; }

    [JsonPropertyName("code")]
    public int Code { get; set; }
}