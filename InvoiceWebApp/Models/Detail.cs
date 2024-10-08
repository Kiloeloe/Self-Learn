using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace InvoiceWebApp.Models;

[Keyless]
public partial class Detail
{
    [StringLength(10)]
    [Unicode(false)]
    public string InvoiceNo { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string Item { get; set; } = null!;

    [Column("Weight(Kg)")]
    public double WeightKg { get; set; }

    public short Qty { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal UnitPrice { get; set; }

    public short? Total { get; set; }
}
