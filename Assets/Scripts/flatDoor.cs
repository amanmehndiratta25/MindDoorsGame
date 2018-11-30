using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class flatDoor : DoorBase
{
    [SerializeField]
    private Room.roomType parentType;
    [SerializeField]
    private int _decision;
    [SerializeField]
    private bool IsfadeAwayCompleted = false;
    [SerializeField]
    private bool IsForDameOption = false;
    [SerializeField]
    private float timeForDameOption = 0.1f;
    [SerializeField]
    private List<GameObject>  _tempObject;
    [SerializeField]
    private GameObject childBackGroundObject;

    public Image img;

    public Room.roomType ParentType
    {
        get
        {
            return parentType;
        }

        set
        {
            parentType = value;
        }
    }

    public int Decision
    {
        get
        {
            return _decision;
        }

        set
        {
            _decision = value;
        }
    }

    public List<GameObject> TempObject
    {
        get
        {
            if(_tempObject == null)
            {
                _tempObject = new List<GameObject>();
            }

            return _tempObject;
        }

        set
        {
            _tempObject = value;
        }
    }

    public GameObject ChildBackGroundObject
    {
        get
        {
            return childBackGroundObject;
        }
        set
        {
            childBackGroundObject = value;
        }
    }

    // Use this for initialization
    public void  Start()
    {

        base.Init(this.gameObject.GetComponent<Button>(),parentType);
        gameobjectName = this.gameObject.name;
        img = this.GetComponent<Image>();
        IsfadeAwayCompleted = false;
        IsForDameOption = false;
        timeForDameOption = 0.1f;
        img.sprite = normalSprite;
        childBackGroundObject = this.GetComponentInChildren<BackGroundScript>().gameObject;
        childBackGroundObject.GetComponent<Image>().sprite = normalBGSprite;

    }

    public void OnDisable()
    {
        //this.gameObject.tag = "door";

        for(int i = 0; i < this._tempObject.Count; i++)
        {
            Destroy(this._tempObject[i]);
        }

        _tempObject.Clear();
    }

    public void OnEnable()
    {
        //this.gameObject.tag = "door";

        for (int i = 0; i < this._tempObject.Count; i++)
        {
            Destroy(this._tempObject[i]);
        }

        _tempObject.Clear();
    }

    public void LateUpdate()
    {
        if (parentType == Room.roomType.dameOption && IsForDameOption)
        {
            timeForDameOption -= Time.deltaTime;

            if (Decision == 2)
            {
                img.sprite = trueSprite;
                childBackGroundObject.GetComponent<Image>().sprite = trueBGSprite;
            }
            else
            {
                img.sprite = falseSprite;
                childBackGroundObject.GetComponent<Image>().sprite = falseBGSprite;
            }

            if (timeForDameOption < 0)
            {
                img.sprite = normalSprite;
                childBackGroundObject.GetComponent<Image>().sprite = normalBGSprite;
                IsForDameOption = false;
                timeForDameOption = 0.1f;
                this.GetComponentInParent<Room>().PerformButtonFordameOption(this.Decision, this.gameObject.GetComponent<Button>());

            }
        }
    }


    // Update is called once per frame
    new void Update()
    {
  
  
    }

    new void Destroy()
    {
        base.Destroy();
    }

    public override void actionToMaterial(int index)
    {
        base.actionToMaterial(index);

        interpretUponRoomType(this.GetComponentInParent<Room>().MyRoomType);
        
    }

    private void interpretUponRoomType(Room.roomType roomType)
    {
        bool IsOke = false;
        int currentPin = 0;

        IsfadeAwayCompleted = false;
        IsForDameOption = false;

		this.GetComponentInParent<GameManager> ().PreviousNumber = this.GetComponentInParent<Room> ().RoomNumber;

        switch (roomType)
        {
            case Room.roomType.beFast:

                if (Decision == 2)
                {
                    IsOke = true;
                }
                else
                {
                    IsOke = false;
                }

                StartCoroutine(fadeAwayForButton(trueSprite, img, IsOke,roomType));

                break;
            case Room.roomType.binaryOption:

                if(Decision == 2)
                {
                    IsOke = true;
                }else
                {
                    IsOke = false;
                }

                StartCoroutine(fadeAwayForButton(trueSprite, img, IsOke,roomType));

                break;
            case Room.roomType.checkPoint:

                if (Decision == 2)
                {
                    IsOke = true;
                }
                else
                {
                    IsOke = false;
                }

                StartCoroutine(fadeAwayForButton(trueSprite, img, IsOke, roomType));

                break;
            case Room.roomType.dameOption:

                IsForDameOption = true;

                break;
            case Room.roomType.devoted:

                if (Decision == 2 || this.gameObject.CompareTag("corruption"))
                {
                    IsOke = true;
                }
                else
                {
                    IsOke = false;
                }

                StartCoroutine(fadeAwayForButton(trueSprite, img, IsOke, roomType));

                break;
            case Room.roomType.dontMistake:

                if (Decision == 2 )
                {
                    IsOke = true;
                }
                else
                {
                    IsOke = false;
                }

                StartCoroutine(fadeAwayForButton(trueSprite, img, IsOke, roomType));

                break;
            case Room.roomType.doRight:

                if (Decision == 2)
                {
                    IsOke = true;
                }
                else
                {
                    IsOke = false;
                }

                StartCoroutine(fadeAwayForButton(trueSprite, img, IsOke, roomType));
       
                break;
            case Room.roomType.doubleRight:

                if (this.img.sprite == trueSprite)
                {
                    return;
                }

                if (Decision == 2 || Decision == 3)
                {
                    IsOke = true;
                }
                else
                {
                    IsOke = false;
                }
                StartCoroutine(fadeAwayForButton(trueSprite, img, IsOke, roomType));

                break;
            case Room.roomType.pin5:
            case Room.roomType.pin6:
            case Room.roomType.pin7:
            case Room.roomType.pin8:
            case Room.roomType.pin9:

               IsOke = false;

                if (this.GetComponentInParent<Room>().PinDict != null)
                {
                    currentPin = this.GetComponentInParent<Room>().PinDict.Count;
                }

                if(Decision == currentPin + 1)
                {
                    IsOke = true;
                }
                else
                {
                    IsOke = false;
                }
                
                
                StartCoroutine(fadeAwayForButton(trueSprite, img, IsOke, roomType));

                break;
            case Room.roomType.quadOption:

                if (Decision == 2)
                {
                    IsOke = true;
                }
                else
                {
                    IsOke = false;
                }

                StartCoroutine(fadeAwayForButton(trueSprite, img, IsOke, roomType));

                break;
            case Room.roomType.signOption:

                if (Decision == 2)
                {
                    IsOke = true;
                }
                else
                {
                    IsOke = false;
                }

                StartCoroutine(fadeAwayForButton(trueSprite, img, IsOke, roomType));

                break;
            case Room.roomType.sortbyItems:

                currentPin = 0;
                IsOke = false;

                if(this.img.sprite == trueSprite)
                {
                    return;
                }

                if (this.GetComponentInParent<Room>().PinDict != null)
                {
                    currentPin = this.GetComponentInParent<Room>().PinDict.Count;
                }

                if (Decision == currentPin + 1)
                {
                    IsOke = true;
                }
                else
                {
                    IsOke = false;
                }
              
                StartCoroutine(fadeAwayForButton(trueSprite, img, IsOke, roomType));

                break;
            case Room.roomType.tripleOption:

                if (Decision == 2)
                {
                    IsOke = true;
                }
                else
                {
                    IsOke = false;
                }

                StartCoroutine(fadeAwayForButton(trueSprite, img, IsOke, roomType));

                break;
        }
    }

    public void wrapperCoroutine()
    {
        bool IsOKe = false;

        if (Decision == 2)
        {
            IsOKe = true;
        }
        else
        {
            IsOKe = false;
        }

        StartCoroutine(fadeAwayForButton(true, this.img, IsOKe, parentType));

    }
    IEnumerator fadeAwayForButton(bool fadeAway, Image img,bool decision, Room.roomType roomType)
    {

        IsfadeAwayCompleted = false;

        Sprite changeSprite = null;

        if(this.IsClickable == true)
        {
            if (decision)
            {
                changeSprite = trueSprite;
                childBackGroundObject.GetComponent<Image>().sprite = trueBGSprite;
            }

            else
            {
                changeSprite = falseSprite;
                childBackGroundObject.GetComponent<Image>().sprite = falseBGSprite;
            }

            img.sprite = changeSprite;
        }

        yield return new WaitForSeconds(0.1f);
        if (!IsClickable)
        {

        }
        else
            switch (roomType)
        {
            case Room.roomType.beFast:
                img.sprite = normalSprite;
                childBackGroundObject.GetComponent<Image>().sprite = normalBGSprite;
                this.GetComponentInParent<Room>().PerformButtonForBeFast(this.Decision, this.gameObject.GetComponent<Button>());

                break;

            case Room.roomType.binaryOption:
            case Room.roomType.tripleOption:
            case Room.roomType.quadOption:
            case Room.roomType.checkPoint:
                img.sprite = normalSprite;
                childBackGroundObject.GetComponent<Image>().sprite = normalBGSprite;
                this.GetComponentInParent<Room>().PerformButtonForNormal(this.Decision, this.gameObject.GetComponent<Button>());

                break;

            case Room.roomType.dameOption:

                break;

            case Room.roomType.devoted:
                img.sprite = normalSprite;
                childBackGroundObject.GetComponent<Image>().sprite = normalBGSprite;
                this.GetComponentInParent<Room>().PerformButtonForDevoted(this.Decision, this.gameObject.GetComponent<Button>());

                break;

            case Room.roomType.dontMistake:
                img.sprite = normalSprite;
                childBackGroundObject.GetComponent<Image>().sprite = normalBGSprite;
                this.GetComponentInParent<Room>().PerformButtonForDontMistake(this.Decision, this.gameObject.GetComponent<Button>());

                break;

            case Room.roomType.doRight:
                img.sprite = normalSprite;
                childBackGroundObject.GetComponent<Image>().sprite = normalBGSprite;
                this.GetComponentInParent<Room>().PerformButtonForDoRight(this.Decision, this.gameObject.GetComponent<Button>());

                break;

            case Room.roomType.doubleRight:
                img.sprite = normalSprite;
                childBackGroundObject.GetComponent<Image>().sprite = normalBGSprite;
                this.GetComponentInParent<Room>().PerformButtonForDoubleRight(this.Decision, this.gameObject.GetComponent<Button>());

                break;

            case Room.roomType.pin5:
            case Room.roomType.pin6:
            case Room.roomType.pin7:
            case Room.roomType.pin8:
            case Room.roomType.pin9:
                    img.sprite = normalSprite;
                    childBackGroundObject.GetComponent<Image>().sprite = normalBGSprite;
                    this.GetComponentInParent<Room>().PerformButtonForPin(this.Decision, this.gameObject.GetComponent<Button>());

                break;

            case Room.roomType.signOption:
                img.sprite = normalSprite;
                childBackGroundObject.GetComponent<Image>().sprite = normalBGSprite;
                this.GetComponentInParent<Room>().PerformButtonForsignOption(this.Decision, this.gameObject.GetComponent<Button>());

                break;

            case Room.roomType.sortbyItems:
                img.sprite = normalSprite;
                childBackGroundObject.GetComponent<Image>().sprite = normalBGSprite;
                this.GetComponentInParent<Room>().PerformButtonForSıra(this.Decision, this.gameObject.GetComponent<Button>());

                break;

        }

        IsfadeAwayCompleted = true;

    }

    public void trueDecision()
    {
        SpriteState spriteState = new SpriteState();

        spriteState = this.GetComponent<Button>().spriteState;

        spriteState.pressedSprite = trueSprite;

        this.GetComponent<Button>().spriteState = spriteState;

    }
    public void wrongDecision()
    {
        SpriteState spriteState = new SpriteState();

        spriteState = this.GetComponent<Button>().spriteState;

        spriteState.pressedSprite = falseSprite;

        this.GetComponent<Button>().spriteState = spriteState;
    }

    public void OnPointerDown()
    {
        if(IsClickable)
        img.sprite = whiteSprite;
    }

    public void OnPointerUp()
    {
        if (IsClickable)
        {
            img.sprite = normalSprite;
            childBackGroundObject.GetComponent<Image>().sprite = normalBGSprite;
        }
           
    }

    public void free_TempObject()
    {
        for(int i = 0; i < _tempObject.Count; i++)
        {
            Destroy(_tempObject[i]);
        }

        this._tempObject.Clear();
    }

}
