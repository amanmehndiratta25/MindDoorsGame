using UnityEngine;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


public class AllLevels : MonoBehaviour {

    public TextAsset textAsset;
    public int lastGameIndex
    {
        get { return _lastGameIndex; }
        set { _lastGameIndex = value; }
    }


    private Levels _levels;

    private List<Stage> _allStage = new List<Stage>();
    

    private int _lastGameIndex;


    private static AllLevels instance;
    public static AllLevels Instance {
        get
        {
            if (instance == null)
            {
                instance = new AllLevels();
            }

            return instance;
        }
    }
    
    private  Inventory _items;
    internal Inventory Items
    {
        get
        {
            return _items;
        }

        set
        {
            _items = value;
        }
    }

    private bool IsShowGUI = false;

    void Awake(){

        DontDestroyOnLoad(this);

        if(instance == null)
        {
            instance = this;
        }
        else
        {
            DestroyObject(gameObject);
        }

        instance._levels = new Levels(textAsset, ref _allStage);

// Inventory is created at once. x

        if( _items == null)
        _items = new Inventory();

        Load();

    }


    /// <summary>
    /// It pushes Stage among Levels
    /// </summary>
    /// <returns></returns>
    public Stage pushStage()
    {

        Stage stage = new Stage();

        stage = _allStage[AllLevels.Instance.lastGameIndex];

        return stage;

    }   


    /// <summary>
    /// saves player data
    /// </summary>
    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.OpenOrCreate);
        PlayerData data = new PlayerData();
        data.lastIndex  = AllLevels.Instance.lastGameIndex;
        data.keys       = AllLevels.Instance.Items.Keys;
        data.values     = AllLevels.Instance.Items.Values;

        bf.Serialize(file, data);
        file.Close();

    }

    /// <summary>
    /// loads player data
    /// </summary>
    public void Load()
    {
        if(File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.OpenOrCreate);
            PlayerData data =(PlayerData)  bf.Deserialize(file);

            file.Close();

            AllLevels.Instance.lastGameIndex = data.lastIndex;
            AllLevels.Instance.Items.Keys = data.keys;
            AllLevels.Instance.Items.Values = data.values;
            AllLevels.Instance.Items.fill_dictionary(AllLevels.Instance.Items.Keys,
                                                     AllLevels.Instance.Items.Values);

        }
        else
        {
            Save();
        }

    }

    /// <summary>
    /// deletes player's data file from local memory
    /// </summary>
    public void Delete_File()
    {
        if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            File.Delete(Application.persistentDataPath + "/playerInfo.dat");
        }
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Delete_File();
            Debug.Log("File was deleted by user ");
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            IsShowGUI = !IsShowGUI;
        }
    }

    public void IncrementLevelIndex()
    {
        ++AllLevels.Instance.lastGameIndex;

        Save();
    }

    public void DecrementLevelIndex()
    {
       ++AllLevels.Instance.lastGameIndex;

        Save();
    }

    void OnGUI()
    {

        if (IsShowGUI)
        {
            GUILayout.BeginArea(new Rect(100, 100, Screen.width, 500));

          

            GUILayout.Label("User Level : ");

            GUILayout.Box("Level number : " + AllLevels.instance.lastGameIndex.ToString(),GUILayout.Width(150),GUILayout.Height(50)) ;

            GUILayout.Label("User Items : ");

            GUILayout.BeginHorizontal();

            for (int i = 0; i< AllLevels.instance.Items.get_slots_number(); i++)
            {
                GUILayout.Box(AllLevels.instance.Items.Keys[i].ToString()+". item : " + 
                    AllLevels.instance.Items.Values[i].ToString(), GUILayout.Width(150), GUILayout.Height(50));
            }

            GUILayout.EndHorizontal();

            GUILayout.Label("Stage's Rooms : ");

            GUILayout.BeginHorizontal();
            
            for(int i = 0; i < GameObject.Find("StageGenerator").GetComponent<stageGenerator>().get_rooms().Count;i++)
            {
                GUILayout.Box( (i+1).ToString() + ". Room : " +
                                GameObject.Find("StageGenerator").GetComponent<stageGenerator>().get_rooms()[i].MyRoomType.ToString(),
                                 GUILayout.Width(150), GUILayout.Height(50));

            }

            GUILayout.EndHorizontal();

            GUILayout.EndArea();
        }
     
    }

}
