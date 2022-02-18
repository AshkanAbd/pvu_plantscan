using System;

namespace PVU_PlantScan
{
    public class PlantTableModel
    {
        public long Id { get; set; }
        public decimal PlantId { get; set; }
        public decimal TokenId { get; set; }
        public DateTime MintTime { get; set; }
        public double BlockNumber { get; set; }
    }
}