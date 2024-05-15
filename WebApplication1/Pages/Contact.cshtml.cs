using System.ComponentModel.DataAnnotations;
using System.Globalization;
using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyApp.Namespace
{
    public class ContactForm
    {
        public string? First_Name { get; set; }
        public string? Last_Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Select_Service { get; set; }
        public string? Select_Price { get; set; }
        public string? Comments { get; set; }
    }

    [IgnoreAntiforgeryToken]
    public class ContactModel : PageModel
    {
        [BindProperty]
        public ContactForm? Data { get; set; }
        private static readonly EmailAddressAttribute emailAddressAttribute = new();
        private readonly ILogger<ContactModel> _logger;
        public ContactModel(ILogger<ContactModel> logger)
        {
            _logger = logger;
        }

        public IActionResult OnPost()
        {
            // foreach (var item in Request.Form)
            // {
            //     _logger.LogInformation($"{item.Key}: {item.Value}");
            // }

            if (Data?.First_Name == null || Data?.Last_Name == null)
            {
                return Content("""<div class="error_message">Attention! You must enter your name.</div>""");
            }
            else if (Data?.Email == null)
            {
                return Content("""<div class="error_message">Attention! Please enter a valid email address.</div>""");
            }
            else if (!IsEmail(Data?.Email))
            {
                return Content("""<div class="error_message">Attention! You have enter an invalid e-mail address, try again.</div""");
            }
            else if (Data?.Comments == null)
            {
                return Content("""<div class="error_message">Attention! Please enter your message.</div>""");
            }
            else
            {
                WriteToLog("contact.csv");
                return Content($@"
                    <fieldset>
	                <div id='success_page'>
	                <h1>Email Sent Successfully.</h1>
	                <p>Thank you <strong>{Data?.First_Name}</strong>, your message has been submitted to us.</p>
	                </div>
	                </fieldset>
                ");
            }
        }
        void WriteToLog(string log_file_path)
        {
            try
            {
                using var sw = new StreamWriter(log_file_path, true);
                using var log = new CsvWriter(sw, CultureInfo.InvariantCulture);

                if (!System.IO.File.Exists(log_file_path))
                {
                    log.WriteHeader<ContactForm>();
                    log.NextRecord();
                }

                log.WriteRecord(Data);
                log.NextRecord();
            }
            catch (WriterException e)
            {
                _logger.LogInformation($"CsvHHelper exception: {e.Message}");
            }
        }
        static bool IsEmail(string? email)
        {
            return emailAddressAttribute.IsValid(email);
        }
    }
}