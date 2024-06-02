using Makaretu.Bridge;
using UnityEngine;

public class BoardWrapper : MonoBehaviour
{
    public Board Board { get; private set; }

    private void Start()
    {
        Board = new Board();
    }
}