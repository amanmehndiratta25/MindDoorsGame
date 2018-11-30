using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Linq;
using System;

public class progressTrigger : MonoBehaviour {



    private Image _image;
    private Sprite _imageSrite;

    [SerializeField]
    private Sprite[] _spriteList = new Sprite[450];

    [SerializeField]
    private float animationSpeed = 1;

    public enum conditionProgess
    {
        init,
        zero,
        triple,
        hex,
        finish
    }
    [SerializeField]
    private conditionProgess myConditionProgress = conditionProgess.zero;

    public conditionProgess MyConditionProgress
    {
        get
        {
            return myConditionProgress;
        }

        set
        {
            myConditionProgress = value;
        }
    }

    void Awake () {

        _image = this.GetComponent<Image>();
        _imageSrite = null;

        for(int i = 0; i<=449; i++)
        {
            if (TestRange(i, 0, 9))
            {
                _imageSrite = Instantiate(Resources.Load("tracking_animasyon/tracking-anim000" + i.ToString(), typeof(Sprite))) as Sprite;
            }
            else if (TestRange(i, 10, 99))
            {
                _imageSrite = Instantiate(Resources.Load("tracking_animasyon/tracking-anim00" + i.ToString(), typeof(Sprite))) as Sprite;
            }
            else if (TestRange(i, 100, 999))
            {
                _imageSrite = Instantiate(Resources.Load("tracking_animasyon/tracking-anim0" + i.ToString(), typeof(Sprite))) as Sprite;
            }

            if(_imageSrite!= null)
            {
                _spriteList[i] = _imageSrite;
                _imageSrite = null;
            }

        }

        _imageSrite = null;

     
    }

    public void OnEnable()
    {
        StartCoroutine(nukeMethod());
    }

    public void ReInit()
    {
        StartCoroutine(nukeMethod());
    }

    // Update is called once per frame
    void Update ()
    {
      
    }

    public IEnumerator nukeMethod()
    {

        myConditionProgress = conditionProgess.zero;
        //destroy all game objects
        for (int i = 0; i < _spriteList.Length; i++)
        {
            _image.sprite = _spriteList[i];

            switch (i)
            {
                case 0:
                    myConditionProgress = conditionProgess.init;
                    break;
                case 149:
                    myConditionProgress = conditionProgess.triple;
                    break;
                case 299:
                    myConditionProgress = conditionProgess.hex;
                    break;
                case 449:
                    myConditionProgress = conditionProgess.finish;
                    break;
            }

            yield return new WaitForSeconds(animationSpeed);
        }

    }

    bool TestRange(int numberToCheck, int bottom, int top)
    {
        return (numberToCheck >= bottom && numberToCheck <= top);
    }
}
