using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace InvoiceWebApp.Models;

[Table("msCourier")]
public partial class MsCourier
{
    [Key]
    [Column("CourierID")]
    public int CourierId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string CourierName { get; set; } = null!;

    public virtual ICollection<LtCourierFee> LtCourierFees { get; set; }
    public virtual ICollection<TrInvoice> TrInvoices { get; set; }
}
