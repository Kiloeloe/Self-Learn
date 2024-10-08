using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace InvoiceWebApp.Models;

[Table("ltCourierFee")]
public partial class LtCourierFee
{
    [Column("WeightID")]
    public int WeightId { get; set; }

    [Column("CourierID")]
    public int CourierId { get; set; }

    public int StartKg { get; set; }

    public int? EndKg { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? Price { get; set; }

    public virtual MsCourier Courier { get; set; }

}
