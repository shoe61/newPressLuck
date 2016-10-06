using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Press_Your_Luck
{
    /*
        class Player
     * 
     *  This class represent a contestent on "Press Your Luck." The class
     *  will keep track of the players earned and passed spins, money and
     *  whammies. 
     *  
     *  data members:
     *      private int numEarnedSpins;
     *      private int numPassedSpins;
     *      private int money;
     *      private int whammies;
     *      private bool active;
     *      
     *  methods
     *      public Player();
     *      public Player(int earned, int passed, int mon, int wham);
     *      public void addMoney(int moneyEarned);
     *      public void addWhammy();
     */
    class Player
    {
        //The number of earned and passed spins a player has.
        private int numEarnedSpins;
        private int numPassedSpins;
        //The players money.
        private int money;
        //The number of whammies a player has landed on.
        private int whammies;
        //Whether the player is still active or not.
        private bool active;
        private String name;

        #region Properties

        public int NumEarnedSpins
        {
            get
            {
                return numEarnedSpins;
            }

            set
            {
                if (value >= 0)
                {
                    numEarnedSpins = value;
                }
                else
                {
                    numEarnedSpins = 0;
                }
            }
        }

        public int NumPassedSpins
        {
            get
            {
                return numPassedSpins;
            }
            set
            {
                if (value >= 0)
                {
                    numPassedSpins = value;
                }
                else
                {
                    numPassedSpins = 0;
                }
            }
        }

        public int Money
        {
            get
            {
                return money;
            }
            set 
            {
                if (value >= 0)
                {
                    money = value;
                }
                else
                {
                    money = 0;
                }
            }
        }

        public int Whammies
        {
            get
            {
                return whammies;
            }
            set
            {
                if (value >= 0 && value <= 4)
                {
                    whammies = value;
                }
                else
                {
                    whammies = 0;
                }
            }
        }

        public bool Active
        {
            get
            {
                return active;
            }
            set
            {
                active = value;
            }
        }

        public String Name
        {
            get 
            {
                return name;
            }
        }

        #endregion

        /*
            public Player()
         * 
         *  The default constructor creates an active player with no spins,
         *  money or whammies. 
         */
        public Player()
        {
            numEarnedSpins = 0;
            numPassedSpins = 0;
            money = 0;
            whammies = 0;
            active = true;
            name = "John Stamos";
        }

        /*
            public Player(int earned, int passed, int mon, int wham)   
         
         *  The parameterized constructor creates an active player with 
         *  custom attributes. 
        */
        public Player(int earned, int passed, int mon, int wham, String name)
        {
            numEarnedSpins = earned;
            numPassedSpins = passed;
            money = mon;
            whammies = wham;
            active = true;
            this.name = name;
        }

        /*
            public void addMoney(int moneyEarned)
         * 
         *  @param: int moneyEarned: the amount of money the player earned.
         *  
         *  This method will add money to a player. This value will never be
         *  negative. 
         */
        public void addMoney(int moneyEarned)
        {
            if (moneyEarned > 0)
            {
                money += moneyEarned;
            }
        }

        /*
            public void addWhammy()
         * 
         *  Adds a whammy to the player. If the player has 4 whammies, they are
         *  set to inactive. 
        */
        public void addWhammy()
        {
            //Adds a whammy.
            whammies++;
            //Sets the money to $0.00.
            money = 0;

            //Once the player has 4 whammies, they are set to inactive. 
            if (whammies == 4)
            {
                active = false;
            }
        }
    }
}
