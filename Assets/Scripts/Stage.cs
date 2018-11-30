using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Stage
{
    public int stageıd { get; set; }
    public int stageLength { get; set; }
    public int hearts { get; set; }
    /* Normal Rooms statistics*/
    public int binaryOption { get; set; }
    public int tripleOption { get; set; }
    public int quadOption { get; set; }
/*  Special Rooms Possibility  */
    public float specialStep { get; set; }
    public float checkPoint { get; set; }
    public float doubleRight { get; set; }
    public float doRight { get; set; }
    public float devoted { get; set; }
    public float dontMistake { get; set; }
    public float beFast { get; set; }
    public float signOption { get; set; }
    public float dameOption { get; set; }
    public float pin5 { get; set; }
    public float pin6 { get; set; }
    public float pin7 { get; set; }
    public float pin8 { get; set; }
    public float pin9 { get; set; }
    public float sortbyItems { get; set; }
    /* Possession of goods */
    public float posGoods { get; set; }
    public float showWrongDoor1 { get; set; }
    public float showWrongDoor2 { get; set; }
    public float tripleHearts { get; set; }
    public float keyItem { get; set; }
    public float goOtherRoom { get; set; }
    public float checkPointItem { get; set; }
    public float neverGoBack { get; set; }
    public float jumpintolastRoom { get; set; }


    public Dictionary<string, GenericObject> dictProperty = new Dictionary<string, GenericObject>();

    public void m_setProperties()
    {
        dictProperty["stageıd"] = new GenericObject().WithValue<int>(stageıd);
        dictProperty["stageLength"] = new GenericObject().WithValue<int>(stageLength);
        dictProperty["hearts"] = new GenericObject().WithValue<int>(hearts);
        dictProperty["binaryOption"] = new GenericObject().WithValue<int>(binaryOption);
        dictProperty["tripleOption"] = new GenericObject().WithValue<int>(tripleOption);
        dictProperty["quadOption"] = new GenericObject().WithValue<int>(quadOption);
        dictProperty["specialStep"] = new GenericObject().WithValue<float>(specialStep);
        dictProperty["checkPoint"] = new GenericObject().WithValue<float>(checkPoint);
        dictProperty["doubleRight"] = new GenericObject().WithValue<float>(doubleRight);
        dictProperty["doRight"] = new GenericObject().WithValue<float>(doRight);
        dictProperty["devoted"] = new GenericObject().WithValue<float>(devoted);
        dictProperty["dontMistake"] = new GenericObject().WithValue<float>(dontMistake);
        dictProperty["beFast"] = new GenericObject().WithValue<float>(beFast);
        dictProperty["signOption"] = new GenericObject().WithValue<float>(signOption);
        dictProperty["dameOption"] = new GenericObject().WithValue<float>(dameOption);
        dictProperty["pin5"] = new GenericObject().WithValue<float>(pin5);
        dictProperty["pin6"] = new GenericObject().WithValue<float>(pin6);
        dictProperty["pin7"] = new GenericObject().WithValue<float>(pin7);
        dictProperty["pin8"] = new GenericObject().WithValue<float>(pin8);
        dictProperty["pin9"] = new GenericObject().WithValue<float>(pin9);
        dictProperty["sortbyItems"] = new GenericObject().WithValue<float>(sortbyItems);
        dictProperty["posGoods"] = new GenericObject().WithValue<float>(posGoods);
        dictProperty["showWrongDoor1"] = new GenericObject().WithValue<float>(showWrongDoor1);
        dictProperty["showWrongDoor2"] = new GenericObject().WithValue<float>(showWrongDoor2);
        dictProperty["tripleHearts"] = new GenericObject().WithValue<float>(tripleHearts);
        dictProperty["keyItem"] = new GenericObject().WithValue<float>(keyItem);
        dictProperty["goOtherRoom"] = new GenericObject().WithValue<float>(goOtherRoom);
        dictProperty["checkPointItem"] = new GenericObject().WithValue<float>(checkPointItem);
        dictProperty["neverGoBack"] = new GenericObject().WithValue<float>(neverGoBack);
        dictProperty["jumpintolastRoom"] = new GenericObject().WithValue<float>(jumpintolastRoom);

    }

}

public class GenericObject
{
    private object value;

    public T GetValue<T>()
    {
        return (T)value;
    }

    public void SetValue<T>(T value)
    {
        this.value = value;
    }

    public GenericObject WithValue<T>(T value)
    {
        this.value = value;
        return this;
    }
}


