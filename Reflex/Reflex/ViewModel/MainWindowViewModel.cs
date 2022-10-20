using Reflex.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace Reflex.ViewModels
{
    class MainWindowViewModel:BindableBase
    {
        private ICommand _ScoreCommand;
        public ICommand ScoreCommand
        {
            get
            {
                return _ScoreCommand;
            }
            set
            {

                _ScoreCommand = value;
            }
        }
        public ReflexGame Game;
        public int Score
        {
            get { return Game.Score; }
            set
            {
                Game.Score = value;
            }
        }
        MainWindowViewModel()
        {
            Game = new ReflexGame();

        }
        private Thickness _Position = new Thickness( 0, 0, 0, 0 );

        public Thickness Position
        {
            get { return _Position; }
            set { _Position = value;
                OnPropertyChanged("Position");
            }
        }
        private void AddScore(string Score)
        {
            Debug.WriteLine("Clicked!");
            Game.AddScore();
        }
    }
}
