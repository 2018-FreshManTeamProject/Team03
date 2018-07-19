using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team03.Actor.Effects
{
    interface IParticleMediator
    {
        Particle generate(string name);
    }
}
