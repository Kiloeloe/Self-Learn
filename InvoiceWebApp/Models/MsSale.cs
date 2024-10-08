using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace InvoiceWebApp.Models;

[Table("msSales")]
public partial class MsSale
{
    [Key]
    [Column("SalesID")]
    public int SalesId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string SalesName { get; set; } = null!;

    public virtual ICollection<TrInvoice> TrInvoices { get; set; }

}
