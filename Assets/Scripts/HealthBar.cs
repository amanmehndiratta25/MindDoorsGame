using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

	public enum HealthBarType{
		blue,
		gray,
		red

	}
	[SerializeField]
	private HealthBarType myHealthBarType;

	public HealthBarType MyHealthBarType
	{
		get
		{
			return myHealthBarType;
		}

		set
		{
			myHealthBarType = value;

            switch (value)
            {
                case HealthBarType.blue:
                    this.gameObject.GetComponent<Image>().sprite = (Sprite)Resources.Load("Images/detectionmarker_0", typeof(Sprite));
                    break;
                case HealthBarType.gray:
                    this.gameObject.GetComponent<Image>().sprite = (Sprite)Resources.Load("Images/detectionmarker_1", typeof(Sprite));
                    break;
                case HealthBarType.red:
                    this.gameObject.GetComponent<Image>().sprite = (Sprite)Resources.Load("Images/detectionmarker_n0", typeof(Sprite));
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
