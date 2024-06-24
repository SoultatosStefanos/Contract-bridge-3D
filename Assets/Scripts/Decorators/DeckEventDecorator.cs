using System;
using System.Collections;
using System.Collections.Generic;
using ContractBridge.Core;
using Events;
using Zenject;

namespace Decorators
{
    public class DeckEventDecorator : IDeck
    {
        private readonly IDeck _deck;

        [Inject]
        private IEventBus _eventBus;

        public DeckEventDecorator(IDeck deck)
        {
            _deck = deck;

            _deck.Shuffled += OnShuffled;
            _deck.Dealt += OnDealt;
        }

        public IEnumerator<ICard> GetEnumerator()
        {
            return _deck.GetEnumerator();
        }

        public bool IsEmpty()
        {
            return _deck.IsEmpty();
        }

        public bool Contains(ICard card)
        {
            return _deck.Contains(card);
        }

        public int Count => _deck.Count;

        public ICard this[int index] => _deck[index];

        public ICard this[Rank rank, Suit suit] => _deck[rank, suit];

        public void Shuffle(Random rng)
        {
            _deck.Shuffle(rng);
        }

        public void Deal(IBoard board)
        {
            _deck.Deal(board);
        }

        public IDeck.Partition InitialPartition => _deck.InitialPartition;

        public event EventHandler Shuffled
        {
            add => _deck.Shuffled += value;
            remove => _deck.Shuffled -= value;
        }

        public event EventHandler<IDeck.DealEventArgs> Dealt
        {
            add => _deck.Dealt += value;
            remove => _deck.Dealt -= value;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private void OnShuffled(object sender, EventArgs e)
        {
            _eventBus.Post(new DeckShuffleEvent(this));
        }

        private void OnDealt(object sender, IDeck.DealEventArgs e)
        {
            _eventBus.Post(new DeckDealEvent(this, e.Board));
        }
    }
}