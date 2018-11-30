using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class CanvasManager : MonoBehaviour {

    public Image firstScreenElement;
    public Image secondScreenElement;

    public Sprite power_off;
    public Sprite power_on;
    public Sprite power_bglight;
    public Sprite play_0;
    public Sprite play_1;
    public Sprite settings_0;
    public Sprite settings_1;
    public Sprite rate_0;
    public Sprite rate_1;
	public Sprite logoZeus;
	public Sprite logoInfiltrata;
    public Sprite[] optionSprites = new Sprite[8];
  

    public GameObject Isscreen;
    public GameObject Isnotscreen;
	public GameObject IslogoObject;
	public GameObject IsInfiltrataObject;
    public GameObject IsLevelPass;
    public GameObject IsOptionObject;
    

    public AudioSource source;
    public Camera mainCamera;

    public Color firstColor;

    public Button playButton;
    public Button rateButton;
    public Button settingsButton;

	public Image logoZeusImg;
	public Image InfiltrataImg;
    public Image passLevelImg;
    public Image soundButtonImg;
    public Image musicButtonImg;
    public Image colorBlindButtonImg;
    public Image colorBlindImageBg;

    public Text passLevelText;

    public Slider soundSlider;
    public Slider musicSlider;

    void Awake () {

		IslogoObject.SetActive (true);
		IsInfiltrataObject.SetActive (false);
        Isscreen.SetActive(false);
		Isnotscreen.SetActive(false);
        firstScreenElement.sprite = power_off;
        firstColor = mainCamera.backgroundColor;
        mainCamera.backgroundColor = Color.black;
        playButton.gameObject.GetComponent<Image>().sprite = play_0;
        settingsButton.gameObject.GetComponent<Image>().sprite = settings_0;
        rateButton.gameObject.GetComponent<Image>().sprite = rate_0;
        if(Application.loadedLevel==1)
        getOptionImage(soundButtonImg, musicButtonImg, colorBlindButtonImg,colorBlindImageBg);

    }

    private void getOptionImage(Image soundButtonImg, Image musicButtonImg, Image colorBlindButtonImg, Image colorBlindImageBg)
    {
        string Sound = PlayerPrefs.GetString("Sound");
        string Music = PlayerPrefs.GetString("Music");
        string ColorBlind = PlayerPrefs.GetString("ColorBlind");

        /////////////////////////////////////////////////////////////////////////////////

        if (Sound == "true")
            OptionMenu.IsSoundEnabled = true;
        else
            OptionMenu.IsSoundEnabled = false;

        if (Music == "true")
            OptionMenu.IsMusicEnabled = true;
        else
            OptionMenu.IsMusicEnabled = false;

        if (ColorBlind == "true")
            OptionMenu.IsColorBlindEnabled = true;
        else
            OptionMenu.IsColorBlindEnabled = false;

        /////////////////////////////////////////////////////////////////////////////////

        if (OptionMenu.IsSoundEnabled == true)
            soundButtonImg.sprite = optionSprites[0];
        else
            soundButtonImg.sprite = optionSprites[1];

        if (OptionMenu.IsMusicEnabled == true)
            musicButtonImg.sprite = optionSprites[2];
        else
            musicButtonImg.sprite = optionSprites[3];


        if (OptionMenu.IsColorBlindEnabled == true)
        {
            colorBlindButtonImg.sprite = optionSprites[4];
            colorBlindImageBg.sprite = optionSprites[6];
        }
        else
        {
            colorBlindButtonImg.sprite = optionSprites[5];
            colorBlindImageBg.sprite = optionSprites[7];
        }

        OptionMenu.soundLevel = PlayerPrefs.GetFloat("soundLevel");
        OptionMenu.musicLevel = PlayerPrefs.GetFloat("musicLevel");

        soundSlider.value = OptionMenu.soundLevel;
        musicSlider.value = OptionMenu.musicLevel;

    }

    // Use this for initialization
    void Start () {

        if(Application.loadedLevel == 0)
		    StartCoroutine(FadeImage(true,logoZeusImg,"Zeus"));
        else if(Application.loadedLevel == 1)
            StartCoroutine(load_menu_element(2f));

    }
    
    // Update is called once per frame
    void Update () {
    
    }

    IEnumerator load_scene_element(float seconds) {

		secondScreenElement.gameObject.SetActive (false);
		secondScreenElement.sprite = null;
		firstScreenElement.sprite = power_off;

		yield return new WaitForSeconds (seconds);

        StartCoroutine (load_scene_element_1 (0.5f));

    }

    IEnumerator load_scene_element_1(float seconds)
    {

        secondScreenElement.gameObject.SetActive(true);
        secondScreenElement.sprite = power_bglight;
        firstScreenElement.sprite = power_on;

        yield return new WaitForSeconds(seconds);

        Isnotscreen.SetActive(false);
        mainCamera.backgroundColor = Color.black;
        Application.LoadLevel(1);

        //StartCoroutine (load_menu_element (2f));

    }

    IEnumerator load_menu_element(float seconds) {

        Isscreen.SetActive(false);
        Isnotscreen.SetActive(false);
        if(source.isPlaying)
        source.Stop();
       
        yield return new WaitForSeconds(seconds);

        mainCamera.backgroundColor = firstColor;
        Isscreen.SetActive(true);
        Isnotscreen.SetActive(false);
        IslogoObject.SetActive(false);
        source.Play();

    }

    public void playGames()
    {
        m_passLevelAnim();
  
    }
    public void shareGames()
    {
        // Uygulama yüklenince buradaki link değiştirilecek. TODO
        Application.OpenURL("market://details?id=com.trollugames.caverun3d");
    }
    public void settingsGames()
    {
       
        Isscreen.SetActive(false);
        IsOptionObject.SetActive(true);      
    }
    public void soundButton()
    {
       if(OptionMenu.IsSoundEnabled == true)
        {
            soundButtonImg.sprite = optionSprites[1];
            PlayerPrefs.SetString("Sound", "false");
            OptionMenu.IsSoundEnabled = false;
            
        }
        else
        {
            soundButtonImg.sprite = optionSprites[0];
            PlayerPrefs.SetString("Sound", "true");
            OptionMenu.IsSoundEnabled = true;
        }

        PlayerPrefs.Save();

    }

    public void musicButton()
    {
        if (OptionMenu.IsMusicEnabled == true)
        {
            musicButtonImg.sprite = optionSprites[3];
            PlayerPrefs.SetString("Music", "false");
            OptionMenu.IsMusicEnabled = false;
        }
        else
        {
            musicButtonImg.sprite = optionSprites[2];
            PlayerPrefs.SetString("Music", "true");
            OptionMenu.IsMusicEnabled = true;
        }

        PlayerPrefs.Save();
    }

    public void colorBlindButton()
    {
        if (OptionMenu.IsColorBlindEnabled == true)
        {
            colorBlindButtonImg.sprite = optionSprites[5];
            colorBlindImageBg.sprite = optionSprites[7];
            PlayerPrefs.SetString("ColorBlind", "false");
            OptionMenu.IsColorBlindEnabled = false;
        }
        else
        {
            colorBlindButtonImg.sprite = optionSprites[4];
            colorBlindImageBg.sprite = optionSprites[6];
            PlayerPrefs.SetString("ColorBlind", "true");
            OptionMenu.IsColorBlindEnabled = true;
        }

        PlayerPrefs.Save();
    }

    public void optionsBackButton()
    {

        Isscreen.SetActive(true);
        IsOptionObject.SetActive(false);
    }

	public IEnumerator FadeImage(bool fadeIAway,Image img,string objName)
	{
		img.raycastTarget = true;
		Color newColor = new Color(1, 1, 1);

		// loop over 1 second
		for (float i = 0; i <= 1f; i += Time.deltaTime)
		{
			// set color with i as alpha
			img.color = new Color(0.5f, 0.5f, 0.5f,i);
			yield return null;
		}
			
		// fade from transparent to opaque

		yield return new WaitForSeconds (1);

		// fade from opaque to transparent
		// loop over 1 second backwards
		for (float i = 1; i >= 0f; i -= Time.deltaTime)
		{
			// set color with i as alpha

			img.color = new Color(0.5f, 0.5f, 0.5f, i);
			yield return null;
		}
			
		img.raycastTarget = false;

		switch (objName) {

		case "Zeus":
			IslogoObject.SetActive (false);
			IsInfiltrataObject.SetActive (true);
			StartCoroutine(FadeImage(true,InfiltrataImg,"Infiltrata"));
			break;
		case "Infiltrata":
			IsInfiltrataObject.SetActive (false);
			Isscreen.SetActive(false);
			Isnotscreen.SetActive(true);
			firstScreenElement.sprite = power_off;
			firstColor = mainCamera.backgroundColor;
			mainCamera.backgroundColor = Color.black;
			playButton.gameObject.GetComponent<Image>().sprite = play_0;
			settingsButton.gameObject.GetComponent<Image>().sprite = settings_0;
			rateButton.gameObject.GetComponent<Image>().sprite = rate_0;
			break;
		
		}
			
	
	}

	public void perform_PowerButton()
	{
        StartCoroutine(load_scene_element(0.01f));
    }

    public void m_passLevelAnim()
    {
        StartCoroutine(passLevelAnim(passLevelImg, passLevelText));
    }

    public IEnumerator passLevelAnim(Image img, Text levelText)
    {
        Isscreen.SetActive(false);
        source.Stop();
        IsLevelPass.SetActive(true);
       // AllLevels.Instance.IncrementLevelIndex();
        levelText.text = AllLevels.Instance.lastGameIndex.ToString();
        // todo HALILU buraya saniyeyi ayarlamak için gelinebilir.
        yield return new WaitForSeconds(1f);
        IsLevelPass.SetActive(false);
        SceneManager.LoadScene(2);
    }

    public void AdjustSound(float soundLevel)
    {
        if(soundLevel > 0)
        {
            OptionMenu.IsSoundEnabled = true;
            soundButtonImg.sprite = optionSprites[0];
            PlayerPrefs.SetString("Sound", "true");
        }
        else if(soundLevel == 0)
        {
            OptionMenu.IsSoundEnabled = false;
            soundButtonImg.sprite = optionSprites[1];
            PlayerPrefs.SetString("Sound", "false");
        }
        PlayerPrefs.Save();
        OptionMenu.soundLevel = soundLevel;
        PlayerPrefs.SetFloat("soundLevel", soundLevel);
        PlayerPrefs.Save();
    }
    public void AdjustMusic(float musicLevel)
    {
        if (musicLevel > 0)
        {
            OptionMenu.IsMusicEnabled = true;
            musicButtonImg.sprite = optionSprites[2];
            PlayerPrefs.SetString("Music", "true");
        }
        else if (musicLevel == 0)
        {
            OptionMenu.IsMusicEnabled = false;
            musicButtonImg.sprite = optionSprites[3];
            PlayerPrefs.SetString("Music", "false");
        }

        PlayerPrefs.Save();
        OptionMenu.musicLevel = musicLevel;
        PlayerPrefs.SetFloat("musicLevel", musicLevel);
        PlayerPrefs.Save();
    }

}
