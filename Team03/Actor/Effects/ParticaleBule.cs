﻿using Microsoft.Xna.Framework;
using Oikake.Device;
using Oikake.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oikake.Actor.Effects
{
    class ParticaleBule : Particle
    {
        private Timer timer;

        public ParticaleBule(string name, Vector2 position, Vector2 velocity, IParticleMediator mediator):base(name,position ,velocity,mediator)
        {
            var random = GameDevice.Instance().GetRandom();
            timer = new CountDownTimer(random.Next(1, 3));
        }

        public ParticaleBule(IParticleMediator mediator):base(mediator)
        {
            var random = GameDevice.Instance().GetRandom();
            timer = new CountDownTimer(random.Next(1, 3));
            name = "particleBlue";
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            timer.Update(gameTime);
            isDeadFlag = timer.IsTime();
        }
    }
}
