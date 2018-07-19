using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Oikake.Device;
using Oikake.Actor.Effects;
using Oikake.Util;

namespace Oikake.Scene
{
    class GoodEnding : IScene,IParticleMediator 
    {
        private bool isEndFlag;
        private IScene backGroundScene;
        private Sound sound;
        private ParticleManager particleManager;
        private ParticleFactory particleFactory;
        private Timer timer;

        public GoodEnding(IScene scene)
        {
            isEndFlag = false;
            backGroundScene = scene;
            var gameDevice = GameDevice.Instance();
            sound = gameDevice.GetSound();

            particleManager = new ParticleManager();
            particleFactory = new ParticleFactory(this);
            timer = new CountDownTimer(1f);
        }
        public void Draw(Renderer renderer)
        {
            backGroundScene.Draw(renderer);

            renderer.Begin();
            renderer.DrawTexture("goodending", new Vector2(100,100));
            particleManager.Draw(renderer);
            renderer.End();
        }

        public Particle generate(string name)
        {
            var particle = particleFactory.create(name);
            particleManager.Add(particle);
            return particle;
        }

        public void Initialize()
        {
            isEndFlag = false;
            particleManager.Initialaize();
        }

        public bool IsEnd()
        {
            return isEndFlag;
        }

        public Scene Next()
        {
            return Scene.Title;
        }

        public void ShutDown()
        {
            sound.StopBGM();
        }

        public void Update(GameTime gameTime)
        {
            //sound.PlayBGM("endingbgm");

            if (Input.GetKeyTrigger(Keys.Space))
            {
                isEndFlag = true;
                sound.PlaySE("endingse");
            }

            var random = GameDevice.Instance().GetRandom();
            var angle = MathHelper.ToRadians(random.Next(-100, -80));
            var velocity = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
            velocity *= 20.0f;
            var particle = particleFactory.create("Particle");
            particle.SetPosition(new Vector2(50, 500));
            particle.SetVelocity(velocity);
            particleManager.Add(particle);

            angle = MathHelper.ToRadians(random.Next(-120, -60));
            velocity = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
            velocity *= 15.0f;
            particle = particleFactory.create("ParticleBule");
            particle.SetPosition(new Vector2(650, 500));
            particle.SetVelocity(velocity);
            particleManager.Add(particle);

            timer.Update(gameTime);
            if (timer.IsTime())
            {
                timer.Initialize();
                //for (int i = 0; i < 100; i++)
                //{
                //    angle = MathHelper.ToRadians(random.Next(-180, 180));
                //    velocity = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
                //    velocity *= 10.0f;
                //    particle = particleFactory.create("Particle");
                //    particle.SetPosition(new Vector2(400, 100));
                //    particle.SetVelocity(velocity);
                //    particleManager.Add(particle);
                //}
                //ParticleBigを生成
                generate("ParticleBig");
            }

            particleManager.Update(gameTime);
        }
    }
}
