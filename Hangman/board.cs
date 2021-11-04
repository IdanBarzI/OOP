using System;
using Windows.UI.Xaml.Controls;

namespace Hangman
{
    class Board
    {
        private const char HIDEN_CHAR = '*';
        private string[] listWords;
        public string mysteryWord;
        private Canvas boardCanvas;
        public char[] answer;
        //public Img Picture = new Img();

        public Board(Canvas cnvs, int level)
        {
            listWords = new string[] {"Unknown" };
            if (level == 1)
            {
                listWords = new string[] { "CAR", "CAT", "DOG", "SUN", "ROW", "SPIN", "TREE", "WOOD", "WICK", "HOME", "END", };
            }
            if (level == 2)
            {
                listWords = new string[] { "AGREE", "AGENCY", "CREATE", "DRIVE", "EDGE", "ENERGY", "FATHER", "FOCUS", "GREEN", "WHITE", "HUMAN", };
            }
            if(level == 3)
            {
                listWords = new string[] { "HUNDRED", "INSTEAD", "BUSINESS", "BROTHER", "BEHAVIOR", "SOMEBODY", "CAMPAIGN", "STANDARD", "WHETHER", "VIOLENCE", "TROUBLE", };
            }
            mysteryWord = WordPicker();
            answer = new char[mysteryWord.Length];
            for (int j = 0; j < mysteryWord.Length; j++)
            {
                answer[j] = HIDEN_CHAR;
            }
            boardCanvas = cnvs;
            
        }

        private string WordPicker()
        {
            Random randGen = new Random();
            var idx = randGen.Next(listWords.Length);
            return listWords[idx];
        }

        public void InsertRightCharToAnswer(char playerGuess)
        {
            for (int i = 0; i < mysteryWord.Length; i++)
            {
                if (playerGuess == mysteryWord[i])
                {
                    answer[i] = playerGuess;
                }
            }
    }

        public bool IsTheWordContainChar(char playerGuess)
        {
            bool isCharInWord = false;
            for (int i = 0; i < mysteryWord.Length; i++)
            {
                if (playerGuess == mysteryWord[i])
                {
                    isCharInWord = true;
                }
            }
            return isCharInWord;
        }

        public bool IsLose()
        {
            if (Picture.picsUri1.Count >= 1)
            {
                return false;
            }
            return true;
        }

        public bool IsWin()
        {
            for (int i = 0; i < mysteryWord.Length; i++)
            {
                if (answer[i] == HIDEN_CHAR)
                {
                    return false;
                }
            }
            return true;
        }

        public void AddElement()
        {
            boardCanvas.Children.Add(this.Picture.Element);
        }

        public void RemoveElement()
        {
            boardCanvas.Children.Remove(this.Picture.Element);
        }
    }
}
