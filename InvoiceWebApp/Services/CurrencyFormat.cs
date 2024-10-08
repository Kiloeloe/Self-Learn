using System.Globalization;

namespace InvoiceWebApp.Services
{
    public class CurrencyFormat
    {
        public static string ToRupiah(decimal amount)
        {
            return amount.ToString("C", new CultureInfo("id-ID"));
        }
    }
}
