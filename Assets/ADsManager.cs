using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class ADsManager : MonoBehaviour
{
    Button showADButton;
    void Start()
    {
        InterstitialAdExample interstitialAdExample = FindObjectOfType<InterstitialAdExample>();
        interstitialAdExample.LoadAd();

        showADButton = GameObject.Find("Canvas").transform.Find("ShowADButton").GetComponent<Button>();
        showADButton.onClick.AddListener(() => interstitialAdExample.ShowAd());

        Player player = FindObjectOfType<Player>();
        interstitialAdExample.onUnityAdsShowComplete = (unityAdsShowCompletionState) =>
        {
            Debug.Log(unityAdsShowCompletionState);

            if (unityAdsShowCompletionState == UnityAdsShowCompletionState.COMPLETED)
                player.OnCompleteWatchingAD();

            interstitialAdExample.LoadAd();
        };
    }
}