using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Oikake.Util
{
    class CountUpTimer : Timer
    {
        public CountUpTimer() : base()
        {
            Initialize();
        }

        public override void Initialize() 
        {
            currentTime = 0.0f;
        }

        public override bool IsTime()
        {
            //制限時間を超えたら時間になっている
            return currentTime >= limitTime;
        }

        public override float Rate()
        {
            return currentTime / limitTime;
        }

        public override void Update(GameTime gameTime)
        {
            //現在の時間を増やす。最大値は制限時間
            currentTime = Math.Min(currentTime + 1, limitTime);
        }
    }
}
