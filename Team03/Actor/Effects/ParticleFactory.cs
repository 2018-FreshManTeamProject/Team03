using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oikake.Actor.Effects
{
    class ParticleFactory
    {
        private IParticleMediator mediator;

        public ParticleFactory(IParticleMediator mediator)
        {
            this.mediator = mediator;
        }

        public  Particle create(string name)
        {
            Particle particle = null;
            if (name == "Particle")
            {
                particle = new Particle(mediator);
            }
            else if (name == "ParticleBule")
            {
                particle = new ParticaleBule(mediator);
            }
            else if (name == ("ParticleBig"))
            {
                particle = new ParticleBig(mediator);
            }
            else if (name == ("ParticleMiddle"))
            {
                particle = new ParticleMiddle(mediator);
            }
            else if (name == ("ParticleSmall"))
            {
                particle = new ParticleSmall(mediator);
            }
            return particle;
        }

        public Particle create(string name,Vector2 position, Vector2 velocity)
        {
            Particle particle = null;
            if (name == "Particle")
            {
                particle = new Particle(name,position,velocity,mediator);
            }
            else if (name == "ParticleBule")
            {
                particle = new ParticaleBule(name, position,velocity,mediator);
            }
            return particle;
        }
    }
}
