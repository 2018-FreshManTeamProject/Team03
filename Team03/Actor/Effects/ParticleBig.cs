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
    class ParticleBig : Particle
    {
        public ParticleBig(IParticleMediator mediator) : base(mediator)
        {
            Random random = GameDevice.Instance().GetRandom();
            velocity.Y = -random.Next(10, 31);
            position = new Vector2(400, 550);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            velocity -= velocity * 0.006f;
            //パーティクルを生成
            var partileSmall = mediator.generate("ParticleSmall");
            //小パーティクルの座標をセット
            partileSmall.SetPosition(position);
            //小パーティクルの速度をセット
            partileSmall.SetVelocity(velocity * 0.1f);
           
            if(velocity.Y >= 0)
            {
                //全方位にパーティクルを発生
                for(int i = 0; i < 100; i++)
                {
                    //ランダム取得
                    Random random = GameDevice.Instance().GetRandom();
                    //角度をランダムで決定する
                    var angle = MathHelper.ToRadians(random.Next(-180, 180));
                    //角度の方向に1の長さ
                    Vector2 v = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
                    //ParticleMiddleを生成
                    var particleMeddle = mediator.generate("ParticleMiddle");
                    //位置をセット
                    particleMeddle.SetPosition(position);
                    //移動量をセット
                    particleMeddle.SetVelocity(v * random.Next(5, 16));
                }
                isDeadFlag = true;
            }
        }
    }
}
