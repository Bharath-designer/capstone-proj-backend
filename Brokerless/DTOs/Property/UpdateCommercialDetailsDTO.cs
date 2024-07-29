﻿using Brokerless.Enums;
using System.ComponentModel.DataAnnotations;

namespace Brokerless.DTOs.Property
{
    public class UpdateCommercialDetailsDTO:UpdateBasePropertyDTO
    {
        [Required]
        public CommercialType? CommercialType { get; set; }
        [Required]
        public int? FloorCount { get; set; }
        [Required]
        public MeasurementUnit? MeasurementUnit { get; set; }
        [Required]
        public double? Length { get; set; }
        [Required]
        public double? Width { get; set; }
        [Required]
        public WaterSupply? WaterSupply { get; set; }
        [Required]
        public Electricity? Electricity { get; set; }
        [Required]
        public int? RestroomCount { get; set; }
        [Required]
        public bool? GatedSecurity { get; set; }
        [Required]
        public int? CarParking { get; set; }
        public double? Height { get; set; }
    }
}
