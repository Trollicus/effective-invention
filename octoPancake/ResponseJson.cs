using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace octoPancake
{
    public class ResponseJson
    {
        public class HydraMember
    {
        [JsonProperty("@id")]
        public string Id { get; set; }

        [JsonProperty("@type")]
        public string Type { get; set; }

        [JsonProperty("@context")]
        public string Context { get; set; }
        public string id { get; set; }
        public string? domain { get; set; }
        public bool isActive { get; set; }
        public bool isPrivate { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
    }

    public class HydraView
    {
        [JsonProperty("@id")]
        public string Id { get; set; }

        [JsonProperty("@type")]
        public string Type { get; set; }

        [JsonProperty("hydra:first")]
        public string HydraFirst { get; set; }

        [JsonProperty("hydra:last")]
        public string HydraLast { get; set; }

        [JsonProperty("hydra:previous")]
        public string HydraPrevious { get; set; }

        [JsonProperty("hydra:next")]
        public string HydraNext { get; set; }
    }

    public class HydraMapping
    {
        [JsonProperty("@type")]
        public string Type { get; set; }
        public string variable { get; set; }
        public string property { get; set; }
        public bool required { get; set; }
    }

    public class HydraSearch
    {
        [JsonProperty("@type")]
        public string Type { get; set; }

        [JsonProperty("hydra:template")]
        public string HydraTemplate { get; set; }

        [JsonProperty("hydra:variableRepresentation")]
        public string HydraVariableRepresentation { get; set; }

        [JsonProperty("hydra:mapping")]
        public List<HydraMapping> HydraMapping { get; set; }
    }

    public class Root
    {
        [JsonProperty("hydra:member")]
        public List<HydraMember> HydraMember { get; set; }

        [JsonProperty("hydra:totalItems")]
        public int HydraTotalItems { get; set; }

        [JsonProperty("hydra:view")]
        public HydraView HydraView { get; set; }

        [JsonProperty("hydra:search")]
        public HydraSearch HydraSearch { get; set; }
    }
    }
}