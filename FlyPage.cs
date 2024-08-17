using Photon.Pun;
using System.Text;
using UnityEngine;

namespace BananaOS.Pages
{
    public class FlyPage : WatchPage
    {
        //What will be shown on the main menu if DisplayOnMainMenu is set to true
        public override string Title => "Fly";

        //Enabling will display your page on the main menu if you're nesting pages you should set this to false
        public override bool DisplayOnMainMenu => true;

        //This method will be ran after the watch is completely setup
        public override void OnPostModSetup()
        {
            //max selection index so the indicator stays on the screen
            selectionHandler.maxIndex = 1;
        }

        public bool Fly { get; private set; }

        //What you return is what is drawn to the watch screen the screen will be updated everytime you press a button
        public override string OnGetScreenContent()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("<color=yellow>==</color> Dean Fly <color=yellow>==</color>");
            stringBuilder.AppendLine(selectionHandler.GetOriginalBananaOSSelectionText(0, "Enable"));
            stringBuilder.AppendLine(selectionHandler.GetOriginalBananaOSSelectionText(1, "Disable"));
            stringBuilder.AppendLine(selectionHandler.GetOriginalBananaOSSelectionText(2, ""));
            stringBuilder.AppendLine(selectionHandler.GetOriginalBananaOSSelectionText(3, "Made by Dean"));
            stringBuilder.AppendLine(selectionHandler.GetOriginalBananaOSSelectionText(4, "Help From FireGiraffe"));
            return stringBuilder.ToString();
        }

        public void Update()
        {
            if (Fly == true) 
            {
                if (PhotonNetwork.CurrentRoom.CustomProperties["gameMode"].ToString().Contains("MODDED_"))
                {
                    if (ControllerInputPoller.instance.rightControllerPrimaryButton)
                    {
                        GorillaLocomotion.Player.Instance.GetComponent<Rigidbody>().velocity = GorillaLocomotion.Player.Instance.headCollider.transform.forward * Time.deltaTime * 1300f;
                    }
                }
            }
        }

        public override void OnButtonPressed(WatchButtonType buttonType)
        {
            switch (buttonType)
            {
                case WatchButtonType.Up:
                    selectionHandler.MoveSelectionUp();
                    break;

                case WatchButtonType.Down:
                    selectionHandler.MoveSelectionDown();
                    break;

                case WatchButtonType.Enter:
                    if (selectionHandler.currentIndex == 0)
                    {
                        Fly = true;
                    }
                    if (selectionHandler.currentIndex == 1)
                    {
                        Fly = false;
                    }
                    return;

                //It is recommended that you keep this unless you're nesting pages if so you should use the SwitchToPage method
                case WatchButtonType.Back:
                    ReturnToMainMenu();
                    break;
            }
        }
    }
}
