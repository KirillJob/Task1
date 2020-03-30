using System;
using System.Collections.Generic;

namespace Task1
{
    class ProcessingContent
    {
        //Указатель для парсинг контента. По умолчанию 1 (1ая пустая строка файла контента)
        int pointer = 1;
        string[] splitContent = null;

        public ProcessingContent(string[] splitContent)
        {
            this.splitContent = splitContent;
        }

        /// <summary>
        /// Преобразование строки в целое положительное число. Строка должна содержать только цифры.
        /// </summary>
        /// <param name="s">Строка для обработки</param>
        /// <returns></returns>
        internal int GetNumberInUpperRow()
        {
            string number = string.Empty;
            int outNum;

            if (string.IsNullOrEmpty(splitContent[0]))
            {
                Exception e = new Exception("Пустая строка. Ожидалось целое положительное число.");
                throw e;
            }

            foreach (char ch in splitContent[0].ToCharArray())
            {
                //проверка, что все символы в строке цифровые
                if (char.IsDigit(ch))
                    number += ch.ToString();
                else
                {
                    Exception e = new Exception("В строке найден не цифровой символ");
                    throw e;
                    //number = string.Empty;
                    //break;
                }
            }  
            
            try
            {
               outNum = Convert.ToInt32(number);
            }
            catch (Exception e)
            {
                throw e;
            }
            //if (!string.IsNullOrEmpty(number))
            //    return Convert.ToInt32(number);
            //else
            //    return 0;

            if (outNum == 0)
            {
                Exception e = new Exception("Число равно нулю. Ожидалось целое положительное число.");
                throw e;
            }
            else
                return outNum;
        }

        internal string[] SearchNextTestBlock()
        {
            ProcessingContent sup = new ProcessingContent();
            int startP = sup.SearchStartPointTestBlock(content, enterPoint);
            int finishP = sup.SearchFinishPointTestBlock(content, enterPoint);
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













        private bool IsRowLetter(string s)
        {
            foreach (char ch in s.ToCharArray())
            {
                if (char.IsLetter(ch))
                    return true;
                else
                    return false;
            }
            return false;
        }

        private bool IsRowNum(string s)
        {
            foreach (char ch in s.ToCharArray())
            {
                if (char.IsDigit(ch))
                    return true;
                else
                    return false;
            }
            return false;
        }

        private List<int> Сalculation(int[] condidates, int numElect)
        {
            List<int> max = new List<int>();
            max.AddRange(new int[condidates.Length]);
            int indexMax = 0;
            int valueMax = 0;

            //Находим максимальное значение и его индекс
            for (int i = 0; i < condidates.Length; i++)
            {
                if(valueMax < condidates[i])
                {
                    indexMax = i;
                    valueMax = condidates[i];
                }
            }

            //Проверка есть ли кондидат с +50%
            if (valueMax > numElect/2)
            {
                max[indexMax] = valueMax;
                return max;
            }

            //Проверяем есть ли еще кондидать с макс. значением, но <=50% и добавляем их в список
            for (int i = 0; i < condidates.Length; i++)
            {
                if (valueMax == condidates[i])
                    max[i] = valueMax;
            }
            return max;
        }

        private int[][] ArrayModify(int[][] inputArray, int iteration, List<int> calculation)
        {

            //Узнаем сколько избирателей продолжают голосовать
            int numElect = 0;
            for (int i = 0; i < inputArray.Length; i++)
            {
                //Перебираем список прошедших кондидатов
                for (int j = 0; j < calculation.Count; j++)
                {
                    //индекс 0 ничему не соответсвует - пропускаем, находим аутсайдера и если в тек. строчке массива голосовали за аутсайдера, забираем
                    if (j != 0 & calculation[j] == 0 & j == inputArray[i][iteration])
                        numElect++;
                }
            }

            int[][] outArray = new int[numElect][];
            int counOutArray = 0;

            for (int i = 0; i < inputArray.Length; i++)
            {
                //Перебираем список прошедших кондидатов
                for (int j = 0; j < calculation.Count; j++)
                {
                    //индекс 0 ничему не соответсвует - пропускаем, находим аутсайдера и если в тек. строчке массива голосовали за аутсайдера, забираем
                    if (j != 0 & calculation[j] == 0 & j == inputArray[i][iteration])
                    {
                        outArray[counOutArray] = inputArray[i];
                        counOutArray++;
                    }
                }
            }
            return outArray;
        }

        internal int SearchStartPointTestBlock(string[] content, int enterP)
        {
            int point = 0;
            for (int i = enterP; i < content.Length; i++)
            {
                if (i + 1 != content.Length) //Проверка на последнюю строку
                {
                    if (GetNumber(content[i]) != 0 & IsRowLetter(content[i + 1]))
                    {
                        point = i;
                        break;
                    }
                }
            }
            return point;
        }

        internal int SearchFinishPointTestBlock(string[] content, int enterP)
        {
            int point = 0;
            for (int i = enterP; i < content.Length; i++)
            {
                if (i+1 != content.Length) //Проверка на последнюю строку
                {
                    if (IsRowNum(content[i]) & string.IsNullOrEmpty(content[i + 1]))
                    {
                        point = i;
                        break;
                    }
                }
                else
                    point = i;
            }
            return point;
        }

        internal int[] ConvertStrToInt(string voteRows, int numCondidate)
        {
            string[] str = voteRows.Split(new char[] { ' ' });
            int[] ar = new int[str.Length];

            for (int i = 0; i < str.Length; i++)
            {
                if (IsRowNum(str[i]))                   //проверка входных данных (строчка содержит только цифры)
                    ar[i] = Convert.ToInt32(str[i]);
                else
                    return null;
                if (ar[i] > numCondidate)               //проверка входных данных (избиратель не может выбрать не существующего кондидата)
                    return null;
            }

            if (ar.Length > numCondidate)               //проверка входных данных (избиратель не может проголосовать дважды за кондидата)
                return null;            

            return ar;
        }

        internal int[] AnalysisVoting(int[][] vote, int numCondidate)
        {
            int numElect = vote.Length;
            int[] condidates = new int[numCondidate+1];
            int[][] intermediate = vote;

            for (int i = 0; i < vote[i].Length; i++)
            {
                // подсчет j-ого тура выборов
                for (int j = 0; j < intermediate.Length; j++)
                    condidates[intermediate[j][i]] = condidates[intermediate[j][i]] + 1;

                List<int> calculation = new List<int>();
                calculation = Сalculation(condidates, numElect);
                int indFiftyPercent = 0;

                // Проверяем кондидатов на победу
                for (int z = 0; z < calculation.Count; z++)
                {
                    //проверяем есть ли кондидат с +50%
                    if (calculation[z] > numElect/2)
                        return new int[1] { z };

                    //проверяем есть ли в списке два кондидата с 50% (вариант возможен только с четным кол-во избирателей)
                    if (numElect % 2 == 0 & calculation[z] == numElect / 2)
                    {
                        if (indFiftyPercent != 0)
                            return new int[2] { indFiftyPercent, z };

                        indFiftyPercent = z;
                    }
                }

                //Так как в текущем туре победитель не определен, переходим на следующий тур
                int[][] buf = ArrayModify(intermediate, i, calculation);
                intermediate = null;
                intermediate = buf;
                buf = null;
            }
            return new int[1] { 0 };
        }
    }
}
