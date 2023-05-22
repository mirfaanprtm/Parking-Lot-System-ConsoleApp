namespace ConsoleApp2.Entity;

public class ParkingLot
{
    private int TotalLots { get; }
    private List<Vehicle> Vehicles { get; }
    private Dictionary<int, Vehicle> Slots { get; }
    
    
    public ParkingLot(int totalLots)
    { 
        TotalLots = totalLots;
        Vehicles = new List<Vehicle>();
        Slots = new Dictionary<int, Vehicle>();
    }
    
    public void CheckIn(string licensePlate, string color, string type)
        {
            if (Vehicles.Count >= TotalLots)
            {
                Console.WriteLine("Sorry, parking lot is full.");
                return;
            }

            if (type != "Mobil Kecil" && type != "Motor")
            {
                Console.WriteLine("Invalid vehicle type.");
                return;
            }

            Vehicle vehicle = new Vehicle
            {
                LicensePlate = licensePlate,
                Color = color,
                Type = type,
                CheckInTime = DateTime.Now
            };

            Vehicles.Add(vehicle);
            Console.WriteLine($"Park '{licensePlate}' '{color}' '{type}' . Allocated slot number '{Vehicles.Count}'");
        }

    public void CheckOut(string licensePlate)
    {
        Vehicle vehicle = Vehicles.Find(v => v.LicensePlate == licensePlate);

        if (vehicle == null)
        {
            Console.WriteLine($"Vehicle with license plate '{licensePlate}' is not found in the parking lot.");
            return;
        }

        Vehicles.Remove(vehicle);
        Slots.Remove(vehicle.SlotNumber); // Hapus entri slot dari dictionary Slots

        TimeSpan duration = DateTime.Now - vehicle.CheckInTime;
        double hours = Math.Ceiling(duration.TotalHours);
        double totalCost = hours * 5000; 

        Console.WriteLine($"Plate number '{licensePlate}' has left. Slot number '{vehicle.SlotNumber}' is free.");
        Console.WriteLine($"Duration: {hours} hours");
        Console.WriteLine($"Total cost: {totalCost} rupiah");
    }

        public void GenerateReports()
        {
            Console.WriteLine("----------- Reports -----------");
            Console.WriteLine($"Total Lots: {TotalLots}");
            Console.WriteLine($"Filled Lots: {Vehicles.Count}");
            Console.WriteLine($"Available Lots: {TotalLots - Vehicles.Count}");

            Console.WriteLine("Vehicles by License Plate (Odd/Even) =>");
            int oddCount = 0;
            int evenCount = 0;

            foreach (var vehicle in Vehicles)
            {
                if (vehicle.LicensePlate.Length > 0)
                {
                    int lastDigit;
                    bool isValidLastDigit = int.TryParse(vehicle.LicensePlate.Substring(vehicle.LicensePlate.Length - 1), out lastDigit);

                    if (isValidLastDigit)
                    {
                        if (lastDigit % 2 == 0)
                            evenCount++;
                        else
                            oddCount++;
                    }
                    else
                    {
                        Console.WriteLine($"Invalid license plate number: {vehicle.LicensePlate}");
                    }
                }
                else
                {
                    Console.WriteLine("Empty license plate number.");
                }
            }

            Console.WriteLine($"Odd Vehicle Count: {oddCount}");
            Console.WriteLine($"Even Vehicle Count: {evenCount}");

            Console.WriteLine("Vehicles by Type =>");
            int smallCarCount = 0;
            int motorCount = 0;
            foreach (var vehicle in Vehicles)
            {
                if (vehicle.Type == "Mobil Kecil")
                    smallCarCount++;
                else if (vehicle.Type == "Motor")
                    motorCount++;
            }
            Console.WriteLine($"Mobil Kecil: {smallCarCount}");
            Console.WriteLine($"Motor: {motorCount}");

            Console.WriteLine("Vehicles by Color and Type =>");
            Dictionary<string, Dictionary<string, int>> colorTypeCount = new Dictionary<string, Dictionary<string, int>>();
            foreach (var vehicle in Vehicles)
            {
                if (!string.IsNullOrEmpty(vehicle.Color))
                {
                    if (!colorTypeCount.ContainsKey(vehicle.Color))
                    {
                        colorTypeCount[vehicle.Color] = new Dictionary<string, int>();
                    }

                    Dictionary<string, int> typeCount = colorTypeCount[vehicle.Color];
                    string vehicleType = vehicle.Type;

                    if (typeCount.ContainsKey(vehicleType))
                    {
                        typeCount[vehicleType]++;
                    }
                    else
                    {
                        typeCount[vehicleType] = 1;
                    }
                }
            }

            foreach (var color in colorTypeCount)
            {
                Console.WriteLine(color.Key + ":");
                foreach (var type in color.Value)
                {
                    Console.WriteLine($"  {type.Key}: {type.Value}");
                }
            }


            Console.WriteLine("$---------------$");
        }
}
