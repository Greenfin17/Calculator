using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace Calculator
{
    class Program
    {
        static char parseLine(ref string line, ref List<int> intList, ref int power)
        {
            line = line.Trim().ToLower();
            char operation = line[0];
            var tmpValues = Array.Empty<string>();

            if (operation == 'A' || operation == 'a')
            {
                if (line.Length >= 3 && line[1] == 'v' && line[2] == 'g')
                {
                    operation = 'a';
                    line = line.Substring(2);
                }
                else operation = '*';

            }
            if (!Char.IsDigit(operation))
            {
                line = line.Substring(1);
            }
            if (operation == '^')
            {
                int powerTest = 0;
                line = line.Trim();
                string testString = Regex.Match(line, @"\d+").Value;
                if (Int32.TryParse(testString, out powerTest))
                {
                    power = powerTest;
                    line = line.Trim();
                    line = line.Substring(testString.Length);
                }
            }
            tmpValues = line.Split(',', ' ', StringSplitOptions.RemoveEmptyEntries);
            int number = 0;
            int index = 0;
            foreach(var value in tmpValues)
            { 
                if (Int32.TryParse(value, out number))
                {
                    intList.Add(number);
                }

            }
            return operation;
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Line Calculator!");
            Console.WriteLine("Enter an operation followed by comma separated list of integers");
            Console.WriteLine("+,-,*,/,^, avg for average");
            string line = Console.ReadLine();
            Console.WriteLine("You entered " + line);
            int power = 1;
            char op;
            List<int> intList = new List<int>();
            op = parseLine(ref line, ref intList, ref power);
            int[] intValues = intList.ToArray();

            switch(op) {

                case '^':
                    int i = 0;
                    int number = 0;
                    Console.WriteLine("The values to the {0}th power are: ", power);
                    foreach(var value in intValues)
                    {
                        Console.Write("{0:G}", Math.Pow(value, power));
                        i += 1;
                        if (i < intValues.Length)
                        {
                            Console.Write(",");
                        }
                    }
                    Console.Write('\n');
                    break;
                case '+':
                    number = 0;
                    int sum = 0;
                    foreach(var value in intValues)
                    {
                        sum += value;
                    }
                    Console.Write("The sum is: {0:G} \n", sum);
                    break;
                case '-':
                    number = 0;
                    sum = 0;
                    bool first = true;
                    foreach (var value in intValues)
                    {
                        if (first)
                        {
                            sum = value;
                        }
                        else sum -= value;
                        first = false;
                    }
                    Console.Write("The sum is: {0:G} \n", sum);
                    break;
                case '/':
                    float quotient = 1;
                    number = 0;
                    first = true;
                    foreach(var value in intValues)
                    {
                        if (first)
                        {
                            quotient = value;
                        }
                        else
                        {
                            quotient /= (float)value;
                        }
                        first = false;
                    }
                    Console.Write("The quotient is: {0:N4} \n", quotient);
                    break;
                case 'a':
                    float average = 0;
                    float index = 0;
                    sum = 0;
                    foreach(var value in intValues)
                    {
                        sum += value;
                        index++;

                    }
                    average = sum / index; 
                    Console.Write("The average is: {0:N4} \n", average);
                    break;
                case '*':
                default:
                    int product = 1;
                    number = 1;
                    foreach(var value in intValues)
                    {
                        product *= value;
                    }
                    Console.WriteLine("The product is " + product);
                    break;
            }
            Console.Write("Press any key to continue");
            Console.ReadKey();

        }
    }
}
