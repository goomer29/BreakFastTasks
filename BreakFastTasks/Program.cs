using CoreCollectionsAsync;
namespace BreakFastTasks

{
    internal class Program
    {

        public static void Message(object? sender, EventArgs e)
        {
            ElectricCar c = sender as ElectricCar;
            Console.WriteLine("Car Engine Stopped");

        }
        static void Main(string[] args)
        {
            //  SimpleBreakfast.MakeBreakfastDemo_1();
         // TaskedBreakFast.MakeBreakfastDemo_4().Wait();
            ElectricCar car=new ElectricCar();
            car.CarShutDown += Message;
            car.StartEngine();
            
           
          
        }
    }
}