using pomogite_423;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class Program()
{
    static void Main(string[] args)
    {
        Farm farm = new Farm();
        farm.StartRam();

        while (true)
        {
            farm.ShowMenu();
            int choice = int.Parse(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    farm.AddRam();
                    break;
                case 2:
                    farm.DelRam();
                    break;
                case 3:
                    farm.ShowRam();
                    break;
                case 0:
                    break;
                default:
                    break;

            }
            if(choice == 0) 
                break;
        }   
    }
}