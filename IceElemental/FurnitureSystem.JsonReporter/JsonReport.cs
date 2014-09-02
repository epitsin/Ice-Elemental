namespace FurnitureSystem.JsonReporter
{
    using System;

    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptIn)]
    public class JsonReport
    {
        [JsonProperty]
        public int FurnitureId { get; set; }

        [JsonProperty]
        public string FurnitureName { get; set; }

        [JsonProperty]
        public decimal Price { get; set; }
    }
}
