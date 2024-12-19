using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Configuration;

using System.Threading.Tasks;

namespace ConsoleEmployeeAppCRUD.Utility
{
    public class Validations
    {
        static string ValidateInput(string pattern, string errorMessage)
        {
            string input;
            while (true)
            {
                input = Console.ReadLine();
                if (Regex.IsMatch(input, pattern))
                {
                    break;
                }
                else
                {
                    Console.WriteLine(errorMessage);
                    Console.Write("Please enter again: ");
                }
            }
            return input;
        }

        static int ValidateSalary(string input = null)
        {
            int salary;
            while (true)
            {
                if (input == null)
                    input = Console.ReadLine();

                if (int.TryParse(input, out salary) && salary > 0)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid Salary (must be a positive integer).");
                    Console.Write("Please enter again: ");
                    input = null;
                }
            }
            return salary;
        }
    }
}
