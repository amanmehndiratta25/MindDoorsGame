using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class ItemHoleParent : MonoBehaviour {

    // Use this for initialization

    private Dictionary<int, string> _items;

    public Dictionary<int, string> Items
    {
        get
        {
            if(_items == null)
            {
                _items = new Dictionary<int, string>();
            }

            return _items;
        }

        set
        {
            _items = value;
        }
    }

    void Start () {
    
    }
    
    // Update is called once per frame
    void Update () {
    
    }

    public void performshowWrongDoor1(GameObject currentRoom,ref bool Success)
    {
        Room currentRoomObject = currentRoom.GetComponent<Room>();
        Image signImage = null;

        // check Room's possibility If It is okay or not 

        if (currentRoomObject.MyRoomType == Room.roomType.checkPoint ||
            currentRoomObject.MyRoomType == Room.roomType.doubleRight ||
            currentRoomObject.MyRoomType == Room.roomType.devoted ||
            currentRoomObject.MyRoomType == Room.roomType.beFast ||
            currentRoomObject.MyRoomType == Room.roomType.signOption ||
            currentRoomObject.MyRoomType == Room.roomType.dameOption ||
            currentRoomObject.MyRoomType == Room.roomType.binaryOption ||
            currentRoomObject.MyRoomType == Room.roomType.tripleOption ||
            currentRoomObject.MyRoomType == Room.roomType.quadOption)
        { }
        else return;

        List<flatDoor> doors = new List<flatDoor>();

        doors.Clear();

        doors = getCurrentWrongDoors(currentRoomObject);

        if (doors.Count > 0)
        {
            Success = true;
        }
        else
        {
            Success = false;

			return;
        }

        int randomCorruptionNumber = UnityEngine.Random.Range(0, doors.Count);

        for (int i = 0;  i < doors.Count; i++)
        {
            if(i == randomCorruptionNumber)
            {               
                signImage = Instantiate(Resources.Load("signOption", typeof(Image))) as Image;
                signImage.gameObject.transform.SetParent(doors[i].gameObject.transform);
                signImage.GetComponent<RectTransform>().ResetRectTransformation();
                signImage.gameObject.GetComponent<Transform>().localScale = new Vector3(1, 1, 1);
                doors[i].gameObject.tag = "tempObject";
                doors[i].TempObject.Add(signImage.gameObject);
                break;
            }
        }       

    }

    private List<flatDoor> getCurrentWrongDoors(Room currentRoom)
    {

        List<flatDoor> doors = new List<flatDoor>();

        doors.Clear();

        switch (currentRoom.MyRoomType)
        {
            case Room.roomType.checkPoint:

                for (int i = 0; i < currentRoom.GetComponentsInChildren<flatDoor>(true).Length; i++)
                {

                    if (currentRoom.GetComponentsInChildren<flatDoor>(true)[i].Decision != 2)
                        if(!currentRoom.GetComponentsInChildren<flatDoor>(true)[i].gameObject.CompareTag("tempObject"))
                        doors.Add(currentRoom.GetComponentsInChildren<flatDoor>(true)[i]);

                }

                break;
            case Room.roomType.doubleRight:

                for (int i = 0; i < currentRoom.GetComponentsInChildren<flatDoor>(true).Length; i++)
                {
                    switch (currentRoom.GetComponentsInChildren<flatDoor>(true)[i].Decision)
                    {
                        case 2:
                            break;
                        case 3:
                            break;
                        default:
                            if (!currentRoom.GetComponentsInChildren<flatDoor>(true)[i].gameObject.CompareTag("tempObject"))
                                doors.Add(currentRoom.GetComponentsInChildren<flatDoor>(true)[i]);
                            break;

                    }
                      
                }

                break;
            case Room.roomType.devoted:

                for (int i = 0; i < currentRoom.GetComponentsInChildren<flatDoor>(true).Length; i++)
                {

                    if (currentRoom.GetComponentsInChildren<flatDoor>(true)[i].Decision != 2 )
                    {
                       if (!currentRoom.GetComponentsInChildren<flatDoor>(true)[i].gameObject.CompareTag("corruption"))
                        {
                            if (!currentRoom.GetComponentsInChildren<flatDoor>(true)[i].gameObject.CompareTag("tempObject"))
                                doors.Add(currentRoom.GetComponentsInChildren<flatDoor>(true)[i]);
                        }
                       
                    }
             
                }

                break;
            case Room.roomType.beFast:

                for (int i = 0; i < currentRoom.GetComponentsInChildren<flatDoor>(true).Length; i++)
                {

                    if (currentRoom.GetComponentsInChildren<flatDoor>(true)[i].Decision != 2 )
                        if (!currentRoom.GetComponentsInChildren<flatDoor>(true)[i].gameObject.CompareTag("tempObject"))
                            doors.Add(currentRoom.GetComponentsInChildren<flatDoor>(true)[i]);

                }

                break;
            case Room.roomType.signOption:

                for (int i = 0; i < currentRoom.GetComponentsInChildren<flatDoor>(true).Length; i++)
                {

                    if (currentRoom.GetComponentsInChildren<flatDoor>(true)[i].Decision != 2 )
                    {
                        if (!currentRoom.GetComponentsInChildren<flatDoor>(true)[i].gameObject.CompareTag("signoption"))
                        {
                            if (!currentRoom.GetComponentsInChildren<flatDoor>(true)[i].gameObject.CompareTag("tempObject"))
                                doors.Add(currentRoom.GetComponentsInChildren<flatDoor>(true)[i]);
                        }
                    }

                }

                break;
            case Room.roomType.dameOption:

                for (int i = 0; i < currentRoom.GetComponentsInChildren<flatDoor>(true).Length; i++)
                {
                    if (currentRoom.GetComponentsInChildren<flatDoor>(true)[i].Decision != 2)
                        if (!currentRoom.GetComponentsInChildren<flatDoor>(true)[i].gameObject.CompareTag("tempObject"))
                            doors.Add(currentRoom.GetComponentsInChildren<flatDoor>(true)[i]);
                }

                break;
            case Room.roomType.binaryOption:

                for (int i = 0; i < currentRoom.GetComponentsInChildren<flatDoor>(true).Length; i++)
                {
                    if (currentRoom.GetComponentsInChildren<flatDoor>(true)[i].Decision != 2)
                        if (!currentRoom.GetComponentsInChildren<flatDoor>(true)[i].gameObject.CompareTag("tempObject"))
                            doors.Add(currentRoom.GetComponentsInChildren<flatDoor>(true)[i]);
                }

                break;
            case Room.roomType.tripleOption:

                for (int i = 0; i < currentRoom.GetComponentsInChildren<flatDoor>(true).Length; i++)
                {
                    if (currentRoom.GetComponentsInChildren<flatDoor>(true)[i].Decision != 2)
                        if (!currentRoom.GetComponentsInChildren<flatDoor>(true)[i].gameObject.CompareTag("tempObject"))
                            doors.Add(currentRoom.GetComponentsInChildren<flatDoor>(true)[i]);
                }

                break;
            case Room.roomType.quadOption:

                for (int i = 0; i < currentRoom.GetComponentsInChildren<flatDoor>(true).Length; i++)
                {
                    if (currentRoom.GetComponentsInChildren<flatDoor>(true)[i].Decision != 2)
                        if (!currentRoom.GetComponentsInChildren<flatDoor>(true)[i].gameObject.CompareTag("tempObject"))
                            doors.Add(currentRoom.GetComponentsInChildren<flatDoor>(true)[i]);
                }

                break;
        }

        return doors;
    }

	internal void performshowWrongDoor2(GameObject currentRoom,ref bool Success)
	{
		Room currentRoomObject = currentRoom.GetComponent<Room>();
		Image signImage = null;
		int anotherRandomNumber = 0;
		int randomCorruptionNumber = 0;

		// check Room's possibility If It is okay or not 

		if (currentRoomObject.MyRoomType == Room.roomType.checkPoint ||
		    currentRoomObject.MyRoomType == Room.roomType.doubleRight ||
		    currentRoomObject.MyRoomType == Room.roomType.devoted ||
		    currentRoomObject.MyRoomType == Room.roomType.beFast ||
		    currentRoomObject.MyRoomType == Room.roomType.signOption ||
		    currentRoomObject.MyRoomType == Room.roomType.dameOption ||
		    currentRoomObject.MyRoomType == Room.roomType.binaryOption ||
		    currentRoomObject.MyRoomType == Room.roomType.tripleOption ||
		    currentRoomObject.MyRoomType == Room.roomType.quadOption) {
		} else
			return;

		List<flatDoor> doors = new List<flatDoor> ();

		doors.Clear ();

		doors = getCurrentWrongDoors (currentRoomObject);

		if (doors.Count > 0) {
			Success = true;
		} else {
			Success = false;

			return;
		}

		// Before doing this , I need some circumstances for WrongDoor2

		if (doors.Count == 1) {

			randomCorruptionNumber = UnityEngine.Random.Range (0, doors.Count );

			for (int i = 0; i < doors.Count; i++) {
				if (i == randomCorruptionNumber) {               
					signImage = Instantiate (Resources.Load ("signOption", typeof(Image))) as Image;
					signImage.gameObject.transform.SetParent (doors [i].gameObject.transform);
					signImage.GetComponent<RectTransform> ().ResetRectTransformation ();
					signImage.gameObject.GetComponent<Transform> ().localScale = new Vector3 (1, 1, 1);
					doors [i].gameObject.tag = "tempObject";
					doors [i].TempObject.Add (signImage.gameObject);
					break;
				}
			}       
				
			return;

		} else {

			randomCorruptionNumber = UnityEngine.Random.Range (0, doors.Count);

			do 
			{
				anotherRandomNumber = UnityEngine.Random.Range (0, doors.Count );

			} while (anotherRandomNumber == randomCorruptionNumber) ;
				
			for (int i = 0; i < doors.Count; i++) {
				if (i == randomCorruptionNumber || i == anotherRandomNumber) {               
					signImage = Instantiate (Resources.Load ("signOption", typeof(Image))) as Image;
					signImage.gameObject.transform.SetParent (doors [i].gameObject.transform);
					signImage.GetComponent<RectTransform> ().ResetRectTransformation ();
					signImage.gameObject.GetComponent<Transform> ().localScale = new Vector3 (1, 1, 1);
					doors [i].gameObject.tag = "tempObject";
					doors [i].TempObject.Add (signImage.gameObject);
				}
			}    
		}		
	}

	internal void performtripleHearts(GameObject currentRoom,ref bool Successfull)
    {
		if (GameObject.FindGameObjectWithTag("itemshieldparent") != null)
		{
			Successfull = false;

			return;
		}

		stageGenerator stageGenerator = GameObject.Find("StageGenerator").GetComponent<stageGenerator>();

		if (stageGenerator != null) {

			stageGenerator.performs_itemshields ();

			Successfull = true;
		}     
    }

	internal void performkeyItem(GameObject currentRoom,ref bool IsSuccessfull)
    {
		IsSuccessfull = true;

		currentRoom.GetComponent<Room> ().nextRoomWrapper ();
    }


	internal void performgoOtherRoom(GameObject currentRoom,ref bool IsSuccessfull)
    {
		IsSuccessfull = true;

		this.GetComponentInParent<GameManager> ().PreviousNumber = currentRoom.GetComponentInParent<Room> ().RoomNumber;

		currentRoom.GetComponent<Room>().goRandomRoom();
    }

	internal void performcheckPointItem(GameObject currentRoom , ref bool IsSuccessfull)
    {
		Room currentRoom_o = currentRoom.GetComponent<Room> ();
		ItemIndicatorParent indicatorParent_o = GameObject.Find ("ItemIndicatorParent").GetComponent<ItemIndicatorParent>();
		stageGenerator stageGenerator = GameObject.Find("StageGenerator").GetComponent<stageGenerator>();
		if (currentRoom_o.MyRoomType == Room.roomType.checkPoint) {

			IsSuccessfull = false;

			return;

		}

		currentRoom_o.convert_to_checkPoint ();

		stageGenerator.Rooms [currentRoom_o.RoomNumber] = currentRoom_o;

		indicatorParent_o.update_RoomNames (currentRoom_o.RoomNumber, currentRoom_o.RoomName);

		indicatorParent_o.convert_to_checkPoint (currentRoom_o.RoomName);

		IsSuccessfull = true;

    }

	internal void performneverGoBack(GameObject currentRoom,ref bool IsSuccessfull)
    {
		if (getGameManager ().IsNeverGoBack == true) {

			IsSuccessfull = false;

			return;
		}
			
		getGameManager ().IsNeverGoBack = true;

		stageGenerator stageGenerator = GameObject.Find("StageGenerator").GetComponent<stageGenerator>();

		if (stageGenerator != null) {

			stageGenerator.performs_itemshields_nevergoback ();
		
		}     

		IsSuccessfull = true;

    }

	internal void performjumpintolastRoom(GameObject currentRoom,ref bool IsSuccessfull)
    {
		if (getGameManager ().StageLong == (currentRoom.GetComponent<Room> ().RoomNumber + 1)) {

			IsSuccessfull = false;

			return;
		}

		stageGenerator stageGenerator = GameObject.Find("StageGenerator").GetComponent<stageGenerator>();

		int index = stageGenerator.RoomObjectList.Count;

		currentRoom.GetComponent<Room> ().goRoomByITem (index-1);

		IsSuccessfull = true;

    }

	private GameManager getGameManager()
	{
		return GameObject.Find ("Canvas").GetComponent<GameManager> ();

	}
}
