using ContractBridge.Core;
using UnityEngine;

namespace Registries
{
    public interface ICardGameObjectRegistry
    {
        void Register(ICard card, GameObject cardObject);

        GameObject GetGameObject(ICard card);
    }
}