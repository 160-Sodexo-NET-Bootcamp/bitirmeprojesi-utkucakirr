using DataAccess.Uow;
using Entities.DataModel;
using Hangfire;
using System;
using System.Net;
using System.Net.Mail;

namespace WebAPI.Jobs
{
    /// <summary>
    /// Sending mails using Hangfire.
    /// </summary>
    public class MailSendJob
    {
        private readonly IUnitOfWork _unitOfWork;
        public MailSendJob(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            BackgroundJob.Schedule(()=>DoJob(),TimeSpan.FromSeconds(2));
        }

        public void DoJob()
        {
            var temp = _unitOfWork.MailRepository.GetListByExpression(x => x.Status == "Not sent");
            if(temp !=null)
            {
                foreach(var item in temp)
                {
                    item.Status = "Sent";
                    _unitOfWork.MailRepository.Update(item);
                    _unitOfWork.Complete();
                    SendMail(item);
                }
            }
        }

        public void SendMail(Mail item)
        {
            MailMessage message = new MailMessage("patikabitirmetemp@gmail.com", item.MailTo, "Bitirme Projesi", item.Message);
            SmtpClient smtp = new SmtpClient();
            smtp.Port = 587;
            smtp.Host = "smtp.gmail.com";
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential("patikabitirmetemp@gmail.com", "asd123def");
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Send(message);
        }
    }
}
