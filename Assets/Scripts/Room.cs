using UnityEngine;
using System;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

[System.Runtime.InteropServices.Guid("0F4B728B-95C3-4182-9537-FE0A8FB50970")]
public class Room : MonoBehaviour
{
    // Oda tiplerini saklamak için Enum
    public enum roomType
    {
        binaryOption,
        tripleOption,
        quadOption,
        checkPoint,
        doubleRight,
        doRight,
        devoted,
        dontMistake,
        beFast,
        signOption,
        dameOption,
        pin5,
        pin6,
        pin7,
        pin8,
        pin9,
        sortbyItems
    }

    // Odanın isim özelliği

    private String roomName;

    // odanın türü

    [SerializeField]
    private roomType _myRoomType = roomType.binaryOption;

    // Odanın sahip olduğu kapı türlerini içinde bulunduran List

    public List<flatDoor> _doors = new List<flatDoor>();

    // Odanın tür özelliği

    public roomType MyRoomType { get { return _myRoomType; } set { _myRoomType = value; } }

    // Odanın Numarasının Özelliği ( Stage Bazlı )
    public int RoomNumber
    {
        get
        {
            return _roomNumber;
        }

        set
        {
            _roomNumber = value;
        }
    }

    // Odanın İsim Özelliği ( Room Bazlı)

    public string RoomName
    {
        get
        {
            return roomName;
        }
        set
        {
            roomName = value;
        }

    }

    // Odanın numarasının kapsullenmiş hali

    [SerializeField]
    private int _roomNumber = 0;

    // Odanın gameobject hali

    [SerializeField]
    private GameObject thisGameObject;

    public GameObject ThisGameObject
    {
        get
        {
            return thisGameObject;
        }

        set
        {
            thisGameObject = value;
        }
    }

    // Çeşitli Sadece bu class'ta kullanılan değişkenler

    const int pinHigh = 175;
    const int _trueIndex = 2;


    // Just for pin Rooms
    [SerializeField]
    private Dictionary<int, Button> _pinDict;
    private Dictionary<int, Image> _pinLineDict;

    // Properties for pinDict

    public Dictionary<int, Button> PinDict
    {
        get
        {
            return _pinDict;
        }
    }

    // Parent GameObject

    [SerializeField]
    private GameManager parentObject;

    // DameProgess Enum

    public enum DameProgress
    {
        init,
        zero,
        triple,
        quad,
        finish
    }
    [SerializeField]
    private DameProgress myDameProgress = DameProgress.zero;

    // DameOption Room Process True Or False Scenario 

    private bool IsDameTrueFalse = false;

    // Room situation 
    public enum roomSituation
    {
       unsolved,
       solved
    }

    [SerializeField]
    private roomSituation _myRoomSituation = roomSituation.unsolved;

    public roomSituation MyRoomSituation
    {
        get
        {
            return _myRoomSituation;
        }

        set
        {
            _myRoomSituation = value;
        }

    }
		
    //  Oda daha önce açılmış mı onun bilgisini veren özellik

    public bool IsBlink1
    {
        get
        {
            return IsBlink;
        }

        set
        {
            IsBlink = value;
        }
    }

  
    // Odaya ait olan item indicator hole 

    [SerializeField]
    private bool IsBlink = false;

	// Oda ' da yanlış yapıldığın da checkpoint yada başa dönmeyi engelleyen lock

	private bool _lockShield = false;

	public bool LockShield{

		get{

			if (GameObject.FindGameObjectWithTag ("itemshieldparent") != null) 
				_lockShield = true;	
			else 
				_lockShield = false;


			return _lockShield;

		}
	}

	private ItemIndicatorParent _itemIndicatorParent;


    /// <summary>
    /// Init All Property
    /// </summary>
    public void Init()
    {

        _myRoomType = roomType.binaryOption;
        if(_doors!= null)
        _doors.Clear();
        _roomNumber = 0;
        thisGameObject = this.gameObject;
        if(_pinDict != null)
        _pinDict.Clear();
        if(_pinLineDict!=null)
        _pinLineDict.Clear();
        myDameProgress = DameProgress.zero;
        IsDameTrueFalse = false;
        _myRoomSituation = roomSituation.unsolved;
        IsBlink = false;
    }

    public void Awake()
    {
        Init();
    }

    public void Start()
    {
        if (parentObject == null)
        {
            parentObject = this.gameObject.GetComponentInParent<GameManager>();
        }

        pinCoroutineMethod();
    }

    public void OnEnable()
    {
        if (parentObject == null)
        {
            parentObject = this.gameObject.GetComponentInParent<GameManager>();
        }

		// Bölüm başlarken bölüm başına gerekli ayarlamalar yapılıyor .

        pinCoroutineMethod();

       // Ui indicator case'leri belirleniyor .

		performChangeUIITemIndicator ();

       // Room acılırken acılan animasyon oluşturuluyor.

        this.gameObject.GetComponent<fadeIn>().performFade(this.gameObject.GetComponent<Image>(),_doors);
    }

    private void pinCoroutineMethod()
    {

        switch (this.MyRoomType)
        {
            case Room.roomType.beFast:

                break;
            case Room.roomType.binaryOption:

                break;
            case Room.roomType.checkPoint:
                break;
            case Room.roomType.dameOption:
                break;
            case Room.roomType.devoted:

                break;
            case Room.roomType.dontMistake:

                break;
            case Room.roomType.doRight:

                break;
            case Room.roomType.doubleRight:

                break;
            case Room.roomType.pin5:
            case Room.roomType.pin6:
            case Room.roomType.pin7:
            case Room.roomType.pin8:
            case Room.roomType.pin9:

                int counter = 0;

                switch (this.MyRoomType)
                {
                    case Room.roomType.pin5:
                        counter = 5;
                        break;
                    case Room.roomType.pin6:
                        counter = 6;
                        break;
                    case Room.roomType.pin7:
                        counter = 7;
                        break;
                    case Room.roomType.pin8:
                        counter = 8;
                        break;
                    case Room.roomType.pin9:
                        counter = 9;
                        break;
                }

                SortedList<int, GameObject> realGameObjectList = new SortedList<int, GameObject>();

                for (int k = 0; k < _doors.Count; k++)
                {
                    if (_doors[k].Decision <= counter)
                    {
                        realGameObjectList.Add(_doors[k].Decision, _doors[k].gameObject);

                    }

                }
                // todo saniye için dönebilirsin !!!
                  StartCoroutine(pinNumerator(0.5f, realGameObjectList));

                break;
            case Room.roomType.quadOption:

                break;
            case Room.roomType.signOption:
            
                break;
            case Room.roomType.sortbyItems:

                performsortbyItemsRoomMethod();

                break;
            case Room.roomType.tripleOption:

                break;
        }
    }

    private void performsortbyItemsRoomMethod()
    {
        // Şuanlık sortByItems odası için birşey yapmaya gerek yok .

    }

    public void OnDisable()
    {

        if (parentObject == null)
        {
            parentObject = this.gameObject.GetComponentInParent<GameManager>();
        }

        switch (this.MyRoomType)
        {
            case roomType.pin5:
            case roomType.pin6:
            case roomType.pin7:
            case roomType.pin8:
            case roomType.pin9:
            case roomType.sortbyItems:

                if (_pinDict != null)
                {
                    for (int m = 0; m < _pinDict.Count; m++)
                    {
                        _pinDict[m+1].gameObject.GetComponent<Image>().sprite = (Sprite)Resources.Load("Images/port_0", typeof(Sprite));
                        _pinDict[m + 1].gameObject.GetComponent<flatDoor>().IsClickable = true;
                        _pinDict[m + 1].gameObject.GetComponent<flatDoor>().ChildBackGroundObject.GetComponent<Image>().sprite =
                            _pinDict[m + 1].gameObject.GetComponent<flatDoor>().normalBGSprite;
                    }

                    _pinDict = null;

                }

                if(_pinLineDict != null)
                {
                    for(int k = 0; k <_pinLineDict.Count; k++)
                    {
                        _pinLineDict[k+1].gameObject.GetComponent<Image>().sprite = (Sprite)Resources.Load("Images/pin_0", typeof(Sprite));
                    }

                    _pinLineDict = null;
                }

                break;

            case roomType.doubleRight:

                if (_pinDict != null)
                {
                    for (int m = 0; m < _pinDict.Count; m++)
                    {
                        _pinDict[m + 1].gameObject.GetComponent<Image>().sprite = _pinDict[m + 1].gameObject.GetComponent<flatDoor>().normalSprite;
                        _pinDict[m + 1].gameObject.GetComponent<flatDoor>().ChildBackGroundObject.GetComponent<Image>().sprite =
                           _pinDict[m + 1].gameObject.GetComponent<flatDoor>().normalBGSprite;
                        _pinDict[m + 1].gameObject.GetComponent<flatDoor>().IsClickable = true;
                    }

                    _pinDict = null;

                }

                if (_pinLineDict != null)
                {
                    _pinLineDict[1].GetComponent<Image>().sprite = (Sprite)Resources.Load("Images/left_0", typeof(Sprite));
                    _pinLineDict[2].GetComponent<Image>().sprite = (Sprite)Resources.Load("Images/right_0", typeof(Sprite));
                    _pinLineDict = null;
                }

                break;
            case roomType.beFast:

                break;

            default:

                break;
        }

                
    }

    public IEnumerator pinNumerator(float second, SortedList<int,GameObject> realList)
    {

        yield return new WaitForSeconds(second);

        foreach (KeyValuePair<int,GameObject> k in realList)
        {
            realList[k.Key].GetComponent<Image>().sprite = (Sprite)Resources.Load("Images/port_y", typeof(Sprite));
            realList[k.Key].GetComponent<flatDoor>().ChildBackGroundObject.GetComponent<Image>().sprite =
            realList[k.Key].GetComponent<flatDoor>().trueBGSprite;
            yield return new WaitForSeconds(second);
            realList[k.Key].GetComponent<Image>().sprite = (Sprite)Resources.Load("Images/port_0", typeof(Sprite));
            realList[k.Key].GetComponent<flatDoor>().ChildBackGroundObject.GetComponent<Image>().sprite =
            realList[k.Key].GetComponent<flatDoor>().normalBGSprite;

        }

    }

    public Room(string room)
    {

        switch (room)
        {
            case "":
                _myRoomType = roomType.binaryOption;
                break;

            case "binaryOption":
                _myRoomType = roomType.binaryOption;
                break;

            case "tripleOption":
                _myRoomType = roomType.tripleOption;
                break;

            case "quadOption":
                _myRoomType = roomType.quadOption;
                break;

            case "checkPoint":
                _myRoomType = roomType.checkPoint;
                break;

            case "doubleRight":
                _myRoomType = roomType.doubleRight;
                break;

            case "doRight":
                _myRoomType = roomType.doRight;
                break;
            case "devoted":
                _myRoomType = roomType.devoted;
                break;
            case "dontMistake":
                _myRoomType = roomType.dontMistake;
                break;
            case "beFast":
                _myRoomType = roomType.beFast;
                break;
            case "signOption":
                _myRoomType = roomType.signOption;
                break;
            case "dameOption":
                _myRoomType = roomType.dameOption;
                break;
            case "pin5":
                _myRoomType = roomType.pin5;
                break;
            case "pin6":
                _myRoomType = roomType.pin6;
                break;
            case "pin7":
                _myRoomType = roomType.pin7;
                break;
            case "pin8":
                _myRoomType = roomType.pin8;
                break;
            case "pin9":
                _myRoomType = roomType.pin9;
                break;
            case "sortbyItems":
                _myRoomType = roomType.sortbyItems;
                break;
        }

    }

    #region first Creator Room

    public void configure_Room(GameObject room)
    {

        switch (_myRoomType)
        {
            case Room.roomType.binaryOption:
                create_binaryOption(room);
                break;

            case Room.roomType.tripleOption:
                create_tripleOption(room,"door");
                break;

            case Room.roomType.quadOption:
                create_quadOption(room,"door");
                break;

            case Room.roomType.checkPoint:
                create_checkPoint(room);
                break;

            case Room.roomType.doubleRight:
                create_doubleRight(room);
                break;

            case Room.roomType.doRight:
                create_doRight(room);
                break;

            case Room.roomType.devoted:
                create_devoted(room);
                break;

            case Room.roomType.dontMistake:
                create_dontMistake(room);
                break;

            case Room.roomType.beFast:
                create_beFast(room);
                break;

            case Room.roomType.signOption:
                create_signOption(room);
                break;

            case Room.roomType.dameOption:
                create_dameOption(room);
                break;

            case Room.roomType.pin5:
                create_pin5(room);
                break;

            case Room.roomType.pin6:
                create_pin6(room);
                break;

            case Room.roomType.pin7:
                create_pin7(room);
                break;

            case Room.roomType.pin8:
                create_pin8(room); 
                break;

            case Room.roomType.pin9:
                create_pin9(room);
                break;

            case Room.roomType.sortbyItems:
                create_sortbyItems(room);
                break;
        }

    }

    #endregion

    /* The room creation is classificated method by method */

    #region Create Rooms

    private void create_sortbyItems(GameObject room)
    {
        Button prefab;
        Image pinImage;
        Image errorLine;
        int pinNumber = 4;
        String name = String.Empty;
        int doorCount = 4;

        prefab = null;

        for (int i = 0; i < doorCount; i++)
        {

            prefab = Instantiate(Resources.Load("door", typeof(Button))) as Button;
            prefab.transform.SetParent(room.transform);
            prefab.GetComponent<flatDoor>().ParentType = this.MyRoomType;
            prefab.GetComponent<RectTransform>().ResetRectTransformation();
            determineDoorSprites(prefab.GetComponent<flatDoor>(), this.MyRoomType);
            name = prefab.gameObject.name.Split('(')[0];
            prefab.gameObject.name = (i + 1).ToString() + name;

            switch (i)
            {
                case 0:
                    prefab.GetComponent<RectTransform>().anchoredPosition = new Vector2(-255, 0);
                    break;
                case 1:
                    prefab.GetComponent<RectTransform>().anchoredPosition = new Vector2(-85, 0);
                    break;
                case 2:
                    prefab.GetComponent<RectTransform>().anchoredPosition = new Vector2(+85, 0);
                    break;
                case 3:
                    prefab.GetComponent<RectTransform>().anchoredPosition = new Vector2(+255, 0);
                    break;
            }

            name = String.Empty;
            prefab = null;

        }

          #region Consisting of pin lines 

        pinImage = null;

        for (int i = 0; i < pinNumber; i++)
        {
            pinImage = Instantiate(Resources.Load("pinLine", typeof(Image))) as Image;
            pinImage.transform.SetParent(room.transform);
            pinImage.GetComponent<RectTransform>().ResetRectTransformation();
            pinImage.gameObject.GetComponent<Transform>().localScale = new Vector3(1, 1, 1);
            name = (i + 1).ToString() + pinImage.gameObject.name.Split('(')[0];
            pinImage.gameObject.name = name;
            pinImage.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, room.GetComponent<RectTransform>().offsetMax.y + 50);

            switch (i)
            {
                case 0:
                    pinImage.GetComponent<RectTransform>().anchoredPosition = new Vector2(-144, 200);
                    break;
                case 1:
                    pinImage.GetComponent<RectTransform>().anchoredPosition = new Vector2(-48, 200);
                    break;
                case 2:
                    pinImage.GetComponent<RectTransform>().anchoredPosition = new Vector2(48, 200);
                    break;
                case 3:
                    pinImage.GetComponent<RectTransform>().anchoredPosition = new Vector2(144, 200);
                    break;
            }

            name = String.Empty;
            prefab = null;
        }

        #endregion


        #region Consisting of error lines 

        errorLine = null;

        errorLine = Instantiate(Resources.Load("errorLine", typeof(Image))) as Image;
        errorLine.transform.SetParent(room.transform);
        errorLine.GetComponent<RectTransform>().ResetRectTransformation();
        errorLine.gameObject.GetComponent<Transform>().localScale = new Vector3(1, 1, 1);
        errorLine.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, room.GetComponent<RectTransform>().offsetMax.y + 50);
        name = 1 + errorLine.gameObject.name.Split('(')[0];
        errorLine.gameObject.name = name;

        errorLine = null;

        errorLine = Instantiate(Resources.Load("errorLine", typeof(Image))) as Image;
        errorLine.transform.SetParent(room.transform);
        errorLine.GetComponent<RectTransform>().ResetRectTransformation();
        errorLine.gameObject.GetComponent<Transform>().localScale = new Vector3(1, 1, 1);
        errorLine.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, room.GetComponent<RectTransform>().offsetMax.y + 75);
        name = 2 + errorLine.gameObject.name.Split('(')[0];
        errorLine.gameObject.name = name;

        errorLine = null;

        errorLine = Instantiate(Resources.Load("errorLine", typeof(Image))) as Image;
        errorLine.transform.SetParent(room.transform);
        errorLine.GetComponent<RectTransform>().ResetRectTransformation();
        errorLine.gameObject.GetComponent<Transform>().localScale = new Vector3(1, 1, 1);
        errorLine.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, room.GetComponent<RectTransform>().offsetMax.y + 100);
        name = 3 + errorLine.gameObject.name.Split('(')[0];
        errorLine.gameObject.name = name;

        errorLine = null;

        #endregion

    }

    private void create_pin9(GameObject room)
    {
        Button prefab;
        String name = String.Empty;
        int doorCount = 9;
        int pinNumber = 9;
        Image pinImage;

        prefab = null;

        for (int i = 0; i < doorCount; i++)
        {

            prefab = Instantiate(Resources.Load("door", typeof(Button))) as Button;
            prefab.transform.SetParent(room.transform);
            prefab.GetComponent<RectTransform>().ResetRectTransformation();
            determineDoorSprites(prefab.GetComponent<flatDoor>(), this.MyRoomType);
            prefab.GetComponent<flatDoor>().ParentType = this.MyRoomType;
            name = prefab.gameObject.name.Split('(')[0];
            prefab.gameObject.name = (i + 1).ToString() + name;

            switch (i)
            {
                case 0:
                    prefab.GetComponent<RectTransform>().anchoredPosition = new Vector2(-170, -150);
                    break;
                case 1:
                    prefab.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -150);
                    break;
                case 2:
                    prefab.GetComponent<RectTransform>().anchoredPosition = new Vector2(170, -150);
                    break;
                case 3:
                    prefab.GetComponent<RectTransform>().anchoredPosition = new Vector2(-170, -25);
                    break;
                case 4:
                    prefab.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -25);
                    break;
                case 5:
                    prefab.GetComponent<RectTransform>().anchoredPosition = new Vector2(+170, -25);
                    break;
                case 6:
                    prefab.GetComponent<RectTransform>().anchoredPosition = new Vector2(-170, 100);
                    break;
                case 7:
                    prefab.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 100);
                    break;
                case 8:
                    prefab.GetComponent<RectTransform>().anchoredPosition = new Vector2(170, 100);
                    break;

            }

            name = String.Empty;
            prefab = null;

        }

        #region Consisting of pin lines 

        pinImage = null;

        for (int i = 0; i < pinNumber; i++)
        {
            pinImage = Instantiate(Resources.Load("pinLine", typeof(Image))) as Image;
            pinImage.transform.SetParent(room.transform);
            pinImage.GetComponent<RectTransform>().ResetRectTransformation();
            pinImage.gameObject.GetComponent<Transform>().localScale = new Vector3(1, 1, 1);
            name = (i + 1).ToString() + pinImage.gameObject.name.Split('(')[0];
            pinImage.gameObject.name = name;
            pinImage.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, room.GetComponent<RectTransform>().offsetMax.y + 50);

            switch (i)
            {
                case 0:
                    pinImage.GetComponent<RectTransform>().anchoredPosition = new Vector2(-384, pinHigh);
                    break;
                case 1:
                    pinImage.GetComponent<RectTransform>().anchoredPosition = new Vector2(-288, pinHigh);
                    break;
                case 2:
                    pinImage.GetComponent<RectTransform>().anchoredPosition = new Vector2(-192, pinHigh);
                    break;
                case 3:
                    pinImage.GetComponent<RectTransform>().anchoredPosition = new Vector2(-96, pinHigh);
                    break;
                case 4:
                    pinImage.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, pinHigh);
                    break;
                case 5:
                    pinImage.GetComponent<RectTransform>().anchoredPosition = new Vector2(96, pinHigh);
                    break;
                case 6:
                    pinImage.GetComponent<RectTransform>().anchoredPosition = new Vector2(192, pinHigh);
                    break;
                case 7:
                    pinImage.GetComponent<RectTransform>().anchoredPosition = new Vector2(288, pinHigh);
                    break;
                case 8:
                    pinImage.GetComponent<RectTransform>().anchoredPosition = new Vector2(384, pinHigh);
                    break;
            }

            name = String.Empty;
            prefab = null;
        }

        #endregion
    }

    private void create_pin8(GameObject room)
    {
        Button prefab;
        String name = String.Empty;
        int doorCount = 9;
        int pinNumber = 8;
        Image pinImage;

        prefab = null;

        for (int i = 0; i < doorCount; i++)
        {

            prefab = Instantiate(Resources.Load("door", typeof(Button))) as Button;
            prefab.transform.SetParent(room.transform);
            prefab.GetComponent<RectTransform>().ResetRectTransformation();
            prefab.GetComponent<flatDoor>().ParentType = this.MyRoomType;
            determineDoorSprites(prefab.GetComponent<flatDoor>(), this.MyRoomType);
            name = prefab.gameObject.name.Split('(')[0];
            prefab.gameObject.name = (i + 1).ToString() + name;

            switch (i)
            {
                case 0:
                    prefab.GetComponent<RectTransform>().anchoredPosition = new Vector2(-170, -150);
                    break;
                case 1:
                    prefab.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -150);
                    break;
                case 2:
                    prefab.GetComponent<RectTransform>().anchoredPosition = new Vector2(170, -150);
                    break;
                case 3:
                    prefab.GetComponent<RectTransform>().anchoredPosition = new Vector2(-170, -25);
                    break;
                case 4:
                    prefab.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -25);
                    break;
                case 5:
                    prefab.GetComponent<RectTransform>().anchoredPosition = new Vector2(+170, -25);
                    break;
                case 6:
                    prefab.GetComponent<RectTransform>().anchoredPosition = new Vector2(-170, 100);
                    break;
                case 7:
                    prefab.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 100);
                    break;
                case 8:
                    prefab.GetComponent<RectTransform>().anchoredPosition = new Vector2(170, 100);
                    break;

            }

            name = String.Empty;
            prefab = null;

        }

        #region Consisting of pin lines 

        pinImage = null;

        for (int i = 0; i < pinNumber; i++)
        {
            pinImage = Instantiate(Resources.Load("pinLine", typeof(Image))) as Image;
            pinImage.transform.SetParent(room.transform);
            pinImage.GetComponent<RectTransform>().ResetRectTransformation();
            pinImage.gameObject.GetComponent<Transform>().localScale = new Vector3(1, 1, 1);
            name = (i + 1).ToString() + pinImage.gameObject.name.Split('(')[0];
            pinImage.gameObject.name = name;
            pinImage.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, room.GetComponent<RectTransform>().offsetMax.y + 50);

            switch (i)
            {
                case 0:
                    pinImage.GetComponent<RectTransform>().anchoredPosition = new Vector2(-336, pinHigh);
                    break;
                case 1:
                    pinImage.GetComponent<RectTransform>().anchoredPosition = new Vector2(-240, pinHigh);
                    break;
                case 2:
                    pinImage.GetComponent<RectTransform>().anchoredPosition = new Vector2(-144, pinHigh);
                    break;
                case 3:
                    pinImage.GetComponent<RectTransform>().anchoredPosition = new Vector2(-48, pinHigh);
                    break;
                case 4:
                    pinImage.GetComponent<RectTransform>().anchoredPosition = new Vector2(48, pinHigh);
                    break;
                case 5:
                    pinImage.GetComponent<RectTransform>().anchoredPosition = new Vector2(144, pinHigh);
                    break;
                case 6:
                    pinImage.GetComponent<RectTransform>().anchoredPosition = new Vector2(240, pinHigh);
                    break;
                case 7:
                    pinImage.GetComponent<RectTransform>().anchoredPosition = new Vector2(336, pinHigh);
                    break;
            }

            name = String.Empty;
            prefab = null;
        }

        #endregion
    }

    private void create_pin7(GameObject room)
    {
        Button prefab;
        String name = String.Empty;
        int doorCount = 9;
        int pinNumber = 7;
        Image pinImage;

        prefab = null;

        for (int i = 0; i < doorCount; i++)
        {

            prefab = Instantiate(Resources.Load("door", typeof(Button))) as Button;
            prefab.transform.SetParent(room.transform);
            prefab.GetComponent<RectTransform>().ResetRectTransformation();
            prefab.GetComponent<flatDoor>().ParentType = this.MyRoomType;
            determineDoorSprites(prefab.GetComponent<flatDoor>(), this.MyRoomType);
            name = prefab.gameObject.name.Split('(')[0];
            prefab.gameObject.name = (i + 1).ToString() + name;

            switch (i)
            {
                case 0:
                    prefab.GetComponent<RectTransform>().anchoredPosition = new Vector2(-170, -150);
                    break;
                case 1:
                    prefab.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -150);
                    break;
                case 2:
                    prefab.GetComponent<RectTransform>().anchoredPosition = new Vector2(170, -150);
                    break;
                case 3:
                    prefab.GetComponent<RectTransform>().anchoredPosition = new Vector2(-170, -25);
                    break;
                case 4:
                    prefab.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -25);
                    break;
                case 5:
                    prefab.GetComponent<RectTransform>().anchoredPosition = new Vector2(+170, -25);
                    break;
                case 6:
                    prefab.GetComponent<RectTransform>().anchoredPosition = new Vector2(-170, 100);
                    break;
                case 7:
                    prefab.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 100);
                    break;
                case 8:
                    prefab.GetComponent<RectTransform>().anchoredPosition = new Vector2(170, 100);
                    break;

            }

            name = String.Empty;
            prefab = null;

        }

        #region Consisting of pin lines 

        pinImage = null;

        for (int i = 0; i < pinNumber; i++)
        {
            pinImage = Instantiate(Resources.Load("pinLine", typeof(Image))) as Image;
            pinImage.transform.SetParent(room.transform);
            pinImage.GetComponent<RectTransform>().ResetRectTransformation();
            pinImage.gameObject.GetComponent<Transform>().localScale = new Vector3(1, 1, 1);
            name = (i + 1).ToString() + pinImage.gameObject.name.Split('(')[0];
            pinImage.gameObject.name = name;
            pinImage.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, room.GetComponent<RectTransform>().offsetMax.y + 50);

            switch (i)
            {
                case 0:
                    pinImage.GetComponent<RectTransform>().anchoredPosition = new Vector2(-288, pinHigh);
                    break;
                case 1:
                    pinImage.GetComponent<RectTransform>().anchoredPosition = new Vector2(-192, pinHigh);
                    break;
                case 2:
                    pinImage.GetComponent<RectTransform>().anchoredPosition = new Vector2(-96, pinHigh);
                    break;
                case 3:
                    pinImage.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, pinHigh);
                    break;
                case 4:
                    pinImage.GetComponent<RectTransform>().anchoredPosition = new Vector2(96, pinHigh);
                    break;
                case 5:
                    pinImage.GetComponent<RectTransform>().anchoredPosition = new Vector2(192, pinHigh);
                    break;
                case 6:
                    pinImage.GetComponent<RectTransform>().anchoredPosition = new Vector2(288, pinHigh);
                    break;
            }

            name = String.Empty;
            prefab = null;
        }

        #endregion
    }

    private void create_pin6(GameObject room)
    {
        Button prefab;
        String name = String.Empty;
        int doorCount = 9;
        int pinNumber = 6;
        Image pinImage;

        prefab = null;

        for (int i = 0; i < doorCount; i++)
        {

            prefab = Instantiate(Resources.Load("door", typeof(Button))) as Button;
            prefab.transform.SetParent(room.transform);
            prefab.GetComponent<RectTransform>().ResetRectTransformation();
            prefab.GetComponent<flatDoor>().ParentType = this.MyRoomType;
            determineDoorSprites(prefab.GetComponent<flatDoor>(), this.MyRoomType);
            name = prefab.gameObject.name.Split('(')[0];
            prefab.gameObject.name = (i + 1).ToString() + name;

            switch (i)
            {
                case 0:
                    prefab.GetComponent<RectTransform>().anchoredPosition = new Vector2(-170, -150);
                    break;
                case 1:
                    prefab.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -150);
                    break;
                case 2:
                    prefab.GetComponent<RectTransform>().anchoredPosition = new Vector2(170, -150);
                    break;
                case 3:
                    prefab.GetComponent<RectTransform>().anchoredPosition = new Vector2(-170, -25);
                    break;
                case 4:
                    prefab.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -25);
                    break;
                case 5:
                    prefab.GetComponent<RectTransform>().anchoredPosition = new Vector2(+170, -25);
                    break;
                case 6:
                    prefab.GetComponent<RectTransform>().anchoredPosition = new Vector2(-170, 100);
                    break;
                case 7:
                    prefab.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 100);
                    break;
                case 8:
                    prefab.GetComponent<RectTransform>().anchoredPosition = new Vector2(170, 100);
                    break;

            }

            name = String.Empty;
            prefab = null;

        }

        #region Consisting of pin lines 

        pinImage = null;

        for (int i = 0; i < pinNumber; i++)
        {
            pinImage = Instantiate(Resources.Load("pinLine", typeof(Image))) as Image;
            pinImage.transform.SetParent(room.transform);
            pinImage.GetComponent<RectTransform>().ResetRectTransformation();
            pinImage.gameObject.GetComponent<Transform>().localScale = new Vector3(1, 1, 1);
            name = (i + 1).ToString() + pinImage.gameObject.name.Split('(')[0];
            pinImage.gameObject.name = name;
            pinImage.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, room.GetComponent<RectTransform>().offsetMax.y + 50);

            switch (i)
            {
                case 0:
                    pinImage.GetComponent<RectTransform>().anchoredPosition = new Vector2(-240, pinHigh);
                    break;
                case 1:
                    pinImage.GetComponent<RectTransform>().anchoredPosition = new Vector2(-144, pinHigh);
                    break;
                case 2:
                    pinImage.GetComponent<RectTransform>().anchoredPosition = new Vector2(-48, pinHigh);
                    break;
                case 3:
                    pinImage.GetComponent<RectTransform>().anchoredPosition = new Vector2(48, pinHigh);
                    break;
                case 4:
                    pinImage.GetComponent<RectTransform>().anchoredPosition = new Vector2(144, pinHigh);
                    break;
                case 5:
                    pinImage.GetComponent<RectTransform>().anchoredPosition = new Vector2(240, pinHigh);
                    break;
            }

            name = String.Empty;
            prefab = null;
        }

        #endregion
    }

    private void create_pin5(GameObject room)
    {
        Button prefab;
        String name = String.Empty;
        int doorCount = 9;
        int pinNumber = 5;
        Image pinImage;
    

        prefab = null;

        for (int i = 0; i < doorCount; i++)
        {

            prefab = Instantiate(Resources.Load("door", typeof(Button))) as Button;
            prefab.transform.SetParent(room.transform);
            prefab.GetComponent<RectTransform>().ResetRectTransformation();
            prefab.GetComponent<flatDoor>().ParentType = this.MyRoomType;
            determineDoorSprites(prefab.GetComponent<flatDoor>(), this.MyRoomType);
            name = prefab.gameObject.name.Split('(')[0];
            prefab.gameObject.name = (i + 1).ToString() + name;

            switch (i)
            {
                case 0:
                    prefab.GetComponent<RectTransform>().anchoredPosition = new Vector2(-170, -150);
                    break;
                case 1:
                    prefab.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -150);
                    break;
                case 2:
                    prefab.GetComponent<RectTransform>().anchoredPosition = new Vector2(170, -150);
                    break;
                case 3:
                    prefab.GetComponent<RectTransform>().anchoredPosition = new Vector2(-170, -25);
                    break;
                case 4:
                    prefab.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -25);
                    break;
                case 5:
                    prefab.GetComponent<RectTransform>().anchoredPosition = new Vector2(+170, -25);
                    break;
                case 6:
                    prefab.GetComponent<RectTransform>().anchoredPosition = new Vector2(-170, 100);
                    break;
                case 7:
                    prefab.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 100);
                    break;
                case 8:
                    prefab.GetComponent<RectTransform>().anchoredPosition = new Vector2(170, 100);
                    break;

            }

            name = String.Empty;
            prefab = null;

        }

        #region Consisting of pin lines 

        pinImage = null;

        for (int i = 0; i < pinNumber; i++)
        {
            pinImage = Instantiate(Resources.Load("pinLine", typeof(Image))) as Image;
            pinImage.transform.SetParent(room.transform);
            pinImage.GetComponent<RectTransform>().ResetRectTransformation();
            pinImage.gameObject.GetComponent<Transform>().localScale = new Vector3(1, 1, 1);
            name = (i+1).ToString() + pinImage.gameObject.name.Split('(')[0];
            pinImage.gameObject.name = name;
            pinImage.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, room.GetComponent<RectTransform>().offsetMax.y + 50);

            switch (i)
            {
                case 0:
                    pinImage.GetComponent<RectTransform>().anchoredPosition = new Vector2(-192, pinHigh);
                    break;
                case 1:
                    pinImage.GetComponent<RectTransform>().anchoredPosition = new Vector2(-96, pinHigh);
                    break;
                case 2:
                    pinImage.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, pinHigh);
                    break;
                case 3:
                    pinImage.GetComponent<RectTransform>().anchoredPosition = new Vector2(96, pinHigh);
                    break;
                case 4:
                    pinImage.GetComponent<RectTransform>().anchoredPosition = new Vector2(192, pinHigh);
                    break;
            }

            name = String.Empty;
            prefab = null;
        }

        #endregion
    }

    private void create_dameOption(GameObject room)
    {
        Button prefab;
        String name = String.Empty;
        int doorCount = 2;
        Image errorLine;

        prefab = null;

        for (int i = 0; i < doorCount; i++)
        {

            prefab = Instantiate(Resources.Load("door", typeof(Button))) as Button;
            prefab.transform.SetParent(room.transform);
            prefab.GetComponent<RectTransform>().ResetRectTransformation();
            prefab.GetComponent<flatDoor>().ParentType = this.MyRoomType;
            determineDoorSprites(prefab.GetComponent<flatDoor>(), this.MyRoomType);
            name = prefab.gameObject.name.Split('(')[0];
            prefab.gameObject.name = (i + 1).ToString() + name;

            switch (i)
            {
                case 0:
                    prefab.GetComponent<RectTransform>().anchoredPosition = new Vector2(-85, 0);
                    break;
                case 1:
                    prefab.GetComponent<RectTransform>().anchoredPosition = new Vector2(+85, 0);
                    break;

            }

            name = String.Empty;
            prefab = null;

        }

        #region Consisting of error lines 

        bool IsEnabledErrorLine = false;
    
        for (int k = 0; k < this.GetComponentsInChildren<Image>().Length; k++)
        {
            if (this.GetComponentsInChildren<Image>()[k].gameObject.CompareTag("errorLine"))
            {
                IsEnabledErrorLine = true;
                break;
            }
        }

        if (IsEnabledErrorLine)
        {
            return;
        }

        errorLine = null;

        errorLine = Instantiate(Resources.Load("errorLine", typeof(Image))) as Image;
        errorLine.transform.SetParent(room.transform);
        errorLine.GetComponent<RectTransform>().ResetRectTransformation();
        errorLine.gameObject.GetComponent<Transform>().localScale = new Vector3(1, 1, 1);
        errorLine.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, room.GetComponent<RectTransform>().offsetMax.y + 50);
        name = 1+errorLine.gameObject.name.Split('(')[0];
        errorLine.gameObject.name = name;

        errorLine = null;

        errorLine = Instantiate(Resources.Load("errorLine", typeof(Image))) as Image;
        errorLine.transform.SetParent(room.transform);
        errorLine.GetComponent<RectTransform>().ResetRectTransformation();
        errorLine.gameObject.GetComponent<Transform>().localScale = new Vector3(1, 1, 1);
        errorLine.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, room.GetComponent<RectTransform>().offsetMax.y + 75);
        name = 2+errorLine.gameObject.name.Split('(')[0];
        errorLine.gameObject.name = name;

        errorLine = null;

        errorLine = Instantiate(Resources.Load("errorLine", typeof(Image))) as Image;
        errorLine.transform.SetParent(room.transform);
        errorLine.GetComponent<RectTransform>().ResetRectTransformation();
        errorLine.gameObject.GetComponent<Transform>().localScale = new Vector3(1, 1, 1);
        errorLine.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, room.GetComponent<RectTransform>().offsetMax.y + 100);
        name = 3+errorLine.gameObject.name.Split('(')[0];
        errorLine.gameObject.name = name;

        errorLine = null;

        #endregion

    }

    private void create_signOption(GameObject room)
    {

        // It will be placed sign on one of the buttons that contain wrong possibility in the wayClass .
        /* DONT FORGET !!! */

        Button prefab;
        String name = String.Empty;

        // Bu sprite değişecek . Ersen Bey verdiği zaman . todo Sprite

        Image signOption;

        int randomCorruptionNumber = UnityEngine.Random.Range(0,4);

        int doorCount = 4;

        prefab = null;

        for (int i = 0; i < doorCount; i++)
        {

            prefab = Instantiate(Resources.Load("door", typeof(Button))) as Button;
            prefab.transform.SetParent(room.transform);
            prefab.GetComponent<flatDoor>().ParentType = this.MyRoomType;
            prefab.GetComponent<RectTransform>().ResetRectTransformation();
            name = prefab.gameObject.name.Split('(')[0];
            prefab.gameObject.name = (i + 1).ToString() + name;

            switch (i)
            {
                case 0:
                    prefab.GetComponent<RectTransform>().anchoredPosition = new Vector2(-255, 0);
                    break;
                case 1:
                    prefab.GetComponent<RectTransform>().anchoredPosition = new Vector2(-85, 0);
                    break;
                case 2:
                    prefab.GetComponent<RectTransform>().anchoredPosition = new Vector2(+85, 0);
                    break;
                case 3:
                    prefab.GetComponent<RectTransform>().anchoredPosition = new Vector2(+255, 0);
                    break;
            }

            if (i == randomCorruptionNumber)
            {
                signOption = Instantiate(Resources.Load("signOption", typeof(Image))) as Image;
                signOption.gameObject.transform.SetParent(prefab.transform);
                signOption.GetComponent<RectTransform>().ResetRectTransformation();
                prefab.gameObject.tag = "signoption";
            }

            determineDoorSprites(prefab.GetComponent<flatDoor>(), this.MyRoomType);
            name = String.Empty;
            prefab = null;

        }
    }

    private void create_beFast(GameObject room)
    {
        Button prefab;
        Image progressBar;
        String name = String.Empty;
        int doorCount = 2;

        prefab = null;

        for (int i = 0; i < doorCount; i++)
        {

            prefab = Instantiate(Resources.Load("door", typeof(Button))) as Button;
            prefab.transform.SetParent(room.transform);
            prefab.GetComponent<RectTransform>().ResetRectTransformation();
            determineDoorSprites(prefab.GetComponent<flatDoor>(), this.MyRoomType);
            prefab.GetComponent<flatDoor>().ParentType = this.MyRoomType;
            name = prefab.gameObject.name.Split('(')[0];
            prefab.gameObject.name = (i + 1).ToString() + name;

            switch (i)
            {
                case 0:
                    prefab.GetComponent<RectTransform>().anchoredPosition = new Vector2(-85, 0);
                    break;
                case 1:
                    prefab.GetComponent<RectTransform>().anchoredPosition = new Vector2(+85, 0);
                    break;

            }

            name = String.Empty;
            prefab = null;

        }

        #region Consisting of progress bar and its background

        progressBar = null;

        progressBar = Instantiate(Resources.Load("progressBar", typeof(Image))) as Image;
        progressBar.transform.SetParent(room.transform);
        progressBar.GetComponent<RectTransform>().ResetRectTransformation();
        progressBar.gameObject.GetComponent<Transform>().localScale = new Vector3(1, 1, 1);
        progressBar.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, room.GetComponent<RectTransform>().offsetMax.y + 50);
        name = progressBar.gameObject.name.Split('(')[0];
        progressBar.gameObject.name = name;
        progressBar = null;

        #endregion
    }

    private void create_dontMistake(GameObject room)
    {
        Button prefab;
        String name = String.Empty;
        int doorCount = 2;

        prefab = null;

        for (int i = 0; i < doorCount; i++)
        {

            prefab = Instantiate(Resources.Load("door_dontdothat", typeof(Button))) as Button;
            prefab.transform.SetParent(room.transform);
            prefab.GetComponent<RectTransform>().ResetRectTransformation();
            determineDoorSprites(prefab.GetComponent<flatDoor>(), this.MyRoomType);
            prefab.GetComponent<flatDoor>().ParentType = this.MyRoomType;
            name = prefab.gameObject.name.Split('(')[0];
            name = prefab.gameObject.name.Split('_')[0];
            prefab.gameObject.name = (i + 1).ToString() + name;

            switch (i)
            {
                case 0:
                    prefab.GetComponent<RectTransform>().anchoredPosition = new Vector2(-85, 0);
                    break;
                case 1:
                    prefab.GetComponent<RectTransform>().anchoredPosition = new Vector2(+85, 0);
                    break;

            }

            name = String.Empty;
            prefab = null;

        }
    }

    private void create_devoted(GameObject room)
    {
        Button prefab;
        String name = String.Empty;
        int doorCount = 4;
        Sprite corruption = Instantiate(Resources.Load("Images/port_corrupted", typeof(Sprite))) as Sprite;
        int randomCorruptionNumber = UnityEngine.Random.Range(0, 4);
        prefab = null;

        for (int i = 0; i < doorCount; i++)
        {

            prefab = Instantiate(Resources.Load("door", typeof(Button))) as Button;
            prefab.transform.SetParent(room.transform);
            prefab.GetComponent<flatDoor>().ParentType = this.MyRoomType;
           
            prefab.GetComponent<RectTransform>().ResetRectTransformation();
            name = prefab.gameObject.name.Split('(')[0];
            prefab.gameObject.name = (i + 1).ToString() + name;

            switch (i)
            {
                case 0:
                    prefab.GetComponent<RectTransform>().anchoredPosition = new Vector2(-255, 0);
                    break;
                case 1:
                    prefab.GetComponent<RectTransform>().anchoredPosition = new Vector2(-85, 0);
                    break;
                case 2:
                    prefab.GetComponent<RectTransform>().anchoredPosition = new Vector2(+85, 0);
                    break;
                case 3:
                    prefab.GetComponent<RectTransform>().anchoredPosition = new Vector2(+255, 0);
                    break;
            }

            if(i == randomCorruptionNumber)
            {
                prefab.GetComponent<Image>().sprite = corruption;
                prefab.gameObject.tag = "corruption";
            }

            determineDoorSprites(prefab.GetComponent<flatDoor>(), this.MyRoomType);

            name = String.Empty;
            prefab = null;

        }
    }

    private void create_doRight(GameObject room)
    {
        Button prefab;
        String name = String.Empty;
        int doorCount = 2;

        prefab = null;

        for (int i = 0; i < doorCount; i++)
        {

            prefab = Instantiate(Resources.Load("door_cross", typeof(Button))) as Button;
            prefab.transform.SetParent(room.transform);
            prefab.GetComponent<flatDoor>().ParentType = this.MyRoomType;
            determineDoorSprites(prefab.GetComponent<flatDoor>(), this.MyRoomType);
            prefab.GetComponent<RectTransform>().ResetRectTransformation();
            name = prefab.gameObject.name.Split('(')[0];
            name = prefab.gameObject.name.Split('_')[0];
            prefab.gameObject.name = (i + 1).ToString() + name;

            switch (i)
            {
                case 0:
                    prefab.GetComponent<RectTransform>().anchoredPosition = new Vector2(-85, 0);
                    break;
                case 1:
                    prefab.GetComponent<RectTransform>().anchoredPosition = new Vector2(+85, 0);
                    break;

            }

            name = String.Empty;
            prefab = null;

        }
    }

    private void create_doubleRight(GameObject room)
    {
        Button prefab;
        String name = String.Empty;
        Image optionIndicator;
        int doorCount = 4;

        prefab = null;

        for (int i = 0; i < doorCount; i++)
        {

            prefab = Instantiate(Resources.Load("door", typeof(Button))) as Button;
            prefab.transform.SetParent(room.transform);
            prefab.GetComponent<flatDoor>().ParentType = this.MyRoomType;
            determineDoorSprites(prefab.GetComponent<flatDoor>(), this.MyRoomType);
            prefab.GetComponent<RectTransform>().ResetRectTransformation();
            name = prefab.gameObject.name.Split('(')[0];
            prefab.gameObject.name = (i + 1).ToString() + name;

            switch (i)
            {
                case 0:
                    prefab.GetComponent<RectTransform>().anchoredPosition = new Vector2(-85,60);
                    break;
                case 1:
                    prefab.GetComponent<RectTransform>().anchoredPosition = new Vector2(85, 60);
                    break;
                case 2:
                    prefab.GetComponent<RectTransform>().anchoredPosition = new Vector2(-85,-60);
                    break;
                case 3:
                    prefab.GetComponent<RectTransform>().anchoredPosition = new Vector2(85, -60);
                    break;
            }

            name = String.Empty;
            prefab = null;

        }

        #region optionindicators is creating below

        optionIndicator = null;

        optionIndicator = Instantiate(Resources.Load("left_0", typeof(Image))) as Image;
        optionIndicator.transform.SetParent(room.transform);
        optionIndicator.GetComponent<RectTransform>().ResetRectTransformation();
        optionIndicator.gameObject.name = optionIndicator.gameObject.name.Split('(')[0];
        optionIndicator.gameObject.transform.localScale = new Vector3(1, 1, 1);
        optionIndicator.GetComponent<RectTransform>().anchoredPosition = new Vector2(-35, 175);

        optionIndicator = null;

        optionIndicator = Instantiate(Resources.Load("right_0", typeof(Image))) as Image;
        optionIndicator.transform.SetParent(room.transform);
        optionIndicator.GetComponent<RectTransform>().ResetRectTransformation();
        optionIndicator.gameObject.name = optionIndicator.gameObject.name.Split('(')[0];
        optionIndicator.gameObject.transform.localScale = new Vector3(1, 1, 1);
        optionIndicator.GetComponent<RectTransform>().anchoredPosition = new Vector2(35, 175);

        optionIndicator = null;

        #endregion

    }

    private void create_checkPoint(GameObject room)
    {

        Button prefab;
        Image checkPoint;
        String name = String.Empty;
        int doorCount = 4;

        prefab = null;
      
        for (int i = 0; i < doorCount; i++)
        {

            prefab = Instantiate(Resources.Load("door", typeof(Button))) as Button;
            prefab.transform.SetParent(room.transform);
            prefab.GetComponent<flatDoor>().ParentType = this.MyRoomType;
            determineDoorSprites(prefab.GetComponent<flatDoor>(), this.MyRoomType);
            prefab.GetComponent<RectTransform>().ResetRectTransformation();
            name = prefab.gameObject.name.Split('(')[0];
            prefab.gameObject.name = (i + 1).ToString() + name;

            switch (i)
            {
                case 0:
                    prefab.GetComponent<RectTransform>().anchoredPosition = new Vector2(-255, 0);
                    break;
                case 1:
                    prefab.GetComponent<RectTransform>().anchoredPosition = new Vector2(-85, 0);
                    break;
                case 2:
                    prefab.GetComponent<RectTransform>().anchoredPosition = new Vector2(+85, 0);
                    break;
                case 3:
                    prefab.GetComponent<RectTransform>().anchoredPosition = new Vector2(+255, 0);
                    break;
            }

            name = String.Empty;
            prefab = null;

        }

        #region checkpoint is creating below

        checkPoint = null;
        checkPoint = Instantiate(Resources.Load("checkPoint", typeof(Image))) as Image;
        checkPoint.transform.SetParent(room.transform);
        checkPoint.GetComponent<RectTransform>().ResetRectTransformation();
        checkPoint.gameObject.name = checkPoint.gameObject.name.Split('(')[0];
        checkPoint.gameObject.transform.localScale= new Vector3(1, 1, 1);
        checkPoint.GetComponent<RectTransform>().anchoredPosition = new Vector2(-room.GetComponent<RectTransform>().offsetMax.x - 50,
                                                                                 room.GetComponent<RectTransform>().offsetMax.y + 50);

        checkPoint = null;

        #endregion
    }

    private void create_tripleOption(GameObject room,string prefabName)
    {
        Button prefab;
        String name = String.Empty;
        int doorCount = 3;

        prefab = null;

        for (int i = 0; i < doorCount; i++)
        {

            prefab = Instantiate(Resources.Load(prefabName, typeof(Button))) as Button;
            prefab.transform.SetParent(room.transform);
            prefab.GetComponent<flatDoor>().ParentType = this.MyRoomType;
            determineDoorSprites(prefab.GetComponent<flatDoor>(),this.MyRoomType);
            prefab.GetComponent<RectTransform>().ResetRectTransformation();
            name = prefab.gameObject.name.Split('(')[0];
            prefab.gameObject.name = (i + 1).ToString() + name;

            switch (i)
            {
                case 0:
                    prefab.GetComponent<RectTransform>().anchoredPosition = new Vector2(-170, 0);
                    break;
                case 1:
                    prefab.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
                    break;
                case 2:
                    prefab.GetComponent<RectTransform>().anchoredPosition = new Vector2(+170, 0);
                    break;
            }

            name = String.Empty;
            prefab = null;

        }
    }

    private void create_quadOption(GameObject room,string PrefabName)
    {
        Button prefab;
        String name = String.Empty;
        int doorCount = 4;

        prefab = null;

        for (int i = 0; i < doorCount; i++)
        {

            prefab = Instantiate(Resources.Load(PrefabName, typeof(Button))) as Button;
            prefab.transform.SetParent(room.transform);
            prefab.GetComponent<flatDoor>().ParentType = this.MyRoomType;
            determineDoorSprites(prefab.GetComponent<flatDoor>(), this.MyRoomType);
            prefab.GetComponent<RectTransform>().ResetRectTransformation();
            name = prefab.gameObject.name.Split('(')[0];
            prefab.gameObject.name = (i + 1).ToString() + name;

            switch (i)
            {
                case 0:
                    prefab.GetComponent<RectTransform>().anchoredPosition = new Vector2(-255, 0);
                    break;
                case 1:
                    prefab.GetComponent<RectTransform>().anchoredPosition = new Vector2(-85, 0);
                    break;
                case 2:
                    prefab.GetComponent<RectTransform>().anchoredPosition = new Vector2(+85, 0);
                    break;
                case 3:
                    prefab.GetComponent<RectTransform>().anchoredPosition = new Vector2(+255, 0);
                    break;
            }

            name = String.Empty;
            prefab = null;

        }

    }

    private void create_binaryOption(GameObject room)
    {
        Button prefab;
        String name = String.Empty;
        int doorCount = 2;

        prefab = null;

        for(int i = 0; i< doorCount; i++)
        {

            prefab = Instantiate(Resources.Load("door", typeof(Button))) as Button;
            prefab.transform.SetParent(room.transform);
            prefab.GetComponent<RectTransform>().ResetRectTransformation();
            prefab.GetComponent<flatDoor>().ParentType = this.MyRoomType;
            determineDoorSprites(prefab.GetComponent<flatDoor>(),this.MyRoomType);
            name = prefab.gameObject.name.Split('(')[0];
            prefab.gameObject.name = (i + 1).ToString() + name;

            switch (i)
            {
                case 0:
                    prefab.GetComponent<RectTransform>().anchoredPosition = new Vector2(-85, 0);
                    break;
                case 1:
                    prefab.GetComponent<RectTransform>().anchoredPosition = new Vector2(+85, 0);
                    break;

            }

            name = String.Empty;
            prefab = null;

        }

    }

    #endregion

    public void Update()
    {
        #region update behaviour for beFast

        if (MyRoomType == roomType.beFast)
        {

            switch (this.GetComponentInChildren<progressTrigger>().MyConditionProgress)
            {
                case progressTrigger.conditionProgess.init:

                    this.setAllChildDoor(this.gameObject);

                    this.setChildValue();

                    this.GetComponentInChildren<progressTrigger>().MyConditionProgress = progressTrigger.conditionProgess.zero;

                    break;

                case progressTrigger.conditionProgess.zero:

                    break;

                case progressTrigger.conditionProgess.triple:

                    this.setAllChildDoor(this.gameObject);

                    this.setChildValue();

                    this.GetComponentInChildren<progressTrigger>().MyConditionProgress = progressTrigger.conditionProgess.zero;

                    break;

                case progressTrigger.conditionProgess.hex:

                    this.setAllChildDoor(this.gameObject);

                    this.setChildValue();

                    this.GetComponentInChildren<progressTrigger>().MyConditionProgress = progressTrigger.conditionProgess.zero;

                    break;

                case progressTrigger.conditionProgess.finish:

                    this.GetComponentInChildren<progressTrigger>().MyConditionProgress = progressTrigger.conditionProgess.finish;

                    break;
            }

        }

        #endregion

        #region lateupdate behaviour for dameOption

        else if (MyRoomType == roomType.dameOption )
        {

            switch (myDameProgress)
            {
                case DameProgress.init:

                    for (int m = 0; m < _doors.Count; m++)
                    {
                        try
                        {
                            Destroy(_doors[m].gameObject);
                        }
                        catch (MissingReferenceException e)
                        {
                            continue;
                        }

                    }

                    this._myRoomType = roomType.dameOption;

                    this.configure_Room(this.gameObject);

                    break;  

                case DameProgress.zero:

                    break;
                case DameProgress.triple:

                    for (int m = 0; m < _doors.Count; m++)
                    {
                        try
                        {
                            Destroy(_doors[m].gameObject);
                        }
                        catch (MissingReferenceException e)
                        {
                            continue;
                        }

                    }

                    this._myRoomType = roomType.tripleOption;

                    this.configure_Room(this.gameObject);

                    this._myRoomType = roomType.dameOption;

                    break;

                case DameProgress.quad:

                    for (int m = 0; m < _doors.Count; m++)
                    {
                        try
                        {
                            Destroy(_doors[m].gameObject);
                        }
                        catch (MissingReferenceException e)
                        {
                            continue;
                        }

                    }

                    this._myRoomType = roomType.quadOption;

                    this.configure_Room(this.gameObject);

                    this._myRoomType = roomType.dameOption;

                    break;

                case DameProgress.finish:

                    myDameProgress = DameProgress.finish;

                    break;
            }

        }

        #endregion

    // For Answers to help developer  // It will be closed when upgrade ends .

      for(int i = 0; i < this._doors.Count; i++)
        {
            if(_doors[i].Decision == 2 )
            {
                Debug.Log(_doors[i].gameObject.name);
            }
        }
    }

    public void LateUpdate()
    {
        #region lateupdate behaviour for beFast

        if (MyRoomType == roomType.beFast)
        {

            switch (this.GetComponentInChildren<progressTrigger>().MyConditionProgress)
            {
                case progressTrigger.conditionProgess.init:

                    for (int m = 0; m < _doors.Count; m++)
                    {
                        try
                        {
                            Destroy(_doors[m].gameObject);
                        }
                        catch (MissingReferenceException e)
                        {
                            continue;
                        }

                    }

                    Destroy(this.GetComponentInChildren<progressTrigger>().gameObject);

                    this._myRoomType = roomType.beFast;

                    this.configure_Room(this.gameObject);

                    break;
                case progressTrigger.conditionProgess.zero:

                    break;

                case progressTrigger.conditionProgess.triple:

                    for (int m = 0; m < _doors.Count; m++)
                    {
                        try
                        {
                            Destroy(_doors[m].gameObject);
                        }
                        catch(MissingReferenceException e)
                        {
                            continue;
                        }
                     
                    }

                    this._myRoomType = roomType.tripleOption;

                    this.configure_Room(this.gameObject);

                    this._myRoomType = roomType.beFast;

                    break;

                case progressTrigger.conditionProgess.hex:

                    for (int m = 0; m < _doors.Count; m++)
                    {

                        try
                        {
                            Destroy(_doors[m].gameObject);
                        }
                        catch (MissingReferenceException e)
                        {
                            continue;
                        }

                    }

                    this._myRoomType = roomType.quadOption;

                    this.configure_Room(this.gameObject);

                    this._myRoomType = roomType.beFast;

                    break;

                case progressTrigger.conditionProgess.finish:

                    this.GetComponentInChildren<progressTrigger>().MyConditionProgress = progressTrigger.conditionProgess.finish;

                    break;
            }

        }

        #endregion

        #region lateupdate behaviour for dameOption

        else if (MyRoomType == roomType.dameOption )
        {

            switch (myDameProgress)
            {
                case DameProgress.init:

                    this.setAllChildDoor(this.gameObject);

                    this.setChildValue();

                    for (int k = 0; k < this.GetComponentsInChildren<Image>().Length; k++)
                    {
                        if (this.GetComponentsInChildren<Image>()[k].gameObject.CompareTag("errorLine"))
                        {
                            this.GetComponentsInChildren<Image>()[k].sprite = (Sprite)Resources.Load("Images/third_n0", typeof(Sprite));
                        }
                    }

                    if (IsDameTrueFalse)
                    {
                        performTrueScenarioForNormal();
                    }
                    else
                    {
                        performFalseScenarioForNormal();
                    }
                
                    myDameProgress = DameProgress.zero;

                    break;
                case DameProgress.zero:
                    break;
                case DameProgress.triple:

                    this.setAllChildDoor(this.gameObject);

                    this.setChildValue();

                    this.setDoorsRoomTypeForDame();

                    myDameProgress = DameProgress.zero;

                    break;
                case DameProgress.quad:

                    this.setAllChildDoor(this.gameObject);

                    this.setChildValue();

                    this.setDoorsRoomTypeForDame();

                    myDameProgress = DameProgress.finish;

                    break;
                case DameProgress.finish:

                    break;
            }

        }

        #endregion

    }

    // Aşağıdaki set metodları stageGenerator 'de çalışacak.
    public void setAllChildDoor(GameObject room)
    {

        if (_doors == null)
        {
            _doors = new List<flatDoor>();
        }
        else
        {
            this._doors.Clear();

            for (int i = 0; i < room.GetComponentsInChildren<flatDoor>(true).Length; i++)
            {

                _doors.Add(room.GetComponentsInChildren<flatDoor>(true)[i]);

            }
        }
    }

    public void setDoorsRoomTypeForDame()
    {

        for (int i = 0; i < this.gameObject.GetComponentsInChildren<flatDoor>(true).Length; i++)
        {

            this.gameObject.GetComponentsInChildren<flatDoor>(true)[i].ParentType = this.MyRoomType;
           
        }

    }

    public void setChildValue()
    {
        UtilityClass.GenerateRandom(_doors.Count, ref _doors);
    }

    public void setChildForPın()
    {
        UtilityClass.GenerateRandom(_doors.Count, ref _doors);
    }
    public void setChildForSorted()
    {
        UtilityClass.GenerateRandom(_doors.Count, ref _doors);
    }

    #region Button Click Handling

    // Aşağıdaki metodları buttonlar çalıştıracak.

    #region Bolum içinde geçen aksiyonlar


    #region Ortak Davranışlar 
    public void PerformButtonForNormal(int index,Button button)
    {
        if (index == _trueIndex)
        {
            performTrueScenarioForNormal();
        }

        else
        {
            performFalseScenarioForNormal();
        }
          
    }

    private void performFalseScenarioForNormal()
    {
        GameObject previosObject = null;
        stageGenerator stageGenerator = GameObject.Find("StageGenerator").GetComponent<stageGenerator>();
        String thisRoomName = this.gameObject.name;
        bool IsCheckPoint = false;
        List<Room> roomList = stageGenerator.get_rooms();
        int nextRoomIndex = 0;
        Room thisRoom = this.gameObject.GetComponent<Room>() as Room;

		if (parentObject.IsNeverGoBack == true) {

			return;
		}

		// 3 hak itemi etkinse bu kod bloğunu çalıştır.
		if (LockShield == true) {

			// Bir tane Item azalt

			ItemShieldParent shieldParent = GameObject.FindGameObjectWithTag ("itemshieldparent").GetComponent<ItemShieldParent>();

			shieldParent.decreaseLastShield ();

			return;
		}

        // Eğer ilk odada bulunuyorsan , orada kalırsın . 

        if (this.RoomNumber == 0 && MyRoomType != roomType.beFast)
        {

            if(this.RoomNumber == 0) { 

                // Kullanıcı 1. odadayken dahi fadein animasyonu calısmalı.

                this.gameObject.GetComponent<fadeIn>().performFade(this.gameObject.GetComponent<Image>(), _doors);
            }


            ///////////////////////////////////////////////////////////////////////////////////

            // Can barı için durum belirleniyor.

            // Bunlar aşamalı odalar olduğu için health bar'ını kırmızıya cevirmiyoruz.
            if (MyRoomType == roomType.pin5 ||
               MyRoomType == roomType.pin6 ||
               MyRoomType == roomType.pin7 ||
               MyRoomType == roomType.pin8 ||
               MyRoomType == roomType.pin9 ||
               MyRoomType == roomType.sortbyItems ||
               MyRoomType == roomType.doubleRight)
            {

            }
            else
            {
                GameObject.FindGameObjectWithTag("healthbarparent").GetComponent<HealthBarParent>().convertBlueToRed();
                return;
            }
           
        }

        ///////////////////////////////////////////////////////////////////////////////////

        ///////////////////////////////////////////////////////////////////////////////////

        // Tüm kapıların tagleri 'door' a cekiliyor .

        make_DoorsTagToDoor();

        // CheckPoint ' e geri dönme olayı aşağıdaki case ' de çözüldü .

        #region CheckPoint Yada En Başa Dönme

        // UI level indicator için durum belirleniyor.

		if (GameObject.Find ("ItemIndicatorParent")!=null) {

			_itemIndicatorParent = GameObject.Find ("ItemIndicatorParent").GetComponent<ItemIndicatorParent> ();


			if (_itemIndicatorParent != null) {

				_itemIndicatorParent.wrongSituation(_myRoomSituation,this._roomNumber);
			}

		}

        ///////////////////////////////////////////////////////////////////////////////////

        for (int k = this.RoomNumber - 1; k >= 0; k--)
        {
            if(roomList[k].MyRoomType == roomType.checkPoint)
            {
                IsCheckPoint = true;
                nextRoomIndex = k;
                break;
            }

        }

        if (this.MyRoomType == roomType.checkPoint)
        {
            previosObject = this.gameObject;
            this.gameObject.SetActive(false);
            previosObject.SetActive(true);
        }

        else
        {
            if (IsCheckPoint)
            {
                previosObject = stageGenerator.RoomObjectList[nextRoomIndex];
                this.gameObject.SetActive(false);
                previosObject.SetActive(true);

            }
            else
            {

                // Eğer şuan ki oda checkpoint ise,  0'a döndürme

                previosObject = stageGenerator.RoomObjectList[0];

                // LevelIndicator ilk başladığı yere geri dönüyor.

                //    this.parentObject.resetItemHoles(previosObject.GetComponent<Room>());
                this.gameObject.SetActive(false);
                previosObject.SetActive(true);
            }
        }

        ///////////////////////////////////////////////////////////////////////////////////

        // Can barı için durum belirleniyor.

        var healthbarParent = GameObject.FindGameObjectWithTag("healthbarparent");
        healthbarParent.GetComponent<HealthBarParent>().convertBlueToRed();

        ///////////////////////////////////////////////////////////////////////////////////

        #endregion
    }

    private void performTrueScenarioForNormal()
    {
        GameObject nextObject = null;
        stageGenerator stageGenerator = GameObject.Find("StageGenerator").GetComponent<stageGenerator>();
        String thisRoomName = this.gameObject.name;
        bool IsEnabled = false;
        List<Room> roomList = stageGenerator.get_rooms();
        int nextRoomIndex = 0;
        Room thisRoom = this.gameObject.GetComponent<Room>() as Room;

        // Tüm kapıların tagleri 'door' a cekiliyor .

        make_DoorsTagToDoor();

        // Bu for döngüsünde ise yeni bölüme geçme yada 
        // bir sonraki odaya geçme case'yi belirleniyor . 

        for (int k = 0; k < roomList.Count; k++)
        {
            if(roomList[k].RoomNumber == thisRoom.RoomNumber)
            {
                nextRoomIndex = k ;

                if(nextRoomIndex == (roomList.Count-1))
                {
                    nextRoomIndex = 0;

                    IsEnabled = true;

                    break;

                }else
                {
                    nextRoomIndex = k + 1;

                    break;
                }
            }

        }

        // Yeni bölüme buradan geçiş yapılıyor . 

        if (nextRoomIndex == 0 &&
           IsEnabled == true)
        {
            performTrueNextStage();

            return;
        }

        // UI level indicator sprite değişiyor.

        _myRoomSituation = roomSituation.solved;

//        this._myIndicatorHole.change_first_level_indicator_sprite("off_solved");

  //      this._myIndicatorHole.change_itemIndicatorholeSolvedInf();

        //////////////////////////////////////////////////////////////////////////////////////////

        // Eğer yeni bölüme geçmeydiysen bir sonraki odaya geçersin .

        nextObject = stageGenerator.RoomObjectList[nextRoomIndex];

        this.gameObject.SetActive(false);

        nextObject.SetActive(true);

        nextObject.GetComponent<Room>().IsBlink1 = true;  

    }

    #endregion

    #region Pin için Davranışlar

    public void PerformButtonForPin(int index,Button button)
    {

        int pinCount = 0;
        bool IsNextStageEnabled = false;
        int pinLineCounter = 0;

        // IsClickable aynı buttona tekrar tıklanamaz ( Pin ve Sort özel odaları için )

        if (button.gameObject.GetComponent<flatDoor>().IsClickable == false)
        {
            return;
        }

        // Pinleri ve indexlerini tutan Dictionary

        if (_pinDict == null)
        {
            _pinDict = new Dictionary<int, Button>();
        }

        // Üstteki Pin Line'lari ve indexlerini tutan Dictionary

        if (_pinLineDict == null)
        {

            _pinLineDict = new Dictionary<int, Image>();

            for (int i = 0; i < this.gameObject.GetComponentsInChildren<Image>().Length; i++)
            {
                if (this.gameObject.GetComponentsInChildren<Image>()[i].CompareTag("pinline"))
                {
                   
                    pinLineCounter++;

                    _pinLineDict.Add(pinLineCounter, this.gameObject.GetComponentsInChildren<Image>()[i]);
                }

            }
        }

        // Pin Numarasına göre pin sayısı belirleniyor .

        switch (this.MyRoomType)
        {
            case roomType.pin5:
                pinCount = 5;
                break;
            case roomType.pin6:
                pinCount = 6;
                break;
            case roomType.pin7:
                pinCount = 7;
                break;
            case roomType.pin8:
                pinCount = 8;
                break;
            case roomType.pin9:
                pinCount = 9;
                break;
        }

        // Eğer İndex pin sayısından büyükse direk normal bir şekilde ilk odaya yada
        // CheckPoint ' e dönülüyor .

        if(index > pinCount)
        {
            PerformButtonForPinFalseScen(index, button, ref _pinDict,ref _pinLineDict);

            return;
        }

        // Eğer İndex pin sayısına eşitse bir sonraki level' a geçme hakkı kazanılıyor .

        else if( index == pinCount )
        {
            IsNextStageEnabled = true;
        }

        // Burada ise rutin pin bir artırılarak , dictionaryler dolduruluyor .

        if (( _pinDict.Count + 1 ) == index)
        {

            PerformButtonForPinTrueScen(index,button,ref _pinDict);

            button.gameObject.GetComponent<flatDoor>().IsClickable = false;

            // Burada ise bir sonraki bölüm okey ise bir sonraki bölüme geçiş yapılıyor .

            if (IsNextStageEnabled == true)
            {
                performTrueScenarioForNormal();

            }

        }
        else
        {

            // Eğer seçim yanlış ise direk normal bir şekilde ilk odaya yada
            // CheckPoint ' e dönülüyor .

            PerformButtonForPinFalseScen(index,button,ref _pinDict,ref _pinLineDict);
        }
    }

    /// <summary>
    /// Pin levelları için
    /// </summary>
    /// <param name="index"></param>
    /// <param name="button"></param>
    /// <param name="pinDict"></param>
    /// <param name="pinLineDict"></param>
    private void PerformButtonForPinFalseScen(int index,Button button,ref Dictionary<int,Button> pinDict,ref Dictionary<int,Image> pinLineDict)
    {
        performFalseScenarioForNormal();

        if (pinDict != null)
        {
            for (int m = 0; m < pinDict.Count; m++)
            {
                pinDict[m + 1].gameObject.GetComponent<Image>().sprite = (Sprite)Resources.Load("Images/port_0", typeof(Sprite));
                pinDict[m + 1].gameObject.GetComponent<flatDoor>().ChildBackGroundObject.GetComponent<Image>().sprite =
                pinDict[m + 1].gameObject.GetComponent<flatDoor>().normalBGSprite;
                pinDict[m + 1].gameObject.GetComponent<flatDoor>().IsClickable = true;
            }

            pinDict = null;

        }

        if (pinLineDict != null)
        {
            for (int k = 0; k < pinLineDict.Count; k++)
            {
                pinLineDict[k + 1].gameObject.GetComponent<Image>().sprite = (Sprite)Resources.Load("Images/pin_0", typeof(Sprite));
            }

            pinLineDict = null;
        }
    }


    /// <summary>
    /// Pin için Doğru Senaryo da GameObjectlerin Değişmesi
    /// </summary>
    /// <param name="index"></param>
    /// <param name="button"></param>
    /// <param name="pinDict"></param>
    private void PerformButtonForPinTrueScen(int index ,Button button, ref Dictionary<int, Button> pinDict)
    {
        pinDict.Add(index, button);

        pinDict[index].gameObject.GetComponent<Image>().sprite = (Sprite)Resources.Load("Images/port_y", typeof(Sprite));
        pinDict[index].gameObject.GetComponent<flatDoor>().ChildBackGroundObject.GetComponent<Image>().sprite = pinDict[index].gameObject.GetComponent<flatDoor>().trueBGSprite;
        GameObject.Find(pinDict.Count.ToString()+ "pinLine").GetComponent<Image>().sprite = (Sprite)Resources.Load("Images/pin_y", typeof(Sprite));

    }

    #endregion

    #region Sıra için Davranışlar

    public void PerformButtonForSıra(int index , Button button)
    {
        int pinCount = 4;
        bool IsNextStageEnabled = false;
        int pinLineCounter = 0;

        if (button.gameObject.GetComponent<flatDoor>().IsClickable == false)
        {
            return;
        }

        if (_pinDict == null )
        {
            _pinDict = new Dictionary<int, Button>();
        }


        if (_pinLineDict == null)
        {

            _pinLineDict = new Dictionary<int, Image>();

            for (int i = 0; i < this.gameObject.GetComponentsInChildren<Image>().Length; i++)
            {
                if (this.gameObject.GetComponentsInChildren<Image>()[i].CompareTag("pinline"))
                {

                    pinLineCounter++;

                    _pinLineDict.Add(pinLineCounter, this.gameObject.GetComponentsInChildren<Image>()[i]);
                }

            }
        }

         if (index == pinCount)
        {
            IsNextStageEnabled = true;
        }

        if ((_pinDict.Count + 1) == index)
        {
            PerformButtonForPinTrueScen(index, button,ref _pinDict);

            button.gameObject.GetComponent<flatDoor>().IsClickable = false;

            if (IsNextStageEnabled == true)
            {
                performTrueScenarioForNormal();
            }

        }
        else
        {
            PerformButtonForPinFalseScen(index, button, ref _pinDict, ref _pinLineDict);
        }

    }

    #endregion

    #region DoubleRight için Davranışlar

    public void PerformButtonForDoubleRight(int decision , Button button)
    {
        int extraTrueDecision = 3;
        int pinLineCounter = 0;


        if (button.gameObject.GetComponent<flatDoor>().IsClickable == false)
        {
            return;
        }

        if (_pinDict == null)
        {
            _pinDict = new Dictionary<int, Button>();
        }

        if (_pinLineDict == null)
        {

            _pinLineDict = new Dictionary<int, Image>();

            for (int i = 0; i < this.gameObject.GetComponentsInChildren<Image>().Length; i++)
            {
                if (this.gameObject.GetComponentsInChildren<Image>()[i].CompareTag("pinline"))
                {

                    pinLineCounter++;

                    _pinLineDict.Add(pinLineCounter, this.gameObject.GetComponentsInChildren<Image>()[i]);
                }

            }

        }

        if (decision == _trueIndex ||
            decision == extraTrueDecision)
        {

            button.gameObject.GetComponent<flatDoor>().IsClickable = false;
            PerformButtonForDoubleRightTrueScen(decision, button, ref _pinDict ,ref _pinLineDict);        
        }

        else
        {

            PerformButtonForDoubleRightFalseScen();        
        }
    }

    private void PerformButtonForDoubleRightFalseScen()
    {
        performFalseScenarioForNormal();

        if (_pinDict != null)
        {
            for (int m = 0; m < _pinDict.Count; m++)
            {
                _pinDict[m + 1].gameObject.GetComponent<Image>().sprite = (Sprite)Resources.Load("Images/port_0", typeof(Sprite));
                _pinDict[m + 1].gameObject.GetComponent<flatDoor>().ChildBackGroundObject.GetComponent<Image>().sprite =
                _pinDict[m + 1].gameObject.GetComponent<flatDoor>().normalBGSprite;
                _pinDict[m + 1].gameObject.GetComponent<flatDoor>().IsClickable = true;
            }

           _pinDict = null;

        }

        if (_pinLineDict != null)
        {
            _pinLineDict[1].GetComponent<Image>().sprite = (Sprite)Resources.Load("Images/left_0", typeof(Sprite));
            _pinLineDict[2].GetComponent<Image>().sprite = (Sprite)Resources.Load("Images/right_0", typeof(Sprite));
            _pinLineDict = null;
        }
    }

    private void PerformButtonForDoubleRightTrueScen(int index, Button button,ref Dictionary<int, Button> pinDict,ref Dictionary<int,Image> pinLineDict)
    {

        switch (pinDict.Count)
        {
            case 0:

                pinDict.Add(1, button);

                pinDict[1].gameObject.GetComponent<Image>().sprite = (Sprite)Resources.Load("Images/port_y", typeof(Sprite));
                pinLineDict[1].GetComponent<Image>().sprite = (Sprite)Resources.Load("Images/left_y", typeof(Sprite));

                break;

            case 1:

                pinDict.Add(2, button);

                pinDict[2].gameObject.GetComponent<Image>().sprite = (Sprite)Resources.Load("Images/port_y", typeof(Sprite));
                pinLineDict[2].GetComponent<Image>().sprite = (Sprite)Resources.Load("Images/right_y", typeof(Sprite));
                performTrueScenarioForNormal();

                break;
        }
    }

    #endregion

    #region doRight için Davranışlar

    public void PerformButtonForDoRight(int decision , Button button)
    {
        if(decision == _trueIndex)
        {
            PerformButtonForDoRightTrueScen();
        }
        else
        {
            PerformButtonForDoRightFalseScen();
        }
    }

    private void PerformButtonForDoRightFalseScen()
    {
        performFalseScenarioForNormal();
    }

    private void PerformButtonForDoRightTrueScen()
    {
        GameObject nextObject = null;
        stageGenerator stageGenerator = GameObject.Find("StageGenerator").GetComponent<stageGenerator>();
        String thisRoomName = this.gameObject.name;
        bool IsEnabled = false;
        List<Room> roomList = stageGenerator.get_rooms();
        int nextRoomIndex = 0;
        Room thisRoom = this.gameObject.GetComponent<Room>() as Room;

        // Bu for döngüsünde ise yeni bölüme geçme yada 
        // en son odaya geçme case'yi belirleniyor . 

        make_DoorsTagToDoor();

        for (int k = 0; k < roomList.Count; k++)
        {
            if (roomList[k].RoomNumber == thisRoom.RoomNumber)
            {
                nextRoomIndex = k;

                if (nextRoomIndex == (roomList.Count - 1))
                {
                    nextRoomIndex = 0;

                    IsEnabled = true;

                    break;

                }
                else
                {
                    nextRoomIndex = roomList.Count - 1;

                    break;
                }
            }

        }

        // Yeni bölüme buradan geçiş yapılıyor . 

        if (nextRoomIndex == 0 &&
           IsEnabled == true)
        {
            performTrueNextStage();

            return;
        }

        // UI level indicator sprite değişiyor.

        _myRoomSituation = roomSituation.solved;

//        this._myIndicatorHole.change_first_level_indicator_sprite("off_solved");

  //      this._myIndicatorHole.change_itemIndicatorholeSolvedInf();

        // Her türlü en son odaya atlıyorsun. 

        nextObject = stageGenerator.RoomObjectList[nextRoomIndex];

        this.gameObject.SetActive(false);

        nextObject.SetActive(true);

        nextObject.GetComponent<Room>().IsBlink1 = true;
    }

   

    #endregion

    #region Fedakar için Davranışlar

    public void PerformButtonForDevoted(int decision, Button button)
    {
        // Inventory ' de item var iken 

        // Eğer corruption tagli itemi seçerse 

        if (button.CompareTag("corruption"))
        {

            if (AllLevels.Instance.Items.Keys.Length > 0)
            {

                parentObject.deleteOneItemFromItemHoleBars();

                PerformButtonForDevotedTrueScen(decision, button);

            }

            // Inventory  de item yokken 
            else
            {
                // Birşey yapma , sonra animasyon gelebilir . todo Animasyon
            }

        }
        else
        {
            PerformButtonForNormal(decision,button);
        }
    
    }

    private void PerformButtonForDevotedTrueScen(int decision, Button button)
    {
        performTrueScenarioForNormal();
    }

    #endregion

    #region dontMistake için Davranışlar

    public void PerformButtonForDontMistake(int decision , Button button)
    {
        if(decision == _trueIndex)
        {
            PerformButtonForDontMistakeTrueScen();
        }
        else
        {
            PerformButtonForDontMistakeFalseScen(decision, button);
           
        }

    }   

    private void PerformButtonForDontMistakeFalseScen(int decision , Button button)
    {
        this.GetComponentInParent<GameManager>().restartStage();
    }

    private void PerformButtonForDontMistakeTrueScen()
    {
        performTrueScenarioForNormal();
    }

    #endregion

    #region beFast için Davranışlar

    public void PerformButtonForBeFast(int decision , Button button)
    {

        if (decision == _trueIndex)
        {
            PerformButtonBeFastTrueScen();
        }
        else
        {
            PerformButtonBeFastFalseScen();
        }
    }

    private void PerformButtonBeFastTrueScen()
    {
        performTrueScenarioForNormal();
    }

    private void PerformButtonBeFastFalseScen()
    {
        performFalseScenarioForNormal();
    }

    #endregion

    #region signOption için Davranışlar

    public void PerformButtonForsignOption(int decision , Button button)
    {
        if (decision == _trueIndex)
        {
            PerformButtonSignOptionTrueScen();
        }
        else
        {
            PerformButtonSignOptionFalseScen();
        }
    }

    private void PerformButtonSignOptionTrueScen()
    {
        performTrueScenarioForNormal();
    }

    private void PerformButtonSignOptionFalseScen()
    {
        Destroy(GameObject.FindGameObjectWithTag("signOptionImg"));
        performFalseScenarioForNormal();
              
    }

    #endregion

    #region dameOption için Davranışlar

    public void PerformButtonFordameOption(int decision, Button button)
    {
        if (decision == _trueIndex)
        {
            //button.gameObject.GetComponent<flatDoor>().wrapperCoroutine(ref IsDamePass);

            PerformButtondameOptionTrueScen();
        }
        else
        {
            //button.gameObject.GetComponent<flatDoor>().wrapperCoroutine(ref IsDamePass);

            PerformButtondameOptionFalseScen();
        }
    }

    private void PerformButtondameOptionTrueScen()
    {
        myDameProgress = DameProgress.init;
        IsDameTrueFalse = true;

    }

    private void PerformButtondameOptionFalseScen()
    {

        int doorCount = this.GetComponentsInChildren<flatDoor>().Length;

        switch (doorCount)
        {
            case 2:
                myDameProgress = DameProgress.triple;

                for (int k = 0; k < this.GetComponentsInChildren<Image>().Length; k++)
                {
                    if (this.GetComponentsInChildren<Image>()[k].gameObject.CompareTag("errorLine"))
                    {
                       if(this.GetComponentsInChildren<Image>()[k].gameObject.name == "1errorLine")
                        {
                            this.GetComponentsInChildren<Image>()[k].sprite = (Sprite)Resources.Load("Images/third_n1", typeof(Sprite));

                            break;
                        }
                    }
                }

                break;
            case 3:
                myDameProgress = DameProgress.quad;

                for (int k = 0; k < this.GetComponentsInChildren<Image>().Length; k++)
                {
                    if (this.GetComponentsInChildren<Image>()[k].gameObject.CompareTag("errorLine"))
                    {
                        if (this.GetComponentsInChildren<Image>()[k].gameObject.name == "2errorLine")
                        {
                            this.GetComponentsInChildren<Image>()[k].sprite = (Sprite)Resources.Load("Images/third_n1", typeof(Sprite));

                            break;
                        }
                    }
                }

                break;
            case 4:
                myDameProgress = DameProgress.finish;

                for (int k = 0; k < this.GetComponentsInChildren<Image>().Length; k++)
                {
                    if (this.GetComponentsInChildren<Image>()[k].gameObject.CompareTag("errorLine"))
                    {
                        if (this.GetComponentsInChildren<Image>()[k].gameObject.name == "3errorLine")
                        {
                            this.GetComponentsInChildren<Image>()[k].sprite = (Sprite)Resources.Load("Images/third_n1", typeof(Sprite));

                            break;
                        }
                    }
                }

                break;
        }

        if(myDameProgress == DameProgress.finish)
        {
            myDameProgress = DameProgress.init;
            IsDameTrueFalse = false;

        }
    
    }

    #endregion

    #endregion

    #region Stage için durumlar

    private void performTrueNextStage()
    {
        parentObject.nextStageandLevel();
    }

    #endregion

    #endregion

    public void determineDoorSprites(flatDoor flatDoor,Room.roomType parentType)
    {
        Sprite normalSprite = null;
        Sprite trueSprite = null;
        Sprite falseSprite = null;
        Sprite whiteSprite = null;
        Sprite trueBGSprite = null;
        Sprite falseBGSprite = null;
        Sprite normalBGSprite = null;
        Sprite whiteBGSprite = null;

        // todo colorBlind imageler için tekrar gelinecek.

        switch (parentType)
        {
            case Room.roomType.beFast:

                if (OptionMenu.IsColorBlindEnabled)
                {
                    trueSprite = (Sprite)Resources.Load("Images/port_y", typeof(Sprite));
                    falseSprite = (Sprite)Resources.Load("Images/port_n", typeof(Sprite));
                }

                else
                {
                    trueSprite = (Sprite)Resources.Load("Images/port_y", typeof(Sprite));
                    falseSprite = (Sprite)Resources.Load("Images/port_n", typeof(Sprite));
                }
                
                normalSprite = (Sprite)Resources.Load("Images/port_0", typeof(Sprite));
                whiteSprite = (Sprite)Resources.Load("Images/port_1", typeof(Sprite));
                trueBGSprite = (Sprite)Resources.Load("Images/y_bg", typeof(Sprite));
                falseBGSprite = (Sprite)Resources.Load("Images/n_bg", typeof(Sprite));
                normalBGSprite = (Sprite)Resources.Load("Images/0_bg", typeof(Sprite));
                whiteBGSprite = (Sprite)Resources.Load("Images/1_bg", typeof(Sprite));

                break;
            case Room.roomType.binaryOption:

                if (OptionMenu.IsColorBlindEnabled)
                {
                    trueSprite = (Sprite)Resources.Load("Images/port_y", typeof(Sprite));
                    falseSprite = (Sprite)Resources.Load("Images/port_n", typeof(Sprite));
                }

                else
                {
                    trueSprite = (Sprite)Resources.Load("Images/port_y", typeof(Sprite));
                    falseSprite = (Sprite)Resources.Load("Images/port_n", typeof(Sprite));
                }

                normalSprite = (Sprite)Resources.Load("Images/port_0", typeof(Sprite));
                whiteSprite = (Sprite)Resources.Load("Images/port_1", typeof(Sprite));
                trueBGSprite = (Sprite)Resources.Load("Images/y_bg", typeof(Sprite));
                falseBGSprite = (Sprite)Resources.Load("Images/n_bg", typeof(Sprite));
                normalBGSprite = (Sprite)Resources.Load("Images/0_bg", typeof(Sprite));
                whiteBGSprite = (Sprite)Resources.Load("Images/1_bg", typeof(Sprite));

                break;
            case Room.roomType.checkPoint:

                if (OptionMenu.IsColorBlindEnabled)
                {
                    trueSprite = (Sprite)Resources.Load("Images/port_y", typeof(Sprite));
                    falseSprite = (Sprite)Resources.Load("Images/port_n", typeof(Sprite));
                }

                else
                {
                    trueSprite = (Sprite)Resources.Load("Images/port_y", typeof(Sprite));
                    falseSprite = (Sprite)Resources.Load("Images/port_n", typeof(Sprite));
                }

                normalSprite = (Sprite)Resources.Load("Images/port_0", typeof(Sprite));
                whiteSprite = (Sprite)Resources.Load("Images/port_1", typeof(Sprite));
                trueBGSprite = (Sprite)Resources.Load("Images/y_bg", typeof(Sprite));
                falseBGSprite = (Sprite)Resources.Load("Images/n_bg", typeof(Sprite));
                normalBGSprite = (Sprite)Resources.Load("Images/0_bg", typeof(Sprite));
                whiteBGSprite = (Sprite)Resources.Load("Images/1_bg", typeof(Sprite));

                break;
            case Room.roomType.dameOption:

                if (OptionMenu.IsColorBlindEnabled)
                {
                    trueSprite = (Sprite)Resources.Load("Images/port_y", typeof(Sprite));
                    falseSprite = (Sprite)Resources.Load("Images/port_n", typeof(Sprite));
                }

                else
                {
                    trueSprite = (Sprite)Resources.Load("Images/port_y", typeof(Sprite));
                    falseSprite = (Sprite)Resources.Load("Images/port_n", typeof(Sprite));
                }

                normalSprite = (Sprite)Resources.Load("Images/port_0", typeof(Sprite));
                whiteSprite = (Sprite)Resources.Load("Images/port_1", typeof(Sprite));
                trueBGSprite = (Sprite)Resources.Load("Images/y_bg", typeof(Sprite));
                falseBGSprite = (Sprite)Resources.Load("Images/n_bg", typeof(Sprite));
                normalBGSprite = (Sprite)Resources.Load("Images/0_bg", typeof(Sprite));
                whiteBGSprite = (Sprite)Resources.Load("Images/1_bg", typeof(Sprite));

                break;
            case Room.roomType.devoted:

                if (OptionMenu.IsColorBlindEnabled)
                {
                    trueSprite = (Sprite)Resources.Load("Images/port_y", typeof(Sprite));
                    falseSprite = (Sprite)Resources.Load("Images/port_n", typeof(Sprite));
                }

                else
                {
                    trueSprite = (Sprite)Resources.Load("Images/port_y", typeof(Sprite));
                    falseSprite = (Sprite)Resources.Load("Images/port_n", typeof(Sprite));
                }

                if (flatDoor.gameObject.CompareTag("corruption"))
                {  
                    normalSprite = (Sprite)Resources.Load("Images/port_corrupted", typeof(Sprite));
                    whiteSprite = (Sprite)Resources.Load("Images/port_1", typeof(Sprite));

                }
                else
                {
                    normalSprite = (Sprite)Resources.Load("Images/port_0", typeof(Sprite));
                    whiteSprite = (Sprite)Resources.Load("Images/port_1", typeof(Sprite));
                }

                trueBGSprite = (Sprite)Resources.Load("Images/y_bg", typeof(Sprite));
                falseBGSprite = (Sprite)Resources.Load("Images/n_bg", typeof(Sprite));
                normalBGSprite = (Sprite)Resources.Load("Images/0_bg", typeof(Sprite));
                whiteBGSprite = (Sprite)Resources.Load("Images/1_bg", typeof(Sprite));

                break;
            case Room.roomType.dontMistake:

                if (OptionMenu.IsColorBlindEnabled)
                {
                    trueSprite = (Sprite)Resources.Load("Images/triangle_y", typeof(Sprite));
                    falseSprite = (Sprite)Resources.Load("Images/triangle_n", typeof(Sprite));
                }

                else
                {
                    trueSprite = (Sprite)Resources.Load("Images/triangle_y", typeof(Sprite));
                    falseSprite = (Sprite)Resources.Load("Images/triangle_n", typeof(Sprite));
                }

                normalSprite = (Sprite)Resources.Load("Images/triangle_z", typeof(Sprite));
                whiteSprite = (Sprite)Resources.Load("Images/triangle_0", typeof(Sprite));
                trueBGSprite = (Sprite)Resources.Load("Images/y_bg", typeof(Sprite));
                falseBGSprite = (Sprite)Resources.Load("Images/n_bg", typeof(Sprite));
                normalBGSprite = (Sprite)Resources.Load("Images/z_bg", typeof(Sprite));
                whiteBGSprite = (Sprite)Resources.Load("Images/z_bg", typeof(Sprite));

                break;
            case Room.roomType.doRight:

                if (OptionMenu.IsColorBlindEnabled)
                {
                    trueSprite = (Sprite)Resources.Load("Images/diamond_y", typeof(Sprite));
                    falseSprite = (Sprite)Resources.Load("Images/diamond_n", typeof(Sprite));
                }

                else
                {
                    trueSprite = (Sprite)Resources.Load("Images/diamond_y", typeof(Sprite));
                    falseSprite = (Sprite)Resources.Load("Images/diamond_n", typeof(Sprite));
                }

                normalSprite = (Sprite)Resources.Load("Images/diamond_z", typeof(Sprite));
                whiteSprite = (Sprite)Resources.Load("Images/diamond_1", typeof(Sprite));
                trueBGSprite = (Sprite)Resources.Load("Images/y_bg", typeof(Sprite));
                falseBGSprite = (Sprite)Resources.Load("Images/n_bg", typeof(Sprite));
                normalBGSprite = (Sprite)Resources.Load("Images/z_bg", typeof(Sprite));
                whiteBGSprite = (Sprite)Resources.Load("Images/z_bg", typeof(Sprite));

                break;
            case Room.roomType.doubleRight:

                if (OptionMenu.IsColorBlindEnabled)
                {
                    trueSprite = (Sprite)Resources.Load("Images/port_y", typeof(Sprite));
                    falseSprite = (Sprite)Resources.Load("Images/port_n", typeof(Sprite));
                }

                else
                {
                    trueSprite = (Sprite)Resources.Load("Images/port_y", typeof(Sprite));
                    falseSprite = (Sprite)Resources.Load("Images/port_n", typeof(Sprite));
                }

                
                normalSprite = (Sprite)Resources.Load("Images/port_0", typeof(Sprite));
                whiteSprite = (Sprite)Resources.Load("Images/port_1", typeof(Sprite));
                trueBGSprite = (Sprite)Resources.Load("Images/y_bg", typeof(Sprite));
                falseBGSprite = (Sprite)Resources.Load("Images/n_bg", typeof(Sprite));
                normalBGSprite = (Sprite)Resources.Load("Images/0_bg", typeof(Sprite));
                whiteBGSprite = (Sprite)Resources.Load("Images/1_bg", typeof(Sprite));

                break;
            case Room.roomType.pin5:

                if (OptionMenu.IsColorBlindEnabled)
                {
                    trueSprite = (Sprite)Resources.Load("Images/port_y", typeof(Sprite));
                    falseSprite = (Sprite)Resources.Load("Images/port_n", typeof(Sprite));
                }

                else
                {
                    trueSprite = (Sprite)Resources.Load("Images/port_y", typeof(Sprite));
                    falseSprite = (Sprite)Resources.Load("Images/port_n", typeof(Sprite));
                }

                normalSprite = (Sprite)Resources.Load("Images/port_0", typeof(Sprite));
                whiteSprite = (Sprite)Resources.Load("Images/port_1", typeof(Sprite));
                trueBGSprite = (Sprite)Resources.Load("Images/y_bg", typeof(Sprite));
                falseBGSprite = (Sprite)Resources.Load("Images/n_bg", typeof(Sprite));
                normalBGSprite = (Sprite)Resources.Load("Images/0_bg", typeof(Sprite));
                whiteBGSprite = (Sprite)Resources.Load("Images/1_bg", typeof(Sprite));

                break;
            case Room.roomType.pin6:

                if (OptionMenu.IsColorBlindEnabled)
                {
                    trueSprite = (Sprite)Resources.Load("Images/port_y", typeof(Sprite));
                    falseSprite = (Sprite)Resources.Load("Images/port_n", typeof(Sprite));
                }

                else
                {
                    trueSprite = (Sprite)Resources.Load("Images/port_y", typeof(Sprite));
                    falseSprite = (Sprite)Resources.Load("Images/port_n", typeof(Sprite));
                }

                normalSprite = (Sprite)Resources.Load("Images/port_0", typeof(Sprite));
                whiteSprite = (Sprite)Resources.Load("Images/port_1", typeof(Sprite));
                trueBGSprite = (Sprite)Resources.Load("Images/y_bg", typeof(Sprite));
                falseBGSprite = (Sprite)Resources.Load("Images/n_bg", typeof(Sprite));
                normalBGSprite = (Sprite)Resources.Load("Images/0_bg", typeof(Sprite));
                whiteBGSprite = (Sprite)Resources.Load("Images/1_bg", typeof(Sprite));

                break;
            case Room.roomType.pin7:

                if (OptionMenu.IsColorBlindEnabled)
                {
                    trueSprite = (Sprite)Resources.Load("Images/port_y", typeof(Sprite));
                    falseSprite = (Sprite)Resources.Load("Images/port_n", typeof(Sprite));
                }

                else
                {
                    trueSprite = (Sprite)Resources.Load("Images/port_y", typeof(Sprite));
                    falseSprite = (Sprite)Resources.Load("Images/port_n", typeof(Sprite));
                }

                normalSprite = (Sprite)Resources.Load("Images/port_0", typeof(Sprite));
                whiteSprite = (Sprite)Resources.Load("Images/port_1", typeof(Sprite));
                trueBGSprite = (Sprite)Resources.Load("Images/y_bg", typeof(Sprite));
                falseBGSprite = (Sprite)Resources.Load("Images/n_bg", typeof(Sprite));
                normalBGSprite = (Sprite)Resources.Load("Images/0_bg", typeof(Sprite));
                whiteBGSprite = (Sprite)Resources.Load("Images/1_bg", typeof(Sprite));

                break;
            case Room.roomType.pin8:

                if (OptionMenu.IsColorBlindEnabled)
                {
                    trueSprite = (Sprite)Resources.Load("Images/port_y", typeof(Sprite));
                    falseSprite = (Sprite)Resources.Load("Images/port_n", typeof(Sprite));
                }

                else
                {
                    trueSprite = (Sprite)Resources.Load("Images/port_y", typeof(Sprite));
                    falseSprite = (Sprite)Resources.Load("Images/port_n", typeof(Sprite));
                }

                normalSprite = (Sprite)Resources.Load("Images/port_0", typeof(Sprite));
                whiteSprite = (Sprite)Resources.Load("Images/port_1", typeof(Sprite));
                trueBGSprite = (Sprite)Resources.Load("Images/y_bg", typeof(Sprite));
                falseBGSprite = (Sprite)Resources.Load("Images/n_bg", typeof(Sprite));
                normalBGSprite = (Sprite)Resources.Load("Images/0_bg", typeof(Sprite));
                whiteBGSprite = (Sprite)Resources.Load("Images/1_bg", typeof(Sprite));

                break;
            case Room.roomType.pin9:

                if (OptionMenu.IsColorBlindEnabled)
                {
                    trueSprite = (Sprite)Resources.Load("Images/port_y", typeof(Sprite));
                    falseSprite = (Sprite)Resources.Load("Images/port_n", typeof(Sprite));
                }

                else
                {
                    trueSprite = (Sprite)Resources.Load("Images/port_y", typeof(Sprite));
                    falseSprite = (Sprite)Resources.Load("Images/port_n", typeof(Sprite));
                }

                normalSprite = (Sprite)Resources.Load("Images/port_0", typeof(Sprite));
                whiteSprite = (Sprite)Resources.Load("Images/port_1", typeof(Sprite));
                trueBGSprite = (Sprite)Resources.Load("Images/y_bg", typeof(Sprite));
                falseBGSprite = (Sprite)Resources.Load("Images/n_bg", typeof(Sprite));
                normalBGSprite = (Sprite)Resources.Load("Images/0_bg", typeof(Sprite));
                whiteBGSprite = (Sprite)Resources.Load("Images/1_bg", typeof(Sprite));

                break;
            case Room.roomType.quadOption:

                if (OptionMenu.IsColorBlindEnabled)
                {
                    trueSprite = (Sprite)Resources.Load("Images/port_y", typeof(Sprite));
                    falseSprite = (Sprite)Resources.Load("Images/port_n", typeof(Sprite));
                }

                else
                {
                    trueSprite = (Sprite)Resources.Load("Images/port_y", typeof(Sprite));
                    falseSprite = (Sprite)Resources.Load("Images/port_n", typeof(Sprite));
                }

                normalSprite = (Sprite)Resources.Load("Images/port_0", typeof(Sprite));
                whiteSprite = (Sprite)Resources.Load("Images/port_1", typeof(Sprite));
                trueBGSprite = (Sprite)Resources.Load("Images/y_bg", typeof(Sprite));
                falseBGSprite = (Sprite)Resources.Load("Images/n_bg", typeof(Sprite));
                normalBGSprite = (Sprite)Resources.Load("Images/0_bg", typeof(Sprite));
                whiteBGSprite = (Sprite)Resources.Load("Images/1_bg", typeof(Sprite));

                break;
            case Room.roomType.signOption:

                if (OptionMenu.IsColorBlindEnabled)
                {
                    trueSprite = (Sprite)Resources.Load("Images/port_y", typeof(Sprite));
                    falseSprite = (Sprite)Resources.Load("Images/port_n", typeof(Sprite));
                }

                else
                {
                    trueSprite = (Sprite)Resources.Load("Images/port_y", typeof(Sprite));
                    falseSprite = (Sprite)Resources.Load("Images/port_n", typeof(Sprite));
                }

                normalSprite = (Sprite)Resources.Load("Images/port_0", typeof(Sprite));
                whiteSprite = (Sprite)Resources.Load("Images/port_1", typeof(Sprite));
                trueBGSprite = (Sprite)Resources.Load("Images/y_bg", typeof(Sprite));
                falseBGSprite = (Sprite)Resources.Load("Images/n_bg", typeof(Sprite));
                normalBGSprite = (Sprite)Resources.Load("Images/0_bg", typeof(Sprite));
                whiteBGSprite = (Sprite)Resources.Load("Images/1_bg", typeof(Sprite));

                break;
            case Room.roomType.sortbyItems:

                if (OptionMenu.IsColorBlindEnabled)
                {
                    trueSprite = (Sprite)Resources.Load("Images/port_y", typeof(Sprite));
                    falseSprite = (Sprite)Resources.Load("Images/port_n", typeof(Sprite));
                }

                else
                {
                    trueSprite = (Sprite)Resources.Load("Images/port_y", typeof(Sprite));
                    falseSprite = (Sprite)Resources.Load("Images/port_n", typeof(Sprite));
                }

                normalSprite = (Sprite)Resources.Load("Images/port_0", typeof(Sprite));
                whiteSprite = (Sprite)Resources.Load("Images/port_1", typeof(Sprite));
                trueBGSprite = (Sprite)Resources.Load("Images/y_bg", typeof(Sprite));
                falseBGSprite = (Sprite)Resources.Load("Images/n_bg", typeof(Sprite));
                normalBGSprite = (Sprite)Resources.Load("Images/0_bg", typeof(Sprite));
                whiteBGSprite = (Sprite)Resources.Load("Images/1_bg", typeof(Sprite));

                break;
            case Room.roomType.tripleOption:

                if (OptionMenu.IsColorBlindEnabled)
                {
                    trueSprite = (Sprite)Resources.Load("Images/port_y", typeof(Sprite));
                    falseSprite = (Sprite)Resources.Load("Images/port_n", typeof(Sprite));
                }

                else
                {
                    trueSprite = (Sprite)Resources.Load("Images/port_y", typeof(Sprite));
                    falseSprite = (Sprite)Resources.Load("Images/port_n", typeof(Sprite));
                }

                normalSprite = (Sprite)Resources.Load("Images/port_0", typeof(Sprite));
                whiteSprite = (Sprite)Resources.Load("Images/port_1", typeof(Sprite));
                trueBGSprite = (Sprite)Resources.Load("Images/y_bg", typeof(Sprite));
                falseBGSprite = (Sprite)Resources.Load("Images/n_bg", typeof(Sprite));
                normalBGSprite = (Sprite)Resources.Load("Images/0_bg", typeof(Sprite));
                whiteBGSprite = (Sprite)Resources.Load("Images/1_bg", typeof(Sprite));

                break;
        }

        flatDoor.trueSprite = trueSprite;
        flatDoor.falseSprite = falseSprite;
        flatDoor.normalSprite = normalSprite;
        flatDoor.whiteSprite = whiteSprite;
        flatDoor.trueBGSprite = trueBGSprite;
        flatDoor.falseBGSprite = falseBGSprite;
        flatDoor.normalBGSprite = normalBGSprite;
        flatDoor.whiteBGSprite = whiteBGSprite;

    }

	private void make_DoorsTagToDoor()
	{
		for (int i = 0; i < this.gameObject.GetComponentsInChildren<flatDoor>(true).Length; i++)
		{
			switch (this.MyRoomType)
			{
			case roomType.signOption:

				if (!this.gameObject.GetComponentsInChildren<flatDoor>(true)[i].CompareTag("signoption"))
				{
					this.gameObject.GetComponentsInChildren<flatDoor>(true)[i].gameObject.tag = "door";
				}else
				{
					this.gameObject.GetComponentsInChildren<flatDoor>(true)[i].gameObject.tag = "signoption";
				}

				break;

			case roomType.devoted:

				if (!this.gameObject.GetComponentsInChildren<flatDoor>(true)[i].CompareTag("corruption"))
				{
					this.gameObject.GetComponentsInChildren<flatDoor>(true)[i].gameObject.tag = "door";
				}else
				{
					this.gameObject.GetComponentsInChildren<flatDoor>(true)[i].gameObject.tag = "corruption";
				}

				break;

			default:

				this.gameObject.GetComponentsInChildren<flatDoor>(true)[i].gameObject.tag = "door";

				break;
			}

		}
	}

	public void nextRoomWrapper()
	{
		switch (this.MyRoomType) {
		case Room.roomType.doRight: 
			PerformButtonForDoRightTrueScen ();
			break;
		default:
			performTrueScenarioForNormal ();
			break;
		}
	}

	public void goRandomRoom(){

		int randomRoomIndex = parentObject.getRandomRoom (this);

		performsendToRandomRoom (randomRoomIndex);

	}

	public void goRoomByITem(int index)
	{
		performsendToRandomRoom (index);

	}

	private void performsendToRandomRoom(int index)
	{
		GameObject nextObject = null;
		stageGenerator stageGenerator = GameObject.Find("StageGenerator").GetComponent<stageGenerator>();
		String thisRoomName = this.gameObject.name;
		bool IsEnabled = false;
		List<Room> roomList = stageGenerator.get_rooms();
		int nextRoomIndex = 0;
		Room thisRoom = this.gameObject.GetComponent<Room>() as Room;

		nextRoomIndex = index;

		if (_itemIndicatorParent == null)
			_itemIndicatorParent = GameObject.Find ("ItemIndicatorParent").GetComponent<ItemIndicatorParent> ();

		_itemIndicatorParent.wrongSituation(_myRoomSituation,this._roomNumber);

		for (int i = 0; i < this.gameObject.GetComponentInParent<GameManager> ().GetComponentsInChildren<Room> (true).Length; i++) {

			if (nextRoomIndex > i)
				this.gameObject.GetComponentInParent<GameManager> ().GetComponentsInChildren<Room> (true) [i].MyRoomSituation = Room.roomSituation.solved;

		}

		make_DoorsTagToDoor();

		// UI level indicator sprite değişiyor.

		_myRoomSituation = roomSituation.solved;

//		this._myIndicatorHole.change_first_level_indicator_sprite("off_solved");

//		this._myIndicatorHole.change_itemIndicatorholeSolvedInf();

		// Her türlü en son odaya atlıyorsun. 

		nextObject = stageGenerator.RoomObjectList[nextRoomIndex];

		this.gameObject.SetActive(false);

		nextObject.SetActive(true);

		nextObject.GetComponent<Room>().IsBlink1 = true;
	}

	private void performChangeUIITemIndicator(){

		int difference = 0;

		difference = 0;

		if(parentObject!=null)
		 difference = this._roomNumber - parentObject.PreviousNumber;

		// UI levelindicator için resim belirleniyor.

		if ( difference < 0 ) {

			if (GameObject.Find ("ItemIndicatorParent")!=null) {

				_itemIndicatorParent = GameObject.Find ("ItemIndicatorParent").GetComponent<ItemIndicatorParent> ();

				if (_itemIndicatorParent != null) {

					_itemIndicatorParent.setActiveIndicatorItemHistory (parentObject.PreviousNumber, this._roomNumber);
				}

			}

		}
		else
		if (GameObject.Find ("ItemIndicatorParent")!=null) {

			_itemIndicatorParent = GameObject.Find ("ItemIndicatorParent").GetComponent<ItemIndicatorParent> ();

			if (_itemIndicatorParent != null) {

				_itemIndicatorParent.setActiveIndicatorItem (_myRoomSituation);
			}
					
		}
	}

	public void convert_to_checkPoint()
	{
		Image checkPoint;
	
		MyRoomType = roomType.checkPoint;
		RoomName = "checkPoint";
	
		#region checkpoint is creating below

		checkPoint = null;
		checkPoint = Instantiate(Resources.Load("checkPoint", typeof(Image))) as Image;
		checkPoint.transform.SetParent(this.gameObject.transform);
		checkPoint.GetComponent<RectTransform>().ResetRectTransformation();
		checkPoint.gameObject.name = checkPoint.gameObject.name.Split('(')[0];
		checkPoint.gameObject.transform.localScale= new Vector3(1, 1, 1);
		checkPoint.GetComponent<RectTransform>().anchoredPosition = new Vector2(-this.gameObject.GetComponent<RectTransform>().offsetMax.x - 50,
			this.gameObject.GetComponent<RectTransform>().offsetMax.y + 50);

		checkPoint = null;

		#endregion

	}
}
