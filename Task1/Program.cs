using System;

namespace Task1
{
    class Program
    {
        static void Main(string[] args)
        {
            string fileName = string.Empty;

            //Проверка передаваемых аргументов
            if (args.Length != 1)
            {
                Console.WriteLine("Отсутствует параметр запуска или их кол-во больше 1");
                Console.ReadKey();
                return;
            }

            fileName = args[0];

            InputFile inputFile = new InputFile(fileName);
            try
            {
                inputFile.Start();
            }
            catch (Exception e)
            {
                Console.WriteLine("Ошибка: {0}", e);
            }
        }
    }
}
