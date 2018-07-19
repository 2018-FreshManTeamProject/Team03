using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using Oikake.Device;
using Oikake.Def;
using Oikake.Scene;
using Oikake.Util;

namespace Oikake.Actor
{
    /// <summary>
    /// 白玉(プレイヤー)
    /// </summary>
    class Player　: Character
    {
        private Sound sound;
        private Motion motion;
        private enum Direction
        {
            DOWN, UP, RIGHT, LEFT
        };
        private Direction direction;
        private Dictionary<Direction, Range> directionRange;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Player(IGameMediator mediator) :base("oikake_player_4anime", mediator)
        {
            var gameDevice = GameDevice.Instance();
            sound = gameDevice.GetSound();
        }
        
        /// <summary>
        /// 初期化メソッド
        /// </summary>
        public override void Initialize()
        {
            //位置を(300,400)に設定
            position = new Vector2(300, 400);

            motion = new Motion();
            //下
            //motion.Add(0, new Rectangle(64 * 0, 64 * 0, 64, 64));
            //motion.Add(1, new Rectangle(64 * 1, 64 * 0, 64, 64));
            //motion.Add(2, new Rectangle(64 * 2, 64 * 0, 64, 64));
            //motion.Add(3, new Rectangle(64 * 3, 64 * 0, 64, 64));
            //上
            //motion.Add(4, new Rectangle(64 * 0, 64 * 1, 64, 64));
            //motion.Add(5, new Rectangle(64 * 1, 64 * 1, 64, 64));
            //motion.Add(6, new Rectangle(64 * 2, 64 * 1, 64, 64));
            //motion.Add(7, new Rectangle(64 * 3, 64 * 1, 64, 64));
            //右
            //motion.Add(8, new Rectangle(64 * 0, 64 * 2, 64, 64));
            //motion.Add(9, new Rectangle(64 * 1, 64 * 2, 64, 64));
            //motion.Add(10, new Rectangle(64 * 2, 64 * 2, 64, 64));
            //motion.Add(11, new Rectangle(64 * 3, 64 * 2, 64, 64));
            //左
            //motion.Add(12, new Rectangle(64 * 0, 64 * 3, 64, 64));
            //motion.Add(13, new Rectangle(64 * 1, 64 * 3, 64, 64));
            //motion.Add(14, new Rectangle(64 * 2, 64 * 3, 64, 64));
            //motion.Add(15, new Rectangle(64 * 3, 64 * 3, 64, 64));

            for(int i = 0; i < 16;  i++)
            {
                motion.Add(i, new Rectangle(64 * (i%4), 64 * (i/4), 64, 64));
            }
            motion.Initialize(new Range(0, 15), new CountDownTimer(0.2f));

            direction = Direction.DOWN;
            directionRange = new Dictionary<Direction, Range>()
            {
                {Direction.DOWN, new Range(0,3) },
                {Direction.UP, new Range(4,7) },
                {Direction.RIGHT, new Range(8,11) },
                {Direction.LEFT, new Range(12,15) },
            };
        }

        /// <summary>
        /// 更新処理
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            Vector2 velocity = Vector2.Zero;

            //移動処理
            float speed = 15.0f;
            position = position + Input.Velocity() * speed;

            //当たり判定
            var min = Vector2.Zero;
            var max = new Vector2(Screen.Width - 64, Screen.Height - 64);
            position = Vector2.Clamp(position, min, max);

            UpdateMotion();
            motion.Update(gameTime);

            if (Input.GetKeyTrigger(Keys.Z))
            {
                if (velocity.Length() <= 0)
                {
                    Dictionary<Direction, Vector2> velocityDict = new Dictionary<Direction, Vector2>()
                    {
                        {Direction.LEFT, new Vector2(-1,0)},
                        {Direction.RIGHT, new Vector2(1,0)},
                        {Direction.UP, new Vector2(0,-1)},
                        {Direction.DOWN, new Vector2(0,1)}
                    };
                    velocity = velocityDict[direction];
                }
                mediator.AddActor(new PlayerBullet(position, mediator, velocity));
            }
        }
        
        /// <summary>
        /// 終了処理
        /// </summary>
        public override void Shutdown()
        {

        }

        public override void Hit(Character other)
        {
            sound.PlaySE("gameplayse");
        }

        public override void Draw(Renderer renderer)
        {
            renderer.DrawTexture(name, position, motion.DrawingRange());
        }

        private void ChangeMotion(Direction direction)
        {
            this.direction = direction;
            motion.Initialize(directionRange[direction], new CountDownTimer(0.2f));
        }

        private void UpdateMotion()
        {
            Vector2 velocity = Input.Velocity();

            if(velocity.Length() <= 0.0f)
            {
                return;
            }

            if((velocity.Y > 0.0f) && (direction != Direction.DOWN))
            {
                ChangeMotion(Direction.DOWN);
            }
            else if((velocity.Y < 0.0f) && (direction != Direction.UP))
            {
                ChangeMotion(Direction.UP);
            }
            else if ((velocity.X > 0.0f) && (direction != Direction.RIGHT))
            {
                ChangeMotion(Direction.RIGHT);
            }
            else if ((velocity.X < 0.0f) && (direction != Direction.LEFT))
            {
                ChangeMotion(Direction.LEFT);
            }
        }

    }
}
