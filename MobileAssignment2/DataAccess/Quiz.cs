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
    }
}