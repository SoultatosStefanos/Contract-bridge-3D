using System;
using ContractBridge.Core;
using Controllers;
using Events;
using Registries;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace LifeCycleManagers
{
    public class CardControllerLifeCycleManager : MonoBehaviour
    {
        [FormerlySerializedAs("Player Seat")]
        [SerializeField]
        private Seat playerSeat;

        [Inject]
        private IBoard _board;

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

                var cardPopUpController = cardGameObject.GetComponent<CardPopUpController>();
                cardPopUpController.enabled = true;
            }
        }

        private void HandleGameTransition()
        {
            foreach (var card in _deck)
            {
                var cardGameObject = _cardGameObjectRegistry.GetGameObject(card);

                var inPlayerHand = _board.Hand(playerSeat).Contains(card);

                if (inPlayerHand)
                {
                    // TODO Conditionally enable follow controller

                    var cardFollowController = cardGameObject.GetComponent<CardFollowController>();
                    cardFollowController.enabled = true;

                    if (DummySeat() is not { } dummySeat)
                    {
                        continue;
                    }

                    var cardPopUpController = cardGameObject.GetComponent<CardPopUpController>();
                    cardPopUpController.enabled = dummySeat != playerSeat;
                }
                else
                {
                    // TODO Conditionally enable follow controller

                    if (DummySeat() is not { } dummySeat)
                    {
                        continue;
                    }

                    var cardPopUpController = cardGameObject.GetComponent<CardPopUpController>();
                    cardPopUpController.enabled = dummySeat == playerSeat.Partner();
                }
            }
        }

        private void HandleGameFollowEvent(GameFollowEvent e)
        {
            var cardGameObject = _cardGameObjectRegistry.GetGameObject(e.Card);

            var cardPopUpController = cardGameObject.GetComponent<CardPopUpController>();
            cardPopUpController.enabled = false;

            var cardFollowController = cardGameObject.GetComponent<CardFollowController>();
            cardFollowController.enabled = false;
        }

        private Seat? DummySeat()
        {
            return _session.Auction?.FinalContract?.Dummy();
        }
    }
}