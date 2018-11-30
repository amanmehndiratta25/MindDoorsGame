using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ItemShieldBox : MonoBehaviour {

	public enum ItemShieldType{
		on,
		off
	}
	[SerializeField]
	private ItemShieldType myShieldBarType;

	public ItemShieldType MyShieldBarType
	{
		get
		{
			return myShieldBarType;
		}

		set
		{
			myShieldBarType = value;

			switch (value)
			{
			case ItemShieldType.on:
				this.gameObject.GetComponent<Image>().sprite = (Sprite)Resources.Load("Images/detectionmarker_0", typeof(Sprite));
				break;
			case ItemShieldType.off:
				this.gameObject.GetComponent<Image>().sprite = (Sprite)Resources.Load("Images/detectionmarker_1", typeof(Sprite));
				break;
			}

		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
