using System;
using System.Collections.Generic;
using System.Text;

namespace Reflex.Model
{
    class ReflexGame:BindableBase
    {
        private int _Score  = 0;
        public int Score { get { return _Score; }
            set { _Score = value;
                OnPropertyChanged("Score");
            }
        }
        private int ElapsedSeconds=0;
        public string Name = "";
        public void AddScore()
        {
            Score++;
        }
        
    }
}
