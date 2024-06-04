using Makaretu.Bridge;
using UnityEngine;

public interface ICardManager
{
    void Register(Card card, GameObject cardObject);

    GameObject GetGameObject(Card card);
}