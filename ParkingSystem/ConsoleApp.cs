using ConsoleApp2.Entity;

public class ConsoleApp
{
    public static void Main(string[] args)
    {
      
        Console.WriteLine("$---------------$");
        
        Console.WriteLine("Parking Lot System");
        
        Console.WriteLine("$---------------$");

        Console.Write("Created a parking lot with 6 slots: (Input number 6) => " );
        int totalLots = int.Parse(Console.ReadLine());
        
        ParkingLot parkingLot = new ParkingLot(totalLots);

        string command = string.Empty;

        while (command != "exit")
        {
            Console.WriteLine("$---------------$");
            Console.WriteLine("1. Check-in ");
            Console.WriteLine("2. Check-out ");
            Console.WriteLine("3. Report ");
            Console.WriteLine("4. Exit ");
            Console.Write("Your choice => ");
            
            command = Console.ReadLine();

            switch (command)
            {
                case "1":
                    Console.Write("Input number plate: (Must format example 'B-1212'):");
                    string licensePlate = Console.ReadLine();

                    Console.Write("Enter vehicle color: ");
                    string color = Console.ReadLine();

                    Console.Write("Enter vehicle type (Mobil Kecil or Motor): ");
                    string type = Console.ReadLine();
                    
                    Console.WriteLine("$---------------$");

                    parkingLot.CheckIn(licensePlate, color, type);
                    break;

                case "2":
                    Console.Write("Enter license plate: ");
                    licensePlate = Console.ReadLine();

                    parkingLot.CheckOut(licensePlate);
                    break;

                case "3":
                    parkingLot.GenerateReports();
                    break;

                case "4":
                    Console.WriteLine("Exit program.");
                    break;

                default:
                    Console.WriteLine("Invalid command.");
                    break;
            }
        }
    }
}