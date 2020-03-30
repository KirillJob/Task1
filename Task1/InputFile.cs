using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace Task1
{
    class InputFile
    {
        string _fileName = string.Empty;
        string _content  = string.Empty;

        public string FileName { get; }
        public string Content  { get; }

        public InputFile(string fileName)
        {
            _fileName = fileName;
            _content  = GetContent(FileName);
        }

        /// <summary>
        /// Запуск обработки поступившего файла
        /// </summary>
        public void Start()
        {
            //TODO: Разбить контент на строковые блоки
            //TODO: Создать из строковых блоков лист элементом которого является TestBlock
            //TODO: В каждом тестовом блоке запустить определение победителя/ей 
        }

        string GetContent(string fn)
        {
            string s = string.Empty;
            // Получаем контент
            try
            {
                StreamReader sr = new StreamReader(fn);
                s = sr.ReadToEnd();
                sr.Close();
            }
            catch (Exception e)
            {
                throw e;
            }

            //Проверяем на пустоту
            if (string.IsNullOrEmpty(s))
            {
                Exception e = new Exception("Файл пуст");
                throw e;
            }
            return s;
        }

        List<TestBlock> GetTestBlock(string content)
        {
            List<TestBlock>   testBlocks = null;
            ProcessingContent processing;
            
            string[] splitContent = content.Split(new string[] { "\r\n" }, StringSplitOptions.None);
            processing = new ProcessingContent(splitContent);

            int numTestBlock = processing.GetNumberInUpperRow();

            for (int i = 0; i < numTestBlock; i++)
            {
                //TODO: Найти очередной кусок тестовый блок в предоставленном контенте
                string[] regTestBlockInContent = processing.SearchNextTestBlock();
                TestBlock regTestBlock = TestBlock.Creator(regTestBlockInContent);
                testBlocks.Add(regTestBlock); //TODO: Создать метод который возвращает экземпляр своего класса
            }
            return testBlocks;

            //TODO: перебор контента и создание сущностей TestBlock

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
        }
    }
}
