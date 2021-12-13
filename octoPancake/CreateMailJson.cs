using System;
using Newtonsoft.Json;

namespace octoPancake
{
    public class CreateMailJson
    {
        [JsonProperty("@context")]
        public string Context { get; set; }

        [JsonProperty("@id")]
        public string Id { get; set; }

        [JsonProperty("@type")]
        public string Type { get; set; }
        public string id { get; set; }
        public string address { get; set; }
        public int quota { get; set; }
        public int used { get; set; }
        public bool isDisabled { get; set; }
        public bool isDeleted { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
    }
}