using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIConfirmTurn : MonoBehaviour
{
    private Image buttonImage;
    [SerializeField] private GameObject buttonTextObj;

    private void Start() 
    {
        buttonImage = GetComponent<Image>();       

        buttonImage.enabled = false;
        buttonTextObj.SetActive(false);

        GameManager.Instance.TurnManager.NewTurnStarted += TurnOnButton;
    }

    private void TurnOnButton()
    {
         buttonImage.enabled = true;
        buttonTextObj.SetActive(true);
    }

    private void HideButton()
    {
        buttonImage.enabled = false;
        buttonTextObj.SetActive(false);
    }

    public void OnClickConfirmTurn()
    {
         GameManager.Instance.PhotonView.RPC("PlayerLockedTurn",PhotonTargets.All);
         HideButton();
    }
}
