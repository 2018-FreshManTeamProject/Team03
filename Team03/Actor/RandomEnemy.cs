using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Team03.Def;
using Team03.Scene;

namespace Team03.Actor
{
    class RandomEnemy : Character
    {
        //乱数オブジェクトはRandomEnemyクラスで共通になるようにする
        private static Random rnd = new Random();
        private int changeTimer;//切り替え時間

        public RandomEnemy(IGameMediator mediator) :base("black",mediator)
        {
            changeTimer = 60;
        }

        public override void Hit(Character character)
        {
            isDeadFlag = true;
            mediator.AddScore(10);
            mediator.AddActor(new RandomEnemy(mediator));
            mediator.AddActor(new RandomEnemy(mediator));
            mediator.AddActor(new BurstEffect(position, mediator));
        }

        public override void Initialize()
        {
            position = new Vector2(rnd.Next(Screen.Width - 64), rnd.Next(Screen.Height - 64));
            changeTimer = 60 * rnd.Next(2, 5);
        }

        public override void Shutdown()
        {
        }

        public override void Update(GameTime gameTime)
        {
            changeTimer -= 1;
            if(changeTimer < 0)
            {
                Initialize();
            }
        }
    }
}
