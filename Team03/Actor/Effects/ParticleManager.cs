using Microsoft.Xna.Framework;
using Oikake.Device;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oikake.Actor.Effects
{
    class ParticleManager
    {
        private List<Particle> particles = new List<Particle>();
        private List<Particle> addNewParticles = new List<Particle>();
        public ParticleManager()
        {

        }

        public void Initialaize()
        {
            particles.Clear();
            addNewParticles.Clear();
        }

        public void Update(GameTime gameTime)
        {
            particles.ForEach(particle => particle.Update(gameTime));
            particles.AddRange(addNewParticles);
            addNewParticles.Clear();
            particles.RemoveAll(particle => particle.IsDead());
        }

        public void Shutdown()
        {
        }

        public void Draw(Renderer renderer)
        {
            particles.ForEach(particle => particle.Draw(renderer));
        }

        public void Add(Particle particle)
        {
            addNewParticles.Add(particle);
        }
    }
}
