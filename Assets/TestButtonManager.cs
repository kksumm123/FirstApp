using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TestButtonManager : MonoBehaviour
{
    public Button baseButton;
    void Start()
    {
        InterstitialAdExample interstitialAdExample = FindObjectOfType<InterstitialAdExample>();
        AddButton("���� ���� �ε�", () => interstitialAdExample.LoadAd());
        AddButton("���� ���� �����ֱ�", () => interstitialAdExample.ShowAd());

        BannerAdExample bannerAdExample = FindObjectOfType<BannerAdExample>();
        bannerAdExample._loadBannerButton = AddButton("��� �ε�", null);
        bannerAdExample._showBannerButton = AddButton("��� �����ֱ�", null);
        bannerAdExample._hideBannerButton = AddButton("��� �����", null);
        
        
        baseButton.gameObject.SetActive(false);
    }

    Button AddButton(string title, UnityAction fn)
    {
        var newButton = Instantiate(baseButton, baseButton.transform.parent);
        newButton.GetComponentInChildren<Text>().text = title;
        newButton.gameObject.name = title;
        if (fn != null)
            newButton.onClick.AddListener(fn);

        return newButton;
    }
}
