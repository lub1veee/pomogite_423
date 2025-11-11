using pomogite_423;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

class Program()
{
    static void Main(string[] args)
    {
        Farm farm = new Farm();
        farm.Initialize();

        while (true)
        {
            farm.ShowMenu();
            int choice = int.Parse(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    farm.AddCow();
                    break;
                case 2:
                    farm.AddCalf();
                    break;
                case 3:
                    farm.AllAnimal();
                    break;
                default:
                    break;
            }
        }
    }
}
