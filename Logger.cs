using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace triton1
{
   static class Logger
    {
        public static void saveLog(string str, string nameFile = "")
        {
            if (nameFile == "") { nameFile = "log.txt"; }
            try
            {
         
                StreamWriter log = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + nameFile, true, Encoding.Unicode);
                log.WriteLine(DateTime.Now + " " + str);
                log.Close();
            }

            catch (Exception e)
            {
                Console.WriteLine("ошибка записи лога в файл: " + e.Message);
            }
        }


    }
}
