using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Team03.Def;
using Team03.Device;
using Team03.Util;

namespace Team03.Actor
{
    class BoundAI : AI
    {
        private Vector2 velocity;

        public BoundAI()
        {
            var gameDevice = GameDevice.Instance();
            var rnd = gameDevice.GetRandom();
            int speed = rnd.Next(5, 21);
            speed = (rnd.Next(2) == 0) ? (speed) : (-speed);
            velocity = new Vector2(speed, 0.0f);
        }

        public override Vector2 Think(Character character)
        {
            character.SetPosition(ref position);
            position = position + velocity;
            Range range = new Range(0, Screen.Width - 64);
            if(range.IsOutOfRange((int)position.X))
            {
                velocity = -velocity;
            }
            return position;
        }
    }
}
