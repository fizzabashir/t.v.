using System;

public abstract class ToolVehicle
{
    public int VehicleID { get; set; }
    public string RegNo { get; set; }
    public string Model { get; set; }
    public string Brand { get; set; }
    public decimal BasePrice { get; set; }
    public string VehicleType { get; set; }

    // Static properties
    public static int TotalVehicles { get; private set; }
    public static int TotalTaxPayingVehicles { get; private set; }
    public static int TotalNonTaxPayingVehicles { get; private set; }
    public static decimal TotalTaxCollected { get; private set; }

 
   public ToolVehicle()
    {
        TotalVehicles++;
    }

  public abstract void PayTax();
    public void PassWithoutPaying()
    {
        TotalNonTaxPayingVehicles++;
    }
    protected void UpdateTaxStats(decimal taxAmount)
    {
        TotalTaxCollected += taxAmount;
        TotalTaxPayingVehicles++;
    }
}

//Car Class

public class Car : ToolVehicle
{
    public Car()
    {
        VehicleType = "Car";
    }

    public override void PayTax()
    {
        const decimal taxAmount = 2.00m;
        UpdateTaxStats(taxAmount);
    }
}
//Bike Class

public class Bike : ToolVehicle
{
    public Bike()
    {
        VehicleType = "Bike";
    }

    public override void PayTax()
    {
        const decimal taxAmount = 1.00m;
        UpdateTaxStats(taxAmount);
    }
}
//HeavyVehicle Class

public class HeavyVehicle : ToolVehicle
{
    public HeavyVehicle()
    {
        VehicleType = "Heavy Vehicle";
    }

    public override void PayTax()
    {
        const decimal taxAmount = 4.00m;
        UpdateTaxStats(taxAmount);
    }
}
//Main Program Class

public class Program
{
    static List<ToolVehicle> vehicles = new List<ToolVehicle>();

    public static void Main()
    {
        bool running = true;
        while (running)
        {
            DisplayMenu();
            int choice = GetUserChoice();

            switch (choice)
            {
                case 1:
                    AddVehicle(new Car());
                    break;
                case 2:
                    AddVehicle(new Bike());
                    break;
                case 3:
                    AddVehicle(new HeavyVehicle());
                    break;
                case 4:
                    PassWithoutPaying();
                    break;
                case 5:
                    ShowTotalVehicles();
                    break;
                case 6:
                    ShowTotalTaxCollected();
                    break;
                case 7:
                    ShowTotalTaxPayingVehicles();
                    break;
                case 8:
                    ShowTotalNonTaxPayingVehicles();
                    break;
                case 9:
                    running = false;
                    break;
                default:
                    Console.WriteLine("Invalid choice, please enter a number between 1 and 8.");
                    break;
            }
        }
    }
    static void DisplayMenu()
    {
        Console.WriteLine("1. Add Car");
        Console.WriteLine("2. Add Bike");
        Console.WriteLine("3. Add Heavy Vehicle");
        Console.WriteLine("4. Show Total Vehicles");
        Console.WriteLine("4. Show Total Tax Collected");
        Console.WriteLine("6. Show Total Tax Paying Vehicles");
        Console.WriteLine("7. Show Total Non-Tax Paying Vehicles");
        Console.WriteLine("8. Exit");
    }
    static int GetUserChoice()
    {
        int choice;
        if (!int.TryParse(Console.ReadLine(), out choice))
        {
            Console.WriteLine("Invalid input, please enter a number between 1 and 9.");
            return -1;
        }
        return choice;
    }
    static void AddVehicle(ToolVehicle vehicle)
    {
        Console.WriteLine($"Enter Registration Number for {vehicle.VehicleType}:");
        vehicle.RegNo = Console.ReadLine();
        Console.WriteLine($"Enter Model for {vehicle.VehicleType}:");
        vehicle.Model = Console.ReadLine();
        Console.WriteLine($"Enter Brand for {vehicle.VehicleType}:");
        vehicle.Brand = Console.ReadLine();
        Console.WriteLine($"Enter Base Price for {vehicle.VehicleType}:");
        vehicle.BasePrice = Convert.ToDecimal(Console.ReadLine());

        vehicles.Add(vehicle);

        Console.WriteLine("Press 1 to pay tax, or 2 to pass without paying:");
        int choice = GetUserChoice();

        if (choice == 1)
        {
            vehicle.PayTax();
            Console.WriteLine($"{vehicle.VehicleType} has paid the tax.");
        }
        else if (choice == 2)
        {
            vehicle.PassWithoutPaying();
            Console.WriteLine($"{vehicle.VehicleType} passed without paying.");
        }
        else
        {
            Console.WriteLine("Invalid choice, please enter 1 or 2.");
        }
    }
    static void PassWithoutPaying()
    {
        Console.WriteLine("Enter Vehicle Type (Car, Bike, Heavy Vehicle):");
        string type = Console.ReadLine().ToLower();
        ToolVehicle vehicle = vehicles.Find(v => v.VehicleType.ToLower() == type);

        if (vehicle != null)
        {
            vehicle.PassWithoutPaying();
            Console.WriteLine($"{vehicle.VehicleType} passed without paying.");
        }
        else
        {
            Console.WriteLine("No such vehicle found.");
        }
    }
    static void ShowTotalVehicles()
    {
        Console.WriteLine($"Total Vehicles: {ToolVehicle.TotalVehicles}");
    }

    static void ShowTotalTaxCollected()
    {
        Console.WriteLine($"Total Tax Collected: {ToolVehicle.TotalTaxCollected:C}");
    }

  static void ShowTotalTaxPayingVehicles()
    {
        Console.WriteLine($"Total Tax Paying Vehicles: {ToolVehicle.TotalTaxPayingVehicles}");
    }
 static void ShowTotalNonTaxPayingVehicles()
    {
        Console.WriteLine($"Total Non-Tax Paying Vehicles: {ToolVehicle.TotalNonTaxPayingVehicles}");
    }
}