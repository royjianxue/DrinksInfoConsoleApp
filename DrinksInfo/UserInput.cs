using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinksInfo
{
    public class UserInput
    {

        public static string AskUserStringInput(string message)
        {
            string output = "";
            Console.Write(message);
            output = Console.ReadLine();
            return output;
        }

        public static int AskUserIntInput(string message)
        {
            int output = 0;
            string input = "";

            do
            {
                Console.Write(message);
                input = Console.ReadLine();

            } while (!int.TryParse(input, out output));

            return output;
        }

    }
}
