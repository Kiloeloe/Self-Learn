using System;
using System.ComponentModel.DataAnnotations;

namespace InvoiceWebApp.ViewModels
{
    public class EditViewModel
    {
        [Required(ErrorMessage = "Invoice No is required.")]
        public string InvoiceNo { get; set; }

        [Required(ErrorMessage = "Invoice Date is required.")]
        public DateTime? InvoiceDate { get; set; }

        [Required(ErrorMessage = "To is required.")]
        public string InvoiceTo { get; set; }

        [Required(ErrorMessage = "ShipTo is required.")]
        public string ShipTo { get; set; }

        [Required(ErrorMessage = "Sales ID is required.")]
        public int? SalesId { get; set; }

        [Required(ErrorMessage = "Courier ID is required.")]
        public int? CourierId { get; set; }

        [Required(ErrorMessage = "Payment Type is required.")]
        public int? PaymentType { get; set; }
    }
}
