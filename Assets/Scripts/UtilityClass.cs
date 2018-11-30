using UnityEngine;
using System.Collections.Generic;

public static class UtilityClass 
{
    public static void destroyGameObject(GameObject gameobject)
    {
        UnityEngine.MonoBehaviour.Destroy(gameobject);
    }

    public static void InstantiateObject(Object gameobject)
    {
        UnityEngine.MonoBehaviour.Instantiate(gameobject);
    }

    public static void InstantiateObject(Object gameobject,Vector3 position,Quaternion rotation)
    {
        UnityEngine.MonoBehaviour.Instantiate(gameobject,position,rotation);
    }

    public static List<int> GenerateRandom(int count)
    {
        // generate count random values.

        List<int> result = new List<int>();
        System.Random random = new System.Random();


        for(int k = 0; k< count; k++)
        {
            result.Add(k + 1);
        }

        // shuffle the results:

        int i = result.Count;

        while (i > 1)
        {
            i--;
            int k = random.Next(i + 1);
            int value = result[k];
            result[k] = result[i];
            result[i] = value;

        }
    
        return result;
    }

    public static List<int> GenerateRandom(int count,ref List<flatDoor> doors)
    {
        // generate count random values.

        List<int> result = new List<int>();
        System.Random random = new System.Random();

        for (int k = 0; k < count; k++)
        {
            result.Add(k + 1);
        }
    
        // shuffle the results:

        int i = result.Count;

        while (i > 1)
        {
            i--;
            int k = random.Next(i + 1);
            int value = result[k];
            result[k] = result[i];
            result[i] = value;

        }
        for (int a = 0; a <= doors.Count - 1; a++)
        {
            doors[a].Decision = result[a];
        }

        return result;
    }

    public static GameObject FindObject(this GameObject parent, string name)
    {
        RectTransform[] trs = parent.GetComponentsInChildren<RectTransform>();

        foreach (RectTransform t in trs)
        {
            if (t.name == name)
            {
                return t.gameObject;
            }
        }
        return null;
    }
}
