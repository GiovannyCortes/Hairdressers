using System.Net;
using System.Net.Mail;

namespace Hairdressers.Helpers {
    public class HelperEmailService {

        private IConfiguration configuration;

        public HelperEmailService(IConfiguration configuration) {
            this.configuration = configuration;
        }

        private MailMessage ConfigureMailMessage (string destinatario, string asunto, string mensaje) {
            string email = this.configuration.GetValue<string>("MailSettings:Credentials:User");
            MailMessage mailMessage = new MailMessage();
                        mailMessage.From = new MailAddress(email);
                        mailMessage.To.Add(new MailAddress(destinatario));
                        mailMessage.Subject = asunto;
                        mailMessage.Body = mensaje;
                        mailMessage.IsBodyHtml = true;
            return mailMessage;
        }

        private SmtpClient ConfigureSmtpClient() {
            string user = this.configuration.GetValue<string>("MailSettings:Credentials:User");
            string password = this.configuration.GetValue<string>("MailSettings:Credentials:Password");

            int port = this.configuration.GetValue<int>("MailSettings:Smtp:Port");
            string host = this.configuration.GetValue<string>("MailSettings:Smtp:Host");

            bool enableSSL = this.configuration.GetValue<bool>("MailSettings:Smtp:EnableSSL");
            bool defaultCredentials = this.configuration.GetValue<bool>("MailSettings:Smtp:DefaultCredentials");

            SmtpClient client = new SmtpClient();
                       client.Port = port;
                       client.Host = host;
                       client.EnableSsl = enableSSL;
                       client.UseDefaultCredentials = defaultCredentials;

            NetworkCredential credentials = new NetworkCredential(user, password);
            client.Credentials = credentials;
            return client;
        }


        public int SendConfirmationEmailConArchivoAniadido(string receiver, string email) {
            /*Random random = new Random();
            int verification_code = random.Next(100000, 1000000);

             Create text content
            TextPart cuerpo = new TextPart("html") {
                Text = @"<html>
                              <head>
                                <meta charset='UTF-8'>
                              </head>
                              <body>
                                <h2 style='text-aling:center;'>Verificación de la cuenta de correo electrónico</h2>
                                <p>Su código de verificación es: <strong>" + verification_code + @"</strong></p>
                                <p>Vuelva a ventana de la aplicación e introduzca dicho código para verificar su cuenta</p>
                                <img src=""cid:Company.jpg"" alt='Company banner image' style=""width:100%;""/>
                              </body>
                            </html>"
            };

           Create an image attachment for the file located at path
           var attachment = new MimePart("image", "jpg") {
               Content = new MimeContent(File.OpenRead("wwwroot/images/company_banner.jpg"), ContentEncoding.Default),
               ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
               ContentTransferEncoding = ContentEncoding.Base64,
               FileName = Path.GetFileName("wwwroot/images/company_banner.jpg")
           };

             Create the multipart/mixed container to hold the message text and the image attachment
            var multipart = new Multipart("mixed");
                multipart.Add(cuerpo);
                multipart.Add(attachment);


             Configuramos el mensaje a enviar
            var mensaje = new MimeMessage();
                mensaje.From.Add(new MailboxAddress(this._sender, this._email));
                mensaje.To.Add(new MailboxAddress(receiver, email));
                mensaje.Subject = "Cut&Go: Código de verificación";
                mensaje.Body = multipart;

             Configuramos la conexión SMTP
            using (var clienteSmtp = new MailKit.Net.Smtp.SmtpClient()) {
                clienteSmtp.Connect(this._host, this._port, false);
                clienteSmtp.Authenticate(this._email, this._password);

                 Envíamos el mensaje y desconectamos el servicio
                clienteSmtp.Send(mensaje);
                clienteSmtp.Disconnect(true);

                return verification_code;
            }*/
            return 1;
        }

        /*public int SendConfirmationEmail(string receiver, string email) {
            Random random = new Random();
            int verification_code = random.Next(100000, 1000000);

            Configuramos el mensaje a enviar
            var mensaje = new MimeMessage();
            mensaje.From.Add(new MailboxAddress(this._sender, this._email));
            mensaje.To.Add(new MailboxAddress(receiver, email));
            mensaje.Subject = "Cut&Go: Código de verificación";

            var builder = new BodyBuilder();

            // In order to reference selfie.jpg from the html text, we'll need to add it
            // to builder.LinkedResources and then use its Content-Id value in the img src.
            var image = builder.LinkedResources.Add("wwwroot/images/company_banner.jpg");
            image.ContentId = MimeUtils.GenerateMessageId();

            // Set the html version of the message text
            builder.HtmlBody = string.Format(@"
                            <html>
                              <head>
                                <meta charset='UTF-8'>
                              </head>
                              <body>
                                <h2 style='text-aling:center;'>Verificación de la cuenta de correo electrónico</h2>
                                <p>Su código de verificación es: <strong>" + verification_code + @"</strong></p>
                                <p>Vuelva a ventana de la aplicación e introduzca dicho código para verificar su cuenta</p>
                                <center><img src=""cid:{0}"" alt='Company banner image'/></center>
                              </body>
                            </html>",
                image.ContentId);

            // Now we just need to set the message body and we're done
            mensaje.Body = builder.ToMessageBody();

            // Configuramos la conexión SMTP
            using (var clienteSmtp = new MailKit.Net.Smtp.SmtpClient()) {
                clienteSmtp.Connect(this._host, this._port, false);
                clienteSmtp.Authenticate(this._email, this._password);

                // Envíamos el mensaje y desconectamos el servicio
                clienteSmtp.Send(mensaje);
                clienteSmtp.Disconnect(true);

                return verification_code;
            }
        }*/

    }
}