using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace CoreCollectionsAsync
{
    public class Battery
    {
        const int MAX_CAPACITY = 1000;
        private static Random r = new Random();




        public event EventHandler ShutDown;// אירוע כיבוי סוללה
        public event EventHandler ReachThreshold;//אירוע כאשר מגיעים לסף סוללה שנדרשת טעינה
        private int Threshold { get; }//סף שמתחתיו נדרש להטעין את הסוללה
        public int Capacity { get; set; }//כמה מיליאפר כרגע טעון בסוללה
        public int Percent//אחוז טעינה
        {
            get
            {
                return 100 * Capacity / MAX_CAPACITY;
            }
        }
        public Battery()
        {
            Capacity = MAX_CAPACITY;
            Threshold = 400;
        }

        public void Usage()
        {
            //בכל פעם - טעינת הסוללה מופחתת בצורה אקראית
            int usage = r.Next(50, 150);
            if (Capacity - usage >= 0)
                Capacity -= usage;
            else
                Capacity = 0;

            //תירה את אירוע טעינה נמוכה
            if (Capacity <= Threshold&&Capacity>=200)
                OnReachThreshold();
            //תירה כיבוי סוללה
            if (Capacity < 200)
                OnShutDown();
            if (Capacity < 0)
                throw new ArgumentOutOfRangeException("Battary is Empty");
        }

        private void OnReachThreshold()
        {
            ReachThreshold?.Invoke(this, new EventArgs());
        }

        private void OnShutDown()
        {
            ShutDown?.Invoke(this, new EventArgs());
        }
    }

    class ElectricCar
    {
        public static int i = 0;
        public event EventHandler CarShutDown;

        public Battery Bat { get; set; }
        public int id { get; set; }

        private bool engingRunning;
        public ElectricCar()
        {
            Bat = new Battery();
            id = ++i;
            engingRunning = false;
            #region Register to battery events
            Bat.ReachThreshold += Battery_ReachThreshold;
            Bat.ShutDown += Battery_ShutDown;
            #endregion
        }
        public void StartEngine()
        {
            engingRunning = true;
            while (Bat.Percent > 15)
            {
                Console.WriteLine($"CAR WITH ID:{this.id} {Bat.Percent}% Thread: {Thread.CurrentThread.ManagedThreadId}");
                Thread.Sleep(1000);
                Bat.Usage();
            }
        }
        private void Battery_ShutDown(object? sender, EventArgs e)
        {
            //  Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(" Low Battery - Shutting down");
            Thread.Sleep(1000);
            OnCarShutDown();
            //  Environment.Exit(0);
        }

        private void Battery_ReachThreshold(object? sender, EventArgs e)
        {
            //   Console.Clear();
            Thread.Sleep(1000);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Low Battery. Please Charge");

        }

        private void OnCarShutDown()
        {
            CarShutDown?.Invoke(this, new EventArgs());
        }

        public void StopEngine()
        {
            engingRunning = false;
        }
    }
    class EventsExercise
    {
        public static List<ElectricCar> runningCars = new List<ElectricCar>();


        public static void Start()
        {
        }


    }
}

