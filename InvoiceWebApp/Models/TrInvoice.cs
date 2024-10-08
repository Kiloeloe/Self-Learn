using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace InvoiceWebApp.Models;

[Table("trInvoice")]
public partial class TrInvoice
{
    [Key]
    [StringLength(10)]
    [Unicode(false)]
    public string InvoiceNo { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime? InvoiceDate { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string? InvoiceTo { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string? ShipTo { get; set; }

    [Column("SalesID")]
    public int? SalesId { get; set; }

    [Column("CourierID")]
    public int? CourierId { get; set; }

    public int? PaymentType { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? CourierFee { get; set; }

    public virtual MsSale Sales { get; set; }
    public virtual MsCourier Courier { get; set; }
    public virtual MsPayment Payment { get; set; }

    public virtual ICollection<TrInvoiceDetail> TrInvoiceDetails { get; set; }
}
