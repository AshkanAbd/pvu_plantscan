using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PVU_PlantScan.data;

namespace PVU_PlantScan.DataHandle
{
    class DataHandle
    {
        private readonly string _apiKey;
        private readonly HttpClient _client = new();

        private readonly JsonSerializerSettings _serializerSettings = new() {
            Error = (_, ev) => ev.ErrorContext.Handled = true
        };

        public DataHandle(string apiKey)
        {
            _apiKey = apiKey;
        }

        public async Task<IEnumerable<LogData>> GetData(string address, string topic, double startBlock, int endBlock)
        {
            var url =
                $"https://api.bscscan.com/api?module=logs&action=getLogs&fromBlock={startBlock}&toBlock={endBlock}&address={address}&topic0={topic}&apikey={_apiKey}";
            var obj = await Get<DataLogSchema>(_client, url).ConfigureAwait(false);
            return obj.Result;
        }

        private async Task<T> Get<T>(HttpClient client, string url)
        {
            var json = await client.GetStringAsync(url);
            var obj = JsonConvert.DeserializeObject<T>(json, _serializerSettings);
            return obj;
        }
    }
}