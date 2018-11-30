using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//It is common to create a class to contain all of your
//extension methods. This class must be static.
public static class ExtensionMethods
{
    private static System.Random rng = new System.Random();

    //Even though they are used like normal methods, extension
    //methods must be declared static. Notice that the first
    //parameter has the 'this' keyword followed by a Transform
    //variable. This variable denotes which class the extension
    //method becomes a part of.
    public static void ResetTransformation(this Transform trans)
    {
        trans.position = Vector3.zero;
        trans.localRotation = Quaternion.identity;
        trans.localScale = new Vector3(1, 1, 1);
    }
    public static void ResetRectTransformation(this RectTransform rect)
    {
        rect.anchoredPosition = new Vector2(0, 0);
    }

    public static void ResetRectTransformationSize(this RectTransform rect)
    {
        rect.sizeDelta = new Vector2(0, 0);
    }

    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
    
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}