using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using MimeKit;
using MimeKit.Text;

namespace Resume.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }

        [HttpPost]
        public IActionResult OnPostSendMail(string name, string email, string message, string subject)
        {
            var emailObj = new MimeMessage();
            emailObj.From.Add(MailboxAddress.Parse(email));
            emailObj.To.Add(MailboxAddress.Parse("messenger@ghalibsaleem.me"));
            emailObj.Subject = "Test Email Subject";
            emailObj.Body = new TextPart(TextFormat.Plain) { Text = "Example Plain Text Message Body" };

            // send email
            using var smtp = new SmtpClient();
            smtp.Connect("smtp.ethereal.email", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("[USERNAME]", "[PASSWORD]");
            smtp.Send(emailObj);
            smtp.Disconnect(true);

            return new JsonResult("");
        }
    }
}


/*
$.ajax({
				type: "POST",
				url: "/Index?handler=GetTime",
				beforeSend: function (xhr) {
					xhr.setRequestHeader("XSRF-TOKEN",
						$('input:hidden[name="__RequestVerificationToken"]').val());
				},
				data: { "name": 'Test Data' },
				success: function (response) {
					alert("Hello: " + response.Name + " .\nCurrent Date and Time: " + response.DateTime);
				},
				failure: function (response) {
					alert(response.responseText);
				},
				error: function (response) {
					alert(response.responseText);
				}
			});
*/