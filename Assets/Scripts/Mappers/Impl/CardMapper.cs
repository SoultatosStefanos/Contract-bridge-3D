using System.Collections.Generic;
using Makaretu.Bridge;
using UnityEngine;

namespace Mappers.Impl
{
    public class CardMapper : ICardMapper
    {
        private readonly Dictionary<Card, GameObject> _cardToGameObjectMap = new Dictionary<Card, GameObject>();

        public void MapGameObject(Card card, GameObject cardObject)
        {
            if (_cardToGameObjectMap.ContainsKey(card)) return;

            _cardToGameObjectMap[card] = cardObject;

            Debug.Log($"Registered card: {card} with game object: {cardObject}");
        }

        public GameObject GetGameObject(Card card)
        {
            return _cardToGameObjectMap.TryGetValue(card, out var obj) ? obj : null;
        }
    }
}