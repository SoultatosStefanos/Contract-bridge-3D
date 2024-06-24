using System;
using System.Collections.Generic;
using ContractBridge.Core;
using Events;
using Zenject;

namespace Decorators
{
    public class GameEventDecorator : IGame
    {
        private readonly IGame _game;

        [Inject]
        private IEventBus _eventBus;

        public GameEventDecorator(IGame game)
        {
            _game = game;

            _game.LeadChanged += OnLeadChanged;
            _game.TurnChanged += OnTurnChanged;
            _game.Followed += OnFollowed;
            _game.TrickWon += OnTrickWon;
            _game.Done += OnDone;
        }

        public bool CanFollow(ICard card, Seat seat)
        {
            return _game.CanFollow(card, seat);
        }

        public bool CanFollow(Seat seat)
        {
            return _game.CanFollow(seat);
        }

        public void Follow(ICard card, Seat seat)
        {
            _game.Follow(card, seat);
        }

        public IBoard Board => _game.Board;

        public TrumpSuit TrumpSuit
        {
            get => _game.TrumpSuit;
            set => _game.TrumpSuit = value;
        }

        public Seat? FirstLead
        {
            get => _game.FirstLead;
            set => _game.FirstLead = value;
        }

        public Seat? Lead => _game.Lead;

        public Seat? Turn => _game.Turn;

        public IEnumerable<ICard> PlayedCards => _game.PlayedCards;

        public event EventHandler<IGame.LeadEventArgs> LeadChanged
        {
            add => _game.LeadChanged += value;
            remove => _game.LeadChanged -= value;
        }

        public event EventHandler<IGame.TurnEventArgs> TurnChanged
        {
            add => _game.TurnChanged += value;
            remove => _game.TurnChanged -= value;
        }

        public event EventHandler<IGame.FollowEventArgs> Followed
        {
            add => _game.Followed += value;
            remove => _game.Followed -= value;
        }

        public event EventHandler<IGame.TrickEventArgs> TrickWon
        {
            add => _game.TrickWon += value;
            remove => _game.TrickWon -= value;
        }

        public event EventHandler Done
        {
            add => _game.Done += value;
            remove => _game.Done -= value;
        }

        private void OnLeadChanged(object sender, IGame.LeadEventArgs e)
        {
            _eventBus.Post(new GameLeadChangeEvent(this, e.Seat));
        }

        private void OnTurnChanged(object sender, IGame.TurnEventArgs e)
        {
            _eventBus.Post(new GameTurnChangeEvent(this, e.Seat));
        }

        private void OnFollowed(object sender, IGame.FollowEventArgs e)
        {
            _eventBus.Post(new GameFollowEvent(this, e.Card, e.Seat));
        }

        private void OnTrickWon(object sender, IGame.TrickEventArgs e)
        {
            _eventBus.Post(new GameTrickWonEvent(this, e.Trick, e.Seat));
        }

        private void OnDone(object sender, EventArgs args)
        {
            _eventBus.Post(new GameDoneEvent(this));
        }
    }
}