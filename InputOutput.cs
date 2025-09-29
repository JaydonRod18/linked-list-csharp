using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedList
{
    public class InputOutput
    {
        public InputOutput() { }

        public void displayMessage(String message) 
        {
            Console.WriteLine(message);
        }

        public String obtainDataFromUser(String message)
        {
            Console.Write(message);
            return Console.ReadLine();
        }

        public void blankLine()
        {
            Console.WriteLine();
        }

        public void skipOneLine()
        {
            Console.WriteLine();
        }
    }
}
