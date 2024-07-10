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
            var dummySeat = DummySeat();

            foreach (var card in _deck)
            {
                var cardGameObject = _cardGameObjectRegistry.GetGameObject(card);

                var inPlayerHand = _board.Hand(playerSeat).Contains(card);

                if (inPlayerHand)
                {
                    var isPlayerNotDummy = dummySeat != playerSeat;

                    var cardPopUpController = cardGameObject.GetComponent<CardPopUpController>();
                    cardPopUpController.enabled = isPlayerNotDummy;

                    var cardFollowController = cardGameObject.GetComponent<CardFollowController>();
                    cardFollowController.enabled = isPlayerNotDummy;
                }
                else
                {
                    var inPartnerHand = _board.Hand(playerSeat.Partner()).Contains(card);

                    if (inPartnerHand)
                    {
                        var isPartnerDummy = dummySeat == playerSeat.Partner();

                        var cardPopUpController = cardGameObject.GetComponent<CardPopUpController>();
                        cardPopUpController.enabled = isPartnerDummy;

                        var cardFollowController = cardGameObject.GetComponent<CardFollowController>();
                        cardFollowController.enabled = isPartnerDummy;
                    }
                    else
                    {
                        var cardPopUpController = cardGameObject.GetComponent<CardPopUpController>();
                        cardPopUpController.enabled = false;

                        var cardFollowController = cardGameObject.GetComponent<CardFollowController>();
                        cardFollowController.enabled = false;
                    }
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

        private Seat DummySeat()
        {
            Debug.Assert(_session.Auction != null, "_session.Auction != null");
            Debug.Assert(_session.Auction.FinalContract != null, "_session.Auction.FinalContract != null");
            return _session.Auction.FinalContract.Dummy();
        }
    }
}