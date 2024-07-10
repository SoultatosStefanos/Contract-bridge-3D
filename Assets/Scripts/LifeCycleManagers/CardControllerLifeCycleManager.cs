using System;
using ContractBridge.Core;
using Controllers;
using Events;
using Registries;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;
using Debug = System.Diagnostics.Debug;

namespace LifeCycleManagers
{
    public class CardControllerLifeCycleManager : MonoBehaviour
    {
        [FormerlySerializedAs("Player Seat")]
        [SerializeField]
        private Seat playerSeat;

        [Inject]
        private ICardGameObjectRegistry _cardGameObjectRegistry;

        [Inject]
        private IDeck _deck;

        [Inject]
        private IEventBus _eventBus;

        [Inject]
        private ISession _session;

        private void OnEnable()
        {
            _eventBus.On<SessionPhaseChangedEvent>(HandleSessionPhaseChangedEvent);
            _eventBus.On<GameFollowEvent>(HandleGameFollowEvent);
        }

        private void OnDisable()
        {
            _eventBus.Off<SessionPhaseChangedEvent>(HandleSessionPhaseChangedEvent);
            _eventBus.Off<GameFollowEvent>(HandleGameFollowEvent);
        }

        private void HandleSessionPhaseChangedEvent(SessionPhaseChangedEvent evt)
        {
            switch (evt.Phase)
            {
                case Phase.Auction:
                    HandleAuctionTransition();
                    break;

                case Phase.Play:
                    HandleGameTransition();
                    break;

                case Phase.Setup:
                case Phase.Scoring:
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void HandleAuctionTransition()
        {
            foreach (var card in _deck)
            {
                var cardGameObject = _cardGameObjectRegistry.GetGameObject(card);

                var cardPopUpComponent = cardGameObject.GetComponent<CardPopUpController>();
                cardPopUpComponent.enabled = true;
            }
        }

        private void HandleGameTransition()
        {
            Debug.Assert(_session.Auction != null, "_session.Auction != null");
            Debug.Assert(_session.Auction.FinalContract != null, "_session.Auction.FinalContract != null");

            var isPlayerDummy = playerSeat == _session.Auction.FinalContract.Dummy();

            foreach (var card in _deck)
            {
                var cardGameObject = _cardGameObjectRegistry.GetGameObject(card);

                var cardFollowComponent = cardGameObject.GetComponent<CardFollowController>();
                cardFollowComponent.enabled = true;

                if (isPlayerDummy)
                {
                    var cardPopUpComponent = cardGameObject.GetComponent<CardPopUpController>();
                    cardPopUpComponent.enabled = false;
                }
            }
        }

        private void HandleGameFollowEvent(GameFollowEvent e)
        {
            var cardGameObject = _cardGameObjectRegistry.GetGameObject(e.Card);

            var cardPopUpComponent = cardGameObject.GetComponent<CardPopUpController>();
            cardPopUpComponent.enabled = false;

            var cardFollowComponent = cardGameObject.GetComponent<CardFollowController>();
            cardFollowComponent.enabled = false;
        }
    }
}