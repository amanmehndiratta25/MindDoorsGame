using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    [SerializeField]
    int _hearts = 0;
    [SerializeField]
    int _stageLong = 0;
    [SerializeField]
    stageGenerator _stageGenerator;
    [SerializeField]
    private int currentRoomNumber = 0;
	[SerializeField]
	private int previousNumber = 0;

	public bool IsNeverGoBack = false;
    public Sprite[] restartAnimSprites = new Sprite[10];
    public GameObject IsRestartStage;
    public Image restartAnimImg;
    private Color _currentColor;
    public GameObject IsLevelPass;
    public Image levelPassImage;
    public Text currentLevelText;

    public int Hearts
    {
        get
        {
            return _hearts;
        }

        set
        {
            _hearts = value;
        }
    }

	public int PreviousNumber
	{
		get
		{
			return previousNumber;
		}

		set
		{
			previousNumber = value;
		}
	}

    public int StageLong
    {
        get
        {
            return _stageLong;
        }

        set
        {
            _stageLong = value;
        }
    }

    public void Init()
    {
        Hearts = 0;
        StageLong = 0;
		IsNeverGoBack = false;
    }

    // Use this for initialization

    void Start ()
    {
        Init();

        _stageGenerator = GameManager.FindObjectOfType<stageGenerator>();
        this.Hearts = AllLevels.Instance.pushStage().hearts;
        this.StageLong = AllLevels.Instance.pushStage().stageLength;

    }
    
    // Update is called once per frame
    void Update () {

        //if (Input.GetKeyDown(KeyCode.L))
        //{
        //    Debug.Log(getCurrentRoomNumber());
        //}
    
    }

    public void restartStage()
    {
        Color currentColor = Camera.main.backgroundColor;
        _currentColor = currentColor;
        DestroyAllChild();
        // Burada Bölüm restart animasyonu başlayacak.
        m_restartStageAnim();
   
    }

    private void DestroyAllChild()
    {
      foreach(RectTransform child in this.GetComponent<RectTransform>())
        {
            if(!child.gameObject.CompareTag("restartAnim"))
            Destroy(child.gameObject);

        }

		IsNeverGoBack = false;
			
    }

    public void nextStageandLevel()
    {
        DestroyAllChild();

        m_passLevelAnim();

    }

    public int getCurrentRoomNumber()
    {
        int number = 0;

        for(int i = 0; i< this.GetComponentsInChildren<Room>(false).Length; i++)
        {
            number = Int32.Parse(this.GetComponentsInChildren<Room>()[i].gameObject.name.Split('r')[0]);

        }

        return number;
    }

	public Room.roomSituation getRoomSituation(int index)
	{
		Room.roomSituation situation = Room.roomSituation.unsolved;

		for(int i = 0; i< this.GetComponentsInChildren<Room>(true).Length; i++)
		{
			if (i == index) {
				situation = this.GetComponentsInChildren<Room> (true) [i].MyRoomSituation;
				break;
			}

		}

		return situation;
	}

    public bool getCurrentRoomBlinkInf()
    {
        bool IsRoomBlink = false;

        for (int i = 0; i < this.GetComponentsInChildren<Room>(false).Length; i++)
        {
            IsRoomBlink = this.GetComponentsInChildren<Room>()[i].IsBlink1;
            
        }

        return IsRoomBlink;
    }

    public Room.roomType getCurrentRoomTypeInf()
    {
        Room.roomType type = Room.roomType.binaryOption;

        for (int i = 0; i < this.GetComponentsInChildren<Room>(false).Length; i++)
        {
            type = this.GetComponentsInChildren<Room>()[i].MyRoomType;

        }

        return type;

    }

    public Room.roomSituation getCurrentRoomNumberSolvedInf()
    {
        Room.roomSituation situation = Room.roomSituation.unsolved;

        for (int i = 0; i < this.GetComponentsInChildren<Room>(false).Length; i++)
        {
            situation  = this.GetComponentsInChildren<Room>()[i].MyRoomSituation;

        }

        return situation;
    }

    public GameObject getCurrentRoom()
    {
        GameObject currentRoom;

        currentRoom = null;

        for (int i = 0; i < this.GetComponentsInChildren<Room>(false).Length; i++)
        {
            currentRoom = this.GetComponentsInChildren<Room>()[i].gameObject;

        }

        return currentRoom;

    }

	public int getRandomRoom(Room currentRoom){

		GameObject randomRoom = null;
		int currentRoomIndex = Int32.Parse(currentRoom.gameObject.name.Split ('r') [0]);
		int anotherRandomNumber = 0;
		do 
		{
			anotherRandomNumber = UnityEngine.Random.Range (0,  this.GetComponentsInChildren<Room>(true).Length );

		} while (anotherRandomNumber == currentRoomIndex) ;

		for (int i = 0; i < this.GetComponentsInChildren<Room>(true).Length; i++)
		{
			if (i == anotherRandomNumber) {

				randomRoom = this.GetComponentsInChildren<Room> (true) [i].gameObject;

				break;
			}

		}

		return Int32.Parse(randomRoom.name.Split ('r') [0]);

	}


    public void deleteOneItemFromItemHoleBars()
    {
        // Itemların tutulduğu dictionary 'den son item silinir.

        AllLevels.Instance.Items.DeleteLastItem(AllLevels.Instance.Items.get_slots_number());

        // Tutarlı olması için keys ve values array'lerinde de silinir.

        AllLevels.Instance.Items.fill_keys_values();

        // Değişiklikler dosyaya kaydedilir.

        AllLevels.Instance.Save();

        // Item bölümünden bir tane item silinecektir.

        GameObject.Find("StageGenerator").GetComponent<stageGenerator>().perform_uItem();
    }

    public void deleteOneItemFromItemHoleBars(int index)
    {
        // Itemların tutulduğu dictionary 'den son item silinir.

        AllLevels.Instance.Items.DeleteItem(index);

        // Tutarlı olması için keys ve values array'lerinde de silinir.

        AllLevels.Instance.Items.fill_keys_values();

        // Değişiklikler dosyaya kaydedilir.

        AllLevels.Instance.Save();

        // Item bölümünden bir tane item eklenecektir.

        GameObject.Find("StageGenerator").GetComponent<stageGenerator>().perform_uItem();

    }

    public void addOneItemToItemHoleBars(string specialItem)
    {
        // Itemların tutulduğu dictionary 'e bir item eklenir.

        AllLevels.Instance.Items.AddItem(specialItem);

        // Tutarlı olması için keys ve values array'lerinde de silinir.

        AllLevels.Instance.Items.fill_keys_values();

        // Değişiklikler dosyaya kaydedilir.

        AllLevels.Instance.Save();

        // Item bölümünden bir tane item eklenecektir.

        GameObject.Find("StageGenerator").GetComponent<stageGenerator>().perform_uItem();
    }

    public void m_restartStageAnim()
    {
        StartCoroutine(restartStageAnim(restartAnimImg,1));
    }

    public IEnumerator restartStageAnim(Image img,int number)
    {
        switch (number)
        {
            case 1:
                Camera.main.backgroundColor = Color.black;
                IsRestartStage.SetActive(true);
                img.raycastTarget = true;
                img.sprite = restartAnimSprites[0];
                yield return new WaitForSeconds(0.03f);
                StartCoroutine(restartStageAnim(img,2));
                break;
            case 2:
                img.sprite = restartAnimSprites[1];
                yield return new WaitForSeconds(0.03f);
                StartCoroutine(restartStageAnim(img,3));
                break;
            case 3:
                img.sprite = restartAnimSprites[2];
                yield return new WaitForSeconds(0.03f);
                StartCoroutine(restartStageAnim(img,4));
                break;
            case 4:
                img.sprite = restartAnimSprites[3];
                yield return new WaitForSeconds(0.03f);
                StartCoroutine(restartStageAnim(img, 5));
                break;
            case 5:
                img.sprite = restartAnimSprites[4];
                yield return new WaitForSeconds(0.03f);
                StartCoroutine(restartStageAnim(img, 6));
                break;
            case 6:
                img.sprite = restartAnimSprites[5];
                yield return new WaitForSeconds(0.03f);
                StartCoroutine(restartStageAnim(img, 7));
                break;
            case 7:
                img.sprite = restartAnimSprites[6];
                yield return new WaitForSeconds(0.03f);
                StartCoroutine(restartStageAnim(img, 8));
                break;
            case 8:
                img.sprite = restartAnimSprites[7];
                yield return new WaitForSeconds(0.03f);
                StartCoroutine(restartStageAnim(img, 9));
                break;
            case 9:
                img.sprite = restartAnimSprites[8];
                yield return new WaitForSeconds(1f);
                StartCoroutine(restartStageAnim(img, 10));
                break;
            case 10:
                img.sprite = restartAnimSprites[9];
                yield return new WaitForSeconds(0.03f);
                StartCoroutine(restartStageAnim(img, 11));
                break;
            case 11:
                img.sprite = null;
                img.color = Color.black;
                yield return new WaitForSeconds(0.5f);
                img.color = Color.white;
                img.raycastTarget = false;
                IsRestartStage.SetActive(false);
                Camera.main.backgroundColor = _currentColor;
                stageGenerator stageGenerator = GameObject.Find("StageGenerator").GetComponent<stageGenerator>();
                Stage _stage = AllLevels.Instance.pushStage();
                stageGenerator.Init(true);
                break;
        }

    }

    public void m_passLevelAnim()
    {
        StartCoroutine(passLevelAnim(levelPassImage,currentLevelText));
    }

    public IEnumerator passLevelAnim(Image img,Text levelText)
    {
        IsLevelPass.SetActive(true);
        AllLevels.Instance.IncrementLevelIndex();
        levelText.text = AllLevels.Instance.lastGameIndex.ToString();
        // todo HALILU buraya saniyeyi ayarlamak için gelinebilir.
        yield return new WaitForSeconds(1f);
        IsLevelPass.SetActive(false);
        stageGenerator stageGenerator = GameObject.Find("StageGenerator").GetComponent<stageGenerator>();
        Stage _stage = AllLevels.Instance.pushStage();
        stageGenerator.Init(false);
    }

}
