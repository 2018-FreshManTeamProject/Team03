﻿using Microsoft.Xna.Framework;
using Team03.Def;
using Team03.Scene;
using Team03.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team03.Actor
{
    class PlayerBullet : Character
    {
        private Vector2 velocity;

        public PlayerBullet(Vector2 position, IGameMediator mediator, Vector2 velocity) : base("white", mediator)
        {
            this.position = position;
            this.velocity = velocity;
        }
        public override void Hit(Character other)
        {
            isDeadFlag = true;
        }

        public override void Initialize()
        {
        }

        public override void Shutdown()
        {
        }

        public override void Update(GameTime gameTime)
        {
            position += velocity * 20;

            Range range = new Range(0, Screen.Width);
            if (range.IsOutOfRange((int)position.X))
            {
                isDeadFlag = true;
            }
            range = new Range(0, Screen.Height);
            if (range.IsOutOfRange((int)position.Y))
            {
                isDeadFlag = true;
            }

        }
    }
}