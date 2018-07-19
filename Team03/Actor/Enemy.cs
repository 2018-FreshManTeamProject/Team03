using System;
using Microsoft.Xna.Framework;
using Team03.Def;
using Team03.Device;
using Team03.Scene;
using Team03.Util;

namespace Team03.Actor
{
    class Enemy :Character
    {
        private AI ai;
        private Random rnd;
        private State state;
        private Timer timer;
        private bool isDisplay;
        private readonly int Impression = 10;
        private int displayCount;
        private Sound sound;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Enemy(IGameMediator mediator,AI ai) :base("black",mediator)
        {
            this.ai = ai;
            var gameDevice = GameDevice.Instance();
            sound = gameDevice.GetSound();
            state = State.Preparation;
        }

        /// <summary>
        /// 初期化メソッド
        /// </summary>
        public override void Initialize()
        {
            //位置を(100,100)に設定
            var gameDevaice = GameDevice.Instance();
            rnd = gameDevaice.GetRandom();
            position = new Vector2(rnd.Next(Screen.Width - 64), rnd.Next(Screen.Height - 64));

            state = State.Preparation;
            timer = new CountDownTimer(0.25f);
            isDisplay = true;
            displayCount = Impression;
        }

        public override void Update(GameTime gameTime)
        {
            switch(state)
            {
                case State.Preparation:
                    PreparationUpdate(gameTime);
                    break;
                case State.Alive:
                    AliveUpdate(gameTime);
                    break;
                case State.Dying:
                    DyingUpdate(gameTime);
                    break;
                case State.Dead:
                    DeadUpdate(gameTime);
                    break;
            }
            position = ai.Think(this);
        }

        /// <summary>
        /// 終了処理
        /// </summary>
        public override void Shutdown()
        {
        }

        public override void Hit(Character character)
        {
            if (state != State.Alive)
            {
                return;
            }
            state = State.Dying;

            int score = 0;
            if (ai is BoundAI)
            {
                score = 100;
            }
            else if (ai is RandomAI)
            {
                score = 50;
            }
            else if (ai is AttackAI)
            {
                score = -50;
                mediator.AddScore(score);
                mediator.AddActor(new Enemy(mediator, ai));
                isDeadFlag = true;
                return;
            }
            mediator.AddScore(score);

            sound.PlaySE("gameplayse");

            AI nextAI = new BoundAI();
            switch (rnd.Next(2))
            {
                case 0:
                    nextAI = new BoundAI();
                    break;
                case 1:
                    nextAI = new RandomAI();
                    break;
            }
            mediator.AddActor(new Enemy(mediator, nextAI));
        }

            //isDeadFlag = true;
            //mediator.AddActor(new BurstEffect(position, mediator));

        public override void Draw(Renderer renderer)
        {
            switch(state)
            {
                case State.Preparation:
                    PreparationDraw(renderer);
                    break;
                case State.Alive:
                    AliveDraw(renderer);
                    break;
                case State.Dying:
                    DyingDraw(renderer);
                    break;
                case State.Dead:
                    DeadDraw(renderer);
                    break;
            }
        }

        private void PreparationUpdate( GameTime gameTime)
        {
            timer.Update(gameTime);
            if(timer.IsTime())
            {
                isDisplay = !isDisplay;
                displayCount -= 1;
                timer.Initialize();
            }
            if(displayCount == 0)
            {
                state = State.Alive;
                timer.Initialize();
                displayCount = Impression;
                isDisplay = true;
            }
        }
        private void PreparationDraw(Renderer renderer)
        {
            if(isDisplay)
            {
                base.Draw(renderer);
            }
        }
        private void AliveUpdate(GameTime gameTime)
        {
            position = ai.Think(this);
        }
        private void AliveDraw(Renderer renderer)
        {
            base.Draw(renderer);
        }
        private void DyingUpdate(GameTime gameTime)
        {
            timer.Update(gameTime);
            if(timer.IsTime())
            {
                displayCount -= 1;
                timer.Initialize();
                isDisplay = !isDisplay;
            }

            if(displayCount == 0)
            {
                state = State.Dead;
            }
        }
        private void DyingDraw(Renderer renderer)
        {
            if(isDisplay)
            {
                renderer.DrawTexture(name, position, Color.Red);
            }
            else
            {
                base.Draw(renderer);
            }
        }
        private void DeadUpdate(GameTime gameTime)
        {
            isDeadFlag = true;
            mediator.AddActor(new BurstEffect(position, mediator));
        }
        private void DeadDraw(Renderer renderer)
        {
        }
    }
}
