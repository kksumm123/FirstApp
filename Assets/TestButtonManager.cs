using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TestButtonManager : MonoBehaviour
{
    public Button baseButton;
    void Awake()
    {
        InterstitialAdExample interstitialAdExample = FindObjectOfType<InterstitialAdExample>();
        AddButton("전면 광고 로드", () => interstitialAdExample.LoadAd());
        AddButton("전면 광고 보여주기", () => interstitialAdExample.ShowAd());

        BannerAdExample bannerAdExample = FindObjectOfType<BannerAdExample>();
        bannerAdExample._loadBannerButton = AddButton("배너 로드", null);
        bannerAdExample._showBannerButton = AddButton("배너 보여주기", null);
        bannerAdExample._hideBannerButton = AddButton("배너 숨기기", null);

        RewardedAdsButton rewardedAdsButton = FindObjectOfType<RewardedAdsButton>();
        AddButton("리워드 광고 로드", () => rewardedAdsButton.LoadAd());
        rewardedAdsButton._showAdButton = AddButton("리워드 광고 보여주기", null);
        
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
