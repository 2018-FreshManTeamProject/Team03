using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

using Oikake.Device;
using Oikake.Util;

namespace Oikake.Actor.Effects
{
    class ParticleMiddle : Particle
    {
        private Timer timer;

        public ParticleMiddle(IParticleMediator mediator) : base(mediator)
        {
            Random random = GameDevice.Instance().GetRandom();
            timer = new CountDownTimer(random.Next(20, 80) / 100.0f);

        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            //空気抵抗
            velocity -= velocity * 0.006f;
            //パーティクルを生成
            var partileSmall = mediator.generate("ParticleSmall");
            //小パーティクルの座標をセット
            partileSmall.SetPosition(position);
            //小パーティクルの速度をセット
            partileSmall.SetVelocity(velocity * 0.1f);
            //タイマー更新
            timer.Update(gameTime);
            //時間がたったら消滅(死亡)
            isDeadFlag = timer.IsTime();
        }
    }
}
