using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;


namespace Oikake.Util
{
    class CountDownTimer : Timer
    {
        public CountDownTimer() : base()
        {
            Initialize();
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="second">設定する秒数</param>
        public CountDownTimer(float　second) : base(second)
        {
            Initialize();
        }

        /// <summary>
        /// 初期化
        /// </summary>
        public override void Initialize()
        {
            currentTime = limitTime;
        }

        /// <summary>
        /// 時間になったかどうか
        /// </summary>
        /// <returns>時間になっていればtrue</returns>
        public override bool IsTime()
        {
            //0以下になったら設定した時間を超えたのでtrueを返す
            return currentTime <= 0.0f;
        }

        public override float Rate()
        {
            return 1.0f - currentTime / limitTime;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="gameTime">ゲーム時間</param>
        public override void Update(GameTime gameTime)
        {
            //現在の時間を減らす。ただし最小値は0.0
            currentTime = Math.Max(currentTime - 1f, 0.0f);
        }
    }
}
