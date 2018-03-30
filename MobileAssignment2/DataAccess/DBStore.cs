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
                        Quiz quiz1 = new Quiz("What is the capital of Turkey?","Turkey Capital", "Ankara", "Istanbul","Antalya", QuizCategory.Geography);
                        Quiz quiz2 = new Quiz("On which Italian island is Palermo?", "Palermo", "Sicily", "Elba","Capri", QuizCategory.Geography);
                        Quiz quiz3 = new Quiz("What island(s), which belonged to Denmark, was independent in 1944?", "Iceland", "Iceland", "Greenland", "Faroe Island", QuizCategory.Geography);
                        Quiz quiz4 = new Quiz("What is the longest river in Europe?", "Wolga", "Wolga", "Danube", "Ural",QuizCategory.Geography);
                        Quiz quiz5 = new Quiz("What is the second largest country in Europe after Russia?", "France", "France", "Germany","Ukraine", QuizCategory.Geography);
                        Quiz quiz6 = new Quiz("What is the largest city in Canada?", "Toronto", "Toronto", "Vancouver","Ontario",QuizCategory.Geography);
                        Quiz quiz7 = new Quiz("On which continent can you visit Sierra Leone?", "Sierra Leone", "Africa", "South America","Australia", QuizCategory.Geography);
                        Quiz quiz8 = new Quiz("What is the largest state of the United States?", "Alaska", "Alaska", "Texas","California", QuizCategory.Geography);
                        Quiz quiz9 = new Quiz("In which country is Krakow located?", "Krakow", "Poland", "Czech Republic","Estonia", QuizCategory.Geography);
                        Quiz quiz10 = new Quiz("Which country did once have the name Rhodesia?", "Zimbabwe", "Zimbabwe", "South Africa","Uganda", QuizCategory.Geography);
                        Quiz quiz11 = new Quiz("What is the Capital of the US State of Pennsylvania", "Harrisburg", "Harrisburg", "Pittsburgh","Scranton", QuizCategory.Geography);
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
                        cxn.Insert(new Quiz("What was the name of Napoleons first wife?", "Empress Joséphine", "Josephine", "Anne","Mary",QuizCategory.History));
                        cxn.Insert(new Quiz("On which island was Napoleon born?", "Early History of Napoleon", "Corsica", "Malta","Sicily", QuizCategory.History));
                        cxn.Insert(new Quiz("Which country was formerly called Ceylon?", "Sri Lanka", "Sri Lanka", "Bangladesh", "Bhutan", QuizCategory.History));
                        cxn.Insert(new Quiz("Which country sent its navy around the world to fight the Japanese in 1904?", "Russo–Japanese War", "Russia", "China", "Australia", QuizCategory.History));
                        cxn.Insert(new Quiz("Who was the first king of Belgium ?", "Leopold I", "Leopold I", "Albert I", "Charles", QuizCategory.History));
                        cxn.Insert(new Quiz("What was the name of the Protestant revolution against the domination of the Pope?", "Reformation", "Reformation", "Schism", "Split", QuizCategory.History));
                        cxn.Insert(new Quiz("Which German war criminal was for 21 years the only inmate of Spandau complex?", "Rudolf Hess", "Rudolf Hess", "Adolf Eichmann", "Wernher von Braun", QuizCategory.History));
                        cxn.Insert(new Quiz("Which city was the capital of Australia from 1901 to 1927?", "Melbourne Early History", "Melbourne", "Canberra","Perth", QuizCategory.History));
                        cxn.Insert(new Quiz("Which war was ended by the signing the armistice on July 27 th, 1953?", "The Korean War Armistice", "The Korean War", "The Vietnam war", "First Indochina War", QuizCategory.History));
                        cxn.Insert(new Quiz("Xerxes ruled a great empire around the fifth century BC. Which empire?", "Xerxes Persia", "The Persian empire", "The Greek Empire","The Roman Empire", QuizCategory.History));
                        //General Knowledge questions
                        cxn.Insert(new Quiz("Entomology is the science that studies", "Entomology", "Insects", "Trees", "Plants", QuizCategory.General_Knowledge));
                        cxn.Insert(new Quiz("Which classic band recorded the following songs: \"I Want To Hold Your Hand\", \"Yesterday\", \"Hey Jude\" and \"Let It Be\"?", "The Beatles", "The Beatles","The Rolling Stones","The Kinks", QuizCategory.General_Knowledge));
                        cxn.Insert(new Quiz("First human heart transplant operation conducted by Dr.Christiaan Barnard on Louis Washkansky, was conducted in", "Louis Washkansky Heart Transplant", "1967", "1968","1922", QuizCategory.General_Knowledge));
                        cxn.Insert(new Quiz("Golf player Vijay Singh belongs to which country?", "Vijay Singh", "Fiji", "USA", "India", QuizCategory.General_Knowledge));
                        cxn.Insert(new Quiz("First China War was fought between", "First opium war", "China and Britain", "China and France", "China and Egypt", QuizCategory.General_Knowledge));
                        cxn.Insert(new Quiz("Federation Cup, World Cup, Allywyn International Trophy and Challenge Cup are awarded to winners of", "Volleyball", "Volleyball", "Tennis", "Basketball", QuizCategory.General_Knowledge));
                        cxn.Insert(new Quiz("During World War II, when did Germany attack France?", "World War II", "1940", "1941","1942", QuizCategory.General_Knowledge));
                        cxn.Insert(new Quiz("Frederick Sanger is a twice recipient of the Nobel Prize for", "Frederick Sanger", "Chemistry in 1958 and 1980", "Physics in 1956 and 1972", "Physics in 1903 and Chemistry in 1911", QuizCategory.General_Knowledge));
                        cxn.Insert(new Quiz("In which year did Father Ted originally Air?", "Father Ted", "1995", "1994","1997", QuizCategory.General_Knowledge));
                        cxn.Insert(new Quiz("Who was the first president of Ireland", "Douglas Hyde", "Douglas Hyde", "Eamon de Valera","Michael D Higgins", QuizCategory.General_Knowledge));
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