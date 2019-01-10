using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIMatchMessage : MonoBehaviour
{
    private Animator matchMessageAnimator;

    [Header("UI Elements")]
    [SerializeField] private GameObject matchMessageHolderObj;
    [SerializeField] private TMP_Text matchMessageText;

    // Start is called before the first frame update
    void Start()
    {
        matchMessageAnimator = GetComponent<Animator>();
        matchMessageHolderObj.SetActive(false);        
    }

    public  void ShowMatchMessage(string matchMessage)
    {
        matchMessageHolderObj.SetActive(true);        
        matchMessageText.text = matchMessage;
        matchMessageAnimator.SetTrigger("ShowMessage");
        StartCoroutine(ShowMessageCooldown());
    }

    IEnumerator ShowMessageCooldown()
    {
        yield return new WaitForSeconds(3.0f);
        matchMessageAnimator.SetTrigger("HideMessage");
        yield return new WaitForSeconds(0.75f);
          matchMessageHolderObj.SetActive(false);        
    }
}
