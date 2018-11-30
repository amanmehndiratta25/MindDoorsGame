using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class DoorBase :MonoBehaviour
{
    [SerializeField]
    public string gameobjectName;
    private int value;
    protected Button button;
    public bool IsClickable = true;
    [SerializeField]
    public Sprite trueSprite;
    [SerializeField]
    public Sprite falseSprite;
    [SerializeField]
    public Sprite normalSprite;
    [SerializeField]
    public Sprite whiteSprite;
    [SerializeField]
    public Sprite trueBGSprite;
    [SerializeField]
    public Sprite falseBGSprite;
    [SerializeField]
    public Sprite normalBGSprite;
    [SerializeField]
    public Sprite whiteBGSprite;

    // Use this for initialization
    internal void Init(Button p_button,Room.roomType parentType)
    {
        button = p_button;
        button.transform.localScale = new Vector3(1, 1, 1);
        button.onClick.AddListener(() => actionToMaterial(value));
    }

    private void determineSprites(Room.roomType parentType)
    {
        switch (parentType)
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
            
                break;
            case Room.roomType.pin6:
       
                break;
            case Room.roomType.pin7:
             
                break;
            case Room.roomType.pin8:
      
                break;
            case Room.roomType.pin9:
            
                break;
            case Room.roomType.quadOption:

                break;
            case Room.roomType.signOption:
              
                break;
            case Room.roomType.sortbyItems:
         
                break;
            case Room.roomType.tripleOption:

                break;
        }
    }

    // Update is called once per frame
    public void Update()
    {

    }

    public void Destroy()
    {
        button.onClick.RemoveListener(() => actionToMaterial(value));
    }
    public virtual void actionToMaterial(int index)
    {

        //Debug.Log("clicked from" + this.gameObject.name);
    }
}
