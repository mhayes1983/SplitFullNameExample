using System;
using System.ComponentModel.DataAnnotations;
using SplitFullNameCore;
using SplitFullNameCore.Interfaces;
using SplitFullNameCore.Modules;

namespace SplitFullName
{
    class Program
    {
        static void Main(string[] args)
        {
            //Display Message
            Console.WriteLine("Please enter person's full name: ");

            //Read Input
            string strFullName = Console.ReadLine();

            //Perform Full Name split
            ISplitFullName splitFullNameModule = new SplitFullNameModule();
            IName name = splitFullNameModule.SplitFullName(strFullName);

            //Print results of split
            Console.WriteLine(string.Format("First Name: '{0}', Middle Name '{1}', Last Name '{2}'", name.FirstName, name.MiddleName, name.LastName));
        }
    }
}
