using System.Collections.Generic;
using ContractBridge.Core;
using UnityEngine;

namespace Registries.Impl
{
    public class CardGameObjectRegistry : ICardGameObjectRegistry
    {
        private readonly Dictionary<ICard, GameObject> _cardToGameObjectMap = new();

        public void Register(ICard card, GameObject cardObject)
        {
            _cardToGameObjectMap.TryAdd(card, cardObject);
        }

        public GameObject GetGameObject(ICard card)
        {
            return _cardToGameObjectMap.GetValueOrDefault(card);
        }
    }
}