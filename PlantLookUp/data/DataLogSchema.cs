using System.Collections.Generic;
using Newtonsoft.Json;

namespace PVU_PlantScan.data
{
    class DataLogSchema
    {
        [JsonProperty("status")] public string Status { get; private set; }
        [JsonProperty("message")] public string Message { get; private set; }
        [JsonProperty("result")] public List<LogData> Result { get; private set; }
    }
}