using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace Amazing2Dgame
{
    public partial class GameScreen : UserControl
    {
        //player1 button control keys
        Boolean leftArrowDown, rightArrowDown;

        //used to draw boxes and character on screen
        SolidBrush drawBrush = new SolidBrush(Color.Black);
        SolidBrush boxBrush = new SolidBrush(Color.White);
        Font drawFont = new Font("Arial", 10, FontStyle.Bold);
        Font scoreFont = new Font("Arial", 20, FontStyle.Bold);
        SolidBrush scoreBrush = new SolidBrush(Color.White);

        //Hero object and values
        Ball hero;
        Ball b;
        int heroSpeed = 6;
        int heroSize = 10;
        int heroStartX = 180;
        int heroStartY = 320;
        int heroHealth = 2;
        int healthLower = 1;
        int healthUpper = 6;
        int powerLower = 3;
        int powerUpper = 8;
        int health1;
        int health2;
        int health3;
        int health4;
        int health5;
        int powerValue;
        int powerLocation;

        //Box valaues 
        List<Ball> boxes1 = new List<Ball>();
        List<Ball> boxes2 = new List<Ball>();
        List<Ball> boxes3 = new List<Ball>();
        List<Ball> boxes4 = new List<Ball>();
        List<Ball> boxes5 = new List<Ball>();

        //healthScore list 
        List<Ball> power3 = new List<Ball>();

        int b1StartX = 3;
        int b2StartX = 76;
        int b3StartX = 149;
        int b4StartX = 222;
        int b5StartX = 295;
        int bSize = 70;
        int bSpeed = 2;
        int newBoxCounter = 0;
        int healthTimer;
        int scoreTimer;
        int score = 1;
        bool heroMoving = true;
        bool heroPower = false;
        Random rand = new Random();


        public GameScreen()
        {
            InitializeComponent();
            OnStart();
        }

        public void OnStart()
        {
            #region add hero ball and box objects to corrisponding lists
            
            //add the hero ball to the screen
            hero = new Ball(heroStartX, heroStartY, heroSize, Color.White, heroHealth);

            //add a box to boxes 1
            b = new Ball(b1StartX, 0, bSize, Color.Black, 2);
            boxes1.Add(b);

            //add a box to boxes 2
            b = new Ball(b2StartX, 0, bSize, Color.Black, 2);
            boxes2.Add(b);

            //add a box to boxes 3
            b = new Ball(b3StartX, 0, bSize, Color.Black, 2);
            boxes3.Add(b);

            //add a box to boxes 4
            b = new Ball(b4StartX, 0, bSize, Color.Black, 2);
            boxes4.Add(b);

            //add a box to boxes 5
            b = new Ball(b5StartX, 0, bSize, Color.Black, 2);
            boxes5.Add(b);

            //add a power up to power3
            b = new Ball(heroStartX, heroStartY - 75, heroSize, Color.Yellow, 2);
            power3.Add(b);
            #endregion
        }

        public void EndGame()
        {
            //reset the game to the main menue screen and reset paramters 
            GameLoop.Enabled = false;
            Form f = this.FindForm();
            f.Controls.Remove(this);
            MainScreen ms = new MainScreen();
            f.Controls.Add(ms);
            heroHealth = 10;
        }

        private void GameScreen_KeyUp(object sender, KeyEventArgs e)
        {
            //hero button releases
            switch (e.KeyCode)
            {
                case Keys.Left:
                    leftArrowDown = false;
                    break;
                case Keys.Right:
                    rightArrowDown = false;
                    break;
            }
        }

        private void GameScreen_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            //hero button releases
            switch (e.KeyCode)
            {
                case Keys.Left:
                    leftArrowDown = true;
                    break;
                case Keys.Right:
                    rightArrowDown = true;
                    break;
            }
        }

        private void GameLoop_Tick(object sender, EventArgs e)
        {
            #region if it is time increase score 
            //update the players score 
            scoreTimer++;
            if (scoreTimer % 200 == 0)
            {
                score++;

                //every 10 score increace the box health and power up value, increase box health more so they will eventually die
                if(score % 10 == 0)
                {
                    healthUpper = healthUpper + 5;
                    healthLower = healthLower + 5;
                    powerUpper = powerUpper + 3;
                    powerLower = powerLower + 3;
                }
            }

            #endregion

            #region move hero

            //if the hero is pressing the left arrow move the hero left 
            if (leftArrowDown == true && hero.rec.X > 0)
            {
                hero.Move(heroSpeed, "left");
            }

            //else move the hero right
            else if (rightArrowDown == true && hero.rec.X < this.Width - 16)
            {
                hero.Move(heroSpeed, "right");
            }
            Refresh();

            #endregion

            #region create a new box

            //only create a new box if the hero is not currently hitting a box 
            if (heroMoving == true)
            {
                //if it is time, create a new box and power up with random health and value and locatio (column 1-5)
                newBoxCounter++;
                if (newBoxCounter == 120)
                {
                    health1 = rand.Next(healthLower, healthUpper);
                    health2 = rand.Next(healthLower, healthUpper);
                    health3 = rand.Next(healthLower, healthUpper);
                    health4 = rand.Next(healthLower, healthUpper);
                    health5 = rand.Next(healthLower, healthUpper);
                    powerLocation = rand.Next(1, 6);
                    powerValue = rand.Next(powerLower, powerUpper);

                    Color c = Color.Black;
                    Color a = Color.Yellow;

                    //create the object for each box 
                    Ball b1 = new Ball(b1StartX, 0, bSize, c, health1);
                    Ball b2 = new Ball(b2StartX, 0, bSize, c, health2);
                    Ball b3 = new Ball(b3StartX, 0, bSize, c, health3);
                    Ball b4 = new Ball(b4StartX, 0, bSize, c, health4);
                    Ball b5 = new Ball(b5StartX, 0, bSize, c, health5);

                    //if the random number is 1 then create the power object in column number 1 and add it to the list 
                    if (powerLocation == 1)
                    {
                        Ball p1 = new Ball(b1StartX + 30, 135, heroSize, a, powerValue);
                        power3.Add(p1);
                    }

                    //if the random number is 2 then create the power object in column number 2 and add it to the list 
                    if (powerLocation == 2)
                    {
                        Ball p1 = new Ball(b2StartX + 30, 135, heroSize, a, powerValue);
                        power3.Add(p1);
                    }

                    //if the random number is 3 then create the power object in column number 3 and add it to the list 
                    if (powerLocation == 3)
                    {
                        Ball p1 = new Ball(b3StartX + 30, 135, heroSize, a, powerValue);
                        power3.Add(p1);
                    }

                    //if the random number is 4 then create the power object in column number 4 and add it to the list 
                    if (powerLocation == 4)
                    {
                        Ball p1 = new Ball(b4StartX + 30, 135, heroSize, a, powerValue);
                        power3.Add(p1);
                    }

                    //if the random number is 5 then create the power object in column number 5 and add it to the list 
                    if (powerLocation == 5)
                    {
                        Ball p1 = new Ball(b5StartX + 30, 135, heroSize, a, powerValue);
                        power3.Add(p1);
                    }

                    //add each box object to their corrisponding lists 
                    boxes1.Add(b1);
                    boxes2.Add(b2);
                    boxes3.Add(b3);
                    boxes4.Add(b4);
                    boxes5.Add(b5);
                    
                    //reset the new box counter as a box has just been created 
                    newBoxCounter = 0;
                }
            }
            #endregion

            #region update the postion of the boxes 

            //only update the postion of the boxes if the hero isnt hitting a box 
            if (heroMoving == true)
            {
                //move each box in the first column down the page 
                foreach (Ball b in boxes1)
                {
                    b.Move(bSpeed);
                }

                //move each box in the second column down the page 
                foreach (Ball b in boxes2)
                {
                    b.Move(bSpeed);
                }

                //move each box in the third column down the page 
                foreach (Ball b in boxes3)
                {
                    b.Move(bSpeed);
                }

                //move each box in the forth column down the page 
                foreach (Ball b in boxes4)
                {
                    b.Move(bSpeed);
                }

                //move each box in the fith column down the page 
                foreach (Ball b in boxes5)
                {
                    b.Move(bSpeed);
                }

                //move each power up in the random column down the page 
                foreach (Ball b in power3)
                {
                    b.Move(bSpeed);
                }
            }
            #endregion

            #region remove the boxes that have went off the screen

            //if box 0 in the 1st colunm is off the screen then remove it
            if (boxes1[0].rec.Y > this.Height)
            {
                boxes1.RemoveAt(0);
            }

            //if box 0 in the 2nd column is off the screen then remove it
            if (boxes2[0].rec.Y > this.Height)
            {
                boxes2.RemoveAt(0);
            }

            //if box 0 in the 3rd column is off the screen then remove it
            if (boxes3[0].rec.Y > this.Height)
            {
                boxes3.RemoveAt(0);
            }

            //if box 0 in the 4th column is off the screen then remove it
            if (boxes4[0].rec.Y > this.Height)
            {
                boxes4.RemoveAt(0);
            }

            //if box 0 in the fifth column is off the screen then remove it
            if (boxes5[0].rec.Y > this.Height)
            {
                boxes5.RemoveAt(0);
            }

            ////if powerup 0 is off the screen then remove it 
            if (heroPower == false)
            {
                if (power3[0].rec.Y > this.Height)
                {
                    power3.RemoveAt(0);
                }
            }
            #endregion

            #region check for collisions between the hero and boxes

            //if box health in first column is 0 then remove it 
            for (int i = 0; i<boxes1.Count; i++)
            {
                if (boxes1[i].health == 0)
                {
                    boxes1.RemoveAt(i);
                    break;
                }
            }

            //for every box in boxes 1 check if it is colliding with the hero and if it is 
            //add to health timer 
            foreach (Ball b in boxes1)
            {
                if (hero.Collision(b))
                {
                    heroMoving = false;
                    healthTimer++;

                    //if the hero is hitting a box and it is time and the health of the box is more than zero 
                    //take away hero and box health 
                    if (healthTimer % 15 == 0 && b.health > 0 && heroHealth > 0)
                    {
                        heroHealth--;
                        b.health--;            
                    }

                    // if the hero runs out of health then end the game
                    else if (heroHealth == 0)
                    {
                        EndGame();                       
                    }
                    return;
                }

                //once the hero is no longer colliding with the box, then set HeroMoving to true so more moxes can be created
                else
                {
                    heroMoving = true;
                }
            }

            //repeat for boxes 2
            for (int j = 0; j < boxes2.Count; j++)
            {
                if (boxes2[j].health == 0)
                {
                    boxes2.RemoveAt(j);
                    break;
                }
            }

            foreach (Ball b in boxes2)
            {
                if (hero.Collision(b))
                {
                    heroMoving = false;
                    healthTimer++;

                    if (healthTimer % 15 == 0 && b.health > 0 && heroHealth > 0)
                    {
                        heroHealth--;
                        b.health--;
                    }

                    else if (heroHealth == 0)
                    {
                        EndGame();
                    }
                    return;
                }

                else
                {
                    heroMoving = true;
                }
            }

            //repeat for boxes 3
            for (int k = 0; k < boxes3.Count; k++)
            {
                if (boxes3[k].health == 0)
                {
                    boxes3.RemoveAt(k);
                    break;
                }
            }

            foreach (Ball b in boxes3)
            {
                if (hero.Collision(b))
                {
                    heroMoving = false;
                    healthTimer++;

                    if (healthTimer % 15 == 0 && b.health > 0 && heroHealth > 0)
                    {
                        heroHealth--;
                        b.health--;
                    }

                    else if (heroHealth == 0)
                    {
                        EndGame();
                    }
                    return;
                }

                else
                {
                    heroMoving = true;
                }
            }

            //repeat for boxes 4
            for (int l = 0; l < boxes4.Count; l++)
            {
                if (boxes4[l].health == 0)
                {
                    boxes4.RemoveAt(l);
                    break;
                }
            }

            foreach (Ball b in boxes4)
            {
                if (hero.Collision(b))
                {
                    heroMoving = false;
                    healthTimer++;

                    if (healthTimer % 15 == 0 && b.health > 0 && heroHealth > 0)
                    {
                        heroHealth--;
                        b.health--;
                    }

                    else if (heroHealth == 0)
                    {
                        EndGame();
                    }
                    return;
                }

                else
                {
                    heroMoving = true;
                }
            }

            //repeat for boxes 5
            for (int m = 0; m < boxes5.Count; m++)
            {
                if (boxes5[m].health == 0)
                {
                    boxes5.RemoveAt(m);
                    break;
                }
            }

            foreach (Ball b in boxes5)
            {
                if (hero.Collision(b))
                {
                    heroMoving = false;
                    healthTimer++;

                    if (healthTimer % 15 == 0 && b.health > 0 && heroHealth > 0)
                    {
                        heroHealth--;
                        b.health--;
                    }

                    else if (heroHealth == 0)
                    {
                        EndGame();
                    }
                    return;
                }

                else
                {
                    heroMoving = true;
                }
            }

            //hero and power up collions 
            foreach (Ball b in power3)
            {
                if (hero.Collision(b))
                {
                    heroHealth = heroHealth + b.health;
                    power3.RemoveAt(0);
                    heroPower = true;
                    return;
                }

                else
                {
                    heroPower = false;
                }
            }
            #endregion         
        }

        private void GameScreen_Paint(object sender, PaintEventArgs e)
        {
            //draw hero and score
            drawBrush.Color = Color.White;
            e.Graphics.FillEllipse(drawBrush, hero.rec);
            e.Graphics.DrawString(heroHealth + "", drawFont, drawBrush, hero.rec.X - 4, hero.rec.Y + 10);
            
            //draw boxes and power ups 
            #region draw the boxes 
            drawBrush.Color = Color.Black;

            //draw all boxes in boxes 1 
            foreach (Ball b in boxes1)
            {
                drawBrush.Color = b.color;
                e.Graphics.FillRectangle(drawBrush, b.rec);
                e.Graphics.DrawString(b.health + "", drawFont, boxBrush, b.rec.X + 30, b.rec.Y + 30);
            }

            //draw all boxes in boxes 2
            foreach (Ball b in boxes2)
            {
                drawBrush.Color = b.color;
                e.Graphics.FillRectangle(drawBrush, b.rec);
                e.Graphics.DrawString(b.health + "", drawFont, boxBrush, b.rec.X + 30, b.rec.Y + 30);
            }

            //draw all boxes in boxes 3
            foreach (Ball b in boxes3)
            {
                drawBrush.Color = b.color;
                e.Graphics.FillRectangle(drawBrush, b.rec);
                e.Graphics.DrawString(b.health + "", drawFont, boxBrush, b.rec.X + 30, b.rec.Y + 30);
            }

            //draw all boxes in boxes 4
            foreach (Ball b in boxes4)
            {
                drawBrush.Color = b.color;
                e.Graphics.FillRectangle(drawBrush, b.rec);
                e.Graphics.DrawString(b.health + "", drawFont, boxBrush, b.rec.X + 30, b.rec.Y + 30);
            }

            //draw all boxes in boxes 5
            foreach (Ball b in boxes5)
            {
                drawBrush.Color = b.color;
                e.Graphics.FillRectangle(drawBrush, b.rec);
                e.Graphics.DrawString(b.health + "", drawFont, boxBrush, b.rec.X + 30, b.rec.Y + 30);
            }

            //draw all the power ups in power3 
            foreach (Ball b in power3)
            {
                drawBrush.Color = b.color;
                e.Graphics.FillEllipse(drawBrush, b.rec);
                e.Graphics.DrawString(b.health + "", drawFont, drawBrush, b.rec.X, b.rec.Y + 10);
            }
            #endregion

            //draw the hero score
            e.Graphics.DrawString("Score:" + score + "", scoreFont, scoreBrush, 130, 10);
        }
    }
}
