using Makaretu.Bridge;
using UnityEngine;
using UnityEngine.Serialization;

public class SeatWrapper : MonoBehaviour
{
    [FormerlySerializedAs("Seat")]
    [SerializeField]
    private Seat seat;

    public Seat Seat => seat;
}