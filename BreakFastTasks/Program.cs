using CoreCollectionsAsync;
namespace BreakFastTasks

{
    internal class Program
    {
        public static async Task StartAll(int n)
        {
            Task[] tasks = new Task[n];
            ElectricCar[] cars = new ElectricCar[n];
            for (int i = 0; i < n; i++)
            {
                cars[i] = new ElectricCar();
                tasks[i] = cars[i].StartEngineAsync();
            }
            await Task.WhenAll(tasks);
        }
        public static void Message(object? sender, EventArgs e)
        {
            ElectricCar c = sender as ElectricCar;
            Console.WriteLine("Car Engine Stopped");

        }
        static async Task Main(string[] args)
        {
            //  SimpleBreakfast.MakeBreakfastDemo_1();
            // TaskedBreakFast.MakeBreakfastDemo_4().Wait();
           await StartAll(10);
            Console.WriteLine("all finished");
            
           
          
        }
    }
}