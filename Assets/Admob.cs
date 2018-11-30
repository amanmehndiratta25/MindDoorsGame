using UnityEngine;
using System.Collections;
using GoogleMobileAds.Api;
using System;

public class Admob : MonoBehaviour {

    [SerializeField]
    private String adId = "ca-app-pub-3940256099942544/6300978111";

    // Use this for initialization
    void Start () {

        showBannerAd();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    private void showBannerAd()
    {

        //***For Testing in the Device***
        AdRequest request = new AdRequest.Builder()
       .AddTestDevice(AdRequest.TestDeviceSimulator)       // Simulator.
       .AddTestDevice("2077ef9a63d2b398840261c8221a0c9b")  // My test device.
       .Build();

        //***For Production When Submit App***
      //  AdRequest request = new AdRequest.Builder().Build();

        BannerView bannerAd = new BannerView(adId, AdSize.SmartBanner, AdPosition.Top);
        bannerAd.LoadAd(request);
    }
}
