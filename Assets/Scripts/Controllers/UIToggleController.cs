using System;
using ContractBridge.Core;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Controllers
{
    public class UIToggleController : MonoBehaviour
    {
        [FormerlySerializedAs("Setup Canvas")]
        [SerializeField]
        private GameObject setupCanvas;

        [FormerlySerializedAs("Auction Canvas")]
        [SerializeField]
        private GameObject auctionCanvas;

        [FormerlySerializedAs("Game Canvas")]
        [SerializeField]
        private GameObject gameCanvas;

        [FormerlySerializedAs("Hide / Show Key")]
        [SerializeField]
        private KeyCode hideShowKey = KeyCode.U;

        [Inject]
        private ISession _session;

        private void Update()
        {
            if (!Input.GetKeyDown(hideShowKey))
            {
                return;
            }

            var canvas = GetCanvasByPhase();
            canvas.SetActive(!canvas.activeSelf);
        }

        private GameObject GetCanvasByPhase()
        {
            return _session.Phase switch
            {
                Phase.Setup => setupCanvas,
                Phase.Auction => auctionCanvas,
                Phase.Play => gameCanvas,
                Phase.Scoring => throw new ArgumentOutOfRangeException(), // TODO
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}