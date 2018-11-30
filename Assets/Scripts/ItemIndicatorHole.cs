using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class ItemIndicatorHole : MonoBehaviour {

	// Use this for initialization

	/* *********************************************************************************************************** */

	[SerializeField]
	private stageGenerator _stageGenerator;
	[SerializeField]
	private string _myRoomName = string.Empty;

	[SerializeField]
	private bool IsEnabled = false;

	[SerializeField]
	private LevelConnector _levelConnector;

    [SerializeField]
    private bool IsSolved = false;

    [SerializeField]
    private int thisObjectNumber = 0;


    public string MyRoomName
	{
		get
		{
			return _myRoomName;
		}

		set
		{
			_myRoomName = value;
		}
	}

	public stageGenerator StageGenerator
	{
		get
		{
			return _stageGenerator;
		}

		set
		{
			_stageGenerator = value;
		}
	}

	public bool ISENABLED
	{
		get
		{
			return IsEnabled;
		}

		set
		{
			IsEnabled = value;
		}
	}

	public Dictionary< int , string > historyPagesSprite = new Dictionary<int, string>();

	/* *********************************************************************************************************** */

	void Start () {

        // = this.gameObject.activeInHierarchy;

        this.IsEnabled = false;

		if (_levelConnector == null)
		{
			_levelConnector = this.gameObject.GetComponentInChildren<LevelConnector>(true);
		}

		setActiveFalseForSubImage();

        if(this.gameObject.name.Length == 19)
        {
            thisObjectNumber = Convert.ToInt32(this.gameObject.name.Substring(17, 2));
        }
        else
        {
            thisObjectNumber = Convert.ToInt32(this.gameObject.name.Substring(17,1));
        }
	}

	public void OnEnable()
	{
		//IsEnabled = true;

		if (_levelConnector == null)
		{
			_levelConnector = this.gameObject.GetComponentInChildren<LevelConnector>(true);
		}
    }

	public void OnDisable()
	{
		//IsEnabled = false;

		if (_levelConnector == null)
		{
			_levelConnector = this.gameObject.GetComponentInChildren<LevelConnector>(true);
		}

    }

	// Update is called once per frame
	void Update () {

        if (getGameManager().getCurrentRoomNumber() >= 16)
        {
            int kalan = (getGameManager().getCurrentRoomNumber() + 1) % 16;

            if(kalan == this.thisObjectNumber)
            {
                IsEnabled = true;
            }
            else
            {
                IsEnabled = false;
            }
        }
        else
        {
            if((getGameManager().getCurrentRoomNumber()+1) == this.thisObjectNumber)
            {
                IsEnabled = true;
            }
            else
            {
                IsEnabled = false;
            }

        }

        if (IsEnabled)
        {
            controlTopItemHole();
        }

        //if (getGameManager().getCurrentRoomNumber == Int32.TryParse(this.gameObject.name.Substring()))
        //{
        //    IsEnabled = true;
        //}
        //else
        //{
        //    IsEnabled = false;
        //}

		//IsEnabled = this.gameObject.activeInHierarchy;

        //if (IsEnabled)
        //{
        //    controlTopItemHole();
        //}   
	}
	public void change_first_level_indicator_sprite(string preffix)
	{
        foreach (KeyValuePair<string, Sprite> pair in StageGenerator.ItemIndicatorSpriteArray)
        {
            if (pair.Key.Contains(MyRoomName + '_' + preffix))
            {
                this.GetComponent<Image>().sprite = pair.Value;
            }
        }

    }

    public void change_itemIndicatorholeSolvedInf()
    {
        IsSolved = true;
    }

	public void changeIndicatorholeUnsolvedInf()
	{
		IsSolved = false;
	}

    public void setActiveFalseForSubImage()
	{
		this._levelConnector.gameObject.SetActive(false);
	}

	public void setActiveTrueForSubImage()
	{
		this._levelConnector.gameObject.SetActive(true);
	}

	private void controlTopItemHole()
	{
		if(this.gameObject.GetComponent<RectTransform>().anchoredPosition.y == 220 )
		{
            setActiveFalseForSubImage();

            return;
		}

        this.GetComponentInParent<ItemIndicatorParent>().setIndicatorConnectors();
    }

    private void changeItemHoleActiveness()
    {
        if (this.IsSolved)
        {
            setActiveTrueForSubImage();
        }
        else
        {
            setActiveFalseForSubImage();
        }
    }

	public void changeSpriteFirst()
	{
		this.GetComponent<Image>().sprite = (Sprite)Resources.Load("Images/levelindicator_0", typeof(Sprite));

	}

	public bool get_IsSolved()
	{

		return IsSolved;

	}

    private GameManager getGameManager()
    {

        return GameObject.Find("Canvas").GetComponent<GameManager>();

    }

}
