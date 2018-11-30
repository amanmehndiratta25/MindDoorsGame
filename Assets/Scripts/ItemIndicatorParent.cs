using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class ItemIndicatorParent : MonoBehaviour {

    // Use this for initialization

    const int diff = 28;
    const int screenUIElementNumber = 16;
	private Dictionary<int,string> _roomNames = new Dictionary<int, string> ();
	private Dictionary<int,ItemIndicatorHole> _indicators = new Dictionary<int, ItemIndicatorHole>();
	private Dictionary<int,bool> _solvedInformations = new Dictionary<int, bool> ();
	private int pageNumber = 1;
	public Dictionary<int,string> RoomNames {

		get{

			return _roomNames;

		}

		set {

			_roomNames = value;

		}
			
	}

	public Dictionary<int,ItemIndicatorHole> Indicators {

		get{

			return _indicators;

		}

		set {

			_indicators = value;

		}

	}

	public Dictionary<int,bool> SolvedInf {

		get{

			return _solvedInformations;
		}
		set{

			_solvedInformations = value;
		}

	}
		

    void Start () {

       // _countForItemHole = 0;
		pageNumber = 1;
    }
    
    // Update is called once per frame
    void Update () {

           
    }


	public void setActiveIndicatorItem(Room.roomSituation roomSituation)
	{
		refreshUIIndicator ();
	
	}

	private int getActiveIndicator()
	{
		int currentRoomNumber = getGameManager ().getCurrentRoomNumber();
		int kalan = 0;

		if (screenUIElementNumber < currentRoomNumber) {
			kalan = currentRoomNumber % screenUIElementNumber+1;
		} else if (screenUIElementNumber == currentRoomNumber) {
			kalan = 1;
		} else if (screenUIElementNumber > currentRoomNumber) {
			kalan = currentRoomNumber+1;
		}
			
		return kalan; 

	}

	private GameManager getGameManager()
	{

		return GameObject.Find ("Canvas").GetComponent<GameManager> ();

	}

	private void refreshUI()
	{
		int currentRoomNumber = getGameManager ().getCurrentRoomNumber();

		if ((currentRoomNumber + 1) > screenUIElementNumber) {



		} else
			return;

		foreach( KeyValuePair<int,ItemIndicatorHole> item in Indicators)
		{
			if (item.Key != 1) {
				item.Value.changeSpriteFirst ();
			} 
			else {

				item.Value.change_first_level_indicator_sprite ("on_unsolved");
			}
		}


			
	}

	public void wrongSituation(Room.roomSituation situation,int currentRoomNumber)
	{
		
		int activeIndicatorIndex = currentRoomNumber + 1;
		if(situation == Room.roomSituation.solved)
			Indicators [getActiveIndicator()].change_first_level_indicator_sprite ("off_solved");
		else
			Indicators [getActiveIndicator()].change_first_level_indicator_sprite ("off_unsolved");

	}	

	public void setActiveIndicatorItemHistory(int previousNumber ,int currentNumber)
	{
		
		refreshUIIndicator ();

	}

	private int determinePageNumber(int currentRealNumber){

		int pageNumber_l = 0;

		if (currentRealNumber <= 16) {

			pageNumber_l = 1;
		} 
		else {

			int kalan = 0;
			int bolum = 0;
			kalan = currentRealNumber % 16;
			bolum = currentRealNumber / 16;

			if (kalan == 0) {

				pageNumber_l = bolum;
			} 
			else 
			{
				pageNumber_l = bolum + 1;
			}

		}
			
		return pageNumber_l;

	}

	private void refreshUIIndicator(){

		int currentRealNumber = getGameManager ().getCurrentRoomNumber()+1;
		string roomName = _roomNames [currentRealNumber];
		pageNumber = determinePageNumber(currentRealNumber);
		int IndicatorIndex = getActiveIndicator();

		foreach( KeyValuePair<int,ItemIndicatorHole> item in Indicators)
		{
			item.Value.changeSpriteFirst ();

			item.Value.changeIndicatorholeUnsolvedInf ();

			if (item.Key <= IndicatorIndex)
				item.Value.change_itemIndicatorholeSolvedInf ();

		}

		int roomNumber = 0;

		foreach( KeyValuePair<int,ItemIndicatorHole> item in Indicators)
		{
			roomNumber = 0;

			if (currentRealNumber <= 16) {

				roomNumber = item.Key;
			} 
			else {

				roomNumber = currentRealNumber + ( item.Key - (currentRealNumber-16*(pageNumber-1)) );

			}
			try{
			item.Value.MyRoomName = item.Value.historyPagesSprite [pageNumber];
			}
			catch(KeyNotFoundException e ) {

				Debug.Log (e.Message.ToString());
			}

			if (getGameManager ().getRoomSituation (roomNumber - 1) == Room.roomSituation.solved) {

				if (item.Key != IndicatorIndex)
					item.Value.change_first_level_indicator_sprite ("off_solved");
				else
					item.Value.change_first_level_indicator_sprite ("on_solved");

			} else {

				if (item.Key == IndicatorIndex)
					item.Value.change_first_level_indicator_sprite ("on_unsolved");
				else
					item.Value.changeSpriteFirst ();
			}

		}


	}

	public void update_RoomNames(int key , string value)
	{
		key = key + 1;
		if(_roomNames.ContainsKey(key))
		_roomNames [key] = value;

	}

	public void convert_to_checkPoint(string value){

		int currentRealNumber = getGameManager ().getCurrentRoomNumber()+1;
		string roomName = _roomNames [currentRealNumber];
		pageNumber = determinePageNumber(currentRealNumber);
		int IndicatorIndex = getActiveIndicator();
		Indicators [IndicatorIndex].historyPagesSprite [pageNumber] = value;
		Indicators [IndicatorIndex].MyRoomName = value;

		if (getGameManager ().getRoomSituation (currentRealNumber - 1) == Room.roomSituation.solved) {

			Indicators [IndicatorIndex].change_first_level_indicator_sprite ("on_solved");

		} 
		else 
		{

			Indicators [IndicatorIndex].change_first_level_indicator_sprite ("on_unsolved");
		}
			
	}
    
    public int getEnableIndicator()
    {
        int a;
        int sayi = 0 ;

        for(a= 0; a < this.GetComponentsInChildren<ItemIndicatorHole>().Length; a++)
        {
            if (this.GetComponentsInChildren<ItemIndicatorHole>()[a].ISENABLED)
            {
                sayi = a + 1;
                break;
            }
        }
   //     Debug.Log(sayi);
        return sayi;

    }

    public void setIndicatorConnectors()
    {
        for(int i = 15; i >= 0; i--)
        {
            if (getEnableIndicator() > i)
                this.GetComponentsInChildren<ItemIndicatorHole>()[i].setActiveTrueForSubImage();
            else
                this.GetComponentsInChildren<ItemIndicatorHole>()[i].setActiveFalseForSubImage();
        }

        //todo
    }
			
}