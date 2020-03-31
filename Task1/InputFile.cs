using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Processing = Task1.ProcessingContent;


namespace Task1
{
    class InputFile
    {
        private string fileName;
        private string[] content;
        private List<TestBlock> testBlocks = null;

        public InputFile(string fileName)
        {
            this.fileName = fileName;
            content = GetContent(this.fileName);
            testBlocks = GetTestBlock(content);
        }

        /// <summary>
        /// Запуск обработки поступившего файла
        /// </summary>
        public void Start()
        {
            foreach (TestBlock testBlock in testBlocks)
            {
                StartRegularElections(testBlock);
            }
        }

        private void StartRegularElections(TestBlock testBlock)
        {
            testBlock.StartElections();
        }

        private string[] GetContent(string fileName) //TODO: надо ли передовать параметры или можно напрямую обратиться к полю?
        {
            string s = string.Empty;
            string[] content = null;
            try
            {
                StreamReader sr = new StreamReader(fileName);
                s = sr.ReadToEnd();
                sr.Close();
            }
            catch (Exception e)
            {
                throw e;
            }

            Processing.CheckNullOrEmpty(s);
            content = s.Split(new string[] { "\r\n" }, StringSplitOptions.None);

            return content;
        }

        private List<TestBlock> GetTestBlock(string[] content)
        {
            int numTestBlocks;
            int enterPoint = 1;
            List<TestBlock> testBlocks = null;

            numTestBlocks = Processing.ConvertStrToInt(content[0]);

            for (int i = 0; i < numTestBlocks; i++)
            {
                TestBlock regTestBlock = Processing.GetBlockFromContent(content, ref enterPoint); 
                testBlocks.Add(regTestBlock); 
            }
            return testBlocks;
        }
    }
}
