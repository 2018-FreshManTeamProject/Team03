using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Team03.Device;


namespace Team03.Actor
{
    class CharacterManager
    {
        private List<Character> players;
        private List<Character> enemys;
        private List<Character> addNewCharacter;

        public CharacterManager()
        {
            Initialize();
        }

        public void Initialize()
        {
            if(players != null)
            {
                players.Clear();
            }
            else
            {
                players = new List<Character>();
            }

            if(enemys != null)
            {
                enemys.Clear();
            }
            else
            {
                enemys = new List<Character>();
            }

            if(addNewCharacter != null)
            {
                addNewCharacter.Clear();
            }
            else
            {
                addNewCharacter = new List<Character>();
            }

            
        }
        public void Add(Character character)
        {
            if(character == null)
            {
                return;
            }
            addNewCharacter.Add(character);
        }

        public void HitToCharacter()
        {
            foreach(var player in players)
            {
                foreach(var enemy in enemys)
                {
                    if (player.IsDead() || enemy.IsDead())
                    {
                        continue;
                    }
                    if(player.IsCollision(enemy))
                    {
                        player.Hit(enemy);
                        enemy.Hit(player);
                    }
                }
            }
        }

        private void RemoveDeadCharacter()
        {
            players.RemoveAll(p => p.IsDead());
            enemys.RemoveAll(e => e.IsDead());
        }

        public void Update(GameTime gameTime)
        {
            foreach(var p in players)
            {
                p.Update(gameTime);
            }
            foreach(var e in enemys)
            {
                e.Update(gameTime);
            }

            foreach(var newChara in addNewCharacter)
            {
                if(newChara is Player)
                {
                    newChara.Initialize();
                    players.Add(newChara);
                }
                else if(newChara is PlayerBullet)
                {
                    newChara.Initialize();
                    players.Add(newChara);
                }
                else
                {
                    newChara.Initialize();
                    enemys.Add(newChara);
                }
            }
            addNewCharacter.Clear();

            HitToCharacter();

            RemoveDeadCharacter();
        }

        public void Draw(Renderer renderer)
        {
            foreach(var e in enemys)
            {
                e.Draw(renderer);
            }
            foreach(var p in players)
            {
                p.Draw(renderer);
            }
        }
    }
}
