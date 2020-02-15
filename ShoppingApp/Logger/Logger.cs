using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Logger
{
    public class Logger
    {
        private StackTrace trace;
        private MethodBase method;
        public void Info(string info, int threadId)
        {
            trace = new StackTrace();
            method = trace.GetFrames()[1].GetMethod();
            WriteLog(InfoLine(trace, method, DateTime.Now, "INFO", threadId, info));
        }
        public void Debug(string info, int threadId)
        {
            trace = new StackTrace();
            method = trace.GetFrames()[1].GetMethod();
            WriteLog(InfoLine(trace, method, DateTime.Now, "DEBUG", threadId, info));
        }
        public void Error(string info, int threadId)
        {
            trace = new StackTrace();
            method = trace.GetFrames()[1].GetMethod();
            string infoLine = InfoLine(trace, method, DateTime.Now, "ERROR", threadId, info);
            WriteLog(infoLine);
            SendEmailAsync(infoLine).GetAwaiter();
        }

        private string InfoLine(StackTrace trace, MethodBase method,
                                DateTime dateTime, string loggerType,
                                int threadId, string info)
            => $"{dateTime} | [{loggerType}] | "+
               $"{trace.GetFrames()[1].GetMethod().ReflectedType.ToString()} | "+
               $"{method.Name} | [{threadId}] | {info}";

        public void WriteLog(string infoLine)
        {
            MessageBox.Show(infoLine);
            WriteInFileAsync(infoLine).GetAwaiter();
            //write in file
        }
        private static async Task SendEmailAsync(string info)
        {
            MailAddress from = new MailAddress("shopping.app180220@gmail.com", "John");
            MailAddress to = new MailAddress("johnturner.2489@gmail.com");
            MailMessage m = new MailMessage(from, to);
            m.Subject = "ERROR in Shopping application";
            m.Body = info;
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.Credentials = new NetworkCredential("shopping.app180220@gmail.com", "gvardia4455");
            smtp.EnableSsl = true;
            await smtp.SendMailAsync(m);
        }
        private static async Task WriteInFileAsync(string info)
        {
            string writePath = System.IO.Path.Combine(Environment.CurrentDirectory, @"logs\output.txt");
            try
            {
                using (StreamWriter sw = new StreamWriter(writePath, false, System.Text.Encoding.Default))
                {
                    await sw.WriteLineAsync(info);
                }

                using (StreamWriter sw = new StreamWriter(writePath, true, System.Text.Encoding.Default))
                {
                    await sw.WriteLineAsync($"Дозапись {info}");
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}
//ShoppindLogger.logger.Error("", Environment.CurrentManagedThreadId);
