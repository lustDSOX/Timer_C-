using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Console;

namespace ConsoleApp1
{
    internal class Program
    {
        static DateTime time;
        static DateTime timer = new DateTime();
        static ManualResetEvent _event;
        static Thread tClock = new Thread(Clock);
        
        static void Main(string[] args)
        {
            _event = new ManualResetEvent(true);
            WriteLine("Список команд - help");
            tClock.Start();
            _event.Reset();
            while (true)
            {
                switch (ReadLine())
                {
                    case "help":
                        PrintHelp();
                        break;
                    case "1":
                        SetCursorPosition(0, 2);
                        WriteLine("         ");
                        SetCursorPosition(0, 2);
                        _event.Reset();
                        break;
                    case "2":
                        _event.Set();
                        break;
                    case "3":
                        break;
                    case "4":
                        break;
                    case "5":
                        SetCursorPosition(0, 2);
                        WriteLine("         ");
                        SetCursorPosition(0, 2);
                        _event.Reset();
                        Thread tWather = new Thread(StopWather);
                        Thread tTimer = new Thread(UpdateTimer);
                        tWather.Start();
                        tTimer.Start();
                        break;
                    case "6":
                        break;
                    case "0":
                        return;
                }
            }
        }

        static void StopWather()
        {
            while(true)
            {
                SetCursorPosition(0, 1);
                WriteLine(timer.ToString("mm:ss:ff"));
               // Thread.Sleep(1);
            }

        }

        static void UpdateTimer()
        {
            while (true)
            {
                timer = timer.AddMilliseconds(1);
                Thread.Sleep(1);
            }
        }
        static void Clock()
        {
            while (true)
            {
                _event.WaitOne();
                time = DateTime.Now;
                SetCursorPosition(0, 1);
                WriteLine(" \n  \n");
                SetCursorPosition(0, 1);
                WriteLine(time.ToLongTimeString());
                Thread.Sleep(900);
            }
        }

        static void PrintHelp()
        {
            Clear();
            WriteLine("1 - остановить часы\n" +
                "2 - запустить часы\n" +
                "3 - настройки времени\n" +
                "4 - таймер\n" +
                "5 - запустить секундомер\n" +
                "6 - остановить секундомер\n" +
                "0 - выход");
        }
    }
}
