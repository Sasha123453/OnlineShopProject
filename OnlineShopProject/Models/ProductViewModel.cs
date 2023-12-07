﻿using OnlineShop.Db.Models;

namespace OnlineShopProject.Models
{
    public class ProductViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public string Category { get; set; }
        public int Ram { get; set; }
        public string Cpu { get; set; }
        public int CoresAmount { get; set; }
        public double MaxFrequency { get; set; }
        public ProductViewModel(string name, string description, string category, int price, int ram, string cpu, int coresAmount, double maxFrequency)
        {
            Category = category;
            Name = name;
            Description = description;
            Price = price;
            Ram = ram;
            Cpu = cpu;
            CoresAmount = coresAmount;
            MaxFrequency = maxFrequency;
        }
        public ProductViewModel()
        {

        }
    }
}