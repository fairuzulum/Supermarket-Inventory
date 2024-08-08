using System.Collections.Generic;

namespace SupermarketInventory.Models
{
    public class Warehouse
    {
        public int WarehouseId { get; set; }
        public string WarehouseName { get; set; }
        public List<Item> Items { get; set; }
    }
}