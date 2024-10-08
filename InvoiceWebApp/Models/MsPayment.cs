using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace InvoiceWebApp.Models;

[Table("msPayment")]
public partial class MsPayment
{
    [Key]
    [Column("PaymentID")]
    public int PaymentId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? PaymentName { get; set; }

    public virtual ICollection<TrInvoice> TrInvoices { get; set; }

}
