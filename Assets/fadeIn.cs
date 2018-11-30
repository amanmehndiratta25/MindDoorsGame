using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class fadeIn : MonoBehaviour
{

    // the image you want to fade, assign in inspector

    public void Start()
    {
       
    }

    public void performFade(Image image, List<flatDoor> doors)
    {
        // fades the image out when you click
        StartCoroutine(FadeImage(true,image));
    }

    IEnumerator FadeImage(bool fadeAway,Image img)
    {
        img.raycastTarget = true;
        Color newColor = new Color(1, 1, 1);
       
        // fade from opaque to transparent
        if (fadeAway)
        {
            // loop over 1 second backwards
            for (float i = 1; i >= 0; i -= Time.deltaTime)
            {
                // set color with i as alpha
            
                img.color = new Color(0.5f, 0.5f, 0.5f, i);
                yield return null;
            }
        }
        // fade from transparent to opaque
        else
        {
            // loop over 1 second
            for (float i = 0; i <= 1; i += Time.deltaTime)
            {
                // set color with i as alpha
                img.color = new Color(0.5f, 0.5f, 0.5f, i);
                yield return null;
            }
        }
        img.raycastTarget = false;
    }
}