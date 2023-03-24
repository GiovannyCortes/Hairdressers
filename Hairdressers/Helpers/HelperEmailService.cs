using System.Net;
using System.Net.Mail;
using static System.Net.WebRequestMethods;

namespace Hairdressers.Helpers {
    public class HelperEmailService {

        private IConfiguration configuration;
        private string image_url;

        public HelperEmailService(IConfiguration configuration) {
            this.configuration = configuration;
            this.image_url = "https://live.staticflickr.com/65535/52768172074_803947c193_h.jpg";
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

        public async Task SendMailAsync(string destinatario, string asunto, string mensaje) {
            MailMessage mail = this.ConfigureMailMessage(destinatario, asunto, mensaje);
            SmtpClient client = this.ConfigureSmtpClient();
            await client.SendMailAsync(mail);
        }

        public async Task SendMailAsync(string destinatario, string asunto, string mensaje, string filePath) {
            MailMessage mail = this.ConfigureMailMessage(destinatario, asunto, mensaje);
            Attachment attachment = new Attachment(filePath);
            mail.Attachments.Add(attachment);
            SmtpClient client = this.ConfigureSmtpClient();
            await client.SendMailAsync(mail);
        }

        public async Task SendTemplateVerificationEmailAsync(string destinatario, string cliente, string token) {
            string mensaje = $@"
                <html>
                <head>
                    <meta charset='UTF-8'>
                </head>
                <body>
                    <h2 style='text-aling:center;'>
                        Verificación de cuenta
                    </h2>
                    <p style='font-size: 1em;'>
                        Estimado/a <strong>{cliente}</strong>, <br>

                        Para poder verificar su correo electrónico en la aplicación Cut&Go, haga clic en el siguiente enlace: <br> <br>

                        <a href='https://localhost:7064/User/ValidateEmail?token={token}' style='
                            text-decoration: none;
                            background-color: #415073;
                            color: white;
                            padding: 10px;
                            border-radius: 25px;
                            font-weight: 700;
                        '>
                            Verificar email
                        </a> <br> <br>

                        Después de hacer clic en el enlace, serás redirigido a nuestra página web. <br>
                        Una vez allí, puedes iniciar sesión con las credenciales que proporcionaste al registrarte. <br> <br>

                        Si no has creado una cuenta en Cut&Go, puedes ignorar este correo electrónico. <br>
                        Si tienes alguna pregunta o problema, no dudes en contactarnos. <br> <br>

                        Atentamente, <br>
                        El equipo de Cut&Go
                    </p> <br>
                    <img src='{this.image_url}' style='max-height: 150px;'/>
                </body>
                </html>";

            await this.SendMailAsync(destinatario, "Cut&Go: Verificación de cuenta", mensaje);
        }

        public async Task SendTemplateRequestAppointment(string destinatario, string cliente, DateTime fecha, TimeSpan hora, string servicios) {
            string mensaje = @"
                <html>
                <head>
                    <meta charset='UTF-8'>
                </head>
                <body>
                    <h2 style='text-aling:center;'>
                        Solicitud de cita
                    </h2>
                    <p>
                        Usuari@ <strong>" + cliente + @"</strong> solicita una cita para el día 
                        <strong>" + fecha.ToShortDateString() + @"</strong> a las <strong>" + hora.ToString() + @"</strong>
                        
                    </p>
                    <p>
                        Vuelva a ventana de la aplicación e introduzca dicho código para verificar su cuenta
                    </p>
                    <center></center>
                </body>
                </html>";
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