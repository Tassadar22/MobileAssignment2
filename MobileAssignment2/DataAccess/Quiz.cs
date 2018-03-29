using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;
namespace MobileAssignment2.DataAccess
{
    //Enum for difficulty
    public enum QuizCategory { Geography, History, General_Knowledge, All}

    [Table("Quiz")]
    class Quiz
    {
        [PrimaryKey, AutoIncrement]
        public int QuizID { get; set; }
        public string Question { get; set; }
        public string RightAnswer { get; set; }
        public string WrongAnswer1 { get; set; }
        public string WrongAnswer2 { get; set; }
        public QuizCategory Category { get; set; }

        public Quiz() { }
        public Quiz(string quest, string rightanswer, string wronganswer1, string wronganswer2, QuizCategory category)
        {
            Question = quest;
            RightAnswer = rightanswer;
            WrongAnswer1 = wronganswer1;
            WrongAnswer2 = wronganswer2;
            Category =category;
        }
        public bool IsAnswerCorrect(string chosenAnswer, Quiz question)
        {
            if(chosenAnswer==question.RightAnswer)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public List<Quiz> PullXRandomQuestions(int amountToTake, List<Quiz> inputList)
        {//Function to return n quiz questions randomly from an inputted list
            int listsize = inputList.Count;
            List<int> indicesToGet = new List<int>();
            List<int> integerList = new List<int>();
            //Generate list of integers as large as the size of the list
            for (int i = 0; i < listsize; i++)
            {
                integerList.Add(i);
            }
            Random rnd = new Random();
            for (int i = 0; i <= amountToTake; i++)
            {
                int randomindex = rnd.Next(0, integerList.Count);
                indicesToGet.Add(integerList[randomindex]);
                integerList.Remove(indicesToGet[i]);
            }
            List<Quiz> OutputList = new List<Quiz>();
            foreach (int i in indicesToGet)
            {
                OutputList.Add(inputList[i]);
            }
            return OutputList;
        }
        public List<Quiz> GetQuizCategory(List<Quiz> UnsortedList, QuizCategory category)
        {
            List<Quiz> Categorical = new List<Quiz>();
            if (category == QuizCategory.All)
            {
                Categorical = UnsortedList;
            }
            else
            {
                foreach (Quiz quiz in UnsortedList)
                {
                    if (quiz.Category == category)
                    {
                        Categorical.Add(quiz);
                    }
                }
            }
            return Categorical;
        }
    }
}