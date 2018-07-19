using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Oikake.Device;
using Oikake.Actor;
using Oikake.Util;

namespace Oikake.Scene
{
    class GamePlay : IScene,IGameMediator
    {
        //private Player player;
        //private List<Character> characters;
        private CharacterManager characterManager;

        private Timer timer;
        private TimerUI timerUI;
        private Score score;
        private Sound sound;

        private bool isEndFlag;
        public GamePlay()
        {
            isEndFlag = false;
            var gameDevice = GameDevice.Instance();
            sound = gameDevice.GetSound();
        }

        public void Draw(Renderer renderer)
        {
            renderer.Begin();

            renderer.DrawTexture("stage", Vector2.Zero);
            timerUI.Draw(renderer);
            score.Draw(renderer);

            //characters.ForEach(c => c.Draw(renderer));

            //player.Draw(renderer);
            characterManager.Draw(renderer);
          
            //if (timer.IsTime())
            //{
            //renderer.DrawTexture("ending", new Vector2(150, 150));
            //}

            renderer.End();
        }

        public void Initialize()
        {
            isEndFlag = false;
            characterManager = new CharacterManager();
            //characterManager.Add(new Player(this));
            Player player = new Player(this);
            characterManager.Add(player);

            characterManager.Add(new Enemy(this,new AttackAI(player)));
            characterManager.Add(new Enemy(this,new BoundAI()));
            for(int  i= 0; i<10; i++)
            {
                characterManager.Add(new Enemy(this,new RandomAI()));
            }

            //プレイヤーの実体生成
            //player = new Player();
            ////プレイヤーの初期化
            //player.Initialize();

            ////Listの実体生成
            //characters = new List<Character>();
            //characters.Add(new Enemy());
            ////BoundEnemyを追加
            //characters.Add(new BoundEnemy());

            //for (int i = 0; i < 10; i++)
            //{
            //    characters.Add(new RandomEnemy());
            //}

            //foreach (var c in characters)
            //{
            //    c.Initialize();
            //}

            timer = new CountDownTimer(20);
            timerUI = new TimerUI(timer);

            score = new Score();
            
        }

        public bool IsEnd()
        {
            return isEndFlag;
        }

        public Scene Next()
        {
            var nextScore = Scene.Ending;
            if(score.GetScore() >= 100)
            {
                nextScore = Scene.GoodEnding;
            }
            return nextScore;
        }

        public void ShutDown()
        {
            sound.StopBGM();
        }

        public void Update(GameTime gameTime)
        {
            sound.PlayBGM("gameplaybgm");
            timer.Update(gameTime);
            score.Update(gameTime);
            characterManager.Update(gameTime);
            //キャラクターの一括更新
            //characters.ForEach(c => c.Update(gameTime));

            //player.Update(gameTime);

            ////衝突判定
            ////敵キャラすべてと判定
            //foreach (var c in characters)
            //{
            //    int num = 0;
            //    if (c is RandomEnemy)
            //    {
            //        num = 10;
            //    }
            //    if (c is BoundEnemy)
            //    {
            //        num = 200;
            //    }
            //    //cとプレイヤーが衝突してたら
            //    if (player.IsCollision(c))
            //    {
            //        //cを初期化(ランダムで位置を決定)
            //        c.Initialize();

            //        //時間切れでないとき
            //        if (!timer.IsTime())
            //        {
            //            score.Add(num);
            //        }
            //    }
            //}

            if (timer.IsTime())
            {
                isEndFlag = true;
                score.ShutDown();
            }
        }

        public void AddActor(Character character)
        {
            characterManager.Add(character);
        }

        public void AddScore()
        {
            score.Add();
        }

        public void AddScore(int num)
        {
            score.Add(num);
        }
    }
}
