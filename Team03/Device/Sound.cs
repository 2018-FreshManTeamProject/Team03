using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;//MP3
using Microsoft.Xna.Framework.Audio;//WAV
using System.Diagnostics;//Assert

namespace Oikake.Device
{
    class Sound
    {
        #region　フィールドとコンストラクタ
        private ContentManager contentManager;
        private Dictionary<string, Song> bgms;
        private Dictionary<string, SoundEffect> soundEffects;
        private Dictionary<string, SoundEffectInstance> seInstances;
        private Dictionary<string, SoundEffectInstance> sePlayDict;

        private string currentBGM;

        public Sound(ContentManager content)
        {
            contentManager = content;
            MediaPlayer.IsRepeating = true;

            bgms = new Dictionary<string, Song>();
            soundEffects = new Dictionary<string, SoundEffect>();
            seInstances = new Dictionary<string, SoundEffectInstance>();
            sePlayDict = new Dictionary<string, SoundEffectInstance>();

            currentBGM = null;
        }

        public void Unload()
        {
            bgms.Clear();
            soundEffects.Clear();
            seInstances.Clear();
            sePlayDict.Clear();
        }

        #endregion　フィールドとコンストラクタ
        private string ErrorMessage(string name)
        {
            return "再生する音データのアセット名(" + name +")がありません"　+ "アセット名の確認、Dictionaryに登録しているか確認してください";
        }

        #region BGM
        public void LoadBGM(string name, string filepath = "./")
        {
            if(bgms.ContainsKey(name))
            {
                return;
            }
            bgms.Add(name, contentManager.Load<Song>(filepath + name));
        }
        
        public bool IsStoppedBGM()
        {
            return (MediaPlayer.State == MediaState.Playing);
        } 

        public bool IsPlayingBGM()
        {
            return (MediaPlayer.State == MediaState.Playing);
        }

        public bool IsPausedBGM()
        {
            return (MediaPlayer.State == MediaState.Playing);
        }

        public void StopBGM()
        {
            MediaPlayer.Stop();
            currentBGM = null;
        }

        public void PlayBGM(string name)
        {
            Debug.Assert(bgms.ContainsKey(name),ErrorMessage(name));

            if(currentBGM == name)
            {
                return;
            }

            if(IsPlayingBGM())
            {
                StopBGM();
            }

            MediaPlayer.Volume = 0.5f;
            currentBGM = name;
            MediaPlayer.Play(bgms[currentBGM]);
        }

        public void ResumeBGM()
        {
            if (IsPausedBGM())
            {
                MediaPlayer.Resume();
            }
        }

        public void ReSumeBGM()
        {
            if(IsPausedBGM())
            {
                MediaPlayer.Resume();
            }
        }

        public void ChangeBGMLoopFlag(bool loopFlag)
        {
            MediaPlayer.IsRepeating = loopFlag;
        }

        #endregion

        #region WAV
        public void LoadSE(string name, string filepath = "./")
        {
            if(soundEffects.ContainsKey(name))
            {
                return;
            }
            soundEffects.Add(name, contentManager.Load<SoundEffect>(filepath + name));
        }

        public void PlaySE(string name)
        {
            Debug.Assert(soundEffects.ContainsKey(name), ErrorMessage(name));
            soundEffects[name].Play();
        }
        #endregion

        #region WAVインスタンス

        public void CreateSEInstance(string name)
        {
            if (seInstances.ContainsKey(name))
            {
                return;
            }
            Debug.Assert(soundEffects.ContainsKey(name),"先に" + name + "の読み込み処理を行って下さい");
            seInstances.Add(name, soundEffects[name].CreateInstance());
        }

        public void PlaySEInstances(string name, int no, bool loopFlag = false)
        {
            Debug.Assert(seInstances.ContainsKey(name), ErrorMessage(name));

            if (sePlayDict.ContainsKey(name + no))
            {
                return;
            }
            var date = seInstances[name];
            date.IsLooped = loopFlag;
            date.Play();
            sePlayDict.Add(name + no, date);
        }

        public void StoppedSE(string name,int no)
        {
            if(sePlayDict.ContainsKey(name + no) == false)
            {
                return;
            }
            if(sePlayDict[name + no].State == SoundState.Playing)
            {
                sePlayDict[name + no].Stop();
            }
        }

        public void StoppedSE()
        {
            foreach(var se in sePlayDict)
            {
                if(se.Value.State == SoundState.Playing)
                {
                    se.Value.Stop();
                }
            }
        }

        public void Remove(string name, int no)
        {
            if(sePlayDict.ContainsKey(name + no) == false)
            {
                return;
            }
            sePlayDict.Remove(name + no);
        }

        public void RemoveSE()
        {
            sePlayDict.Clear();
        }

        public void PauseSE(string name,int no)
        {
            if(sePlayDict.ContainsKey(name + no))
            {
                return;
            }
            if (sePlayDict[name + no].State == SoundState.Playing)
            {
                sePlayDict[name + no].Pause();
            }
        }

        public void PauseSE()
        {
            foreach(var se in sePlayDict)
            {
                if (se.Value.State == SoundState.Playing)
                {
                    se.Value.Pause();
                }
            }
        }

        public void Resume(string name , int no)
        {
            if(sePlayDict.ContainsKey(name + no) == false)
            {
                return;
            }

            if(sePlayDict[name + no].State ==SoundState.Paused)
            {
                sePlayDict[name + no].Resume();
            }

        }

        public void ResumeSE()
        {
            foreach(var se in sePlayDict)
            {
                if (se.Value.State == SoundState.Paused)
                {
                    se.Value.Resume();
                }
            }
        }

        public bool IsPlayingSEInstance(string name , int no)
        {
            return sePlayDict[name + no].State == SoundState.Playing;
        }

        public bool IsStoppedSEInstance(string name, int no)
        {
            return sePlayDict[name + no].State == SoundState.Stopped;
        }

        public bool IsPausedSEInstance(string name, int no)
        {
            return sePlayDict[name + no].State == SoundState.Paused;
        }

        #endregion
    }
}
