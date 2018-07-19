using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Oikake.Device;
using Oikake.Scene;

namespace Oikake.Actor
{
    /// <summary>
    /// 抽象キャラクタークラス
    /// </summary>
    abstract class Character
    {
        //親と子クラスだけの共通部分はprotected
        protected Vector2 position;//位置
        protected string name;//画像の名前
        protected bool isDeadFlag;//死亡フラグ
        protected IGameMediator mediator;
        protected enum State
        {
            Preparation,
            Alive,
            Dying,
            Dead
        };

        public Character (string name,IGameMediator mediator)
        {
            this.name = name;
            position = Vector2.Zero;
            isDeadFlag = false;
            this.mediator = mediator;
        }

        //抽象メソッド(子クラスで必ず再定義しなけらばならないメソッド
        public abstract void Initialize();//抽象初期化メソッド

        public abstract void Update(GameTime gameTime);//抽象更新メソッド

        public abstract void Shutdown();//抽象終了メソッド

        public abstract void Hit(Character other);

        /// <summary>
        /// 死んでいるか
        /// </summary>
        /// <returns>死んでいたらtrue</returns>
        public bool IsDead()
        {
            return isDeadFlag;
        }

        /// <summary>
        /// 描画
        /// </summary>
        /// <param name="renderer">描画オブジェクト</param>
        public virtual void Draw( Renderer renderer)
        {
            renderer.DrawTexture(name, position);
        }

        /// <summary>
        /// 衝突判定(2点間の距離と円の半径)
        /// </summary>
        /// <param name="other">他のキャラクター</param>
        /// <returns>当たっていればture</returns>
        public bool IsCollision( Character other)
        {
            //自分と相手の位置の長さを計算(2点間の距離)
            float length = (position - other.position).Length();

            //自分の半径と相手の半径の和
            float randiusSum = 32f + 32f;
            //距離<=半径の和の時
            if( length <= randiusSum )
            {
                return true;
            }
            return false;
        }

        public void SetPosition(ref Vector2 other)
        {
            other = position;
        }
    }
}
