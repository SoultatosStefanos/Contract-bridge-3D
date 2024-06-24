using System;
using System.Collections.Generic;
using ContractBridge.Core;
using Domain.Events;
using Events;
using Zenject;

namespace Domain.Decorators
{
    public class PairEventDecorator : IPair
    {
        private readonly IPair _pair;

        [Inject]
        private IEventBus _eventBus;

        public PairEventDecorator(IPair pair)
        {
            _pair = pair;

            _pair.TrickWon += OnTrickWon;
            _pair.Scored += OnScored;
        }

        public void WinTrick(ITrick trick)
        {
            _pair.WinTrick(trick);
        }

        public int Score
        {
            get => _pair.Score;
            set => _pair.Score = value;
        }

        public Partnership Partnership => _pair.Partnership;

        public IEnumerable<ITrick> AllTricksWon => _pair.AllTricksWon;

        public event EventHandler<IPair.TrickEventArgs> TrickWon
        {
            add => _pair.TrickWon += value;
            remove => _pair.TrickWon -= value;
        }

        public event EventHandler<IPair.ScoreEventArgs> Scored
        {
            add => _pair.Scored += value;
            remove => _pair.Scored -= value;
        }

        private void OnScored(object sender, IPair.ScoreEventArgs e)
        {
            _eventBus.Post(new PairScoreEvent(this, e.Score));
        }

        private void OnTrickWon(object sender, IPair.TrickEventArgs e)
        {
            _eventBus.Post(new PairTrickWonEvent(this, e.Trick));
        }
    }
}