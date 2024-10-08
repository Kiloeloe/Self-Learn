using InvoiceWebApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InvoiceWebApp.ViewModels;
using InvoiceWebApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace InvoiceWebApp.Controllers
{
    public class InvoiceController : Controller
    {
        private readonly AssessmentDbContext _context;

        public InvoiceController(AssessmentDbContext context)
        {
            _context = context;
        }


        
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var viewModel = new InvoiceViewModel
            {
                Invoices = new List<TrInvoice>(),
                Details = new List<TrInvoiceDetail>(),
                Products = new List<MsProduct>(),
                Couriers = new List<MsCourier>(),
                CourierFees = new List<LtCourierFee>(),
                TotalWeight = 0,
                TotalAmmount = 0,
                CourierFee = 0
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Search(string invoiceId)
        {         
            var invoice = await _context.TrInvoices
                .Include(tr => tr.TrInvoiceDetails)
                .ThenInclude(p => p.Product)
                .Include(c => c.Courier)
                .ThenInclude(cf => cf.LtCourierFees)
                .Where(i => i.InvoiceNo.Contains(invoiceId))
                .ToListAsync();


            var viewModel = new InvoiceViewModel
            {
                Invoices = invoice,
                Details = invoice.SelectMany(tr => tr.TrInvoiceDetails).ToList(),
                Products = invoice.SelectMany(tr => tr.TrInvoiceDetails)
                    .Select(p => p.Product).Distinct().ToList(),
                Couriers = invoice.Select(tr => tr.Courier).Distinct().ToList(),
                CourierFees = invoice.SelectMany(tr => tr.Courier.LtCourierFees).Distinct().ToList(),

              
                TotalWeight = invoice.Sum(tr => tr.TrInvoiceDetails.Sum(detail => (decimal)detail.Product.Weight * detail.Qty)),

              
                SubTotal = invoice.Sum(tr => tr.TrInvoiceDetails.Sum(detail => (decimal)detail.Product.Price * detail.Qty)),

                
                CourierFee = invoice.Sum(tr =>
                    tr.TrInvoiceDetails.Sum(detail =>
                    {
                        var productWeight = (decimal)detail.Product.Weight;
                        var qty = detail.Qty;

                        var courierFee = tr.Courier.LtCourierFees
                            .FirstOrDefault(f => productWeight >= f.StartKg && productWeight <= f.EndKg)?.Price ?? 0;

                        return courierFee;
                    })
                ),
            };

            viewModel.TotalAmmount = viewModel.SubTotal + viewModel.CourierFee;
            return View("Index", viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var invoice = await _context.TrInvoices
                .Include(s => s.Sales)
                .Include(c => c.Courier)
                .Include(p => p.Payment)
                 .FirstOrDefaultAsync(i => i.InvoiceNo == id);

            var sales = await _context.MsSales.ToListAsync();
            var couriers = await _context.MsCouriers.ToListAsync();
            var payments = await _context.MsPayments.ToListAsync();

            /*ViewData["sales"] = new SelectList(_context.MsSales, "SalesId", "SalesName");*/

            if (invoice == null)
            {
                return View("Error");
            }

            var viewModel = new EditViewModel
            {
                InvoiceNo = invoice.InvoiceNo,
                InvoiceDate = invoice.InvoiceDate,
                InvoiceTo = invoice.InvoiceTo,
                ShipTo = invoice.ShipTo,
                SalesId = invoice.SalesId,
                CourierId = invoice.CourierId,
                PaymentType = invoice.PaymentType,
            };

            ViewData["SalesList"] = new SelectList(sales, "SalesId", "SalesName", invoice.SalesId);
            ViewData["CourierList"] = new SelectList(couriers, "CourierId", "CourierName", invoice.CourierId);
            ViewData["PaymentList"] = new SelectList(payments, "PaymentId", "PaymentName", invoice.PaymentType);
            
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                              .Select(e => e.ErrorMessage)
                              .ToList();

                return BadRequest(new { message = "Invalid parameters", errors = errors });
                return View(viewModel);
            }

            var existingInvoice = await _context.TrInvoices
                .FirstOrDefaultAsync(i => i.InvoiceNo == viewModel.InvoiceNo);

            if (existingInvoice == null)
            {
                ModelState.AddModelError(string.Empty, "Invoice not found.");
                return View(viewModel);
            }

            existingInvoice.InvoiceDate = viewModel.InvoiceDate;
            existingInvoice.InvoiceTo = viewModel.InvoiceTo;
            existingInvoice.ShipTo = viewModel.ShipTo;
            existingInvoice.SalesId = viewModel.SalesId;
            existingInvoice.CourierId = viewModel.CourierId;
            existingInvoice.PaymentType = viewModel.PaymentType;

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }



        /*[HttpPost]
        public async Task<IActionResult> Edit(InvoiceViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var invoiceToUpdate = viewModel.Invoices.FirstOrDefault();

                if (invoiceToUpdate == null)
                {
                    ModelState.AddModelError(string.Empty, "Invoice data is missing.");
                    return View(viewModel); // Redisplay the form with error
                }

                // Retrieve the existing invoice from the database using the InvoiceNo
                var existingInvoice = await _context.TrInvoices
                    .FindAsync(invoiceToUpdate.InvoiceNo);

                if (existingInvoice != null)
                {
                    // Update the existing invoice properties
                    existingInvoice.InvoiceDate = viewModel.Invoices.FirstOrDefault()?.InvoiceDate;
                    existingInvoice.InvoiceTo = viewModel.Invoices.FirstOrDefault()?.InvoiceTo;
                    existingInvoice.SalesId = viewModel.Invoices.FirstOrDefault()?.SalesId;
                    existingInvoice.CourierId = viewModel.Invoices.FirstOrDefault()?.CourierId;
                    existingInvoice.ShipTo = viewModel.Invoices.FirstOrDefault()?.ShipTo;
                    existingInvoice.PaymentType = viewModel.Invoices.FirstOrDefault()?.PaymentType;

                    // Save changes to the existing invoice
                    _context.Update(existingInvoice);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invoice not found.");
                }
            }

            // If we got this far, something failed, redisplay the form with the view model
            return View(viewModel);
        }*/

        /*[HttpPost]
        public async Task<IActionResult> Edit(InvoiceViewModel viewModel)
        {
            var invoice = await _context.TrInvoices.FirstOrDefaultAsync(i => i.InvoiceNo == viewModel.LastInvoice.InvoiceNo);
            if (invoice == null)
            {
                return View("Error");
            }

            // Update properties based on the submitted viewModel
            invoice.InvoiceDate = viewModel.LastInvoice.InvoiceDate;
            invoice.InvoiceTo = viewModel.LastInvoice.InvoiceTo;
            invoice.SalesId = viewModel.LastInvoice.SalesId;
            invoice.CourierId = viewModel.LastInvoice.CourierId;
            invoice.ShipTo = viewModel.LastInvoice.ShipTo;
            invoice.PaymentType = viewModel.LastInvoice.PaymentType;

            _context.Update(invoice);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }*/




    }
}
