using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace InvoiceWebApp.Models;

[Table("trInvoiceDetail")]
public partial class TrInvoiceDetail
{
    [StringLength(10)]
    [Unicode(false)]
    public string InvoiceNo { get; set; } = null!;

    [Column("ProductID")]
    public int ProductId { get; set; }

    public double Weight { get; set; }

    public short Qty { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal Price { get; set; }

    public virtual TrInvoice Invoice { get; set; }
    public virtual MsProduct Product { get; set; }
}
