using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace prjMSITUCookServices.Implement
{
    public class EmailSender
    {
        private string _subject { get; set; }
        private string _body { get; set; }
        private string _receiverEmail { get; set; }

        public EmailSender(string subject,string body,string receiverEmail) { 
            _subject = subject;
            _body = body;
            _receiverEmail = receiverEmail;
        }

        public bool Send() {
            try
            {
                // 使用 Google Mail Server 發信
                string GoogleID = "a870224tw@gmail.com"; //Google 發信帳號
                string TempPwd = "lsyaoatzmxjkcytz"; //應用程式密碼

                string SmtpServer = "smtp.gmail.com";
                int SmtpPort = 587;
                MailMessage mms = new MailMessage();
                mms.From = new MailAddress(GoogleID);
                mms.Subject = _subject;
                mms.Body = _body;
                mms.IsBodyHtml = true;
                mms.SubjectEncoding = Encoding.UTF8;
                mms.To.Add(new MailAddress(_receiverEmail));
                using (SmtpClient client = new SmtpClient(SmtpServer, SmtpPort))
                {
                    client.EnableSsl = true;
                    client.Credentials = new NetworkCredential(GoogleID, TempPwd);//寄信帳密 
                    client.Send(mms); //寄出信件
                }
                return true;
            }
            catch {
                return false;
            }
        }
    }
}
