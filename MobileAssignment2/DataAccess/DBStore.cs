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
                        Quiz quiz3 = new Quiz("What island, which belonged to Denmark, was independent in 1944?", "Iceland", "Greenland", QuizCategory.Geography);
                        Quiz quiz4 = new Quiz("What is the longest river in Europe?", "Wolga", "Danube", QuizCategory.Geography);
                        Quiz quiz5 = new Quiz("What is the second largest country in Europe after Russia?", "France", "Germany", QuizCategory.Geography);
                        Quiz quiz6 = new Quiz("What is the largest city in Canada?", "Toronto", "Vancouver",QuizCategory.Geography);
                        Quiz quiz7 = new Quiz("On which continent can you visit Sierra Leone?", "Africa", "South America", QuizCategory.Geography);
                        Quiz quiz8 = new Quiz("What is the largest state of the United States?", "Alaska", "Texas", QuizCategory.Geography);
                        Quiz quiz9 = new Quiz("In which country is Krakow located?", "Poland", "Czech Republic", QuizCategory.Geography);
                        Quiz quiz10 = new Quiz("Which country did once have the name Rhodesia?", "Zimbabwe", "South Africa", QuizCategory.Geography);
                        Quiz quiz11 = new Quiz("What is the Capital of the US State of Pennsylvania", "Harrisburg", "Pittsburgh", QuizCategory.Geography);
                        cxn.Insert(quiz1);
                        cxn.Insert(quiz2);
                        cxn.Insert(quiz3);
                        cxn.Insert(quiz4);
                        cxn.Insert(quiz5);
                        cxn.Insert(quiz6);
                        cxn.Insert(quiz8);
                        cxn.Insert(quiz9);
                        cxn.Insert(quiz10);
                        cxn.Insert(quiz11);
                        //Format: public Quiz(string quest, string rightanswer, string wronganswer, QuizCategory category)
                        cxn.Insert(new Quiz("What was the name of Napoleon s first wife?", "Josephine", "Anne",QuizCategory.History));
                        cxn.Insert(new Quiz("On which island was Napoleon born?", "Corsica", "Malta", QuizCategory.History));
                        cxn.Insert(new Quiz("Which country was formerly called Ceylon?", "Sri Lanka", "Bangladesh", QuizCategory.History));
                        cxn.Insert(new Quiz("Which country sent its navy around the world to fight the Japanese in 1904?", "Russia", "China", QuizCategory.History));
                        cxn.Insert(new Quiz("Who was the first king of Belgium ?", "Leopold I", "Albert I", QuizCategory.History));
                        cxn.Insert(new Quiz("What was the name of the Protestant revolution against the domination of the Pope?", "Reformation", "Schism", QuizCategory.History));
                        cxn.Insert(new Quiz("Which German war criminal was for 21 years the only inmate of Spandau complex?", "Rudolf Hess", "Adolf Eichmann", QuizCategory.History));
                        cxn.Insert(new Quiz("Which city was the capital of Australia from 1901 to 1927?", "Melbourne", "Canberra", QuizCategory.History));
                        cxn.Insert(new Quiz("Which war was ended by the signing the armistice on July 27 th, 1953?", "The Korean War", "The Vietnam war", QuizCategory.History));
                        cxn.Insert(new Quiz("Xerxes ruled a great empire around the fifth century BC. Which empire?", "The Persian empire", "The Greek Empire", QuizCategory.History));
                        //General Knowledge questions
                        cxn.Insert(new Quiz("Entomology is the science that studies", "Insects", "Trees", QuizCategory.General_Knowledge));
                        cxn.Insert(new Quiz("Which classic band recorded the following songs: \"I Want To Hold Your Hand\", \"Yesterday\", \"Hey Jude\" and \"Let It Be\"?","The Beatles","The Rolling Stones", QuizCategory.General_Knowledge));
                        cxn.Insert(new Quiz("First human heart transplant operation conducted by Dr.Christiaan Barnard on Louis Washkansky, was conducted in ", "", "", QuizCategory.General_Knowledge));
                        cxn.Insert(new Quiz("", "", "", QuizCategory.General_Knowledge));
                        cxn.Insert(new Quiz("", "", "", QuizCategory.General_Knowledge));
                        cxn.Insert(new Quiz("", "", "", QuizCategory.General_Knowledge));
                        cxn.Insert(new Quiz("", "", "", QuizCategory.General_Knowledge));
                        cxn.Insert(new Quiz("", "", "", QuizCategory.General_Knowledge));
                        cxn.Insert(new Quiz("", "", "", QuizCategory.General_Knowledge));
                        cxn.Insert(new Quiz("", "", "", QuizCategory.General_Knowledge));
                    }
                }
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        public List<Quiz> GetQuizList()
        {
            try
            {
                using (SQLiteConnection cxn = new SQLiteConnection(DBLocation))
                {
                    List<Quiz> quizList = cxn.Query<Quiz>("SELECT * FROM Quiz");
                    return quizList;
                }
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}