using Makaretu.Bridge;
using UnityEngine;

public class HelloBridge : MonoBehaviour
{
    private void Start()
    {
        var bidding = new Auction(Seat.North)
        {
            new Bid(1, Denomination.Clubs),
            Bid.Pass,
            Bid.Pass,
            Bid.Pass
        };

        Debug.Assert("1C by North" == bidding.FinalContract().ToString());

        Debug.Log("Hello, Contract bridge 3D!");
    }
}