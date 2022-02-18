using System;

namespace PVU_PlantScan
{
    class PlantAtt
    {
        public bool IsMotherTree { get; set; }
        public int Rarity { get; set; }
        public string Race { get; set; }
        public int Cycle { get; set; }
        public int BaseLE { get; set; }
    }

    class Methods
    {
        public static PlantAtt GetPlant(int plantId)
        {
            var plantIdSt = plantId.ToString();
            var plant = new PlantAtt();

            plant.IsMotherTree = plantIdSt[0] == '2';

            var rarityTag = Int32.Parse(plantIdSt[6] + plantIdSt[7].ToString());

            plant.Rarity = rarityTag switch {
                <= 59 => 1,
                >= 60 and <= 88 => 2,
                >= 89 and <= 98 => 3,
                _ => 4
            };

            var raceTag = plantIdSt[3] + plantIdSt[4].ToString();
            var result = raceTag switch {
                "00" => "Fire,48,400,500,600,800",
                "01" => "Fire,48,400,500,600,800",
                "02" => "Ice,60,500,610,680,850",
                "03" => "Electro,60,500,610,680,850",
                "04" => "Water,108,950,1100,1200,1400",
                "05" => "Water,108,950,1100,1200,1400",
                "06" => "Ice,60,500,610,680,850",
                "07" => "Fire,48,400,500,600,800",
                "08" => "Electro,48,500,610,680,850",
                "09" => "Wind,72,750,860,950,1150",
                "10" => "Wind,72,750,860,950,1150",
                "11" => "Parasite,120,900,1010,1010,1250",
                "12" => "Parasite,120,900,1010,1010,1250",
                "13" => "Parasite,120,900,1010,1010,1250",
                "14" => "Dark,192,1200,1900,2300,2500",
                "15" => "Electro,48,500,610,680,850",
                "16" => "Wind,96,900,1010,1100,1250",
                "17" => "Fire,72,650,760,900,1100",
                "18" => "Light,240,1200,1310,1400,1500",
                "19" => "Light,240,1200,1310,1400,1500",
                "20" => "Light,312,1600,1710,1800,2000",
                "21" => "Light,312,1600,1710,1800,2000",
                "22" => "Parasite,168,1300,1410,1500,1650",
                "23" => "Parasite,168,1300,1410,1500,1650",
                "24" => "Parasite,168,1300,1410,1500,1650",
                "25" => "Metal,336,3500,4300,4800,5200",
                "26" => "Metal,336,3500,4300,4800,5200",
                "27" => "Metal,480,5500,6400,6800,7100",
                "28" => "Metal,480,5500,6400,6800,7100",
                "29" => "Ice,96,800,910,1000,1250",
                "30" => "Fire,72,650,760,900,1100",
                "31" => "Dark,192,1200,1900,2300,2500",
                "32" => "Electro,60,650,760,900,1100",
                "33" => "Dark,216,1400,2100,2500,2800",
                "34" => "Electro,60,650,760,900,1100",
                "35" => "Dark,216,1400,2100,2500,2800",
                "36" => "Water,108,950,1100,1200,1400",
                "37" => "Wind,98,900,1010,1100,1250",
                "38" => "Water,120,1050,1200,1300,1500",
                "39" => "Water,120,1050,1200,1300,1500",
                "90" => "Fire,48,750,1100,1300,1500",
                "91" => "Light,240,1400,1750,1940,2120",
                "92" => "Ice,96,1050,1400,1600,1800",
                "93" => "Dark,216,2800,2950,3100,3300",
                _ => "NotDefined,000,000,000,000,000"
            };

            var array = result.Split(',');
            plant.Race = array[0];
            plant.Cycle = int.Parse(array[1]);

            plant.BaseLE = plant.Rarity switch {
                1 => int.Parse(array[2]),
                2 => int.Parse(array[3]),
                3 => int.Parse(array[4]),
                4 => int.Parse(array[5]),
                _ => plant.BaseLE
            };

            return plant;
        }

        public static int HexToDec(string hex)
        {
            return Convert.ToInt32(hex, 16);
        }

        public static DateTime UnixTimeStampToDateTime(int dec)
        {
            var dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(dec);
            return dtDateTime;
        }

        public static string ExtractWalletAddress(string data)
        {
            var chars = data.ToCharArray();
            string address = null;

            for (var i = 0; i < 67; i++) {
                if (i > 25 && i < 66) {
                    address += chars[i];
                }
            }

            return address;
        }

        public static string ExtractPlantIdHex(string data)
        {
            var chars = data.ToCharArray();
            var plantIdHex = "";
            for (var i = 130; i < data.Length; i++) {
                plantIdHex += chars[i];
            }

            return plantIdHex;
        }

        public static string ExtractTokenIdHex(string data)
        {
            var chars = data.ToCharArray();
            var tokenIdHex = "";
            for (var i = 66; i < 130; i++) {
                tokenIdHex += chars[i];
            }

            return tokenIdHex;
        }
    }
}