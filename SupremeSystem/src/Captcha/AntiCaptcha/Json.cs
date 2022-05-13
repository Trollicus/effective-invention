namespace SupremeSystem.Captcha.AntiCaptcha;

public class Json
{
    public class GetTaskResult
    {
        [JsonPropertyName("taskId")] public uint? TaskId { get; set; }
        
        [JsonPropertyName("clientKey")] public string? clientKei { get; set; }
    }

    public class CreateTaskResult
    {
        [JsonPropertyName("errorId")] public ushort ErrorId { get; set; }

        [JsonPropertyName("taskId")] public uint? TaskId { get; set; }

        [JsonPropertyName("errorCode")] public string? ErrorCode { get; set; }

        [JsonPropertyName("errorDescription")] public string? ErrorDescription { get; set; }
    }

    public class GetTaskResultResponse
    {
        [JsonPropertyName("status")] public string? CurrentStatus { get; set; }

        [JsonPropertyName("solution")] public ResultSolution? Solution { get; set; }

        [JsonPropertyName("errorId")] public ushort ErrorId { get; set; }

        [JsonPropertyName("errorCode")] public string? ErrorCode { get; set; }

        [JsonPropertyName("errorDescription")] public string? ErrorDescription { get; set; }

        public class ResultSolution
        {
            [JsonPropertyName("gRecaptchaResponse")] public string? CaptchaSolution { get; set; }
            
        }


    }
}