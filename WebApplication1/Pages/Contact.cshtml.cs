using System.ComponentModel.DataAnnotations;
using System.Globalization;
using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyApp.Namespace
{
    public class ContactForm
    {
        [Display(Name = "First Name")]
        public string? First_Name { get; set; }

        [Display(Name = "Last Name")]
        public string? Last_Name { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        public string? Phone { get; set; }

        [Display(Name = "Service")]
        public string? SelectedService { get; set; }

        [Display(Name = "Price")]
        public string? SelectedPrice { get; set; }

        public string? Comments { get; set; }
    }

    [IgnoreAntiforgeryToken]
    public class ContactModel : PageModel
    {
        [BindProperty]
        public ContactForm? Data { get; set; }
        private static readonly EmailAddressAttribute EmailValidator = new();
        private readonly ILogger<ContactModel> _logger;
        public ContactModel(ILogger<ContactModel> logger)
        {
            _logger = logger;
        }

        public IActionResult OnPost()
        {
            if (string.IsNullOrWhiteSpace(Data?.First_Name) || string.IsNullOrWhiteSpace(Data?.Last_Name))
            {
                return Content("""<div class="error_message">Attention! You must enter your name.</div>""");
            }
            else if (string.IsNullOrWhiteSpace(Data?.Email))
            {
                return Content("""<div class="error_message">Attention! Please enter a valid email address.</div>""");
            }
            else if (!IsEmailValid(Data?.Email))
            {
                return Content("""<div class="error_message">Attention! You have enter an invalid e-mail address, try again.</div""");
            }
            else if (string.IsNullOrWhiteSpace(Data?.Comments))
            {
                return Content("""<div class="error_message">Attention! Please enter your message.</div>""");
            }
            else
            {
                WriteToCsv("contact.csv");
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
        private void WriteToCsv(string filePath)
        {
            try
            {
                using var streamWriter = new StreamWriter(filePath, true);
                using var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture);

                if (!System.IO.File.Exists(filePath))
                {
                    csvWriter.WriteHeader<ContactForm>();
                    csvWriter.NextRecord();
                }

                csvWriter.WriteRecord(Data);
                csvWriter.NextRecord();
            }
            catch (WriterException exception)
            {
                _logger.LogInformation($"CsvHelper exception: {exception.Message}");
            }
        }

        private static bool IsEmailValid(string? email)
        {
            return EmailValidator.IsValid(email);
        }
    }
}
