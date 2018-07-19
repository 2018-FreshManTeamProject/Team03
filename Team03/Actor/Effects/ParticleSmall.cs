using Microsoft.Xna.Framework;
using Team03.Device;
using Team03.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team03.Actor.Effects
{
    class ParticleSmall : Particle
    {
        private Timer timer;

        public ParticleSmall(IParticleMediator mediator) : base(mediator)
        {
            Random random = GameDevice.Instance().GetRandom();
            timer = new CountDownTimer(random.Next(10, 60) / 100.0f);

        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            velocity -= velocity * 0.006f;
            timer.Update(gameTime);
            isDeadFlag = timer.IsTime();
        }

    }
}
