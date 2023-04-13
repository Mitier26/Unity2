using GoogleMobileAds.Api;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AdManager : MonoBehaviour
{
    public static AdManager instance;

    private string adUnitTestId = "ca-app-pub-3940256099942544/6300978111";
    private string adUnitRealId = "ca-app-pub-8717943473199820/4195450553";

    BannerView bannerView;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        MobileAds.Initialize((InitializationStatus initStatus) =>
        {
        });
        LoadAd();
    }


    public void CreateBannerView()
    {
        if(bannerView != null)
        {
            DestroyAd();
        }

        bannerView = new BannerView(adUnitTestId, AdSize.GetLandscapeAnchoredAdaptiveBannerAdSizeWithWidth(AdSize.FullWidth),AdPosition.Bottom);
        //bannerView = new BannerView(adUnitTestId, AdSize.IABBanner, AdPosition.Bottom);
    }

    public void LoadAd()
    {
        if(bannerView == null)
        {
            CreateBannerView();
        }

        var adRequest = new AdRequest.Builder().AddKeyword("sample").Build();

        bannerView.LoadAd(adRequest);
    }
      
    public void BannerShow()
    {
        bannerView.Show();
    }


    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneChange;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneChange;
    }

    private void OnSceneChange(Scene scene, LoadSceneMode mode)
    {
        if(scene.name != "Title")
            BannerShow();
    }

    public void DestroyAd()
    {
        if(bannerView != null )
        {
            bannerView.Destroy();
            bannerView = null;
        }
    }
}
