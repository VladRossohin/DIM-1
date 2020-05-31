using System;

namespace DIMS.BL.BusinessModels
{
    public class WordsAmount
    {
        private readonly char[] delimiters = new char[] { ' ', '\r', '\n', ',', '.', ':' };
        private readonly string _val = String.Empty;

        public WordsAmount(string val)
        {
            _val = val;
        }

        public string Value => _val;

        public int GetWordsAmount => _val.Split(delimiters, StringSplitOptions.RemoveEmptyEntries).Length;
    }
}
