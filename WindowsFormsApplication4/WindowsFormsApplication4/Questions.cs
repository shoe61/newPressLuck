using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WindowsFormsApplication4
{
    /*
        class Questions
     * 
     *  This class will read the questions from a file and store them in a 
     *  dictionary. This class will send question and answer information to 
     *  the form and validate answers. 
     *  
     *  data members:
     *      private Dictionary<String, String> questions;
     *      private int index;
     *      
     *  methods:
     *      public Questions();
     *      private void loadQuestions();
     *      public String getNextQuestion();
     *      public bool checkAnswer(String ans);
     *      public String getAnswer()
     */
    class Questions
    {
        //The questions are stored in a dictionary
        private Dictionary<String, String> questions = null;
        //The index will keep track of the current question. 
        private int index;
        private Random rand;

        /*  
            public Questions()
         * 
         *  The default constructor for the Questions class. The index is set
         *  to -1 and the dictionary is loaded with questions. 
         */
        public Questions()
        {
            questions = new Dictionary<string, string>();
            rand = new Random();
            loadQuestions();
            index = rand.Next() % questions.Count;
        }

        /*
            private void loadQuestions()
         * 
         *  loadQuestions() will read the questions and answers from a text
         *  file into a dictionary. This will allow us to quickly lookup the
         *  answer to a question. 
         */
        private void loadQuestions()
        {
            String question = null;
            String answer = null;

            //The StreamReader pulls the data from questions.txt
            using (StreamReader sr = new StreamReader("questions.txt"))
            {
                //Loops until the end of the file is reached. 
                while (sr.Peek() != -1)
                {
                    //The question and answer are read from the file. 
                    question = sr.ReadLine();
                    answer = sr.ReadLine();

                    //Adds the question to the dictionary if it is not already
                    //there. 
                    if (!questions.ContainsKey(question) && question != "")
                    {
                        questions.Add(question, answer);
                    }
                }

                //Closes the StreamReader. 
                sr.Close();
            }
        }

        /*
            public String getNextQuestion()
         * 
         *  This method will return the next question in the dictionary.
         */
        public String getNextQuestion()
        {
            String result = null;

            //The index is a random number between 0 and the number of questions.
            index = rand.Next() % questions.Count;
            result = questions.Keys.ToArray()[index];

            return result;
        }

        /*
            public bool checkAnswer(String ans)
         * 
         *  @param: String ans: the answer that the user typed in. 
         * 
         *  This method receives the answer that the user gave and 
         *  decides whether the answer was correct or not. 
         */
        public bool checkAnswer(String ans)
        {
            bool result = true;
            String quest = questions.Keys.ToArray()[index];
            //The answer is trimmed for spaces and taken to upper case. 
            String answer = questions[quest].Trim().ToUpper();
            //The user answer is trimmed and taked to upper case. 
            ans = ans.Trim().ToUpper();

            //Logical check to see if the answer is correct. 
            result = answer == ans;

            return result;
        }

        /*
            public String getAnswer()
         * 
         *  This method returns the answer to the current question. Based
         *  on the index. 
         */
        public String getAnswer()
        {
            String result = null;
            String quest = questions.Keys.ToArray()[index];

            result = questions[quest];

            return result;
        }
    }
}
