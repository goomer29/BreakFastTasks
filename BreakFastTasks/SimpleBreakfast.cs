using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BreakFastTasks;

namespace CoreCollectionsAsync
{

    class SimpleBreakfast
    {
        public static DateTime startTime;
        public static void MakeBreakfastDemo_1()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            DateTime start = DateTime.Now;
            //Prepare Omlette
            startTime = DateTime.Now;
            Console.WriteLine($"Start preparing the Omlette at {startTime.ToString()}");
            Omlette myOmlette = new Omlette("myOmlette");
            myOmlette.OnProgressUpdate += Progress;
            myOmlette.OnFinish += Finish;
            myOmlette.Start();

            //Prepare toast
            startTime = DateTime.Now;
            Console.WriteLine($"Start preparing the toast at {startTime.ToString()}");

            Toast toast = new Toast("toast");
            toast.OnProgressUpdate += Progress;
            toast.OnFinish += Finish;
            toast.Start();

            //Prepare first cucumbers
            startTime = DateTime.Now;
            Console.WriteLine($"Start preparing the first cucumber at {startTime.ToString()}");

            Cucumber cucumber1 = new Cucumber("first cucumber");
            cucumber1.OnProgressUpdate += Progress;
            cucumber1.OnFinish += Finish;
            cucumber1.Start();

            //Prepare second cucumbers
            startTime = DateTime.Now;
            Console.WriteLine($"Start preparing the second cucumber at {startTime.ToString()}");

            Cucumber cucumber2 = new Cucumber("second cucumber");
            cucumber2.OnProgressUpdate += Progress;
            cucumber2.OnFinish += Finish;
            cucumber2.Start();

            //Prepare tomato
            startTime = DateTime.Now;
            Console.WriteLine($"Start preparing the tomato at {startTime.ToString()}");
            Tomato tomato = new Tomato("tomato");
            tomato.OnProgressUpdate += Progress;
            Console.WriteLine();
            tomato.OnFinish += Finish;
            tomato.Start();

            stopWatch.Stop();
            Console.WriteLine($"Breakfast is ready after {stopWatch.Elapsed.TotalSeconds} seconds");
            DateTime end = DateTime.Now;
            TimeSpan length = end - start;
            Console.WriteLine($"Breakfast is ready at {end.ToString()}");
            Console.WriteLine($"Total time in seconds: {length.TotalSeconds}");

        }

        //The event OnProgressUpdate will fire this function! 
        static void Progress(Object? sender, int percent)
        {
            if (sender is TaskExecutor)
            {

                TaskExecutor obj = (TaskExecutor)sender;
                Console.Write($"Progress for {obj.Name}: {percent}%");
                Console.SetCursorPosition((Console.CursorLeft -(obj.Name.Length+percent.ToString().Length+16)), Console.CursorTop);
            }
        }

        //The event OnFinish will fire this function! 
        static void Finish(Object? sender, EventArgs e)
        {
            if (sender is TaskExecutor)
            {
                TaskExecutor obj = (TaskExecutor)sender;
                Console.WriteLine($"\n{obj.Name} is ready!");
            }
        }









    }
}
