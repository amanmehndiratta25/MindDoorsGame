using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class ItemHole : MonoBehaviour
{

    [SerializeField]
    private string _myItemName;
    private int value;
    private Button button;
    [SerializeField]
    private Sprite onStateSprite;
    [SerializeField]
    private Sprite offStateSprite;
    [SerializeField]
    private bool IsEnabled = false;
    [SerializeField]
    float counter = 0.1f;

    GameManager _gameManager;
    ItemHoleParent _itemHoleParent;

    const string nullValue = "null";

    public string MyItemName
    {
        get
        {
            return _myItemName;
        }

        set
        {
            _myItemName = value;

         }
    }

    // Use this for initialization
    void Start()
    {

        button = this.gameObject.GetComponent<Button>();
        button.transform.localScale = new Vector3(1, 1, 1);
        button.onClick.AddListener(() => actionToMaterial(value));
        offStateSprite = this.gameObject.GetComponent<Image>().sprite;
        onStateSprite = (Sprite)Resources.Load("Images/" + getStateSprite(offStateSprite.name), typeof(Sprite));

    }

    // Update is called once per frame
    void Update()
    {

        if (IsEnabled == false)
        {
            this.gameObject.GetComponent<Image>().sprite = offStateSprite;
        }
        else
        {
            this.gameObject.GetComponent<Image>().sprite = onStateSprite;

            counter -= Time.deltaTime;

            if (counter < 0)
            {
                counter = 0.1f;    
                IsEnabled = false;
            }

        }

    }
    public void Destroy()
    {
        button.onClick.RemoveListener(() => actionToMaterial(value));
    }

    public virtual void actionToMaterial(int index)
    {
        IsEnabled = !IsEnabled;

		// If itemname is value , the process will interrupt . 

        if(this.MyItemName == nullValue)
        {
            return;
        }
			
        _gameManager = this.gameObject.GetComponentInParent<GameManager>();
        _itemHoleParent = this.gameObject.GetComponentInParent<ItemHoleParent>();
        GameObject currentRoom = _gameManager.getCurrentRoom();

		// Did the item used to in process ? 
        bool IsSuccessfull = false;
   
		// Differentiating with ItemName 
        switch (_myItemName)
        {

            case "showWrongDoor1":
                _itemHoleParent.performshowWrongDoor1(currentRoom,ref IsSuccessfull);
                break;
            case "showWrongDoor2":
				_itemHoleParent.performshowWrongDoor2(currentRoom,ref IsSuccessfull);
                break;
            case "tripleHearts":
			_itemHoleParent.performtripleHearts(currentRoom,ref IsSuccessfull);
                break;
            case "keyItem":
			_itemHoleParent.performkeyItem(currentRoom,ref IsSuccessfull);
                break;
            case "goOtherRoom":
			_itemHoleParent.performgoOtherRoom(currentRoom,ref IsSuccessfull);
                break;
            case "checkPointItem":
			_itemHoleParent.performcheckPointItem(currentRoom , ref IsSuccessfull);
                break;
            case "neverGoBack":
			_itemHoleParent.performneverGoBack(currentRoom,ref IsSuccessfull);
                break;
            case "jumpintolastRoom":
			_itemHoleParent.performjumpintolastRoom(currentRoom,ref IsSuccessfull);
                break;
        }

        if (IsSuccessfull)
        {
            _gameManager.deleteOneItemFromItemHoleBars(get_key());
            turn_to_null();
        }

    }

    private string getStateSprite(String name)
    {
        int lastIndex = name.LastIndexOf('_');
        string firstPart = string.Empty;
        string secondPart = string.Empty;

        if (lastIndex + 1 < name.Length)
        {
            firstPart = name.Substring(0, lastIndex);
            secondPart = name.Substring(lastIndex + 1);
        }

        return firstPart + "_1";
    }

    public void turn_to_null()
    {
        this.MyItemName = nullValue;
        this.onStateSprite = (Sprite)Resources.Load("Images/itemindicator_1", typeof(Sprite));
        this.offStateSprite = (Sprite)Resources.Load("Images/itemindicator_0", typeof(Sprite));
        this.gameObject.GetComponent<Image>().sprite = this.offStateSprite;
    }

    private int get_key()
    {
        int key = 0;

        string key_string = this.gameObject.name.Substring(8);

        key = Int32.Parse(key_string) - 1;

        return key;
    }

}


