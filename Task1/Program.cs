using System;

//Как лучше читать данные из файла?
//Тернарная запись, на сколько удобно?
//Нормально ли использовать статические переменные в методах?
//нормально ли использовать переменные с одинаковыми именами в разных методах/лог.конструкциях?
//Как лучше организовывать структуру проекта/решения?
//С чего лучше начинать работу?

namespace Task1
{
    class Program
    {
        static void Main(string[] args)
        {
            string text = string.Empty;
            int numTestBlock;
            Processing processing = new Processing();

            //Проверка передаваемых аргументов
            if (args.Length != 1)
            {
                Console.WriteLine("Отсутствует параметр запуска или их кол-во больше 1");
                Console.ReadKey();
                return;
            }

            text = processing.GetFileContent(args[0]);
            
            //Проверка на пустоту
            if (string.IsNullOrEmpty(text))
            {
                Console.WriteLine("Файл не был открыт или отсутствует содержимое");
                Console.ReadKey();
                return;
            }

            string[] content = text.Split(new string[] { "\r\n" }, StringSplitOptions.None);
            numTestBlock = processing.GetNumTestBlock(content[0]);

            //Проверка кол-ва тестовых блоков (по условию: число - целое, положительное)
            if (numTestBlock == 0)
            {
                Console.WriteLine("Количество тестовых блоков не является целым положительным числом");
                Console.ReadKey();
                return;
            }

            //Поиск всех тестовых блоков и разбитие их на массивы
            string[][] testBlock = new string[numTestBlock][];
            for (int i = 0; i < numTestBlock; i++)
            {
                testBlock[i] = processing.GetNextTestBlock(content);
                if (testBlock[i] == null)
                {
                    Console.WriteLine("Ошибка входных данных");
                    Console.ReadKey();
                    return;
                }
            }

            //Вывод победителей в консоль
            for (int i = 0; i < testBlock.Length; i++)
            {
                string[] winner = processing.GetWinners(testBlock[i]);
                
                if (winner == null)
                    Console.WriteLine("Входные данные тестового блока номер {0} ошибочны", i+1); //+1 для понимания на каком блоке посыпался 
                else
                {
                    for (int j = 0; j < winner.Length; j++)
                        Console.WriteLine(winner[j]); //имя победителя
                }
                Console.WriteLine("\r\n");
            }
            Console.ReadKey();
        }
    }
}
