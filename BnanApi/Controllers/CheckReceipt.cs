using BnanApi.DTOS;
using BnanApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace BnanApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CheckReceipt : Controller
    {
        private readonly BnanKSAContext _context;

        public CheckReceipt(BnanKSAContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult GetReceipt(string Code)
        {
            var pdfModel = new ReceiptsVM();
            if (Code.Contains("-1401-"))
            {
                // Contract
                var contract = _context.CrCasRenterContractBasics
                    .Where(x => x.CrCasRenterContractBasicNo == Code)
                    .OrderByDescending(x => x.CrCasRenterContractBasicCopy)
                    .FirstOrDefault();

                if (contract != null)
                {
                    pdfModel.ArPdf = contract.CrCasRenterContractBasicArPdfFile?.Replace("~", "");
                    pdfModel.EnPdf = contract.CrCasRenterContractBasicEnPdfFile?.Replace("~", "");
                    return Ok(pdfModel);
                }
                else
                {
                    return NotFound("The contract number is incorrect.");
                }
            }
            else if (Code.Contains("-1301-"))
            {
                // Receipt
                var receipt = _context.CrCasAccountReceipts.FirstOrDefault(x => x.CrCasAccountReceiptNo == Code);
                if (receipt != null)
                {
                    pdfModel.ArPdf = receipt.CrCasAccountReceiptArPdfFile?.Replace("~", "");
                    pdfModel.EnPdf = receipt.CrCasAccountReceiptEnPdfFile?.Replace("~", "");
                    return Ok(pdfModel);
                }
                else
                {
                    return NotFound("The receipt number is incorrect.");
                }
            }
            else if (Code.Contains("-1308-"))
            {
                // Invoice
                var invoice = _context.CrCasAccountInvoices.FirstOrDefault(x => x.CrCasAccountInvoiceNo == Code);
                if (invoice != null)
                {
                    pdfModel.ArPdf = invoice.CrCasAccountInvoiceArPdfFile?.Replace("~", "");
                    pdfModel.EnPdf = invoice.CrCasAccountInvoiceEnPdfFile?.Replace("~", "");
                    return Ok(pdfModel);
                }
                else
                {
                    return NotFound("The invoice number is incorrect.");
                }
            }

            // If the provided code doesn't match any of the predefined patterns
            return NotFound("The code provided is not recognized.");
        }
    }
}
