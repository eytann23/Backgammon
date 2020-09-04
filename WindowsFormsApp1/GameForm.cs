using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using BackgammonWorld.Properties;


namespace BackgammonWorld
{
    public partial class BackgammonGameUI : Form
    {
        public BackgammonGameUI()
        {
            InitializeComponent();
            initializeBoard();
        }

        BackgammonGame backgammonGame = new BackgammonGame();

        void initializeBoard()
        {
            for (int i = 0; i <= 25; i++)
            {
                FlowLayoutPanel triangleContainer = backgammonGame.GetTriangleContainer(i);
                if (triangleContainer != null)
                {
                    triangleContainer.Click += new EventHandler(Container_Click);
                    triangleContainer.MouseEnter += new EventHandler(Triangle_MouseEnter);
                    triangleContainer.MouseLeave += new EventHandler(Triangle_MouseLeave);

                    this.Controls.Add(triangleContainer);
                }

            }
            this.Controls.Add(backgammonGame.GetBlackBeatedPlaceContainer());
            this.Controls.Add(backgammonGame.GetWhiteBeatedPlaceContainer());
            backgammonGame.GetBlackBeatedPlaceContainer().Click += new EventHandler(Container_Click);
            backgammonGame.GetWhiteBeatedPlaceContainer().Click += new EventHandler(Container_Click);
            backgammonGame.GetBlackBeatedPlaceContainer().MouseEnter += new EventHandler(BeatedPlace_MouseEnter);
            backgammonGame.GetBlackBeatedPlaceContainer().MouseLeave += new EventHandler(BeatedPlace_MouseLeave);
            backgammonGame.GetWhiteBeatedPlaceContainer().MouseEnter += new EventHandler(BeatedPlace_MouseEnter);
            backgammonGame.GetWhiteBeatedPlaceContainer().MouseLeave += new EventHandler(BeatedPlace_MouseLeave);
            startButton.Click += new EventHandler(startButton_Click);

        }
        void gameOpening()
        {
            if (gameStarted)
                return;
            
            rollingAnimation();

            int blackUserDice = backgammonGame.GetRandomDiceNumber();
            int whiteUserDice = backgammonGame.GetRandomDiceNumber();

            if (blackUserDice == whiteUserDice)
            {
                gameOpening();
                return;
            }
            else
            {
                dicePictureBox1.Image = getDiceImage(blackUserDice);
                dicePictureBox2.Image = getDiceImage(whiteUserDice);
            }

            if (blackUserDice > whiteUserDice)
            {
                backgammonGame.PassToBlackTurn();
                MessageBox.Show("Black player is starting!");
            }
                
            else
            {
                backgammonGame.PassToWhiteTurn();
                MessageBox.Show("White player is starting!");
            }
            dicePictureBox1.Image = null;
            dicePictureBox2.Image = null;
            startButton.Enabled = false;
            startButton.Visible = false;
            blackStartLabel.Visible = false;
            whiteStartLabel.Visible = false;
            rollButton.Visible = true;
            gameStarted = true;
        }


        int clickedContainer;
        bool gameStarted = false;
        bool userRolledTheDices = false;
       

        void rollingAnimation()
        {
            dicePictureBox3.Image = null;
            dicePictureBox4.Image = null;
            for (int i=0;i<3;i++)
            {
                dicePictureBox1.Image = Resources.Rolling1;
                dicePictureBox2.Image = Resources.Rolling6;
                dicePictureBox1.Refresh();
                dicePictureBox2.Refresh();
                Thread.Sleep(70);
                dicePictureBox1.Image = Resources.Rolling2;
                dicePictureBox2.Image = Resources.Rolling5;
                dicePictureBox1.Refresh();
                dicePictureBox2.Refresh();
                Thread.Sleep(70);
                dicePictureBox1.Image = Resources.Rolling3;
                dicePictureBox2.Image = Resources.Rolling4;
                dicePictureBox1.Refresh();
                dicePictureBox2.Refresh();
                Thread.Sleep(70);
                dicePictureBox1.Image = Resources.Rolling4;
                dicePictureBox2.Image = Resources.Rolling3;
                dicePictureBox1.Refresh();
                dicePictureBox2.Refresh();
                Thread.Sleep(70);
                dicePictureBox1.Image = Resources.Rolling5;
                dicePictureBox2.Image = Resources.Rolling2;
                dicePictureBox1.Refresh();
                dicePictureBox2.Refresh();
                Thread.Sleep(70);
                dicePictureBox1.Image = Resources.Rolling6;
                dicePictureBox2.Image = Resources.Rolling1;
                dicePictureBox1.Refresh();
                dicePictureBox2.Refresh();
            }
            
        }
        
        //Events
        private void startButton_Click(object sender, EventArgs e)
                {
                    gameOpening();
                }
        private void rollButton_Click(object sender, EventArgs e)
        {
            dicePictureBox3.Image = null;
            dicePictureBox4.Image = null;
            dicePictureBox3.Refresh();
            dicePictureBox4.Refresh();
            rollingAnimation();

            backgammonGame.RollDices();

            rollButton.Enabled = false;
            rollButton.Visible = false;
            userRolledTheDices = true;
            moveBackgroundWorker.RunWorkerAsync();
        }
        public void BeatedPlace_MouseEnter(object sender, EventArgs e)
        {
            FlowLayoutPanel container = sender as FlowLayoutPanel;
            if (container.Enabled == true)
                container.BackgroundImage = Resources.greenBeated;
        }
        public void BeatedPlace_MouseLeave(object sender, EventArgs e)
        {
            FlowLayoutPanel container = sender as FlowLayoutPanel;
            if (container.Enabled == true)
                container.BackgroundImage = null;
        }
        public void Triangle_MouseEnter(object sender, EventArgs e)
        {
            FlowLayoutPanel container = sender as FlowLayoutPanel;
            if (container.Enabled == true && container.TabIndex<13 && container.TabIndex >0)
                container.BackgroundImage = Resources.greenBottom;
            if (container.Enabled == true && container.TabIndex < 25 && container.TabIndex > 12)
                container.BackgroundImage = Resources.greenTop;
            if (container.Enabled == true && (container.TabIndex == 25 || container.TabIndex == 0))
                container.BackgroundImage = Resources.greenSides;
        }
        public void Triangle_MouseLeave(object sender, EventArgs e)
        {
            FlowLayoutPanel container = sender as FlowLayoutPanel;
            if (container.Enabled == true)
                container.BackgroundImage = null;
        }
        private void Game_Click(object sender, EventArgs e)
        {
            sourceSelectionCanceled = true;
            clickOnForm.Set();
        }
        public void Container_Click(object sender, EventArgs e)
        {
            FlowLayoutPanel container = sender as FlowLayoutPanel;
            
            if (userRolledTheDices)
            {
                if (container.Enabled == true && container.TabIndex < 13 && container.TabIndex > 0)
                    container.BackgroundImage = Resources.greenBottom;
                if (container.Enabled == true && container.TabIndex < 25 && container.TabIndex > 12)
                    container.BackgroundImage = Resources.greenTop;
                clickedContainer = container.TabIndex;
                backgammonGame.SetClickedContainer(container.TabIndex);
                triangleSelected.Set();
            }
            
        }
        AutoResetEvent triangleSelected = new AutoResetEvent(false);




        //Move
        int sourceTriagle, destinationTriangle;
        AutoResetEvent clickOnForm = new AutoResetEvent(false);
        AutoResetEvent resetMoveFinished = new AutoResetEvent(false);
        bool sourceSelectionCanceled;
        List<int> dicesData;
        Image getDiceImage (int number)
        {
            switch (number)
            {
                case 1:
                    return Resources.diceOne;
                case 2:
                    return Resources.DiceTwo;
                case 3:
                    return Resources.DiceThree;
                    
                case 4:
                    return Resources.DiceFour;
                    
                case 5:
                    return Resources.DiceFive;
                    
                case 6:
                    return Resources.DiceSix;       
            }
            return null;
        }
        void showDices(List<int> dicesData)
        {
            dicePictureBox3.Image = null;
            dicePictureBox1.Image = null;
            dicePictureBox2.Image = null;
            dicePictureBox4.Image = null;
            dicePictureBox3.Refresh();
            dicePictureBox4.Refresh();
            if (dicesData.Count == 1)
                dicePictureBox1.Image = getDiceImage(dicesData[0]);
  
            else if (dicesData.Count > 1)
            {
                int diceA = dicesData[0];
                int diceB = dicesData[1];

                dicePictureBox1.Image = getDiceImage(diceA);
                dicePictureBox2.Image = getDiceImage(diceB);
                if (dicesData.Count > 2)
                {
                    dicePictureBox3.Image = getDiceImage(diceA);
                    if (dicesData.Count > 3)
                        dicePictureBox4.Image = getDiceImage(diceA);
                }                   
            }
        }
        void clearAllIndications()
        {
            for (int i = 0; i <= 25; i++)
                backgammonGame.GetTriangleContainer(i).BackgroundImage = null;
            backgammonGame.GetBlackBeatedPlaceContainer().BackgroundImage = null;
            backgammonGame.GetWhiteBeatedPlaceContainer().BackgroundImage = null;
        }
        enum Status { MoveStarted, SourceSelected, DestinationSelected }
        
        //BackgroundWorker
        private void moveBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            dicesData = backgammonGame.GetDicesData();
            while (dicesData.Count!=0)
            {
                //Check for a winner
                if (backgammonGame.IsBlackOutsideStockFull() || backgammonGame.IsWhiteOutsideStockFull())
                    break;

                //Start
                moveBackgroundWorker.ReportProgress((int)Status.MoveStarted);
                resetMoveFinished.WaitOne();
                if (moveBackgroundWorker.CancellationPending)
                    return;
                
                //Source
                triangleSelected.WaitOne();
                moveBackgroundWorker.ReportProgress((int)Status.SourceSelected);
                
                sourceSelectionCanceled = false;
                clickOnForm.Reset();
                triangleSelected.Reset();

                //Destination
                WaitHandle.WaitAny(new WaitHandle[] { clickOnForm, triangleSelected });
                if (sourceSelectionCanceled)
                    continue;
                moveBackgroundWorker.ReportProgress((int)Status.DestinationSelected);
                Thread.Sleep(200);


                clearAllIndications();  
            }
        }
        private void moveBackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage==(int)Status.MoveStarted)
            {
                clearAllIndications();
                backgammonGame.EnableOnlyOptionalSources();
                 
                
                showDices(backgammonGame.GetDicesData());
                if (!backgammonGame.EnableOnlyOptionalSources())
                {
                    MessageBox.Show("You have no optional moves!");
                    moveBackgroundWorker.CancelAsync();
                }
                sourceSelectionCanceled = false;

                clickOnForm.Reset();
                resetMoveFinished.Set();

            }
                

            if (e.ProgressPercentage== (int)Status.SourceSelected)
            {
                sourceTriagle = clickedContainer;
                backgammonGame.SetSelectedSourceTriangle(sourceTriagle);
                backgammonGame.EnableOnlyOptionalDestinations();
            }


            if (e.ProgressPercentage == (int)Status.DestinationSelected)
            {
                destinationTriangle = clickedContainer;
                backgammonGame.SetSelectedDestinationTriangle(destinationTriangle);

                if (backgammonGame.IsBeatedCheckerWaiting())
                    backgammonGame.RemoveCheckerFromBeatedPlace();
                
                else
                {
                    int usedDice = destinationTriangle - sourceTriagle;
                    usedDice = usedDice < 0 ? -usedDice : usedDice;
                    if (destinationTriangle == 0 || destinationTriangle==25)
                        if (!dicesData.Contains(usedDice))//if dice=6 and user took out from 3
                            dicesData.RemoveAt(0);
                        else
                            dicesData.Remove(usedDice);
                    else
                        dicesData.Remove(usedDice);
                    
                    backgammonGame.RemovePieceFromTriangle(sourceTriagle);//Remove

                }
                if (backgammonGame.IsBeatingOccurs())//Beat
                    backgammonGame.BeatPiece(destinationTriangle);

                backgammonGame.AddPieceToTriangle(destinationTriangle);//Add

                showDices(backgammonGame.GetDicesData());
            }
        }
        private void moveBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            backgammonGame.PassTurn();
            backgammonGame.DisableAllBoard();
            userRolledTheDices = false;
            rollButton.Enabled = true;
            rollButton.Visible = true;

            if (backgammonGame.IsBlackOutsideStockFull())
            {
                MessageBox.Show("Black player is the WINNER!");
                rollButton.Enabled = false;
                rollButton.Visible = false;
            }
                
            if (backgammonGame.IsWhiteOutsideStockFull())
            {
                MessageBox.Show("White player is the WINNER!");
                rollButton.Enabled = false;
                rollButton.Visible = false;
            }

            if (!backgammonGame.IsAnyMoveOptionAvailableBeforeRollingDices())
            {
                string colorTurn = backgammonGame.IsBlackTurnToString();
                MessageBox.Show(colorTurn+" player have no optional moves!");
                backgammonGame.PassTurn();
            }


            
            
        }


    }


}
