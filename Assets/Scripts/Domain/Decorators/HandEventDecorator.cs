using System;
using System.Collections;
using System.Collections.Generic;
using ContractBridge.Core;
using Domain.Events;
using Events;
using Zenject;

namespace Domain.Decorators
{
    public class HandEventDecorator : IHand
    {
        private readonly IHand _hand;

        [Inject]
        private IEventBus _eventBus;

        public HandEventDecorator(IHand hand)
        {
            _hand = hand;

            _hand.Added += OnAdded;
            _hand.Removed += OnRemoved;
            _hand.Cleared += OnCleared;
            _hand.Emptied += OnEmptied;
        }

        public int Count => _hand.Count;

        public ICard this[int index] => _hand[index];

        public ICard this[Rank rank, Suit suit] => _hand[rank, suit];

        public IEnumerator<ICard> GetEnumerator()
        {
            return _hand.GetEnumerator();
        }

        public bool IsEmpty()
        {
            return _hand.IsEmpty();
        }

        public bool Contains(ICard card)
        {
            return _hand.Contains(card);
        }

        public string ToPbn()
        {
            return _hand.ToPbn();
        }

        public void Add(ICard card)
        {
            _hand.Add(card);
        }

        public void Remove(ICard card)
        {
            _hand.Remove(card);
        }

        public void Clear()
        {
            _hand.Clear();
        }

        public event EventHandler<IHand.AddEventArgs> Added
        {
            add => _hand.Added += value;
            remove => _hand.Added -= value;
        }

        public event EventHandler<IHand.RemoveEventArgs> Removed
        {
            add => _hand.Removed += value;
            remove => _hand.Removed -= value;
        }

        public event EventHandler Cleared
        {
            add => _hand.Cleared += value;
            remove => _hand.Cleared -= value;
        }

        public event EventHandler Emptied
        {
            add => _hand.Emptied += value;
            remove => _hand.Emptied -= value;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private void OnEmptied(object sender, EventArgs e)
        {
            _eventBus.Post(new HandEmptiedEvent(this));
        }

        private void OnCleared(object sender, EventArgs e)
        {
            _eventBus.Post(new HandClearEvent(this));
        }

        private void OnRemoved(object sender, IHand.RemoveEventArgs e)
        {
            _eventBus.Post(new HandCardRemoveEvent(this, e.Card));
        }

        private void OnAdded(object sender, IHand.AddEventArgs e)
        {
            _eventBus.Post(new HandCardAddEvent(this, e.Card));
        }
    }
}