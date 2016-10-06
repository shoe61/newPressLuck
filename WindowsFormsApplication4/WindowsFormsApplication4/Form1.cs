using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication4
{
    public partial class gameForm : Form
    {
        private Player player1 = null;
        private Player player2 = null;
        private Player player3 = null;
        private Player activePlayer = null;
        private Questions quest = null;
        private int questionCount = 0;
        // True - Question Round; False - Spin Round;
        ///private bool roundType = true;
        private int cashMoney;
        private int newSpins;
        private bool ohNoWammy;
        private bool bigMoney;

        public gameForm()
        {
            InitializeComponent();
            instrucGroupBx.BringToFront();
        }
        
        private void spinButton_Click(object sender, EventArgs e)
        {
            //toggles spin timer
            spinTimer.Enabled = !spinTimer.Enabled;
            
            //changes appearance of playing area for spin round
            centerBox.Visible = true;
            spinButton.BringToFront();
            passButton.BringToFront();
            
            if (!spinTimer.Enabled)
            {
                if (activePlayer.NumPassedSpins > 0)
                {
                    activePlayer.NumPassedSpins--;
                }
                else if (activePlayer.NumEarnedSpins > 0)
                {
                    activePlayer.NumEarnedSpins--;
                }
                else
                {
                    promptQuestion();
                }

                if (cashMoney != 0 && newSpins == 0)
                {
                    MessageBox.Show(String.Format("Congrats {0}, You earned ${1}!", activePlayer.Name, cashMoney));
                    activePlayer.addMoney(cashMoney);
                }
                else if (cashMoney != 0 && newSpins != 0)
                {
                    //TODO: account for single vs. Multiple Spins.
                    MessageBox.Show(String.Format("Congrats {0}, You earned ${1} and {2} spins!", activePlayer.Name, cashMoney, newSpins));
                    activePlayer.addMoney(cashMoney);
                    activePlayer.NumEarnedSpins += newSpins;
                }
                else if (ohNoWammy)
                {
                    MessageBox.Show(String.Format("Oh No, {0}! You got a Whammy!", activePlayer.Name));
                    activePlayer.addWhammy();
                    if (!activePlayer.Active)
                    {
                        if (activePlayer == player1)
                        {
                            player1Label.BackColor = Color.Red;
                            if (!player2.Active || !player3.Active)
                            {
                                endGame();
                            }
                            else
                            {
                                if (player2.Active)
                                {
                                    activePlayer = player2;
                                }
                                else
                                {
                                    activePlayer = player3;
                                }
                            }
                        }
                        if (activePlayer == player2)
                        {
                            player2Label.BackColor = Color.Red;
                            if (!player1.Active || !player3.Active)
                            {
                                endGame();
                            }
                            else
                            {
                                if (player1.Active)
                                {
                                    activePlayer = player1;
                                }
                                else
                                {
                                    activePlayer = player3;
                                }
                            }
                        }
                        if (activePlayer == player3)
                        {
                            player3Label.BackColor = Color.Red;
                            if (!player2.Active || !player1.Active)
                            {
                                endGame();
                            }
                            else
                            {
                                if (player2.Active)
                                {
                                    activePlayer = player2;
                                }
                                else
                                {
                                    activePlayer = player1;
                                }
                            }
                        }
                    }
                }
                else if (bigMoney)
                {
                    MessageBox.Show(String.Format("Congrats {0}, You earned $10,000 by langing on Big Bucks!", activePlayer.Name));
                    activePlayer.addMoney(cashMoney);
                }

                beginSpinRound();
            }

            updateScoreboard();
        }

        //as long as enabled, spinTimer_Tick calls spinner every 300 milliseconds
        private void spinTimer_Tick(object sender, EventArgs e)
        {
            spinner();
        }

        private void spinner()
        {
            //list of dollar values indexed to images in tiles array
            #region dollar values;
            List<int> dollars = new List<int>();
            dollars.Add(10000);     // 0- big bucks
            dollars.Add(0);     // 1- wammy
            dollars.Add(470);
            dollars.Add(500);
            dollars.Add(500);   // 4- plus one spin
            dollars.Add(0);     // 5- wamm
            dollars.Add(525);
            dollars.Add(530);
            dollars.Add(0);     // 8- wammy
            dollars.Add(650);
            dollars.Add(0);     // 10- wammy
            dollars.Add(740);
            dollars.Add(750);
            dollars.Add(750);   // 13- plus one spin
            dollars.Add(800);
            dollars.Add(0);     // 15- wammy
            dollars.Add(900);
            dollars.Add(1000);
            dollars.Add(1000);  // 18- plus one spin
            dollars.Add(1100);
            dollars.Add(1200);
            dollars.Add(1250);
            dollars.Add(1300);
            dollars.Add(1400);  // 23
            dollars.Add(1500);
            dollars.Add(1500);  // 25- plus one spin
            dollars.Add(1600);
            dollars.Add(1750);
            dollars.Add(1750);  // 28- plus one spin
            dollars.Add(1900);
            dollars.Add(2000);
            dollars.Add(2000);  // 31- plus one spin
            dollars.Add(2250);
            dollars.Add(2500);
            dollars.Add(2500);  // 34- plus one spin
            dollars.Add(2750);
            dollars.Add(3000);
            dollars.Add(3000);  // 37- plus one spin
            dollars.Add(3500);
            dollars.Add(4000);
            dollars.Add(4000);  // 40- plus one spin
            dollars.Add(0);     // 41- wammy
            #endregion

            //list of spins indexed to tiles array
            #region spins;
            List<int> spins = new List<int>();
            spins.Add(0);
            spins.Add(0);
            spins.Add(0);
            spins.Add(0);
            spins.Add(1);       // 4
            spins.Add(0);
            spins.Add(0);
            spins.Add(0);
            spins.Add(0);       //8
            spins.Add(0);
            spins.Add(0);
            spins.Add(0);
            spins.Add(0);
            spins.Add(1);       //13
            spins.Add(0);
            spins.Add(0);
            spins.Add(0);
            spins.Add(0);
            spins.Add(1);       //18
            spins.Add(0);
            spins.Add(0);
            spins.Add(0);
            spins.Add(0);       //22
            spins.Add(0);
            spins.Add(0);
            spins.Add(1);       //25
            spins.Add(0);
            spins.Add(0);
            spins.Add(1);       //28
            spins.Add(0);
            spins.Add(0);
            spins.Add(1);
            spins.Add(0);
            spins.Add(0);
            spins.Add(1);       //34
            spins.Add(0);
            spins.Add(0);
            spins.Add(1);       //37
            spins.Add(0);
            spins.Add(0);
            spins.Add(1);       //40
            spins.Add(0);
            #endregion

            //boolean list of Wammies
            #region wammy;
            List<bool> wammy = new List<bool>();
            wammy.Add(false);
            wammy.Add(true);    // 1  
            wammy.Add(false);
            wammy.Add(false);
            wammy.Add(false);
            wammy.Add(true);    //5
            wammy.Add(false);
            wammy.Add(false);
            wammy.Add(true);    //8
            wammy.Add(false);
            wammy.Add(true);    //10
            wammy.Add(false);
            wammy.Add(false);
            wammy.Add(false);
            wammy.Add(false);
            wammy.Add(true);    //15
            wammy.Add(false);
            wammy.Add(false);
            wammy.Add(false);
            wammy.Add(false);
            wammy.Add(false);   //20   
            wammy.Add(false);
            wammy.Add(false);
            wammy.Add(false);
            wammy.Add(false);
            wammy.Add(false);   //25
            wammy.Add(false);
            wammy.Add(false);
            wammy.Add(false);
            wammy.Add(false);
            wammy.Add(false);   //30
            wammy.Add(false);
            wammy.Add(false);
            wammy.Add(false);
            wammy.Add(false);
            wammy.Add(false);   //35
            wammy.Add(false);
            wammy.Add(false);
            wammy.Add(false);
            wammy.Add(false);
            wammy.Add(false);   //40
            wammy.Add(true);    //41
            #endregion

            //boolean list of big bucks
            #region bigbucks;
            List<bool> bigBucks = new List<bool>();
            bigBucks.Add(true);     // 0 the only one
            bigBucks.Add(false);
            bigBucks.Add(false);
            bigBucks.Add(false);
            bigBucks.Add(false);
            bigBucks.Add(false);    // 5
            bigBucks.Add(false);
            bigBucks.Add(false);
            bigBucks.Add(false);
            bigBucks.Add(false);
            bigBucks.Add(false);    // 10
            bigBucks.Add(false);
            bigBucks.Add(false);
            bigBucks.Add(false);
            bigBucks.Add(false);
            bigBucks.Add(false);    // 15
            bigBucks.Add(false);
            bigBucks.Add(false);
            bigBucks.Add(false);
            bigBucks.Add(false);
            bigBucks.Add(false);    // 20
            bigBucks.Add(false);
            bigBucks.Add(false);
            bigBucks.Add(false);
            bigBucks.Add(false);
            bigBucks.Add(false);    //25
            bigBucks.Add(false);
            bigBucks.Add(false);
            bigBucks.Add(false);
            bigBucks.Add(false);
            bigBucks.Add(false);    // 30
            bigBucks.Add(false);
            bigBucks.Add(false);
            bigBucks.Add(false);
            bigBucks.Add(false);
            bigBucks.Add(false);    // 35
            bigBucks.Add(false);
            bigBucks.Add(false);
            bigBucks.Add(false);
            bigBucks.Add(false);
            bigBucks.Add(false);    // 40
            bigBucks.Add(false);    // 41
            #endregion

            //create tiles array and load from tileList
            List<Image> tiles = new List<Image>();

            for (int i = 0; i < tileList.Images.Count; i++)
            {
                tiles.Add(tileList.Images[i]);
            }
         

            //create lights array and load from liteList
            List<Image> lights = new List<Image>();

            for (int i = 0; i < lightList.Images.Count; i++)
            {
                lights.Add(lightList.Images[i]);
            }


            //These are the arrays of the picture boxes, as opposed to the arrays
            //of images. Here we associate the light (active) picture box with 
            //the game tile that appears inside it.

            PictureBox[] lites = new PictureBox[] { pictureBox1, pictureBox2, pictureBox3, pictureBox4,
            pictureBox5, pictureBox6, pictureBox7, pictureBox8, pictureBox9, pictureBox10, pictureBox11,
            pictureBox12, pictureBox13, pictureBox14, pictureBox15, pictureBox16, pictureBox17,
            pictureBox18};

            PictureBox[] gameTiles = new PictureBox[] { pictureBox19, pictureBox20, pictureBox21, 
            pictureBox22, pictureBox23, pictureBox24, pictureBox25, pictureBox26, pictureBox27,
            pictureBox28, pictureBox29, pictureBox30, pictureBox31, pictureBox32, pictureBox33, 
            pictureBox34, pictureBox35, pictureBox36};

            //use separate randomly generated numbers for tiles and lights
            Random idx = new Random();
            int x = idx.Next(18);
            int y = idx.Next(42);

            //randomly assign 17 gray and 1 yellow (lit) lite boxes
            foreach (PictureBox pictureBox in lites)
            {
                pictureBox.Image = lights[(x % 18)];
                x++;
            }


            //randomly assign 18 game tiles
            foreach (PictureBox pictureBox in gameTiles)
            {
                pictureBox.Image = tiles[(y % 42)];
                y++;
            }

            //"hit" is the index at which the active ("lit up") tile is found in lites[]
            int hit = 0, k = 0;

            //required do-while loop
            do
            {
                if (lites[k].Image == lights[17])
                {
                    hit = k;    // index plus one is the picture box number
                }
                k++;

            } while (k < 17);

            //links the image of the highlighted tile with its associated values through lookUp
            Image target = gameTiles[hit].Image;
            int tgtd = tiles.IndexOf(target);
            lookUp(tgtd, dollars, spins, wammy, bigBucks);
        }

        //lookUp provides the values to updateScoreboard
        private void lookUp(int indx, List<int> dols, List<int> spns, List<bool> wmmy, List<bool> bbks)
        {
            cashMoney = dols[indx];
            newSpins = spns[indx];
            ohNoWammy = wmmy[indx];
            bigMoney = bbks[indx];
        }

        //button is displayed with the instruction screen; disappears when game starts
        private void startButton_Click(object sender, EventArgs e)
        {

            instrucGroupBx.Visible = false;
            quest = new Questions();
            player1 = new Player(0, 0, 0, 0, "Mason");
            player2 = new Player(0, 0, 0, 0, "Scott");
            player3 = new Player(0, 0, 0, 0, "Dr. Stringfellow");
            //roundType = true;
            updateScoreboard();
            promptQuestion();
        }

        private void promptQuestion()
        {
            spinButton.Visible = false;
            passButton.Visible = false;
            passOrSpinTextBox.Visible = false;
            passOrSpinTextBox.SendToBack();
            questionText.Visible = true;
            questionLabel.Visible = true;
            answerBox.Visible = true;
            answerLabel.Visible = true;

            String question = quest.getNextQuestion();
            questionCount++;
            questionText.Text = question;
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            //String answer = null;

            if (!answerBox.Focused)
            {
                if (e.KeyChar.ToString() == "1")
                {
                    MessageBox.Show(String.Format("{0} buzzed in!", player1.Name));
                    activePlayer = player1;
                    answerBox.Enabled = true;
                    answerBox.Focus();
                }
                if (e.KeyChar.ToString() == "2")
                {
                    MessageBox.Show(String.Format("{0} buzzed in!", player2.Name));
                    activePlayer = player2;
                    answerBox.Enabled = true;
                    answerBox.Focus();
                }
                if (e.KeyChar.ToString() == "3")
                {
                    MessageBox.Show(String.Format("{0} buzzed in!", player3.Name));
                    activePlayer = player3;
                    answerBox.Enabled = true;
                    answerBox.Focus();
                }
            }
            else
            {
                if (e.KeyChar.Equals((char) Keys.Enter))
                {
                    checkAnswer();
                }
            }
        }

        private void answerButton_Click(object sender, EventArgs e)
        {
            checkAnswer();
        }

        private void checkAnswer()
        {
            if (activePlayer == null)
            {
                MessageBox.Show("Nobody buzzed in.");
            }
            else
            {
                readAnswer();
            }
        }

        private void readAnswer()
        {
            String answer = answerBox.Text;

            if (quest.checkAnswer(answer))
            {
                MessageBox.Show("Correct! You earned a spin.");
                activePlayer.NumEarnedSpins += 3;
                updateScoreboard();
            }
            else
            {
                MessageBox.Show(String.Format("Incorrect! The correct answer was {0}.", quest.getAnswer()));
            }

            answerBox.Text = "";
            activePlayer = null;
            answerBox.Enabled = false;

            if (questionCount < 3)
            {
                promptQuestion();
            }
            else
            {//pass to spin round with the last player to answer correctly as active player ss
                /*if (player1.Active && player1.NumEarnedSpins > 0)
                {
                    activePlayer = player1;
                }
                else if (player2.Active && player2.NumEarnedSpins > 0)
                {
                    activePlayer = player2;
                }
                else
                {
                    activePlayer = player3;
                }*/
                questionCount = 0; // reset question count ss
                beginSpinRound();
            }
        }

        private void beginSpinRound()
        {
            passOrSpinTextBox.BringToFront();
            spinButton.Visible = true;
            passButton.Visible = true;
            questionText.Visible = false;
            questionLabel.Visible = false;
            answerBox.Visible = false;
            answerLabel.Visible = false;

            if (activePlayer.Active && (activePlayer.NumEarnedSpins > 0 || activePlayer.NumPassedSpins > 0))
            {
                if (activePlayer.NumPassedSpins > 0)
                {
                    passOrSpinTextBox.Text = String.Format("{0}, you have {1} passed spins. You must spin.", activePlayer.Name, activePlayer.NumPassedSpins);
                    spinButton.Enabled = true;
                    passButton.Enabled = false;
                }
                else
                {
                    passOrSpinTextBox.Text = String.Format("{0}, Would you like to spin or pass?", activePlayer.Name);
                    spinButton.Enabled = true;
                    passButton.Enabled = true;
                }
            }

            else
            {
                if (activePlayer == player1)
                { 
                    if(player2.NumEarnedSpins > 0 && player2.Active)
                    {
                        activePlayer = player2;
                        beginSpinRound();
                    }
                    else if (player3.NumEarnedSpins > 0 && player3.Active)
                    {
                        activePlayer = player3;
                        beginSpinRound();
                    }
                    else
                    {
                        promptQuestion();
                    }
                }
                if (activePlayer == player2)
                {
                    if (player3.NumEarnedSpins > 0 && player3.Active)
                    {
                        activePlayer = player3;
                        beginSpinRound();
                    }
                    else if (player1.NumEarnedSpins > 0 && player1.Active)
                    {
                        activePlayer = player1;
                        beginSpinRound();
                    }
                    else
                    {
                        promptQuestion();
                    }
                }
                if (activePlayer == player3)
                {
                    if (player1.NumEarnedSpins > 0 && player1.Active)
                    {
                        activePlayer = player1;
                        beginSpinRound();
                    }
                    else if (player2.NumEarnedSpins > 0 && player2.Active)
                    {
                        activePlayer = player2;
                        beginSpinRound();
                    }
                    else
                    {
                        questionCount = 0;
                        promptQuestion();
                    }
                }
            }
            passOrSpinTextBox.Visible = true;

        }

        private void updateScoreboard()
        {
            player1Label.Text = String.Format("{0}:\n\nMoney: {1}\n\nEarnedSpins: {2}\n\nPassedSpins: {3}\n\nWhammies:{4}",
                player1.Name, player1.Money, player1.NumEarnedSpins, player1.NumPassedSpins, player1.Whammies);
            player2Label.Text = String.Format("{0}:\n\nMoney: {1}\n\nEarnedSpins: {2}\n\nPassedSpins: {3}\n\nWhammies:{4}", 
                player2.Name, player2.Money, player2.NumEarnedSpins, player2.NumPassedSpins, player2.Whammies);
            player3Label.Text = String.Format("{0}:\n\nMoney: {1}\n\nEarnedSpins: {2}\n\nPassedSpins: {3}\n\nWhammies:{4}", 
                player3.Name, player3.Money, player3.NumEarnedSpins, player3.NumPassedSpins, player3.Whammies);
        }

        private void passButton_Click(object sender, EventArgs e)
        {
            passOrSpinTextBox.Text = "Click on the player to whom you would like to pass.";
            player1Label.Enabled = true;
            player2Label.Enabled = true;
            player3Label.Enabled = true;
        }

        #region Player Pass Clicks

        private void player1Label_Click(object sender, EventArgs e)
        {
            if (activePlayer != player1)
            {
                player1.NumPassedSpins += activePlayer.NumEarnedSpins;
                activePlayer.NumEarnedSpins = 0;
                activePlayer = player1;
                updateScoreboard();
                beginSpinRound();
            }
        }

        private void player2Label_Click(object sender, EventArgs e)
        {
            if (activePlayer != player2)
            {
                player2.NumPassedSpins += activePlayer.NumEarnedSpins;
                activePlayer.NumEarnedSpins = 0;
                activePlayer = player2;
                updateScoreboard();
                beginSpinRound();
            }
        }

        private void player3Label_Click(object sender, EventArgs e)
        {
            if (activePlayer != player3)
            {
                player3.NumPassedSpins += activePlayer.NumEarnedSpins;
                activePlayer.NumEarnedSpins = 0;
                activePlayer = player3;
                updateScoreboard();
                beginSpinRound();
            }
        }

        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            if (activePlayer != null)
                button1.Text = activePlayer.Name;
            else
                button1.Text = "Nobody";
        }

        private void endGame()
        {
            if (player1.Active)
            {
                activePlayer = player1;
            }
            else if (player2.Active)
            {
                activePlayer = player2;
            }
            else
            {
                activePlayer = player3;
            }
            MessageBox.Show(String.Format("Congrats {0}, You won the Game!", activePlayer.Name));
        }
    }
}
