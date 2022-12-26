using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreakFastTasks
{
    public static class TaskedBreakFast
    {
        //The event OnProgressUpdate will fire this function! 
        static void Progress(Object? sender, int percent)
        {
            if (sender is TaskExecutor)
            {

                TaskExecutor obj = (TaskExecutor)sender;
                Console.WriteLine($"Progress for {obj.Name}: {percent}%");
                // Console.SetCursorPosition((Console.CursorLeft -(obj.Name.Length+percent.ToString().Length+16)), Console.CursorTop);
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


        static async Task<Omlette> PrepareOmlette()
        {
            Console.WriteLine($"Start preparing the Omlette at {DateTime.Now.ToString()}");
            Omlette myOmlette = new Omlette("myOmlette");
            myOmlette.OnProgressUpdate += Progress;
            await Task.Run(myOmlette.Start);
            Console.WriteLine("Omlete is ready");
            return myOmlette;
        }

        static async Task<Toast> PrepareToast()
        {
            Console.WriteLine($"Start preparing the toast at {DateTime.Now.ToString()}");
            Toast toast = new Toast("toast");
            toast.OnProgressUpdate += Progress;
            await Task.Run(toast.Start);
            Console.WriteLine("Toast is ready!");
            return toast;
        }

        static async Task PrepareSalad()
        {
            //Prepare first cucumbers
            Console.WriteLine($"Start preparing the first cucumber at {DateTime.Now.ToString()}");
            Cucumber cucumber1 = new Cucumber("first cucumber");
            cucumber1.OnProgressUpdate += Progress;
            cucumber1.Start();

            //Prepare second cucumbers
            Console.WriteLine($"Start preparing the second cucumber at {DateTime.Now.ToString()}");
            Cucumber cucumber2 = new Cucumber("second cucumber");
            cucumber2.OnProgressUpdate += Progress;
            cucumber2.Start();

            //Prepare tomato
            Console.WriteLine($"Start preparing the tomato at {DateTime.Now.ToString()}");
            Tomato tomato = new Tomato("tomato");
            tomato.OnProgressUpdate += Progress;
            tomato.Start();

        }




        public static async Task MakeBreakfastDemo_4()
        {
            DateTime start = DateTime.Now;
            //Prepare Omlette
            Console.WriteLine($"Start preparing the Omlette at {DateTime.Now.ToString()}");
            Task<Omlette> omlTask = PrepareOmlette();


            //Prepare toast
            Console.WriteLine($"Start preparing the toast at {DateTime.Now.ToString()}");
            Task<Toast> toastTask = PrepareToast();

            //Prepare Salad
            Console.WriteLine($"Start preparing the Salad at {DateTime.Now.ToString()}");
            Task saladTask = PrepareSalad();


            //wait for all tasks to be over!
            //while (tasks.Count > 0) { }
            await Task.WhenAll(omlTask, toastTask, saladTask);
            DateTime end = DateTime.Now;
            TimeSpan length = end - start;
            Console.WriteLine($"Breakfast is ready at {end.ToString()}");
            Console.WriteLine($"Total time in seconds: {length.TotalSeconds}");

        }

        public static async Task MakeBreakfastDemo_5()
        {
            DateTime start = DateTime.Now;
            //Prepare Omlette
            Console.WriteLine($"Start preparing the Omlette at {DateTime.Now.ToString()}");
            Task<Omlette> omlTask = PrepareOmlette();


            //Prepare toast
            Console.WriteLine($"Start preparing the toast at {DateTime.Now.ToString()}");
            Task<Toast> toastTask = PrepareToast();

            //Prepare Salad
            Console.WriteLine($"Start preparing the Salad at {DateTime.Now.ToString()}");
            Task saladTask = PrepareSalad();


            //wait for all tasks to be over!
            List<Task> taskList = new List<Task> { omlTask, toastTask, saladTask };
            while (taskList.Count > 0)
            {
                Task finishedTask = await Task.WhenAny(taskList);
                if (finishedTask == omlTask)
                {
                    Omlette o = await omlTask;
                    Console.WriteLine($"{o.Name} is ready");
                }
                else if (finishedTask == toastTask)
                {
                    Toast t = await toastTask;
                    Console.WriteLine($"{t.Name} is ready");
                }
                else if (finishedTask == saladTask)
                {
                    Console.WriteLine("Salad is ready");
                }
                taskList.Remove(finishedTask);
            }


            DateTime end = DateTime.Now;
            TimeSpan length = end - start;
            Console.WriteLine($"Breakfast is ready at {end.ToString()}");
            Console.WriteLine($"Total time in seconds: {length.TotalSeconds}");

        }
    }
      
}

