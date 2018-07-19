﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Team03.Device;


namespace Team03.Scene
{
    interface IScene
    {
        /// <summary>
        /// シーンインタフェース
        /// </summary>
        void Initialize();
        void Update(GameTime gameTime);
        void Draw(Renderer renderer);
        void ShutDown();

        //シーン管理用
        bool IsEnd();
        Scene Next();
    }
}
