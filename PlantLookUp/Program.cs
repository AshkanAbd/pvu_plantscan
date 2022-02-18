using System;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace PVU_PlantScan
{
    class Program : Methods
    {
        private static async Task Main(string[] args)
        {
            Config.LoadConfiguration();
            await FetchDate();
        }

        private static async Task FetchDate()
        {
            //Config The Bot
            var dh = new DataHandle.DataHandle(Config.Configuration["ApiToken"]);
            // Give The Block You Want Strat Track There ( First Block On PVU : 8974488 )
            var fromBlock = Convert.ToDouble(Config.Configuration["StartBlock"]);
            // The Last Block You Want ( No Need To Change It Gives You The First 1000 One) 
            var toBlock = Convert.ToInt32(Config.Configuration["EndBlock"]);

            while (true) {
                try {
                    //Run
                    var lastBlockDetails = await SaveTheDataAsync(dh, Config.Address, Config.Topic, fromBlock, toBlock);

                    Console.WriteLine("Mint Record Saved This Time: " + lastBlockDetails.Count);
                    Console.WriteLine("last Block Number: " + lastBlockDetails.LastBlock);
                    Console.WriteLine("Last Block Time: " + lastBlockDetails.LastBlockTime);
                    Console.WriteLine("-----------------------------------------");

                    while (lastBlockDetails.Count != 0) {
                        Console.WriteLine("Waiting 1 Sec...");
                        Thread.Sleep(1000);

                        lastBlockDetails = await SaveTheDataAsync(dh, Config.Address, Config.Topic,
                            lastBlockDetails.LastBlock,
                            toBlock);

                        Console.WriteLine("Mint Record Saved This Time: " + lastBlockDetails.Count);
                        Console.WriteLine("last Block Number: " + lastBlockDetails.LastBlock);
                        Console.WriteLine("Last Block Time: " + lastBlockDetails.LastBlockTime);
                        Console.WriteLine("-----------------------------------------");
                    }
                }
                catch (HttpRequestException e) {
                    Console.WriteLine(e);
                }
            }
        }

        private static async Task<DataOutput> SaveTheDataAsync(DataHandle.DataHandle dh, string address, string topic,
            double fromBlock, int toBlock)
        {
            Console.WriteLine("Getting Data...");
            var s = await dh.GetData(address, topic, fromBlock, toBlock);

            var plants = s.Select(item =>
                new PlantTableModel {
                    BlockNumber = HexToDec(item.BlockNumber),
                    MintTime = UnixTimeStampToDateTime(HexToDec(item.TimeStamp)),
                    PlantId = Convert.ToInt64(ExtractPlantIdHex(item.Data), 16),
                    TokenId = Convert.ToInt64(ExtractTokenIdHex(item.Data), 16)
                }).ToList();

            Console.WriteLine("Saving Data...");

            await using (var db = new PlantContextFactory().CreateDbContext(new string[] { })) {
                await db.Plants.AddRangeAsync(plants);

                await db.SaveChangesAsync();
            }

            var count = plants.Count;

            return new DataOutput {
                Count = count,
                LastBlock = plants.Last().BlockNumber,
                LastBlockTime = plants.Last().MintTime,
            };
        }
    }

    public class DataOutput
    {
        public int Count { get; set; }
        public double LastBlock { get; set; }
        public DateTime LastBlockTime { get; set; }
    }
}