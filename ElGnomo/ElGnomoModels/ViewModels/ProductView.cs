using System.ComponentModel.DataAnnotations;

namespace ElGnomoModels.ViewModels;

public partial class ProductView
{
    public int Id { get; set; }

    [Display(Name="Nombre")]
    public string? Name { get; set; }

    [Display(Name="Precio")]
    public decimal? Price { get; set; }

    [Display(Name="Categoría")]
    public string? Category { get; set; }

    [Display(Name="Marca")]
    public string? Brand { get; set; }

    [Display(Name="Costo")]
    public decimal? Cost { get; set; }

    [Display(Name="Descuento")]
    public decimal? Discount { get; set; }

    [Display(Name="Cantidad en inventario")]
    public int? QuantityInStock { get; set; }
}
