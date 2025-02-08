using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;


namespace Infrastructure_FiapTechChallenge.Util.Email
{
    public class Email
    {

        public bool EnviarEmailConsultaAprovada(string emailPaciente, string paciente, string medico, string emailMedico ,string horario, string data)
        {
            try 
            {
                var smtpClient = new SmtpClient("smtp.office365.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("robot.consultores01@rvc.law", "robot@@2019@!"),
                    EnableSsl = true,
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress("robot.consultores01@rvc.law"),
                    Subject = "HACKATOON FIAP - Consulta aprovada com sucesso",
                    Body = "<h5>Olá " + paciente +"<h5><br>Sua consulta com o Dr." + medico + " foi aprovada.<br> A consulta será : " + data + " as " + horario ,
                    IsBodyHtml = true,
                   
                };

                mailMessage.CC.Add(emailMedico);
                mailMessage.To.Add(emailPaciente);

                smtpClient.Send(mailMessage);

                return true;
            }
            catch 
            {
                return false;
            }
            
        }
    }
}
