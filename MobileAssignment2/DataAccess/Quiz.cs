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
    public enum QuizCategory { Geography, History, General_Knowledge}

    [Table("GeographyQuiz")]
    class Quiz
    {
        [PrimaryKey, AutoIncrement]
        public int QuizID { get; set; }
        public string Question { get; set; }
        public string RightAnswer { get; set; }
        public string WrongAnswer { get; set; }
        public QuizCategory Category { get; set; }

        public Quiz() { }
        public Quiz(string quest, string rightanswer, string wronganswer, QuizCategory category)
        {
            Question = quest;
            RightAnswer = rightanswer;
            WrongAnswer = wronganswer;
            Category =category;
        }
    }
}