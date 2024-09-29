using System;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Configuration;
using System.Net.Mail;
using System.Net;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using nmedia2.Models; // Assuming your model class is in this namespace
using System.Data;
using System.Linq;

namespace nmedia2.Controllers
{
    public class ContactController : Controller
    {
        // GET: Index
        [Route("Contact")]
        public ActionResult Contact()
        {
            ViewBag.SuccessMessage = TempData["SuccessMessage"] as string;
            ViewBag.ErrorMessage = TempData["ErrorMessage"] as string;
            return View();
        }

        [HttpPost]
        public ActionResult Contact(Contact model, string captcha)
        {
            if (IsCaptchaValid(captcha))
            {
                if (ModelState.IsValid)
                {
                    string connectionString = ConfigurationManager.ConnectionStrings["DefualtConnection"].ConnectionString;
                    try
                    {
                    //    using (SqlConnection conn = new SqlConnection(connectionString))
                    //    {
                    //        using (SqlCommand cmd = new SqlCommand("InsertContact", conn))
                    //        {
                    //            cmd.CommandType = CommandType.StoredProcedure;
                    //            cmd.Parameters.AddWithValue("@Name", model.Name);
                    //            cmd.Parameters.AddWithValue("@Email", model.Email);
                    //            cmd.Parameters.AddWithValue("@Number", model.Number);
                    //            cmd.Parameters.AddWithValue("@Subject", model.Subject);
                    //            cmd.Parameters.AddWithValue("@Message", model.Message);

                    //            conn.Open();
                    //            cmd.ExecuteNonQuery();
                    //        }
                    //    }

                        // Send confirmation email
                        string subject = "Contact Form Submission";
                        string body = $"Thank you for contacting us, {model.Name}. We have received your message and will get back to you shortly.";
                        SendEmail(model.Email, subject, body);

                        return RedirectToAction("contact");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error in SubmitForm method: {0}", ex.ToString());
                        ModelState.AddModelError("", "An error occurred while processing your request. Please try again later.");
                    }
                }
            }
            else
            {
                //return Content("<script> alert('Invalid Captcha'); </script>", "text/html");
                TempData["ErrorMessage"] = "Invalid CAPTCHA.";
            }

            return View("contact");
        }

        private bool IsCaptchaValid(string captcha)
        {
            var storedCaptcha = Session["CaptchaText"] as string;
            if (string.IsNullOrEmpty(storedCaptcha) || string.IsNullOrEmpty(captcha))
            {
                return false;
            }

            Session["CaptchaText"] = null;
            return storedCaptcha.Equals(captcha, StringComparison.OrdinalIgnoreCase);
        }

        public void SendEmail(string toEmail, string subject, string body)
        {
            try
            {
                var fromAddress = new MailAddress("nmediasoft@gmail.com", "Nmediasoft");
                var toAddress = new MailAddress(toEmail);
                var smtpPassword = ConfigurationManager.AppSettings["jrnkwnghoanrzlek"]; // Fetch password from config

                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, "jrnkwnghoanrzlek")
                };

                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body
                })
                {
                    smtp.Send(message);
                }
                Console.WriteLine("Email sent successfully to {0}", toEmail);
            }
            catch (SmtpException smtpEx)
            {
                Console.WriteLine("SMTP Exception caught in SendEmail(): {0}", smtpEx.ToString());
                ViewBag.ErrorMessage = "An error occurred while sending the email. Please try again later.";
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught in SendEmail(): {0}", ex.ToString());
                ViewBag.ErrorMessage = "An error occurred while sending the email. Please try again later.";
            }
        }

        public ActionResult GenerateCaptcha()
        {
            var captchaImage = GenerateCaptchaImage();
            return File(captchaImage, "image/png");
        }

        private byte[] GenerateCaptchaImage()
        {
            var randomText = GenerateRandomText();

            using (var bitmap = new Bitmap(200, 50))
            {
                using (var graphics = Graphics.FromImage(bitmap))
                {
                    graphics.Clear(Color.White);
                    using (var font = new Font("Arial", 20))
                    {
                        graphics.DrawString(randomText, font, Brushes.Black, new PointF(10, 10));
                    }
                }

                using (var ms = new MemoryStream())
                {
                    bitmap.Save(ms, ImageFormat.Png);
                    Session["CaptchaText"] = randomText; // Store the CAPTCHA text in session
                    return ms.ToArray();
                }
            }
        }

        private string GenerateRandomText()
        {
            var random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, 5)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
