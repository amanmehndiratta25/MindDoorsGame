using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemShieldParent : MonoBehaviour {

	[SerializeField]
	private int itemshieldCount;

	private Dictionary<int, ItemShieldBox> _itemShieldBars;

	public int ItemShieldCount
	{
		get
		{
			return itemshieldCount;
		}

		set
		{
			itemshieldCount = value;
		}
	}

	public Dictionary<int, ItemShieldBox> ItemShieldBars
	{
		get
		{
			if(_itemShieldBars == null)
			{
				_itemShieldBars = new Dictionary<int, ItemShieldBox>();

			}

			return _itemShieldBars;
		}

		set
		{
			_itemShieldBars = value;
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void decreaseLastShield() {

		if (getLastItemShield () == 1) {

			Destroy (this.gameObject);
		
		}
		else 
		{
			ItemShieldBox boxItem = this._itemShieldBars [getLastItemShield ()];

			this._itemShieldBars.Remove (getLastItemShield ());

			Destroy (boxItem.gameObject);

		}

	}

	private int getLastItemShield(){

		return ItemShieldBars.Count;
	}
}
