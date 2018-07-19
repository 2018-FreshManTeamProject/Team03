using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Team03.Def;
using Team03.Device;

namespace Team03.Actor.Effects
{
    class Particle
    {
        protected readonly float GRAVITY = 0.5f;
        protected string name;
        protected bool isDeadFlag;
        protected Vector2 position;
        protected Vector2 velocity;
        protected IParticleMediator mediator;

        public Particle(string name, Vector2 position, Vector2 velocity, IParticleMediator mediator)
        {
            this.name = name;
            this.position = position;
            this.velocity = velocity;
            this.mediator = mediator;
            isDeadFlag = false;

        }

        public Particle(IParticleMediator mediator) : this("particle", Vector2.Zero,Vector2.Zero,mediator)
        {
            isDeadFlag = false; 
        }
        public void SetTexture(string name)
        {
            this.name = name;
        }

        public void SetPosition(Vector2 position)
        {
            this.position = position;
        }

        public void SetVelocity(Vector2 velocity)
        {
            this.velocity = velocity;
        }

        public string GetTexture()
        {
            return name;
        }

        public Vector2 GetPosition()
        {
            return position;
        }
        public Vector2 GetVelocity()
        {
            return velocity;
        }

        public virtual void Update(GameTime gameTime)
        {
            position += velocity;
            velocity.Y += GRAVITY;
            isDeadFlag = (position.Y > Screen.Height);
        }

        public virtual void Draw(Renderer renderer)
        {
            renderer.DrawTexture(name, position);
        }

        public bool IsDead()
        {
            return isDeadFlag;
        }
    }
}
