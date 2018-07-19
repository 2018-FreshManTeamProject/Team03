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
    class BoundEnemy : Character
    {
        //フィールド
        private Vector2 velocity;
        //ランダム
        private static Random rnd = new Random();

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public BoundEnemy(IGameMediator mediator) : base("black",mediator)
        {
            velocity = Vector2.Zero;
        }

        /// <summary>
        ///初期化
        /// </summary>
        public override void Initialize()
        {
            //位置をランダムで決める
            position = new Vector2(rnd.Next(Screen.Width - 64), rnd.Next(Screen.Height - 64));
            //最初は左移動
            velocity = new Vector2(-10f, 0);
        }

        /// <summary>
        //終了処理
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Shutdown()
        {
        }

        /// <summary>
        ///更新
        /// </summary>
        /// <param name="gameTime">ゲーム時間</param>
        public override void Update(GameTime gameTime)
        {
            //左壁で反射
            if(position.X < 0)
            {
                //移動量を反転
                velocity = -velocity;
            }
            //右壁で反射
            else if (Screen.Width - 64 <= position.X )
            {
                //移動量を反転
                velocity = -velocity;
            }


            //移動処理
            position += velocity;
        }

        public override void Hit(Character character)
        {
            isDeadFlag = true;
            mediator.AddScore(100);
            mediator.AddActor(new BoundEnemy(mediator));
            mediator.AddActor(new BoundEnemy(mediator));
            mediator.AddActor(new BurstEffect(position, mediator));

        }
    }
}
