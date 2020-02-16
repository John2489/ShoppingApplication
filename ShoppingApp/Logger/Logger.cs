using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;

namespace Logger
{
    public class Logger
    {
        private const int sizeFileLog = 30_000;
        private StackTrace trace;
        private MethodBase method;
        private static DateTime dateFileLog;
        private static int counterLog = 0;
        public void Info(string info, int threadId)
        {
            trace = new StackTrace();
            method = trace.GetFrames()[1].GetMethod();
            DateTime now = DateTime.Now;
            WriteLog(InfoLine(trace, method, now, "INFO", threadId, info), now);
        }
        public void Debug(string info, int threadId)
        {
            trace = new StackTrace();
            method = trace.GetFrames()[1].GetMethod();
            DateTime now = DateTime.Now;
            WriteLog(InfoLine(trace, method, now, "DEBUG", threadId, info), now);
        }
        public void Error(string info, int threadId)
        {
            trace = new StackTrace();
            method = trace.GetFrames()[1].GetMethod();
            DateTime now = DateTime.Now;
            string infoLine = InfoLine(trace, method, now, "ERROR", threadId, info);
            WriteLog(infoLine, now);
            SendEmailAsync(infoLine).GetAwaiter();
        }

        private string InfoLine(StackTrace trace, MethodBase method,
                                DateTime dateTime, string loggerType,
                                int threadId, string info)
            => $"{dateTime.ToString("yyyy'-'MM'-'dd' | 'HH':'mm':'ss'.'fffffff")} | [{loggerType}] | "+
               $"{trace.GetFrames()[1].GetMethod().ReflectedType.ToString()} | "+
               $"{method.Name} | [{threadId}] | {info}";

        public void WriteLog(string infoLine, DateTime now)
        {
            WriteInFileAsync(infoLine, now).GetAwaiter();
        }
        private static async Task SendEmailAsync(string info)
        {
            MailAddress from = new MailAddress("shopping.app180220@gmail.com", "SPL Application");
            MailAddress to = new MailAddress("johnturner.2489@gmail.com");
            MailMessage m = new MailMessage(from, to);
            m.Subject = "ERROR in Shopping application";
            m.Body = info;
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.Credentials = new NetworkCredential("shopping.app180220@gmail.com", "gvardia4455");
            smtp.EnableSsl = true;
            await smtp.SendMailAsync(m);
        }
        private static async Task WriteInFileAsync(string info, DateTime now)
        {
            string writePath;

            if (dateFileLog == null || dateFileLog.ToString("yyyyMMdd") == now.ToString("yyyyMMdd"))
            {
                writePath = Path.Combine(Environment.CurrentDirectory, 
                                         @$"logs\log {now.ToString("yyyyMMdd")}_[{counterLog}].txt");
                if(File.Exists(writePath))
                {
                    FileInfo fileInfo = new FileInfo(writePath);
                    if (fileInfo.Length >= sizeFileLog)
                    {
                        counterLog++;
                        writePath = Path.Combine(Environment.CurrentDirectory,
                                                 @$"logs\log {now.ToString("yyyyMMdd")}_[{counterLog}].txt");
                    }
                }
                else
                {
                    writePath = Path.Combine(Environment.CurrentDirectory,
                                             @$"logs\log {now.ToString("yyyyMMdd")}_[{counterLog}].txt");
                }
            }
            else
            {
                dateFileLog = now;
                counterLog = 0;
                writePath = Path.Combine(Environment.CurrentDirectory,
                                         @$"logs\log {now.ToString("yyyyMMdd")}_[{counterLog}].txt");
            }

            try
            {
                if (File.Exists(writePath))
                {
                    using (StreamWriter sw = new StreamWriter(writePath, true, System.Text.Encoding.Default))
                    {
                        await sw.WriteLineAsync(info);
                    }
                }
                else
                {
                    using (StreamWriter sw = new StreamWriter(writePath, false, System.Text.Encoding.Default))
                    {
                        await sw.WriteLineAsync(info);
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}