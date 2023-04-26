﻿namespace ElGnomoModels.ViewModels;

public partial class ProductView
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public decimal? Price { get; set; }

    public string? Category { get; set; }

    public string? Brand { get; set; }

    public decimal? Cost { get; set; }

    public string? Image { get; set; }

    public decimal? Discount { get; set; }

    public int? QuantityInStock { get; set; }
}
