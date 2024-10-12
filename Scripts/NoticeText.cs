using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class NoticeText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;

    void Start()
    {
        HideText();
    }

    public void ShowText() {
        text.gameObject.SetActive(true);
    }

    public void HideText() {
        text.gameObject.SetActive(false);
    }
}
