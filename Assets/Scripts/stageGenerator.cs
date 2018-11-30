using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Collections;

public class stageGenerator : MonoBehaviour {

    private Stage _stage;
    public static float c_1 = 1;
    public static float c_005 = 0.05f;
    public static float c_095 = 0.95f;
    private List<Room> _rooms;
    public Dictionary<int, GameObject> _roomObjectList;
    public Dictionary<string, Sprite> _itemSpriteArray;
    public Dictionary<string, Sprite> _itemIndicatorSpriteArray;
    public Dictionary<string, Sprite> _itemHealthBarDict;
    public GameObject IsItemWon;
    public Image itemImage;
    public Sprite[] itemSprites = new Sprite[8];

    public Dictionary<int, GameObject> RoomObjectList
    {
        get
        {
            return _roomObjectList;
        }

        set
        {
            _roomObjectList = value;
        }
    }

    public Dictionary<string, Sprite> ItemIndicatorSpriteArray
    {
        get
        {
            return _itemIndicatorSpriteArray;
        }

    }

    public Dictionary<string, Sprite> ItemHealthBarDict
    {
        get
        {
            return _itemHealthBarDict;
        }
    }

	public List<Room> Rooms
	{
		get{

			return _rooms;
		}
		set {

			_rooms = value;
		}

	}

    // Use this for initialization

    public void Init(Boolean restartIndicator)
    {
        _stage = null;

        if (_rooms != null)
        {
            _rooms.Clear();
            _rooms = null;
        }

        if (_roomObjectList != null)
        {
            _roomObjectList.Clear();
            _roomObjectList = null;
        }

        if (_itemSpriteArray != null)
        {
            _itemSpriteArray.Clear();
            _itemSpriteArray = null;
        }

        if (_itemIndicatorSpriteArray != null)
        {
            _itemIndicatorSpriteArray.Clear();
            _itemIndicatorSpriteArray = null;
        }

        if (_itemHealthBarDict != null)
        {
            _itemHealthBarDict.Clear();
            _itemHealthBarDict = null;
        }

        m_Start_Adapter(restartIndicator);

    }
    void Start () {

        m_Start_Adapter(false);

    }

    public void m_Start_Adapter(Boolean restartIndicator)
    {
        /*  Initialize components  */
        if (_stage == null)
            _stage = AllLevels.Instance.pushStage();

        if (_rooms == null)
            _rooms = new List<Room>();

        if (RoomObjectList == null)
            RoomObjectList = new Dictionary<int, GameObject>();

        #region Fill all images from Resources Folder

        fill_itemInventorySpriteArray();

        fill_itemindicatorSpriteArray();

        fill_itemdicthealthBarArray();

        #endregion

        /* Generate stage from All levels push stage method */
        if (_stage != null)
            generate_Stage(_stage, restartIndicator);
    }

    private void fill_itemInventorySpriteArray()
    {
        if (_itemSpriteArray == null)
        {
            _itemSpriteArray = new Dictionary<string, Sprite>();
            _itemSpriteArray["showWrongDoor1"] = (Sprite)Resources.Load("Images/item_1n_0", typeof(Sprite));
            _itemSpriteArray["showWrongDoor2"] = (Sprite)Resources.Load("Images/item_2n_0", typeof(Sprite));
            _itemSpriteArray["tripleHearts"] = (Sprite)Resources.Load("Images/item_3lives_0", typeof(Sprite));
            _itemSpriteArray["keyItem"] = (Sprite)Resources.Load("Images/item_login_0", typeof(Sprite));
            _itemSpriteArray["goOtherRoom"] = (Sprite)Resources.Load("Images/item_random_0", typeof(Sprite));
            _itemSpriteArray["checkPointItem"] = (Sprite)Resources.Load("Images/item_save_0", typeof(Sprite));
            _itemSpriteArray["neverGoBack"] = (Sprite)Resources.Load("Images/item_lock_0", typeof(Sprite));
            _itemSpriteArray["jumpintolastRoom"] = (Sprite)Resources.Load("Images/item_jump_0", typeof(Sprite));
 
        }

    }

    private void fill_itemdicthealthBarArray()
    {
        if(_itemHealthBarDict == null)
        {
            _itemHealthBarDict = new Dictionary<string, Sprite>();
            _itemHealthBarDict["detectionmarker_0"] = (Sprite)Resources.Load("Images/detectionmarker_0", typeof(Sprite));
            _itemHealthBarDict["detectionmarker_1"] = (Sprite)Resources.Load("Images/detectionmarker_1", typeof(Sprite));
            _itemHealthBarDict["detectionmarker_n0"] = (Sprite)Resources.Load("Images/detectionmarker_n0", typeof(Sprite));

        }
    }

    private void fill_itemindicatorSpriteArray()
    {
        if (_itemIndicatorSpriteArray == null)
        {
            _itemIndicatorSpriteArray = new Dictionary<string, Sprite>();


            _itemIndicatorSpriteArray["binaryOption_off_unsolved"] = (Sprite)Resources.Load("Images/level2_unsolved_0", typeof(Sprite));
            _itemIndicatorSpriteArray["binaryOption_off_solved"] = (Sprite)Resources.Load("Images/level2_solved_0", typeof(Sprite));
            _itemIndicatorSpriteArray["binaryOption_on_unsolved"] = (Sprite)Resources.Load("Images/level2_unsolved_1", typeof(Sprite));
            _itemIndicatorSpriteArray["binaryOption_on_solved"] = (Sprite)Resources.Load("Images/level2_solved_1", typeof(Sprite));

            _itemIndicatorSpriteArray["tripleOption_off_unsolved"] = (Sprite)Resources.Load("Images/level3_unsolved_0", typeof(Sprite));
            _itemIndicatorSpriteArray["tripleOption_off_solved"] = (Sprite)Resources.Load("Images/level3_solved_0", typeof(Sprite));
            _itemIndicatorSpriteArray["tripleOption_on_unsolved"] = (Sprite)Resources.Load("Images/level3_unsolved_1", typeof(Sprite));
            _itemIndicatorSpriteArray["tripleOption_on_solved"] = (Sprite)Resources.Load("Images/level3_solved_1", typeof(Sprite));

            _itemIndicatorSpriteArray["quadOption_off_unsolved"] = (Sprite)Resources.Load("Images/level4_unsolved_0", typeof(Sprite));
            _itemIndicatorSpriteArray["quadOption_off_solved"] = (Sprite)Resources.Load("Images/level4_solved_0", typeof(Sprite));
            _itemIndicatorSpriteArray["quadOption_on_unsolved"] = (Sprite)Resources.Load("Images/level4_unsolved_1", typeof(Sprite));
            _itemIndicatorSpriteArray["quadOption_on_solved"] = (Sprite)Resources.Load("Images/level4_solved_1", typeof(Sprite));

            _itemIndicatorSpriteArray["checkPoint_off_unsolved"] = (Sprite)Resources.Load("Images/level4_unsolved_0_saved", typeof(Sprite));
            _itemIndicatorSpriteArray["checkPoint_off_solved"] = (Sprite)Resources.Load("Images/level4_solved_0_saved", typeof(Sprite));
            _itemIndicatorSpriteArray["checkPoint_on_unsolved"] = (Sprite)Resources.Load("Images/level4_unsolved_0_saved", typeof(Sprite));
            _itemIndicatorSpriteArray["checkPoint_on_solved"] = (Sprite)Resources.Load("Images/level4_solved_0_saved", typeof(Sprite));

            _itemIndicatorSpriteArray["doubleRight_off_unsolved"] = (Sprite)Resources.Load("Images/leveldouble_unsolved_0", typeof(Sprite));
            _itemIndicatorSpriteArray["doubleRight_off_solved"] = (Sprite)Resources.Load("Images/leveldouble_solved_0", typeof(Sprite));
            _itemIndicatorSpriteArray["doubleRight_on_unsolved"] = (Sprite)Resources.Load("Images/leveldouble_unsolved_1", typeof(Sprite));
            _itemIndicatorSpriteArray["doubleRight_on_solved"] = (Sprite)Resources.Load("Images/leveldouble_solved_1", typeof(Sprite));

            _itemIndicatorSpriteArray["doRight_off_unsolved"] = (Sprite)Resources.Load("Images/leveljump_unsolved_0", typeof(Sprite));
            _itemIndicatorSpriteArray["doRight_off_solved"] = (Sprite)Resources.Load("Images/leveljump_solved_0", typeof(Sprite));
            _itemIndicatorSpriteArray["doRight_on_unsolved"] = (Sprite)Resources.Load("Images/leveljump_unsolved_1", typeof(Sprite));
            _itemIndicatorSpriteArray["doRight_on_solved"] = (Sprite)Resources.Load("Images/leveljump_solved_1", typeof(Sprite));

            _itemIndicatorSpriteArray["devoted_off_unsolved"] = (Sprite)Resources.Load("Images/level4_unsolved_0", typeof(Sprite));
            _itemIndicatorSpriteArray["devoted_off_solved"] = (Sprite)Resources.Load("Images/level4_solved_0", typeof(Sprite));
            _itemIndicatorSpriteArray["devoted_on_unsolved"] = (Sprite)Resources.Load("Images/level4_unsolved_1", typeof(Sprite));
            _itemIndicatorSpriteArray["devoted_on_solved"] = (Sprite)Resources.Load("Images/level4_solved_1", typeof(Sprite));

            _itemIndicatorSpriteArray["dontMistake_off_unsolved"] = (Sprite)Resources.Load("Images/levelkick_unsolved_0", typeof(Sprite));
            _itemIndicatorSpriteArray["dontMistake_off_solved"] = (Sprite)Resources.Load("Images/levelkick_solved_0", typeof(Sprite));
            _itemIndicatorSpriteArray["dontMistake_on_unsolved"] = (Sprite)Resources.Load("Images/levelkick_unsolved_1", typeof(Sprite));
            _itemIndicatorSpriteArray["dontMistake_on_solved"] = (Sprite)Resources.Load("Images/levelkick_solved_1", typeof(Sprite));

            _itemIndicatorSpriteArray["beFast_off_unsolved"] = (Sprite)Resources.Load("Images/leveltimer_unsolved_0", typeof(Sprite));
            _itemIndicatorSpriteArray["beFast_off_solved"] = (Sprite)Resources.Load("Images/leveltimer_solved_0", typeof(Sprite));
            _itemIndicatorSpriteArray["beFast_on_unsolved"] = (Sprite)Resources.Load("Images/leveltimer_unsolved_1", typeof(Sprite));
            _itemIndicatorSpriteArray["beFast_on_solved"] = (Sprite)Resources.Load("Images/leveltimer_solved_1", typeof(Sprite));


            _itemIndicatorSpriteArray["signOption_off_unsolved"] = (Sprite)Resources.Load("Images/level4_unsolved_0", typeof(Sprite));
            _itemIndicatorSpriteArray["signOption_off_solved"] = (Sprite)Resources.Load("Images/level4_solved_0", typeof(Sprite));
            _itemIndicatorSpriteArray["signOption_on_unsolved"] = (Sprite)Resources.Load("Images/level4_unsolved_1", typeof(Sprite));
            _itemIndicatorSpriteArray["signOption_on_solved"] = (Sprite)Resources.Load("Images/level4_solved_1", typeof(Sprite));

            _itemIndicatorSpriteArray["dameOption_off_unsolved"] = (Sprite)Resources.Load("Images/levelstrike_unsolved_0", typeof(Sprite));
            _itemIndicatorSpriteArray["dameOption_off_solved"] = (Sprite)Resources.Load("Images/levelstrike_solved_0", typeof(Sprite));
            _itemIndicatorSpriteArray["dameOption_on_unsolved"] = (Sprite)Resources.Load("Images/levelstrike_unsolved_1", typeof(Sprite));
            _itemIndicatorSpriteArray["dameOption_on_solved"] = (Sprite)Resources.Load("Images/levelstrike_solved_1", typeof(Sprite));

            _itemIndicatorSpriteArray["pin5_off_unsolved"] = (Sprite)Resources.Load("Images/levelpin_unsolved_0", typeof(Sprite));
            _itemIndicatorSpriteArray["pin5_off_solved"] = (Sprite)Resources.Load("Images/levelpin_solved_0", typeof(Sprite));
            _itemIndicatorSpriteArray["pin5_on_unsolved"] = (Sprite)Resources.Load("Images/levelpin_unsolved_1", typeof(Sprite));
            _itemIndicatorSpriteArray["pin5_on_solved"] = (Sprite)Resources.Load("Images/levelpin_solved_1", typeof(Sprite));

            _itemIndicatorSpriteArray["pin6_off_unsolved"] = (Sprite)Resources.Load("Images/levelpin_unsolved_0", typeof(Sprite));
            _itemIndicatorSpriteArray["pin6_off_solved"] = (Sprite)Resources.Load("Images/levelpin_solved_0", typeof(Sprite));
            _itemIndicatorSpriteArray["pin6_on_unsolved"] = (Sprite)Resources.Load("Images/levelpin_unsolved_1", typeof(Sprite));
            _itemIndicatorSpriteArray["pin6_on_solved"] = (Sprite)Resources.Load("Images/levelpin_solved_1", typeof(Sprite));

            _itemIndicatorSpriteArray["pin7_off_unsolved"] = (Sprite)Resources.Load("Images/levelpin_unsolved_0", typeof(Sprite));
            _itemIndicatorSpriteArray["pin7_off_solved"] = (Sprite)Resources.Load("Images/levelpin_solved_0", typeof(Sprite));
            _itemIndicatorSpriteArray["pin7_on_unsolved"] = (Sprite)Resources.Load("Images/levelpin_unsolved_1", typeof(Sprite));
            _itemIndicatorSpriteArray["pin7_on_solved"] = (Sprite)Resources.Load("Images/levelpin_solved_1", typeof(Sprite));

            _itemIndicatorSpriteArray["pin8_off_unsolved"] = (Sprite)Resources.Load("Images/levelpin_unsolved_0", typeof(Sprite));
            _itemIndicatorSpriteArray["pin8_off_solved"] = (Sprite)Resources.Load("Images/levelpin_solved_0", typeof(Sprite));
            _itemIndicatorSpriteArray["pin8_on_unsolved"] = (Sprite)Resources.Load("Images/levelpin_unsolved_1", typeof(Sprite));
            _itemIndicatorSpriteArray["pin8_on_solved"] = (Sprite)Resources.Load("Images/levelpin_solved_1", typeof(Sprite));

            _itemIndicatorSpriteArray["pin9_off_unsolved"] = (Sprite)Resources.Load("Images/levelpin_unsolved_0", typeof(Sprite));
            _itemIndicatorSpriteArray["pin9_off_solved"] = (Sprite)Resources.Load("Images/levelpin_solved_0", typeof(Sprite));
            _itemIndicatorSpriteArray["pin9_on_unsolved"] = (Sprite)Resources.Load("Images/levelpin_unsolved_1", typeof(Sprite));
            _itemIndicatorSpriteArray["pin9_on_solved"] = (Sprite)Resources.Load("Images/levelpin_solved_1", typeof(Sprite));

            _itemIndicatorSpriteArray["sortbyItems_off_unsolved"] = (Sprite)Resources.Load("Images/levelsort_unsolved_0", typeof(Sprite));
            _itemIndicatorSpriteArray["sortbyItems_off_solved"] = (Sprite)Resources.Load("Images/levelsort_solved_0", typeof(Sprite));
            _itemIndicatorSpriteArray["sortbyItems_on_unsolved"] = (Sprite)Resources.Load("Images/levelsort_unsolved_1", typeof(Sprite));
            _itemIndicatorSpriteArray["sortbyItems_on_solved"] = (Sprite)Resources.Load("Images/levelsort_solved_1", typeof(Sprite));

        }
    }

    // Update is called once per frame
    void Update () {

    }


    /// <summary>
    /// generates stage according to Stage parameter.
    /// </summary>
    /// <param name="stage"></param>
    public void generate_Stage(Stage stage,Boolean restartIndicator)
    {

        #region It ıs determined whether stage has special room 

        bool Isgenerate = false;
        bool IsItemgenerate = false;

        if (stage.specialStep == c_1)
        {
            Isgenerate = true;
        }

        else 
        {
            Isgenerate = (performs_spe_poss(stage.specialStep, 1 - stage.specialStep) == stage.specialStep.ToString());

        }

        #endregion

        #region If there is not special room 

        if (!Isgenerate)
        {
            performs_without_special(stage);
        }

        #endregion

        #region if there is special room

        else
        {
            performs_with_special(stage);

        }

        #endregion

        #region It ıs determined whether stage has possibility items 

        if (stage.posGoods == c_1)
        {
            IsItemgenerate = true;
        }
        else
        {
            IsItemgenerate = (performs_spe_poss(stage.posGoods, 1 - stage.posGoods) == stage.posGoods.ToString());
        }

        #endregion


        #region It ıs performed according to IsItemgenerate's value.

        if(IsItemgenerate && restartIndicator == false)
        {

            m_wonItemIndicator(stage);
           
        }
        else
        {
            #region Try to generate current stage with _rooms and AllLevels.Instance.Items

            performs_current_stage(_rooms, AllLevels.Instance.Items, stage);

            #endregion
        }

        #endregion
    }

    public void m_wonItemIndicator(Stage stage) {
        if(AllLevels.Instance.Items.get_slots_number() == 4)
        performs_current_stage(_rooms, AllLevels.Instance.Items, stage);
        else
        StartCoroutine(wonItemIndicator(stage));

    }

    IEnumerator wonItemIndicator(Stage stage)
    {
        IsItemWon.SetActive(true);
        string specialItem = performs_which_special_item(stage);
        switch (specialItem)
        {

            case "showWrongDoor1":
                itemImage.sprite = itemSprites[0];
               
                break;
            case "showWrongDoor2":
                itemImage.sprite = itemSprites[1];

                break;
            case "tripleHearts":
                itemImage.sprite = itemSprites[2];

                break;
            case "keyItem":
                itemImage.sprite = itemSprites[3];

                break;
            case "goOtherRoom":
                itemImage.sprite = itemSprites[4];

                break;
            case "checkPointItem":
                itemImage.sprite = itemSprites[5];

                break;
            case "neverGoBack":
                itemImage.sprite = itemSprites[6];

                break;
            case "jumpintolastRoom":
                itemImage.sprite = itemSprites[7];

                break;
        }

        yield return new WaitForSeconds(1f);
        IsItemWon.SetActive(false);
        performs_special_item(stage,specialItem);
        performs_current_stage(_rooms, AllLevels.Instance.Items, stage);
    }



    /// <summary>
    /// generates stage with special room
    /// </summary>
    /// <param name="stage"></param>
    private void performs_with_special(Stage stage)
    {

        string _specialRoom  = performs_which_special(stage);
        Room specialRoom = new Room(_specialRoom);
        System.Random rnd = new System.Random();

        performs_without_special(stage);

        int rndIndex = rnd.Next(0, _rooms.Count);

        _rooms.RemoveAt(rndIndex);

        _rooms.Add(specialRoom);

        _rooms.Shuffle();

    }

    /// <summary>
    /// generates stage without special room
    /// </summary>
    /// <param name="stage"></param>
    private void performs_without_special(Stage stage)
    {

        _rooms.Clear();

        foreach(KeyValuePair<string,GenericObject> entry in stage.dictProperty)
        {
            switch (entry.Key)
            {
                case "binaryOption":
                    for(int i = 0; i< entry.Value.GetValue<int>(); i++) { 

                        Room room = new Room("binaryOption");
                        _rooms.Add(room);
                    }
                    break;
                case "tripleOption":
                    for (int i = 0; i < entry.Value.GetValue<int>(); i++)
                    {

                        Room room = new Room("tripleOption");
                        _rooms.Add(room);

                    }
                    break;
                case "quadOption":
                    for (int i = 0; i < entry.Value.GetValue<int>(); i++)
                    {

                        Room room = new Room("quadOption");
                        _rooms.Add(room);

                    }
                    break;
            }
           
        }

        _rooms.Shuffle();

    }


    /// <summary>
    /// determines whether Stage has speical item or room according to list parameters
    /// </summary>
    /// <param name="list"></param>
    /// <returns></returns>
    public string performs_spe_poss(params float[] list)
    {
        int listCount = list.Length;
        string return_value = String.Empty;

        Randomizator3000.Item<string>[] list_poss;

        list_poss = new Randomizator3000.Item<string>[listCount];


        for(int i = 0; i< listCount; i++)
        {
            list_poss[i] = new Randomizator3000.Item<string>();
            list_poss[i].weight = list[i] * 100;
            list_poss[i].value  = list[i].ToString();

        }

        return_value = Randomizator3000.PickOne<string>(list_poss);

		return return_value;
    }


    /// <summary>
    /// determines which special room is to be generated according to stage's possible values
    /// </summary>
    /// <param name="stage"></param>
    /// <returns></returns>
    public string performs_which_special(Stage stage)
    {
        int listCount = 14;
        string return_value = String.Empty;
        List<String> keys_from_dic = new List<string>();
        List<GenericObject> values_from_dic = new List<GenericObject>();

        Randomizator3000.Item<string>[] list_poss;

        list_poss = new Randomizator3000.Item<string>[listCount];

        foreach (KeyValuePair<string,GenericObject> keys in stage.dictProperty)
        {
            keys_from_dic.Add(keys.Key);
            values_from_dic.Add(keys.Value);
          
        }

        for(int i = 0; i<14; i++ )
        {
            list_poss[i] = new Randomizator3000.Item<string>();
            list_poss[i].weight = values_from_dic[i+7].GetValue<float>() * 100;
            list_poss[i].value = keys_from_dic[i+7].ToString();

        }

        return_value = Randomizator3000.PickOne<string>(list_poss);
        // todo room generate 
		return return_value;
    }


    /// <summary>
    ///  determines which special item is to be generated according to stage's possible values
    /// </summary>
    /// <param name="stage"></param>
    /// <returns></returns>
    public string performs_which_special_item(Stage stage)
    {
        int listCount = 8;
        string return_value = String.Empty;
        List<String> keys_from_dic = new List<string>();
        List<GenericObject> values_from_dic = new List<GenericObject>();

        Randomizator3000.Item<string>[] list_poss;

        list_poss = new Randomizator3000.Item<string>[listCount];

        foreach (KeyValuePair<string, GenericObject> keys in stage.dictProperty)
        {
            keys_from_dic.Add(keys.Key);
            values_from_dic.Add(keys.Value);

        }

        for (int i = 0; i < 8; i++)
        {
            list_poss[i] = new Randomizator3000.Item<string>();
            list_poss[i].weight = values_from_dic[i + 22].GetValue<float>() * 100;
            list_poss[i].value = keys_from_dic[i + 22].ToString();

        }

        return_value = Randomizator3000.PickOne<string>(list_poss);

		return return_value;
    }


    /// <summary>
    /// performs special item and saves it to local memory 
    /// </summary>
    /// <param name="stage"></param>
    private void performs_special_item(Stage stage,String specialItem)
    {
     
        AllLevels.Instance.Items.AddItem(specialItem);
       
        AllLevels.Instance.Items.fill_keys_values();

        AllLevels.Instance.Save();
    }


    /// <summary>
    /// performs current stage according to parameters
    /// </summary>
    /// <param name="current_rooms"></param>
    /// <param name="current_ıtems"></param>
    /// <param name="stage"></param>
    private void performs_current_stage(List<Room>  current_rooms,Inventory inventory ,Stage stage)
    {
        /* Current stage is preparing according to parameters */

        GameObject newRoom;
        GameObject Canvas = GameObject.Find("Canvas");
        bool IsDevoded = false;
        bool IsSignOption = false;

        #region Current Stage Creation

        #region Rooms creation

        newRoom = null;

        RoomObjectList.Clear();

        for (int i = 0; i < current_rooms.Count; i++)
        {
            newRoom = Instantiate(Resources.Load("room", typeof(GameObject))) as GameObject;
            newRoom.transform.parent = GameObject.Find("Canvas").transform;
            newRoom.gameObject.name = i.ToString() + "room";
            newRoom.GetComponent<RectTransform>().ResetRectTransformation();
            newRoom.GetComponent<RectTransform>().ResetRectTransformationSize();
            newRoom.GetComponent<Transform>().localScale = new Vector3(c_1, c_1, c_1);
            newRoom.GetComponent<Room>().MyRoomType = current_rooms[i].MyRoomType;
            newRoom.GetComponent<Room>().RoomNumber = i;
            current_rooms[i].RoomNumber = i;
            newRoom.GetComponent<Room>().ThisGameObject = newRoom;
            newRoom.GetComponent<Room>().configure_Room(newRoom);
            newRoom.GetComponent<Room>().setAllChildDoor(newRoom);
            
            // Oda türüne göre Child'ların valueleri set edilecek. //todo HALILU

            switch (newRoom.GetComponent<Room>().MyRoomType)
            {
                case Room.roomType.beFast:
                    newRoom.GetComponent<Room>().setChildValue();
                    newRoom.GetComponent<Room>().RoomName = "beFast";
                    break;
                case Room.roomType.binaryOption:
                    newRoom.GetComponent<Room>().setChildValue();
                    newRoom.GetComponent<Room>().RoomName = "binaryOption";
                    break;
                case Room.roomType.checkPoint:
                    newRoom.GetComponent<Room>().setChildValue();
                    newRoom.GetComponent<Room>().RoomName = "checkPoint";
                    break;
                case Room.roomType.dameOption:
                    newRoom.GetComponent<Room>().setChildValue();
                    newRoom.GetComponent<Room>().RoomName = "dameOption";
                    break;
                case Room.roomType.devoted:

                    newRoom.GetComponent<Room>().setChildValue();
                    newRoom.GetComponent<Room>().RoomName = "devoted";

                    while (!IsDevoded)
                    {
                        for (int m = 0; m < newRoom.GetComponent<Room>()._doors.Count; m++)
                        {

                            if (newRoom.GetComponent<Room>()._doors[m].Decision == 2)
                            {

                                if (newRoom.GetComponent<Room>()._doors[m].gameObject.CompareTag("corruption"))
                                {

                                    IsDevoded = false;

                                    newRoom.GetComponent<Room>().setChildValue();

                                    break;

                                }
                                else
                                {
                                    IsDevoded = true;

                                    break;

                                }
                            }
                        }

                    }

                    break;
                case Room.roomType.dontMistake:
                    newRoom.GetComponent<Room>().setChildValue();
                    newRoom.GetComponent<Room>().RoomName = "dontMistake";
                    break;
                case Room.roomType.doRight:
                    newRoom.GetComponent<Room>().setChildValue();
                    newRoom.GetComponent<Room>().RoomName = "doRight";
                    break;
                case Room.roomType.doubleRight:
                    newRoom.GetComponent<Room>().setChildValue();
                    newRoom.GetComponent<Room>().RoomName = "doubleRight";
                    break;
                case Room.roomType.pin5:
                    newRoom.GetComponent<Room>().setChildForPın();
                    newRoom.GetComponent<Room>().RoomName = "pin5";
                    break;
                case Room.roomType.pin6:
                    newRoom.GetComponent<Room>().setChildForPın();
                    newRoom.GetComponent<Room>().RoomName = "pin6";
                    break;
                case Room.roomType.pin7:
                    newRoom.GetComponent<Room>().setChildForPın();
                    newRoom.GetComponent<Room>().RoomName = "pin7";
                    break;
                case Room.roomType.pin8:
                    newRoom.GetComponent<Room>().setChildForPın();
                    newRoom.GetComponent<Room>().RoomName = "pin8";
                    break;
                case Room.roomType.pin9:
                    newRoom.GetComponent<Room>().setChildForPın();
                    newRoom.GetComponent<Room>().RoomName = "pin9";
                    break;
                case Room.roomType.quadOption:
                    newRoom.GetComponent<Room>().setChildValue();
                    newRoom.GetComponent<Room>().RoomName = "quadOption";
                    break;
                case Room.roomType.signOption:

                    newRoom.GetComponent<Room>().setChildValue();
                    newRoom.GetComponent<Room>().RoomName = "signOption";

                    while (!IsSignOption)
                    {
                        for (int m = 0; m < newRoom.GetComponent<Room>()._doors.Count; m++)
                        {

                            if (newRoom.GetComponent<Room>()._doors[m].Decision == 2)
                            {

                                if (newRoom.GetComponent<Room>()._doors[m].gameObject.CompareTag("signoption"))
                                {

                                    IsSignOption = false;

                                    newRoom.GetComponent<Room>().setChildValue();

                                    break;

                                }
                                else
                                {
                                    IsSignOption = true;

                                    break;

                                }
                            }
                        }

                    }

                    break;
                case Room.roomType.sortbyItems:
                    newRoom.GetComponent<Room>().setChildForSorted();
                    newRoom.GetComponent<Room>().RoomName = "sortbyItems";
                    break;
                case Room.roomType.tripleOption:
                    newRoom.GetComponent<Room>().setChildValue();
                    newRoom.GetComponent<Room>().RoomName = "tripleOption";
                    break;
            }

            RoomObjectList.Add(i, newRoom);

            newRoom = null;

        }

        // İlk oda aktif , diğer odalar pasif olarak kapatılıyor. 

        foreach (KeyValuePair<int, GameObject> pair in RoomObjectList)
        {
            if (pair.Key != 0)
            {
                RoomObjectList[pair.Key].SetActive(false);
            }else
            {
                RoomObjectList[pair.Key].GetComponent<Room>().IsBlink1 = true;
            }
        }
          
        #endregion

        #region Generate UI Items 

        perform_uItem();

        #endregion

        #region Generate Level Indicator's UI

        perform_LevelIndicatorUI(stage);

        #endregion

        #region Generate Health Bar for Stage

        performs_healthBar_Stage(stage);

        #endregion

        #endregion

    }

    public List<Room> get_rooms()
    {
       return _rooms;

    }

    public void perform_uItem()
    {
        GameObject parentItemHole;
        Image newItemhole;

        // Generating four holes for items

        if (GameObject.FindGameObjectWithTag("itemholeparent") != null)
        {
            Destroy(GameObject.FindGameObjectWithTag("itemholeparent").gameObject);
        }

        parentItemHole = null;

        parentItemHole = Instantiate(Resources.Load("ItemHoleParent", typeof(GameObject))) as GameObject;
        Destroy(parentItemHole.GetComponent<Room>());
        parentItemHole.transform.parent = GameObject.Find("Canvas").transform;
        parentItemHole.gameObject.name = "ItemHoleParent";
        parentItemHole.GetComponent<RectTransform>().ResetRectTransformation();
        parentItemHole.GetComponent<Transform>().localScale = new Vector3(c_1, c_1, c_1);

        newItemhole = null;

        for (int i = 0; i < 4; i++)
        {
            newItemhole = Instantiate(Resources.Load("itemhole", typeof(Image))) as Image;
            newItemhole.transform.SetParent(parentItemHole.transform);
            newItemhole.GetComponent<RectTransform>().ResetRectTransformation();
            newItemhole.gameObject.transform.localScale = new Vector3(c_1, c_1, c_1);
            newItemhole.gameObject.name = newItemhole.gameObject.name.Split('(')[0] + (i + 1).ToString();
            newItemhole.GetComponent<ItemHole>().MyItemName = "null";

            switch (i)
            {
                case 0:

                    if (AllLevels.Instance.Items.Keys.Length >= i + 1)
                    {
                        newItemhole.GetComponent<Image>().sprite = _itemSpriteArray[AllLevels.Instance.Items.Values[i]];
                        newItemhole.GetComponent<ItemHole>().MyItemName = AllLevels.Instance.Items.Values[i];
                    }

                    newItemhole.GetComponent<RectTransform>().anchoredPosition = new Vector2(parentItemHole.GetComponent<RectTransform>().offsetMax.x +
                                                                                             newItemhole.GetComponent<RectTransform>().sizeDelta.x / 4,
                                                                                             0);
                    break;
                case 1:

                    if (AllLevels.Instance.Items.Keys.Length >= i + 1)
                    {
                        newItemhole.GetComponent<Image>().sprite = _itemSpriteArray[AllLevels.Instance.Items.Values[i]];
                        newItemhole.GetComponent<ItemHole>().MyItemName = AllLevels.Instance.Items.Values[i];
                    }

                    newItemhole.GetComponent<RectTransform>().anchoredPosition = new Vector2(parentItemHole.GetComponent<RectTransform>().offsetMax.x +
                                                                                             newItemhole.GetComponent<RectTransform>().sizeDelta.x / 4,
                                                                                              -60);
                    break;
                case 2:

                    if (AllLevels.Instance.Items.Keys.Length >= i + 1)
                    {
                        newItemhole.GetComponent<Image>().sprite = _itemSpriteArray[AllLevels.Instance.Items.Values[i]];
                        newItemhole.GetComponent<ItemHole>().MyItemName = AllLevels.Instance.Items.Values[i];
                    }

                    newItemhole.GetComponent<RectTransform>().anchoredPosition = new Vector2(parentItemHole.GetComponent<RectTransform>().offsetMax.x +
                                                                                             newItemhole.GetComponent<RectTransform>().sizeDelta.x / 4,
                                                                                             -120);
                    break;
                case 3:

                    if (AllLevels.Instance.Items.Keys.Length >= i + 1)
                    {
                        newItemhole.GetComponent<Image>().sprite = _itemSpriteArray[AllLevels.Instance.Items.Values[i]];
                        newItemhole.GetComponent<ItemHole>().MyItemName = AllLevels.Instance.Items.Values[i];
                    }

                    newItemhole.GetComponent<RectTransform>().anchoredPosition = new Vector2(parentItemHole.GetComponent<RectTransform>().offsetMax.x +
                                                                                             newItemhole.GetComponent<RectTransform>().sizeDelta.x / 4,
                                                                                             -180);

                    break;
            }

            newItemhole = null;
        }
    }

    public void perform_LevelIndicatorUI(Stage stage)
    {
        GameObject parentIndicator;
        Image newItemhole;
        const int downValue = -200;
        int stageLong = stage.stageLength;
		int pageNumbers = 0;

        // generating item parent gameobject 

        if (GameObject.FindGameObjectWithTag("itemindicatorparent") != null)
        {
            Destroy(GameObject.FindGameObjectWithTag("itemindicatorparent").gameObject);
        }

        parentIndicator = null;

        parentIndicator = Instantiate(Resources.Load("ItemIndicatorParent", typeof(GameObject))) as GameObject;
        Destroy(parentIndicator.GetComponent<Room>());
        parentIndicator.transform.parent = GameObject.Find("Canvas").transform;
        parentIndicator.gameObject.name = "ItemIndicatorParent";
        parentIndicator.GetComponent<RectTransform>().ResetRectTransformation();
        parentIndicator.GetComponent<Transform>().localScale = new Vector3(c_1, c_1, c_1);

        newItemhole = null;

		for (int i = 0; i < stageLong; i++) {

			parentIndicator.GetComponent<ItemIndicatorParent>().RoomNames.Add(i+1 , RoomObjectList [i].GetComponent<Room> ().RoomName);

		}

		for (int i = 0; i < 16; i++) {
			
			newItemhole = Instantiate (Resources.Load ("itemindicatorhole", typeof(Image))) as Image;
			newItemhole.transform.SetParent (parentIndicator.transform);
			newItemhole.GetComponent<RectTransform> ().ResetRectTransformation ();
			newItemhole.gameObject.transform.localScale = new Vector3 (c_1, c_1, c_1);
			newItemhole.gameObject.name = newItemhole.gameObject.name.Split ('(') [0] + (i + 1).ToString ();
			newItemhole.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (parentIndicator.GetComponent<RectTransform> ().offsetMin.x -
			newItemhole.GetComponent<RectTransform> ().sizeDelta.x * 2,
				downValue + newItemhole.GetComponent<RectTransform> ().sizeDelta.x * 3 / 2 * i + i);

			parentIndicator.GetComponent<ItemIndicatorParent>().Indicators.Add(i+1 , newItemhole.GetComponent<ItemIndicatorHole>());
			// Till sixteen elements are determined by Roomtype , The process handles.

			if ((i + 1) <= stageLong) {
				
//				RoomObjectList [i].GetComponent<Room> ().MyIndicatorHole = newItemhole.GetComponent<ItemIndicatorHole> ();
				newItemhole.gameObject.GetComponent<ItemIndicatorHole> ().MyRoomName = RoomObjectList [i].GetComponent<Room> ().RoomName;
				newItemhole.gameObject.GetComponent<ItemIndicatorHole> ().StageGenerator = this;

				if (i == 0) {
					newItemhole.gameObject.GetComponent<ItemIndicatorHole> ().change_first_level_indicator_sprite ("on_unsolved");
					newItemhole.gameObject.GetComponent<ItemIndicatorHole> ().change_itemIndicatorholeSolvedInf ();
				}

				// Changing first hole object according to first chosed room name 

			}

                // Other Items has emtpy room type .

                else {
				newItemhole.GetComponent<ItemIndicatorHole> ().MyRoomName = String.Empty;
				newItemhole.GetComponent<ItemIndicatorHole> ().StageGenerator = this;
			}

		}

		int activeIndicator = 0;

		for (int i = 1; i <= stageLong; i++) {

			if (i <= 16) {

				pageNumbers = 1;
			} 
			else {

				int kalan = 0;
				int bolum = 0;
				kalan = i % 16;
				bolum = i / 16;

				if (kalan == 0) {

					pageNumbers = bolum;
				} 
				else 
				{
					pageNumbers = bolum + 1;
				}


			}

			if (i <= 16) {

				activeIndicator = i;
			}
			else {

				int bolum2 = 0;
				int kalan2 = 0;

				kalan2 = i % 16;
				bolum2 = i / 16;

				if (kalan2 == 0)
					activeIndicator = 16;
				else
				activeIndicator = kalan2;

			}

			parentIndicator.GetComponent<ItemIndicatorParent> ().Indicators [activeIndicator].historyPagesSprite.Add (pageNumbers,
				parentIndicator.GetComponent<ItemIndicatorParent> ().RoomNames [i]);

		//	Debug.Log (activeIndicator + pageNumbers.ToString () + parentIndicator.GetComponent<ItemIndicatorParent> ().RoomNames [i]);

		}
    }



    /// <summary>
    /// Change element sprite in _itemIndicatorSpriteArray
    /// </summary>
    /// <param name="roomName"></param>
    /// <param name="newItemHole"></param>

    private void performs_itemhearts_shield(Stage stage)
    {
        GameObject parentHealthBar;

        Image newHealthBarBox = null;
        int healthBarLong = stage.hearts;
        int offsetforHealthBar = 16;

        if (GameObject.FindGameObjectWithTag("healthbarparent") != null)
        {
            Destroy(GameObject.FindGameObjectWithTag("healthbarparent").gameObject);
        }

        // generating item parent gameobject 

        parentHealthBar = null;

        parentHealthBar = Instantiate(Resources.Load("healthBarParent", typeof(GameObject))) as GameObject;
        parentHealthBar.transform.parent = GameObject.Find("Canvas").transform;
        parentHealthBar.gameObject.name = "healthBarParent";
        parentHealthBar.GetComponent<RectTransform>().ResetRectTransformation();
        parentHealthBar.GetComponent<Transform>().localScale = new Vector3(c_1, c_1, c_1);
        parentHealthBar.GetComponent<HealthBarParent>().HealthBarBoxCount = healthBarLong;
        parentHealthBar.GetComponent<HealthBarParent>().RedBars.Clear();
        parentHealthBar.GetComponent<HealthBarParent>().WhiteBars.Clear();
        parentHealthBar.GetComponent<HealthBarParent>().BlueBars.Clear();

        if (healthBarLong <= 30)
        {
            for (int i = 1; i <= healthBarLong; i++)
            {
                newHealthBarBox = Instantiate(Resources.Load("healthbarBox", typeof(Image))) as Image;
                newHealthBarBox.transform.SetParent(parentHealthBar.transform);
                newHealthBarBox.GetComponent<RectTransform>().ResetRectTransformation();
                newHealthBarBox.gameObject.transform.localScale = new Vector3(c_1, c_1, c_1);
                newHealthBarBox.gameObject.name = "bluebar" + (i).ToString();
                newHealthBarBox.GetComponent<Image>().sprite = _itemHealthBarDict["detectionmarker_0"];
                newHealthBarBox.GetComponent<HealthBar>().MyHealthBarType = HealthBar.HealthBarType.blue;
                parentHealthBar.GetComponent<HealthBarParent>().BlueBars.Add(i, newHealthBarBox.GetComponent < HealthBar>());

                    if (i > 1)
                    {
                        newHealthBarBox.GetComponent<RectTransform>().anchoredPosition = new Vector2(parentHealthBar.GetComponent<RectTransform>().offsetMax.x + (i-1) * offsetforHealthBar + 100,
                                                                                                     parentHealthBar.GetComponent<RectTransform>().offsetMin.y - offsetforHealthBar);
                    }
                    else
                    {
                        newHealthBarBox.GetComponent<RectTransform>().anchoredPosition = new Vector2(parentHealthBar.GetComponent<RectTransform>().offsetMax.x + 100 ,
                                                                                                    parentHealthBar.GetComponent<RectTransform>().offsetMin.y - offsetforHealthBar);
                    }
            }

        }
        else
        {
            int whiteGrayBoxCount = healthBarLong / 30;
            int blueBoxCount = healthBarLong % 30;

            //  Generating blue box

            for (int i = 1; i <= blueBoxCount; i++)
            {
                newHealthBarBox = Instantiate(Resources.Load("healthbarBox", typeof(Image))) as Image;
                newHealthBarBox.transform.SetParent(parentHealthBar.transform);
                newHealthBarBox.GetComponent<RectTransform>().ResetRectTransformation();
                newHealthBarBox.gameObject.transform.localScale = new Vector3(c_1, c_1, c_1);
                newHealthBarBox.gameObject.name = "bluebar" + (i).ToString();
                newHealthBarBox.GetComponent<Image>().sprite = _itemHealthBarDict["detectionmarker_0"];
                newHealthBarBox.GetComponent<HealthBar>().MyHealthBarType = HealthBar.HealthBarType.blue;
                parentHealthBar.GetComponent<HealthBarParent>().BlueBars.Add(i, newHealthBarBox.GetComponent<HealthBar>());

                if (i > 1)
                {
                    newHealthBarBox.GetComponent<RectTransform>().anchoredPosition = new Vector2(parentHealthBar.GetComponent<RectTransform>().offsetMax.x + (i - 1) * offsetforHealthBar + 100,
                                                                                                 parentHealthBar.GetComponent<RectTransform>().offsetMin.y - offsetforHealthBar);
                }
                else
                {
                    newHealthBarBox.GetComponent<RectTransform>().anchoredPosition = new Vector2(parentHealthBar.GetComponent<RectTransform>().offsetMax.x + 100,
                                                                                                parentHealthBar.GetComponent<RectTransform>().offsetMin.y - offsetforHealthBar);
                }
            }

            // Generating open white box

            for (int i = 1; i <= whiteGrayBoxCount; i++)
            {
                newHealthBarBox = Instantiate(Resources.Load("healthbarBox", typeof(Image))) as Image;
                newHealthBarBox.transform.SetParent(parentHealthBar.transform);
                newHealthBarBox.GetComponent<RectTransform>().ResetRectTransformation();
                newHealthBarBox.gameObject.transform.localScale = new Vector3(c_1, c_1, c_1);
                newHealthBarBox.gameObject.name = "whitebar" + (i).ToString();
                newHealthBarBox.GetComponent<Image>().sprite = _itemHealthBarDict["detectionmarker_1"];
                newHealthBarBox.GetComponent<HealthBar>().MyHealthBarType = HealthBar.HealthBarType.gray;
                parentHealthBar.GetComponent<HealthBarParent>().WhiteBars.Add(i, newHealthBarBox.GetComponent<HealthBar>());


                if (i > 1)
                {
                    newHealthBarBox.GetComponent<RectTransform>().anchoredPosition = new Vector2(parentHealthBar.GetComponent<RectTransform>().offsetMax.x + (i - 1) * offsetforHealthBar + 100,
                                                                                                 parentHealthBar.GetComponent<RectTransform>().offsetMin.y - offsetforHealthBar * 2);
                }
                else
                {
                    newHealthBarBox.GetComponent<RectTransform>().anchoredPosition = new Vector2(parentHealthBar.GetComponent<RectTransform>().offsetMax.x + 100,
                                                                                                parentHealthBar.GetComponent<RectTransform>().offsetMin.y - offsetforHealthBar * 2);
                }
            }
        }
    }


	private void performs_healthBar_Stage(Stage stage)
	{
		GameObject parentHealthBar;

		Image newHealthBarBox = null;
		int healthBarLong = stage.hearts;
		int offsetforHealthBar = 16;

		if (GameObject.FindGameObjectWithTag("healthbarparent") != null)
		{
			Destroy(GameObject.FindGameObjectWithTag("healthbarparent").gameObject);
		}

		// generating item parent gameobject 

		parentHealthBar = null;

		parentHealthBar = Instantiate(Resources.Load("healthBarParent", typeof(GameObject))) as GameObject;
		parentHealthBar.transform.parent = GameObject.Find("Canvas").transform;
		parentHealthBar.gameObject.name = "healthBarParent";
		parentHealthBar.GetComponent<RectTransform>().ResetRectTransformation();
		parentHealthBar.GetComponent<Transform>().localScale = new Vector3(c_1, c_1, c_1);
		parentHealthBar.GetComponent<HealthBarParent>().HealthBarBoxCount = healthBarLong;
		parentHealthBar.GetComponent<HealthBarParent>().RedBars.Clear();
		parentHealthBar.GetComponent<HealthBarParent>().WhiteBars.Clear();
		parentHealthBar.GetComponent<HealthBarParent>().BlueBars.Clear();

		if (healthBarLong <= 30)
		{
			for (int i = 1; i <= healthBarLong; i++)
			{
				newHealthBarBox = Instantiate(Resources.Load("healthbarBox", typeof(Image))) as Image;
				newHealthBarBox.transform.SetParent(parentHealthBar.transform);
				newHealthBarBox.GetComponent<RectTransform>().ResetRectTransformation();
				newHealthBarBox.gameObject.transform.localScale = new Vector3(c_1, c_1, c_1);
				newHealthBarBox.gameObject.name = "bluebar" + (i).ToString();
				newHealthBarBox.GetComponent<Image>().sprite = _itemHealthBarDict["detectionmarker_0"];
				newHealthBarBox.GetComponent<HealthBar>().MyHealthBarType = HealthBar.HealthBarType.blue;
				parentHealthBar.GetComponent<HealthBarParent>().BlueBars.Add(i, newHealthBarBox.GetComponent < HealthBar>());

				if (i > 1)
				{
					newHealthBarBox.GetComponent<RectTransform>().anchoredPosition = new Vector2(parentHealthBar.GetComponent<RectTransform>().offsetMax.x + (i-1) * offsetforHealthBar + 100,
						parentHealthBar.GetComponent<RectTransform>().offsetMin.y - offsetforHealthBar);
				}
				else
				{
					newHealthBarBox.GetComponent<RectTransform>().anchoredPosition = new Vector2(parentHealthBar.GetComponent<RectTransform>().offsetMax.x + 100 ,
						parentHealthBar.GetComponent<RectTransform>().offsetMin.y - offsetforHealthBar);
				}
			}

		}
		else
		{
			int whiteGrayBoxCount = healthBarLong / 30;
			int blueBoxCount = healthBarLong % 30;

			//  Generating blue box

			for (int i = 1; i <= blueBoxCount; i++)
			{
				newHealthBarBox = Instantiate(Resources.Load("healthbarBox", typeof(Image))) as Image;
				newHealthBarBox.transform.SetParent(parentHealthBar.transform);
				newHealthBarBox.GetComponent<RectTransform>().ResetRectTransformation();
				newHealthBarBox.gameObject.transform.localScale = new Vector3(c_1, c_1, c_1);
				newHealthBarBox.gameObject.name = "bluebar" + (i).ToString();
				newHealthBarBox.GetComponent<Image>().sprite = _itemHealthBarDict["detectionmarker_0"];
				newHealthBarBox.GetComponent<HealthBar>().MyHealthBarType = HealthBar.HealthBarType.blue;
				parentHealthBar.GetComponent<HealthBarParent>().BlueBars.Add(i, newHealthBarBox.GetComponent<HealthBar>());

				if (i > 1)
				{
					newHealthBarBox.GetComponent<RectTransform>().anchoredPosition = new Vector2(parentHealthBar.GetComponent<RectTransform>().offsetMax.x + (i - 1) * offsetforHealthBar + 100,
						parentHealthBar.GetComponent<RectTransform>().offsetMin.y - offsetforHealthBar);
				}
				else
				{
					newHealthBarBox.GetComponent<RectTransform>().anchoredPosition = new Vector2(parentHealthBar.GetComponent<RectTransform>().offsetMax.x + 100,
						parentHealthBar.GetComponent<RectTransform>().offsetMin.y - offsetforHealthBar);
				}
			}

			// Generating open white box

			for (int i = 1; i <= whiteGrayBoxCount; i++)
			{
				newHealthBarBox = Instantiate(Resources.Load("healthbarBox", typeof(Image))) as Image;
				newHealthBarBox.transform.SetParent(parentHealthBar.transform);
				newHealthBarBox.GetComponent<RectTransform>().ResetRectTransformation();
				newHealthBarBox.gameObject.transform.localScale = new Vector3(c_1, c_1, c_1);
				newHealthBarBox.gameObject.name = "whitebar" + (i).ToString();
				newHealthBarBox.GetComponent<Image>().sprite = _itemHealthBarDict["detectionmarker_1"];
				newHealthBarBox.GetComponent<HealthBar>().MyHealthBarType = HealthBar.HealthBarType.gray;
				parentHealthBar.GetComponent<HealthBarParent>().WhiteBars.Add(i, newHealthBarBox.GetComponent<HealthBar>());


				if (i > 1)
				{
					newHealthBarBox.GetComponent<RectTransform>().anchoredPosition = new Vector2(parentHealthBar.GetComponent<RectTransform>().offsetMax.x + (i - 1) * offsetforHealthBar + 100,
						parentHealthBar.GetComponent<RectTransform>().offsetMin.y - offsetforHealthBar * 2);
				}
				else
				{
					newHealthBarBox.GetComponent<RectTransform>().anchoredPosition = new Vector2(parentHealthBar.GetComponent<RectTransform>().offsetMax.x + 100,
						parentHealthBar.GetComponent<RectTransform>().offsetMin.y - offsetforHealthBar * 2);
				}
			}
		}
	}
		
	public void performs_itemshields()
	{
		GameObject parentShieldsBar;
		int itemShieldCount = 3; 

		Image newShieldBox = null;
		int offsetforHealthBar = 16;

		if (GameObject.FindGameObjectWithTag("itemshieldparent") != null)
		{
			Destroy(GameObject.FindGameObjectWithTag("itemshieldparent").gameObject);
		}

		// generating item parent gameobject 

		parentShieldsBar = null;

		parentShieldsBar = Instantiate(Resources.Load("itemshieldParent", typeof(GameObject))) as GameObject;
		parentShieldsBar.transform.parent = GameObject.Find("Canvas").transform;
		parentShieldsBar.gameObject.name = "itemshieldParent";
		parentShieldsBar.GetComponent<RectTransform>().ResetRectTransformation();
		parentShieldsBar.GetComponent<Transform>().localScale = new Vector3(c_1, c_1, c_1);
		parentShieldsBar.GetComponent<ItemShieldParent>().ItemShieldCount = itemShieldCount;
		parentShieldsBar.GetComponent<ItemShieldParent>().ItemShieldBars.Clear();
	
		for (int i = 1; i <= itemShieldCount; i++)
			{
			newShieldBox = Instantiate(Resources.Load("itemshieldBox", typeof(Image))) as Image;
			newShieldBox.transform.SetParent(parentShieldsBar.transform);
			newShieldBox.GetComponent<RectTransform>().ResetRectTransformation();
			newShieldBox.gameObject.transform.localScale = new Vector3(c_1, c_1, c_1);
			newShieldBox.gameObject.name = "shield" + (i).ToString();
			newShieldBox.GetComponent<Image>().sprite = _itemHealthBarDict["detectionmarker_0"];
			newShieldBox.GetComponent<ItemShieldBox>().MyShieldBarType = ItemShieldBox.ItemShieldType.off;
			parentShieldsBar.GetComponent<ItemShieldParent>().ItemShieldBars.Add(i, newShieldBox.GetComponent < ItemShieldBox>());
			newShieldBox.GetComponent<RectTransform>().anchoredPosition = new Vector2(parentShieldsBar.GetComponent<RectTransform>().offsetMax.x + (i-1) * offsetforHealthBar + 12,
						parentShieldsBar.GetComponent<RectTransform>().offsetMin.y - offsetforHealthBar);

			}
	}  


	public void performs_itemshields_nevergoback()
	{
		GameObject parentShieldsBar;
		int itemShieldCount = 1; 

		Image newShieldBox = null;
		int offsetforHealthBar = 16;

		if (GameObject.FindGameObjectWithTag("itemshieldparent") != null)
		{
			Destroy(GameObject.FindGameObjectWithTag("itemshieldparent").gameObject);
		}

		// generating item parent gameobject 

		parentShieldsBar = null;

		parentShieldsBar = Instantiate(Resources.Load("itemshieldParent", typeof(GameObject))) as GameObject;
		parentShieldsBar.transform.parent = GameObject.Find("Canvas").transform;
		parentShieldsBar.gameObject.name = "itemshieldParent";
		parentShieldsBar.GetComponent<RectTransform>().ResetRectTransformation();
		parentShieldsBar.GetComponent<Transform>().localScale = new Vector3(c_1, c_1, c_1);
		parentShieldsBar.GetComponent<ItemShieldParent>().ItemShieldCount = itemShieldCount;
		parentShieldsBar.GetComponent<ItemShieldParent>().ItemShieldBars.Clear();

		for (int i = 1; i <= itemShieldCount; i++)
		{
			newShieldBox = Instantiate(Resources.Load("itemshieldBox", typeof(Image))) as Image;
			newShieldBox.transform.SetParent(parentShieldsBar.transform);
			newShieldBox.GetComponent<RectTransform>().ResetRectTransformation();
			newShieldBox.gameObject.transform.localScale = new Vector3(c_1, c_1, c_1);
			newShieldBox.gameObject.name = "shield" + (i).ToString();
			newShieldBox.GetComponent<Image>().sprite = _itemHealthBarDict["detectionmarker_0"];
			newShieldBox.GetComponent<ItemShieldBox>().MyShieldBarType = ItemShieldBox.ItemShieldType.off;
			parentShieldsBar.GetComponent<ItemShieldParent>().ItemShieldBars.Add(i, newShieldBox.GetComponent < ItemShieldBox>());
			newShieldBox.GetComponent<RectTransform>().anchoredPosition = new Vector2(parentShieldsBar.GetComponent<RectTransform>().offsetMax.x + (i-1) * offsetforHealthBar + 12,
				parentShieldsBar.GetComponent<RectTransform>().offsetMin.y - offsetforHealthBar);

		}
	}  	
}
