#pragma warning disable CS8618
namespace SupremeSystem.MailTM;

public class Json
{
    public class HydraMember
    {
        [JsonPropertyName("@id")] public string Id { get; set; }

        [JsonPropertyName("@type")] public string Type { get; set; }

        [JsonPropertyName("@context")] public string Context { get; set; }
        public string id { get; set; }
        public string? domain { get; set; }
        public bool isActive { get; set; }
        public bool isPrivate { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
    }

    public class HydraView
    {
        [JsonPropertyName("@id")] public string Id { get; set; }

        [JsonPropertyName("@type")] public string Type { get; set; }

        [JsonPropertyName("hydra:first")] public string HydraFirst { get; set; }

        [JsonPropertyName("hydra:last")] public string HydraLast { get; set; }

        [JsonPropertyName("hydra:previous")] public string HydraPrevious { get; set; }

        [JsonPropertyName("hydra:next")] public string HydraNext { get; set; }
    }

    public class HydraMapping
    {
        [JsonPropertyName("@type")] public string Type { get; set; }
        public string variable { get; set; }
        public string property { get; set; }
        public bool required { get; set; }
    }

    public class HydraSearch
    {
        [JsonPropertyName("@type")] public string Type { get; set; }

        [JsonPropertyName("hydra:template")] public string HydraTemplate { get; set; }

        [JsonPropertyName("hydra:variableRepresentation")]
        public string HydraVariableRepresentation { get; set; }

        [JsonPropertyName("hydra:mapping")] public List<HydraMapping> HydraMapping { get; set; }
    }

    public class Root
    {
        [JsonPropertyName("hydra:member")] public List<HydraMember> HydraMember { get; set; }

        [JsonPropertyName("hydra:totalItems")] public int HydraTotalItems { get; set; }

        [JsonPropertyName("hydra:view")] public HydraView HydraView { get; set; }

        [JsonPropertyName("hydra:search")] public HydraSearch HydraSearch { get; set; }
    }

    public class CreateMailJson
    {
        [JsonPropertyName("@context")] public string Context { get; set; }

        [JsonPropertyName("@id")] public string Id { get; set; }

        [JsonPropertyName("@type")] public string Type { get; set; }
        public string id { get; set; }
        public string address { get; set; }
        public int quota { get; set; }
        public int used { get; set; }
        public bool isDisabled { get; set; }
        public bool isDeleted { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
    }

    public class Discord
    {
        [JsonPropertyName("fingerprint")] public string fingerPrint { get; set; }
    }
}