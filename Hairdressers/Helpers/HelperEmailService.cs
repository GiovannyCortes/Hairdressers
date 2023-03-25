using NuGet.Common;
using System.Net;
using System.Net.Mail;
using static System.Net.WebRequestMethods;

namespace Hairdressers.Helpers {
    public class HelperEmailService {

        private IConfiguration configuration;
        private string image_url;
        private string local;

        public HelperEmailService(IConfiguration configuration) {
            this.configuration = configuration;
            this.image_url = "https://live.staticflickr.com/65535/52768172074_803947c193_h.jpg";
            this.local = "https://localhost:7064";
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

                        <center><a href='{this.local}/User/ValidateEmail?token={token}' style='
                            text-decoration: none;
                            background-color: #415073;
                            color: white;
                            padding: 10px;
                            border-radius: 25px;
                            font-weight: 700;
                            width: 90%;
                            display: inline-block;
                        '>
                            Verificar email
                        </a></center> <br> <br>

                        Después de hacer clic en el enlace, serás redirigido a nuestra página web. <br>
                        Una vez allí, puedes iniciar sesión con las credenciales que proporcionaste al registrarte. <br> <br>

                        Si no has creado una cuenta en Cut&Go, puedes ignorar este correo electrónico. <br>
                        Si tienes alguna pregunta o problema, no dudes en contactarnos. <br> <br>

                        Atentamente, <br>
                        El equipo de Cut&Go
                    </p> <br>
                    <center><img src='{this.image_url}' style='max-height: 150px;'/></center>
                </body>
                </html>";

            await this.SendMailAsync(destinatario, "Cut&Go: Verificación de cuenta", mensaje);
        }

        public async Task SendTemplateRequestAppointment
            (string[] destinatarios, string cliente, string email, string fecha, string hora, 
            List<string> servicios, decimal coste, string token, int hairdresser_id, int appointment_id) {

            string services = "";
            foreach (string servicio in servicios) {
                services += "<li>";
                services += servicio;
                services += "</li>";
            }

            string mensaje = $@"
                <html>
                <head>
                    <meta charset='UTF-8'>
                </head>
                <body>
                    <h2 style='text-aling:center;'>
                        Solicitud de cita
                    </h2>
                    <p>
                        Usuari@ <strong>{cliente}</strong> solicita una cita para el día 
                        <strong>{fecha}</strong> a las <strong>{hora}</strong>
                        para realizar los siguientes servicios:
                    </p>
                    <ul>{services}</ul>
                    <p>Coste Total: {coste}</p>
                    <p>Puede confirmar la cita pulsando el botón de más abajo, desde la aplicación o contactando directamente con el usuario</p> <br>
                    <center>
                        <a href='{this.local}/Appointments/AppointmentConfirm?token={token}&hid={hairdresser_id}&apid={appointment_id}' style='
                            text-decoration: none;
                            background-color: #415073;
                            color: white;
                            padding: 10px;
                            margin-inline: 10px;
                            border-radius: 25px;
                            font-weight: 700;
                            width: 80%;
                            display: inline-block;
                        '>
                            Aceptar
                        </a>
                    </center> <br> 
                    <p>Datos de contacto: {email}</p>
                    <center><img src='{this.image_url}' style='max-height: 150px;'/></center>
                </body>
                </html>";

            foreach (string des in destinatarios) {
                await this.SendMailAsync(des, "Cut&Go: Solicitud de cita", mensaje);
            }
        }

    }
}