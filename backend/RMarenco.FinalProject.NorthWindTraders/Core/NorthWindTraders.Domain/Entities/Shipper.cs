﻿using System.Text.Json.Serialization;

namespace NorthWindTraders.Domain.Entities
{
    public class Shipper
    {
        public int ShipperID { get; set; }
        public string CompanyName { get; set; }
        public string Phone { get; set; }
    }
}
