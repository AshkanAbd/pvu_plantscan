using System.IO;
using Microsoft.Extensions.Configuration;

namespace PVU_PlantScan
{
    public class Config
    {
        public static string Address = "0x5Ab19e7091dD208F352F8E727B6DCC6F8aBB6275";
        public static string Topic = "0x393ccba6479926a7c4a6471a3b4584229e2df48a1858c8caf57680927dced5ff";

        public static IConfigurationRoot Configuration { get; set; }

        public static void LoadConfiguration()
        {
            if (Configuration != null) return;

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true);

            Configuration = builder.Build();
        }
    }
}