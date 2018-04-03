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
    //Enum for difficulty ratings 
    public enum QuizCategory { Geography, History, General_Knowledge, All}

    [Table("Quiz")]
    class Quiz
    {
        [PrimaryKey, AutoIncrement]
        public int QuizID { get; set; }
        public string Question { get; set; }
        public string GoogleSearchItem { get; set; }
        public string RightAnswer { get; set; }
        public string WrongAnswer1 { get; set; }
        public string WrongAnswer2 { get; set; }
        public QuizCategory Category { get; set; }
        public int imageQuizID { get; set; }

        public Quiz() { }
        public Quiz(string quest, string googleSearchItem, string rightanswer, string wronganswer1, string wronganswer2, QuizCategory category)
        {
            Question = quest;
            GoogleSearchItem = googleSearchItem;
            RightAnswer = rightanswer;
            WrongAnswer1 = wronganswer1;
            WrongAnswer2 = wronganswer2;
            Category =category;
            switch(category)
            {//Custom constructor which assigns the image to the object based off of the enum Category
                case QuizCategory.General_Knowledge:
                    imageQuizID = Resource.Drawable.Generalknowledge;
                    break;
                case QuizCategory.Geography:
                    imageQuizID = Resource.Drawable.Geography;
                    break;
                case QuizCategory.History:
                    imageQuizID = Resource.Drawable.History;
                    break;
            }
        }
        public bool IsAnswerCorrect(string chosenAnswer, Quiz question)
        {//Direct string comparison to see if the correct answer has been chosen
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
        {//Function to return n quiz questions randomly from an inputted list in a random order
            int listsize = inputList.Count; 
            List<int> indicesToGet = new List<int>();
            List<int> integerList = new List<int>();
            //Generate list of integers as large as the number of required requestions
            for (int i = 0; i < listsize; i++)
            {
                integerList.Add(i);
            }
            Random rnd = new Random();
            for (int i = 0; i <= amountToTake; i++)
            {//Randomly draw from that list of integers a number which is equal to the number required
                int randomindex = rnd.Next(0, integerList.Count);
                indicesToGet.Add(integerList[randomindex]);
                integerList.Remove(indicesToGet[i]);
            }
            List<Quiz> OutputList = new List<Quiz>();
            foreach (int i in indicesToGet)
            {//Randomly pull questions by index which match the random integers inputted
                OutputList.Add(inputList[i]);
            }
            return OutputList;
        }
        public List<Quiz> GetQuizCategory(List<Quiz> UnsortedList, QuizCategory category)
        {//Function to return list of quiz questions based off of category input by user
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