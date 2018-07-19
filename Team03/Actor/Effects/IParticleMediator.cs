using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oikake.Actor.Effects
{
    interface IParticleMediator
    {
        Particle generate(string name);
    }
}
