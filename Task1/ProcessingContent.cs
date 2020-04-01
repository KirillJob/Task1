using System;
using System.Collections.Generic;

namespace Task1
{
    static class ProcessingContent
    {
        /// <summary>
        /// Преобразование строки в целое положительное число. Строка должна содержать только цифры.
        /// </summary>
        /// <param name="s">Строка для обработки</param>
        /// <returns></returns>
        public static int ConvertStrToInt(string s)
        {
            string number = string.Empty;
            int outNum;

            CheckNullOrEmpty(s);
            CheckIsRowNum(s);

            foreach (char ch in s.ToCharArray())
                number += ch.ToString(); 
            
            try
            {
               outNum = Convert.ToInt32(number);
            }
            catch (Exception e)
            {
                throw e;
            }
            
            CheckZero(outNum);
            return outNum;
        }

        public static int[] ConvertStrToIntArr(string s)
        {
            string[] strArr = s.Split(new string[] { " " }, StringSplitOptions.None);
            int[] intArr = new int[strArr.Length];
            for (int i = 0; i < strArr.Length; i++)
            {
                intArr[i] = ConvertStrToInt(strArr[i]);
            }
            return intArr;
        }

        public static void CheckNullOrEmpty(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                throw new Exception("Пустая строка. Ожидалось целое положительное число.");
            }
        }

        private static void CheckZero(int i)
        {
            if (i == 0)
            {
                throw new Exception("Число равно нулю. Ожидалось целое положительное число.");
            }
        }

        private static void CheckIsRowNum(string s)
        {
            foreach (char ch in s.ToCharArray())
            {
                if (!char.IsDigit(ch))
                {
                    Exception e = new Exception("В строке найден не цифровой символ. Ожидалось целое положительное число.");
                    throw e;
                }
            }
        }

        public static TestBlock GetBlockFromContent(string[] content, ref int enterPoint)
        {
            string[] testBlockAsRow = SearchBlockInContent(content, ref enterPoint);
            TestBlock testBlock = new TestBlock(testBlockAsRow);
            return testBlock; 
        }

        private static string[] SearchBlockInContent(string[] content, ref int enterP)
        {
            int startP = SearchPoint(content, enterP, Point.Start);
            int finishP = SearchPoint(content, enterP, Point.Finish);

            int arrSize = finishP - startP + 1; // +1 для захвата всех строчкек тестового блока
            string[] arr = new string[arrSize];
            Array.Copy(content, startP, arr, 0, arrSize);

            enterP = finishP + 1; //установка указателя на пустую строку между тестовыми блоками
            return arr;
        }

        enum Point
        {
            Start,
            Finish
        }

        private static int SearchPoint(string[] content, int pointer, Point p)
        {
            int point = 0;
            int i = pointer;

            switch (p)
            {
                case Point.Start:
                    while (i < content.Length & i + 1 != content.Length)
                    {
                        if (IsRowNum(content[i]) & IsRowLetter(content[i + 1]))
                        {
                            point = i;
                            break;
                        }
                        i++;
                    }
                    break;
                case Point.Finish:
                    while (i < content.Length & i + 1 != content.Length)
                    {
                        if (IsRowNum(content[i]) & string.IsNullOrEmpty(content[i + 1])) break;
                        
                        i++;
                        point = i;
                    }
                    break;
            }
            return point;
        }

        private static bool IsRowNum(string s)
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

        private static bool IsRowLetter(string s)
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
    }
}
