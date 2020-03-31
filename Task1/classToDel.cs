using System;
using System.IO;

namespace Task1
{
    class classToDel
    {
        private static int enterPoint = 1;

        /// <summary>
        /// Считывает содержимое текстового файла в одну строку
        /// </summary>
        /// <param name="filename">Имя файла</param>
        /// <returns></returns>
        //internal string GetFileContent(string filename)
        //{
        //    string str = null;
        //    try
        //    {
        //        StreamReader sr = new StreamReader(filename);
        //        str = sr.ReadToEnd();
        //        sr.Close();
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine("Ошибка чтения файла: " + e.Message);
        //    }
        //    return str;
        //}

        /// <summary>
        /// Получаем количество тестовых блоков
        /// </summary>
        /// <param name="s">Строка в которой указано кол-во</param>
        /// <returns></returns>
        //internal int GetNumTestBlock(string s)
        //{
        //    ProcessingContent sup = new ProcessingContent();
        //    int numTestBlock = sup.GetNumber(s);
        //    return numTestBlock;
        //}

        /// <summary>
        /// Получаем массив строк очередного тестового блока
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        internal string[] GetNextTestBlock(string[] content)
        {
            ProcessingContent sup = new ProcessingContent();
            int startP = sup.SearchStartPointTestBlock(content,enterPoint);
            int finishP = sup.SearchFinishPointTestBlock(content,enterPoint);
            if (startP != 0 & finishP != 0)
            {
                enterPoint = finishP + 1; //установка указателя на пустую строку между тестовыми блоками
                int arrSize = finishP - startP + 1; // +1 для захвата всех строчкек тестового блока
                string[] arr = new string[arrSize];
                Array.Copy(content, startP, arr, 0, arrSize);
                return arr;
            }
            return null;
        }

        /// <summary>
        /// Получаем список победителей
        /// </summary>
        /// <param name="testBlock">Тестовый блок</param>
        /// <returns></returns>
        internal string[] GetWinners(string[] testBlock)
        {
            ProcessingContent sup = new ProcessingContent();
            int numCondidate = sup.GetNumber(testBlock[0]);

            //Проверка на кол-во кондидатов (условия задачи)
            if (numCondidate == 0 || numCondidate > 20)
                return null;

            //Массив с именами кондидатов
            string[] nameCondidates = new string[numCondidate];
            Array.Copy(testBlock, 1, nameCondidates, 0, numCondidate);

            //Проверка длинны имени кондидата (условия задачи)
            for (int i = 0; i < nameCondidates.Length; i++)
                if (nameCondidates[i].Length >= 80)
                    return null;

            //Помещаем голосование в отдельный массив
            int startRow = numCondidate + 1;
            int arrSize = testBlock.Length - numCondidate - 1;
            string[] voteRows = new string[arrSize];
            Array.Copy(testBlock, startRow, voteRows, 0, arrSize);

            //Проверка максимального количества избирателей (условия задачи)
            if (voteRows.Length >= 999)
                return null;

            //Создаем целочисленный зубчатый массив, в который помещаем данные о голосовании
            int[][] vote = new int[voteRows.Length] [];
            for (int i = 0; i < voteRows.Length; i++)
            {
                vote[i] = sup.ConvertStrToInt(voteRows[i], numCondidate);
                if (vote[i] == null)
                    return null;
            }

            //Получаем массив победных номеров
            int[] numWinners = sup.AnalysisVoting(vote, numCondidate);
            string[] nameWinners = new string[numWinners.Length];
            for (int i = 0; i < numWinners.Length; i++)
                nameWinners[i] = nameCondidates[numWinners[i] - 1];

            return nameWinners;
        }
    }
}
