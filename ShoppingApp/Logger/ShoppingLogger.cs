using System;
using System.IO;

namespace Logger
{
    public static class ShoppingLogger
    {
        public static Logger logger;
        public static void InitLogger()
        {
            logger = new Logger();
            string writePath = System.IO.Path.Combine(Environment.CurrentDirectory, @"logs");
            if (!Directory.Exists(writePath)) Directory.CreateDirectory(writePath);
        }
    }
}
