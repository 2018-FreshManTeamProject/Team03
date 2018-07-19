using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

namespace Oikake.Util
{
    abstract class Timer
    {
        //フィールド
        //制限時間
        protected float limitTime;
        //現在の時間
        protected float currentTime;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="second"></param>
        public Timer (float second)
        {
            //60fps×毎秒
            limitTime = 60 * second;
        }

        /// <summary>
        /// デフォルトコンストラクタ
        /// </summary>
        public Timer() : this (1)
        {
        }

        //抽象メソッド
        public abstract void Initialize();

        public abstract void Update(GameTime gameTime);

        public abstract bool IsTime();

        public abstract float Rate();

        /// <summary>
        /// 制限時間を設定
        /// </summary>
        /// <param name="second"></param>
        public void SetTime(float second)
        {
            limitTime = 60 * second;
        }

        /// <summary>
        /// 現在時間を取得
        /// </summary>
        /// <returns>秒</returns>
        public float Now()
        {
            return currentTime / 60f;
        }


    }
}
