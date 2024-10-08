using InvoiceWebApp.Models;


namespace InvoiceWebApp.ViewModels
{
    public class InvoiceViewModel
    {
        public List<TrInvoice> Invoices { get; set; }
        public List<TrInvoiceDetail> Details { get; set; }
        public List<MsProduct> Products { get; set; }   
        public List<MsCourier> Couriers { get; set; }
        public List<LtCourierFee> CourierFees { get; set; }
        public List<MsSale> Sales { get; set; }
        public List<MsPayment> Payments { get; set; }

        public decimal TotalWeight { get; set; }

        public decimal TotalAmmount { get; set; }

        public decimal CourierFee { get; set; }

        public decimal SubTotal { get; set; }
    }
}
