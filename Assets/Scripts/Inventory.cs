
using System;
using System.Collections.Generic;


internal class Inventory
{
    Dictionary<int , string> items = new Dictionary<int, string>();
    private int[] keys;
    private string[] values;

    /// <summary>
    /// It is which item can be contained in inventory
    /// </summary>
    enum itemName
    {
        showWrongDoor1 ,
        showWrongDoor2 ,
        tripleHearts ,
        keyItem ,
        goOtherRoom ,
        checkPointItem ,
        neverGoBack ,
        jumpintolastRoom   
    }

    public Inventory()
    {
            
    }

    public int[] Keys
    {
        get
        {
            return keys;
        }

        set
        {
            keys = value;
        }
    }

    public string[] Values
    {
        get
        {
            return values;
        }

        set
        {
            values = value;
        }
    }

    /// <summary>
    /// adds item to inventory if inventory's slot's number is less than 4 
    /// </summary>
    /// <param name="itemName"></param>
    public void AddItem(string itemName)
    {
        if (get_slots_number() == 4)
        {
            return;
        }

        items.Add(get_slots_number(), itemName);  
    }

    /// <summary>
    /// deletes item from inventory
    /// </summary>
    /// <param name="itemNumber"></param>
    public void DeleteItem(int itemNumber)
    {
        if(itemNumber == 3)
        {
            items.Remove(itemNumber);
        }
        else
        {
            string tempValue = items[itemNumber];
			string lastValue = String.Empty;
           
            if(get_slots_number()!= 0)
            {
				
                lastValue = items[get_slots_number() - 1];
                items[get_slots_number() - 1] = tempValue;
				items[itemNumber] = lastValue;
                items.Remove(get_slots_number() - 1);
            }
            else
            {
                lastValue = items[get_slots_number()];
                items[get_slots_number()] = tempValue;
				items[itemNumber] = lastValue;
                items.Remove(get_slots_number());
            }    
        }
    }

    public void DeleteLastItem(int itemNumber)
    {
        items.Remove(itemNumber - 1);
    }

    /// <summary>
    /// deletes all items from inventory
    /// </summary>
    public void DeleteAll()
    {
        items.Clear();
    }

    /// <summary>
    /// Fill arrays using Inventory dictionary items
    /// </summary>
    public void fill_keys_values()
    {
        int index = 0;

        if (keys != null)
        {
            if(get_slots_number()!= 0)
            {
                Array.Clear(keys, 0, get_slots_number() - 1);
                Array.Resize(ref keys, get_slots_number());
            }
            else
            {
                Array.Clear(keys, 0, 0);
                Array.Resize(ref keys, get_slots_number());
            }
        }

        else
            keys = new int[get_slots_number()];

        if (values != null)
        {
            if (get_slots_number() != 0)
            {
                Array.Clear(values, 0, get_slots_number() - 1);
                Array.Resize(ref values, get_slots_number());
            }
            else
            {
                Array.Clear(values, 0, 0);
                Array.Resize(ref values, get_slots_number());
            }

        }

        else
            values = new string[get_slots_number()];

        foreach (KeyValuePair<int, string> pair in items)
        {
            keys[index] = pair.Key;
            values[index] = pair.Value;

            index++;
        }
    }

    /// <summary>
    /// fills dictionary using keys and values parameters ,"it will come to local memory"
    /// </summary>
    /// <param name="keys"></param>
    /// <param name="values"></param>
    public void fill_dictionary(int[] keys ,string[] values)
    {
        int index = keys.Length;

        DeleteAll();

        for (int i = 0; i<index;i++)
        {
            items.Add(keys[i], values[i]);
        }

    }

    /// <summary>
    /// determines how much is slot's number in dictionary
    /// </summary>
    /// <returns></returns>
    public int get_slots_number()
    {
        int number;

        number = items.Count;

        return number;
    }



}