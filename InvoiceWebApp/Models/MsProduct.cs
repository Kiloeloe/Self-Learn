using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace InvoiceWebApp.Models;

[Table("msProduct")]
public partial class MsProduct
{
    [Column("ProductID")]
    public int ProductId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string ProductName { get; set; } = null!;

    public double Weight { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? Price { get; set; }

    public virtual ICollection<TrInvoiceDetail> TrInvoiceDetails { get; set; }
    public virtual TrInvoice Invoice { get; set; }

}
