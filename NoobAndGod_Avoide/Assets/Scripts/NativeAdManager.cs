using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using UnityEngine.UI;

public class NativeAdManager : MonoBehaviour
{
    string nativeTestId = "ca-app-pub-3940256099942544/2247696110";
    string nativeRealId = "ca-app-pub-8717943473199820/9351288906";

    private bool nativeAdLoaded = false;
    private NativeAd nativeAd;

    public GameObject adNativePanel;
    public RawImage adIcon;
    public RawImage adChoices;
    public Text adHeadline;
    public Text adCallToAction;
    public Text adAdvertiser;

    private void Awake()
    {
        adNativePanel.SetActive(false);
    }

    private void Start()
    {
        MobileAds.Initialize((InitializationStatus initStatus) =>
        {
            RequestNativeAd();
        });
    }

    private void Update()
    {
        if (nativeAdLoaded)
        {
            nativeAdLoaded = false;

            // Headline, image, body, icon, call to action, star rating, store, price, adbertiser, adChoices icon
            Texture2D iconTexture = this.nativeAd.GetIconTexture();
            Texture2D iconAdChoices = this.nativeAd.GetAdChoicesLogoTexture();
            string headline = this.nativeAd.GetHeadlineText();
            string cta = this.nativeAd.GetCallToActionText();
            string advertiser = this.nativeAd.GetAdvertiserText();

            adIcon.texture = iconTexture;
            adChoices.texture = iconAdChoices;
            adHeadline.text = headline;
            adCallToAction.text = cta;
            adAdvertiser.text = advertiser;

            nativeAd.RegisterIconImageGameObject(adIcon.gameObject);
            nativeAd.RegisterAdChoicesLogoGameObject(adChoices.gameObject);
            nativeAd.RegisterHeadlineTextGameObject(adHeadline.gameObject);
            nativeAd.RegisterCallToActionGameObject(adCallToAction.gameObject);
            nativeAd.RegisterAdvertiserTextGameObject(adAdvertiser.gameObject);

            adNativePanel.SetActive(true);
        }
    }

    private void RequestNativeAd()
    {
        AdLoader adLoader = new AdLoader.Builder(nativeTestId)
            .ForNativeAd()
            .Build();

        adLoader.OnNativeAdLoaded += this.HandleNativeAdLoaded;
        adLoader.OnAdFailedToLoad += this.HandleAdFailedToLoad;
        adLoader.LoadAd(new AdRequest.Builder().Build());
    }

    private void HandleNativeAdLoaded(object sender, NativeAdEventArgs args)
    {
        this.nativeAd = args.nativeAd;
        this.nativeAdLoaded = true;

        //adIcon.texture = nativeAd.GetIconTexture();
        //adChoices.texture = nativeAd.GetAdChoicesLogoTexture();
        //adHeadline.text = nativeAd.GetHeadlineText();
        //adCallToAction.text = nativeAd.GetCallToActionText();
        //adAdvertiser.text = nativeAd.GetAdvertiserText();

        //if(!nativeAd.RegisterIconImageGameObject(adIcon.gameObject))
        //{
        //    Debug.Log("error registering icon");
        //}
        //if (!nativeAd.RegisterAdChoicesLogoGameObject(adChoices.gameObject))
        //{
        //    Debug.Log("error registering adChoices");
        //}
        //if (!nativeAd.RegisterHeadlineTextGameObject(adHeadline.gameObject))
        //{
        //    Debug.Log("error registering adHeadline");
        //}
        //if (!nativeAd.RegisterCallToActionGameObject(adCallToAction.gameObject))
        //{
        //    Debug.Log("error registering adCallToAction");
        //}
        //if (!nativeAd.RegisterAdvertiserTextGameObject(adAdvertiser.gameObject))
        //{
        //    Debug.Log("error registering adAdvertiser");
        //}

        //adNativePanel.SetActive(true);
    }

    private void HandleAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        Debug.Log("Native ad failed to load: " + args.LoadAdError.GetMessage());
    }
}
