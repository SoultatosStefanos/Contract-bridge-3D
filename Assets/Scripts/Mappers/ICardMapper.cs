using Makaretu.Bridge;
using UnityEngine;

namespace Mappers
{
    public interface ICardMapper
    {
        void MapGameObject(Card card, GameObject cardObject);

        GameObject GetGameObject(Card card);
    }
}