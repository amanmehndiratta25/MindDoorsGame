using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

public class HealthBarParent : MonoBehaviour {

	[SerializeField]
	private int healthBarBoxCount;

	[SerializeField]
	private int grayBoxCount;

	[SerializeField]
	private int blueBoxCount;

	[SerializeField]
	private int redBoxCount;

	private Dictionary<int, HealthBar> _blueBars;
	private Dictionary<int, HealthBar> _whiteBars;
	private Dictionary<int, HealthBar> _redBars;


	public int HealthBarBoxCount
	{
		get
		{
			return healthBarBoxCount;
		}

		set
		{
			healthBarBoxCount = value;
		}
	}

	public Dictionary<int, HealthBar> BlueBars
	{
		get
		{
			if(_blueBars == null)
			{
				_blueBars = new Dictionary<int, HealthBar>();

			}

			return _blueBars;
		}

		set
		{
			_blueBars = value;
		}
	}

	public Dictionary<int, HealthBar> WhiteBars
	{
		get
		{
			if( _whiteBars == null)
			{
				_whiteBars = new Dictionary<int, HealthBar>();
			}

			return _whiteBars;
		}

		set
		{
			_whiteBars = value;
		}
	}

	public Dictionary<int, HealthBar> RedBars
	{
		get
		{
			if(_redBars == null)
			{
				_redBars = new Dictionary<int, HealthBar>();

			}

			return _redBars;
		}

		set
		{
			_redBars = value;
		}
	}

	// Use this for initialization
	void Start () {

		if (healthBarBoxCount > 30)
		{
			grayBoxCount = healthBarBoxCount / 30;
			blueBoxCount = healthBarBoxCount % 30;
		}
		else
		{
			blueBoxCount = healthBarBoxCount;
		}

		redBoxCount = 0;

	}

	void Decrease()
	{
		// todo HALILU
	}
	
	// Update is called once per frame
	void Update () {

		

	}

	public int get_bluebar_count()
	{
		int count = 0;

        count = BlueBars.Count;

	    return count;
	}

	public int get_whitebar_count()
	{
		int count = 0;

        count = WhiteBars.Count;

		return count;
	}

	public int get_redbar_count()
	{
		int count = 0;

        count = RedBars.Count;

		return count;

	}

	public int get_allbar_count()
	{
		int count = 0;

        count = BlueBars.Count + RedBars.Count + WhiteBars.Count;

		return count;

	}

    public void convertBlueToRed()
    {
        bool IsBlueOkay = controlWhetherBlueisOkay();

        // Eğer Blue bar hiç yok ise 

        if (!IsBlueOkay)
        {
            bool IsCheckWhiteIsOkay = checkWhetherWhiteIsOkay();

            // Eğer Beyaz bar yok ise 

            if (!IsCheckWhiteIsOkay)
            {
                gameOverFromHearts();
            }
            else
            {
                // Beyaz bar var ise , Beyaz bardan 30 tane Blue bar üretme 

                fillBlueBarsFromWhiteBar();
            }
        }

        // Eğer Blue Bar var ise , direk sonuncu blue bari alıp kırmızıya çevir .

        else
        {
            pick_last_blue_bar().GetComponent<HealthBar>().MyHealthBarType = HealthBar.HealthBarType.red;
            RedBars.Add(get_redbar_count() + 1, pick_last_blue_bar().GetComponent<HealthBar>());
            BlueBars.Remove(BlueBars.Count);

        }
    }

    public bool controlWhetherBlueisOkay()
    {
        bool IsBlueEnabled = false;
        int blueBarsCount = 0;

        blueBarsCount = BlueBars.Count-1;

        if(blueBarsCount < 0)
        {
            blueBarsCount = 0;
        }

        if(blueBarsCount == 0)
        {
            IsBlueEnabled = false;
        }
        else
        {
            IsBlueEnabled = true;
        }

        return IsBlueEnabled;
        
    }

    public bool checkWhetherWhiteIsOkay()
    {
        if(WhiteBars.Count > 0 )
        {
            return true;
        }

        return false;
    }

    public bool checkWhetherReadIsOkay()
    {
        if (RedBars.Count > 0)
        {
            return true;
        }

        return false;
    }

    private void fillBlueBarsFromWhiteBar()
    {

        Image newHealthBarBox = null;
        int offsetforHealthBar = 16;

        if (checkWhetherWhiteIsOkay()== false)
        {
            return;
        }

        DestroyLastWhiteBar();
        DestroyAllRedBars();
        BlueBars.Clear();

        for (int i = 1; i <= 30 ; i++)
        {
            newHealthBarBox = Instantiate(Resources.Load("healthbarBox", typeof(Image))) as Image;
            newHealthBarBox.transform.SetParent(this.gameObject.transform);
            newHealthBarBox.GetComponent<RectTransform>().ResetRectTransformation();
            newHealthBarBox.gameObject.transform.localScale = new Vector3(stageGenerator.c_1, stageGenerator.c_1, stageGenerator.c_1);
            newHealthBarBox.gameObject.name = "bluebar" + (i).ToString();
            newHealthBarBox.GetComponent<Image>().sprite = GameObject.Find("StageGenerator").GetComponent<stageGenerator>().ItemHealthBarDict["detectionmarker_0"];
            newHealthBarBox.GetComponent<HealthBar>().MyHealthBarType = HealthBar.HealthBarType.blue;
            BlueBars.Add(i, newHealthBarBox.GetComponent<HealthBar>());

            if (i > 1)
            {
                newHealthBarBox.GetComponent<RectTransform>().anchoredPosition = new Vector2(this.gameObject.GetComponent<RectTransform>().offsetMax.x + (i - 1) * offsetforHealthBar + 100,
                                                                                             this.gameObject.GetComponent<RectTransform>().offsetMin.y - offsetforHealthBar);
            }
            else
            {
                newHealthBarBox.GetComponent<RectTransform>().anchoredPosition = new Vector2(this.gameObject.GetComponent<RectTransform>().offsetMax.x + 100,
                                                                                            this.gameObject.GetComponent<RectTransform>().offsetMin.y - offsetforHealthBar);
            }
        }

    }

    public void gameOverFromHearts()
    {
        GameManager parentObject = this.gameObject.GetComponentInParent<GameManager>();
        ClearAllBars();
        parentObject.restartStage();
    }

    public void DestroyLastWhiteBar()
    {      
        Destroy(pick_last_white_bar());
        WhiteBars.Remove(WhiteBars.Count);
    }

    public GameObject pick_last_white_bar()
    {
        GameObject lastWhiteBar = WhiteBars[WhiteBars.Count].gameObject;
        return lastWhiteBar;
    }

    public GameObject pick_last_blue_bar()
    {
        GameObject lastBlueBar = BlueBars[BlueBars.Count].gameObject;
        return lastBlueBar;
    }

    public void DestroyAllRedBars()
    {
        foreach(KeyValuePair<int,HealthBar> pair in RedBars)
        {
            Destroy(pair.Value.gameObject);
        }

        RedBars.Clear();
    }

    public void ClearAllBars()
    {
        RedBars.Clear();
        WhiteBars.Clear();
        BlueBars.Clear();
    }

}
