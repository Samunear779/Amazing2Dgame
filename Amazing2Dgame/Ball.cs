using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Amazing2Dgame
{
    class Ball
    {
        public Color color;
        public Rectangle rec;
        public int health;

        //create a ball/box with the following parameters 
        public Ball(int _x, int _y, int _size, Color _color, int _health)
        {
            color = _color;
            health = _health;
            rec = new Rectangle(_x, _y, _size,_size);
        }

        //code to move the hero left and right 
        public void Move(int speed, string direction)
        {
            if (direction == "left")
            {
                rec.X -= speed;
      
            }

            if (direction == "right")
            {
                rec.X += speed;
             
            }
        }

        //code to move the boxes and power ups down the screen 
        public void Move(int speed)
        {
            rec.Y += speed;
        }

        //if the hero is colliding with the boxes return true, if not return false;
        public Boolean Collision(Ball b)
        {
            if (rec.IntersectsWith(b.rec))
            {
                return true;
            }

            else
            {
                return false;
            }
        }
    }
}
