using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        static TimeSpan timer;
        static ManualResetEvent _event;
        static ManualResetEvent _watch;
        static Stopwatch stopWatch = new Stopwatch();
        static void Main(string[] args)
        {
            _event = new ManualResetEvent(true);
            _watch = new ManualResetEvent(true);
            WriteLine("Список команд - help");
            Thread tClock = new Thread(Clock);
            tClock.Start();
            Thread tWather = new Thread(StopWather);
            _event.Reset();
            _watch.Reset();
            tWather.Start();
            

            while (true)
            {
                switch (ReadLine())
                {
                    case "help":
                        PrintHelp();
                        break;
                    case "1":
                        Clear();
                        WriteLine("Список команд - help");
                        _event.Reset();
                        break;
                    case "2":
                        Clear();
                        WriteLine("Список команд - help");
                        _event.Set();
                        break;
                    case "3":
                        break;
                    case "4":
                        _event.Reset();
                        _watch.Reset();
                        SetCursorPosition(0, 1);
                        WriteLine("Введите кол-во секунд");
                        int n = int.Parse(ReadLine());
                        Clear();
                        WriteLine("Список команд - help");
                        Thread tTimer = new Thread(Timer);
                        tTimer.Start(n);
                        break;
                    case "5":
                        _event.Reset();
                        Clear();
                        WriteLine("Список команд - help");
                        _watch.Set();
                        stopWatch.Start();
                        break;
                    case "6":
                        stopWatch.Stop();
                        _watch.Reset();
                        Clear();
                        WriteLine("Список команд - help");
                        break;
                    case "7":
                        _watch.Reset();
                        stopWatch.Reset();
                        Clear();
                        WriteLine("Список команд - help");
                        break;
                    case "0":
                        return;
                }
            }
        }

        static void StopWather()
        {
            while (true)
            {
                _watch.WaitOne();
                timer = stopWatch.Elapsed;
                SetCursorPosition(0, 1);
                WriteLine(timer.ToString(@"mm\:ss\:ff"));
                Thread.Sleep(1);
            }

        }

        static void Timer(object n)
        {
            if(n is int trigger)
            {
                DateTime span = new DateTime();
                for (int i = 0; i < trigger; i++)
                {
                    span = span.AddSeconds(1);
                    SetCursorPosition(0, 1);
                    WriteLine(span.ToString("mm:ss"));
                    Thread.Sleep(1000);
                }
                WriteLine("Время вышло");
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
                "7 - выключить секундомер\n" +
                "0 - выход");
        }
    }
}
