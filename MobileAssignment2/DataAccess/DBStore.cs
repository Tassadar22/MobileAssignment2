using System;
using System.Collections.Generic;
using System.IO;
using SQLite;


namespace MobileAssignment2.DataAccess
{
    class DBStore
    {
        public static string DBLocation
        {
            get;
        }
        static DBStore()
        {
            if(string.IsNullOrEmpty(DBLocation))
            {
                DBLocation = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal),
                                          "QuizQuestions.db3");
                AddQuizQuestions();
            }
        }

        private static void AddQuizQuestions()
        {
            try
            {
                using (SQLiteConnection cxn = new SQLiteConnection(DBLocation))
                {
                    cxn.DropTable<Quiz>();
                    cxn.CreateTable<Quiz>();
                    TableQuery<Quiz> quizAddingQuery = cxn.Table<Quiz>();
                    if(quizAddingQuery.Count()==0)
                    {

                        //GET QUESTIONS FROM HERE https://www.quiz-questions.net/geography.php
                        Quiz quiz1 = new Quiz("What is the capital of Turkey?", "Ankara", "Istanbul", QuizCategory.Geography);
                        Quiz quiz2 = new Quiz("On which Italian island is Palermo?", "Sicily", "Elba", QuizCategory.Geography);
                        Quiz quiz3 = new Quiz("", "", "", QuizCategory.Geography);
                        Quiz quiz4 = new Quiz("", "", "", QuizCategory.Geography);
                        Quiz quiz5 = new Quiz("", "", "", QuizCategory.Geography);
                        Quiz quiz6 = new Quiz("", "", "", QuizCategory.Geography);
                        Quiz quiz7 = new Quiz("", "", "", QuizCategory.Geography);
                        Quiz quiz8 = new Quiz("", "", "", QuizCategory.Geography);
                        Quiz quiz9 = new Quiz("", "", "", QuizCategory.Geography);
                        Quiz quiz10 = new Quiz("", "", "", QuizCategory.Geography);
                    }
                }
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}