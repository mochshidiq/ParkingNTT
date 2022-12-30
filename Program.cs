using System;
using System.Collections.Generic;
using System.Linq;

namespace Parking
{
    class Program
    {
        public class ParkingData
        {
            public string RegNumber { get; set; }
            public string Colour { get; set; }
            public string Type { get; set; }
            public int Slot { get; set; }
            public char isExist { get; set; }
            public string ClockIn { get; set; }
            public string ClockOut { get; set; }
            public double amount { get; set; }
        }
        static void Main(string[] args)
        {
            int parkingslot = 0, menu = 0;
            List<ParkingData> parkingDatas = new List<ParkingData>();
            Console.WriteLine("Welcome!!!");
            Console.WriteLine("1. Create Parking Slot");
            Console.WriteLine("2. Allocate Parking Slot");
            Console.WriteLine("3. Misallocate Parking Slot");
            Console.WriteLine("4. Status Parking Slot");
            Console.WriteLine("5. Search Count Slot by Type of Vehicle");
            Console.WriteLine("6. Search Registration Number by Odd Plate");
            Console.WriteLine("7. Search Registration Number by Even Plate");
            Console.WriteLine("8. Search Registration Number by Colour");
            Console.WriteLine("9. Search Parking Slot by Colour");
            Console.WriteLine("10. Search Parking Slot by Registration Number");
            Console.WriteLine("11. Exit");
            again:
            Console.Write("Choose your menu : ");
            menu = Int32.Parse(Console.ReadLine());

            switch (menu)
            {
                case 1:
                    Console.Write("create_parking_slot ");
                    parkingslot = Int32.Parse(Console.ReadLine());
                    Console.WriteLine("Created a parking slot with " + parkingslot + " Slots");
                    break;
                case 2:
                    coba:
                    Console.WriteLine("Input the information [Registration Number](space)[Colour](space)[Type]");
                    Console.Write("park ");
                    var data = Console.ReadLine().Split(' ');
                    //string[] data = datum.Split(' ');
                    if (data.Length > 3)
                    {
                        Console.WriteLine("Your input is wrong or too much spaces");
                        goto coba;
                    }
                    else if(parkingDatas.Where(x => x.isExist == 'A').Count() == parkingslot)
                    {
                        Console.WriteLine("Sorry, parking slot is full");
                    }
                    else
                    {
                        int slot = 0;
                        if (parkingDatas.Count() == 0)
                        {
                            slot = 1;
                        }
                        else
                        {
                            for (int i = 1; i <= parkingslot; i++)
                            {
                                int a = parkingDatas.Where(x => x.Slot == i && x.isExist == 'A').Count();
                                if (a == 0)
                                {
                                    slot = i;
                                    break;
                                }
                            }
                        }
                        parkingDatas.Add(new ParkingData
                        {
                            RegNumber = data[0],
                            Colour = data[1],
                            Type = data[2].ToLower,
                            Slot = slot,
                            isExist = 'A',
                            ClockIn = DateTime.Now.ToString("HH:mm")
                        });
                        Console.WriteLine("Allocated slot number : " + slot);
                    }

                    break;
                case 3:
                    Console.Write("leave ");
                    int removedslot = Int32.Parse(Console.ReadLine());
                    ParkingData parkingData = parkingDatas.Where(x => x.Slot == removedslot && x.isExist == 'A').FirstOrDefault();
                    if (parkingData == null)
                        Console.WriteLine("Slot number " + removedslot + " is unavailable");
                    else
                    {
                        parkingData.isExist = 'D';
                        parkingData.ClockOut = DateTime.Now.ToString("HH:mm");
                        var clock = (DateTime.ParseExact(parkingData.ClockOut, "HH:mm", System.Globalization.CultureInfo.InvariantCulture) -
                            DateTime.ParseExact(parkingData.ClockOut, "HH:mm", System.Globalization.CultureInfo.InvariantCulture)).TotalHours;
                        parkingData.amount = clock < 1 ? 5000 : (5000 + (clock * 2000));
                        Console.WriteLine("Reg. Number " + parkingData.RegNumber + " amount is : " + parkingData.amount);

                        Console.WriteLine("Slot number " + removedslot + " is free");
                    }
                    break;
                case 4:
                    Console.WriteLine("Status");
                    Console.WriteLine("Slot" + new string(' ', 3) + "No." + new string(' ', 10) + "Type " + new string(' ', 8) + "Colour" + new string(' ', 5) + "Clock In");
                    foreach (var dt in parkingDatas.Where(x => x.isExist == 'A').ToList().OrderBy(x => x.Slot))
                    {
                        Console.WriteLine(dt.Slot + new string(' ', 6) + dt.RegNumber + new string(' ', 3) + dt.Type +(dt.Type.ToLower() == "car" ? new string(' ', 8) : new string(' ', 2)) + dt.Colour + new string(' ', 5) + dt.ClockIn);
                    }
                    break;
                case 5:
                    Console.Write("type_of_vehicles ");
                    string tipe = Console.ReadLine();
                    if (tipe.ToLower() != "mobil" && tipe.ToLower() != "motor")
                    {
                        Console.WriteLine("Wrong Type");
                    }
                    else
                    {
                        Console.WriteLine(parkingDatas.Where(x => x.Type.ToLower().Equals(tipe.ToLower())).Count());
                    }
                    break;
                case 6:
                    Console.WriteLine("registration_numbers_for_vehicles_with_odd_plate");
                    var pdro = parkingDatas.Select(x => x.RegNumber).ToList();
                    List<string> regno = new List<string>();
                    foreach (var pdro2 in pdro)
                    {
                        var pdro3 = pdro2.Split('-');
                        if (double.Parse(pdro3[1]) % 2 == 1)
                        {
                            regno.Add(pdro2);
                        }
                    }
                    Console.WriteLine(string.Join(", ", regno));
                    break;
                case 7:
                    Console.WriteLine("registration_numbers_for_vehicles_with_even_plate");
                    var pdre = parkingDatas.Select(x => x.RegNumber).ToList();
                    List<string> regne = new List<string>();
                    foreach (var pdre2 in pdre)
                    {
                        var pdre3 = pdre2.Split('-');
                        if (double.Parse(pdre3[1]) % 2 == 0)
                        {
                            regne.Add(pdre2);
                        }
                    }
                    Console.WriteLine(string.Join(", ", regne));
                    break;
                case 8:
                    Console.WriteLine("registration_numbers_for_vehicles_with_colour");
                    string rcolour = Console.ReadLine();
                    var dtrc = parkingDatas.Where(x => x.Colour.ToLower().Equals(rcolour.ToLower())).Select(x => x.RegNumber).ToList();
                    
                    Console.WriteLine(string.Join(", ", dtrc));
                    break;
                case 9:
                    Console.WriteLine("slot_numbers_for_vehicles_with_colour");
                    string scolour = Console.ReadLine();
                    var dtsc = parkingDatas.Where(x => x.Colour.ToLower().Equals(scolour.ToLower())).Select(x => x.Slot).ToList();

                    Console.WriteLine(string.Join(", ", dtsc));
                    Console.WriteLine(dtsc.Count());
                    break;
                case 10:
                    Console.WriteLine("slot_number_for_registration_number");
                    string rn = Console.ReadLine();
                    var dts = parkingDatas.Where(x => x.RegNumber.ToLower().Equals(rn.ToLower())).FirstOrDefault().Slot;

                    Console.WriteLine(dts == 0 ? "Not Found" : dts);                    
                    break;
                case 11:
                    Console.WriteLine("Good Bye");
                    break;
            }
            if (menu != 11)
            {
                string lagi = menulagi();
                if (lagi.ToLower() == "y")
                    goto again;
            }else if(menu > 11)
            {
                Console.WriteLine("Wrong Choice");
                goto again;
            }
            Console.ReadKey();
        }

        public static string menulagi()
        {
            lagi:
            string menu2 = "";
            Console.Write("Access the menu again? [Y/N] : ");
            menu2 = Console.ReadLine();
            if(menu2.ToLower() != "y" && menu2.ToLower() != "n")
            {
                Console.WriteLine("Your choice is wrong!!!");
                goto lagi;
            }

            return menu2;

        }
    }
}
