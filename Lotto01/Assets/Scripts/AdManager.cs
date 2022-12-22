using EasyMobile;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdManager : MonoBehaviour
{
    private void Awake()
    {
        if (!RuntimeManager.IsInitialized())
            RuntimeManager.Init();
    }

    public void ShowBanner()
    {
        Advertising.ShowBannerAd(BannerAdPosition.Top);
    }
}
