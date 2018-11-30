using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text.RegularExpressions;

public sealed class Levels {

    private TextAsset _textAsset;
    private List<Stage> _allStage;

    public TextAsset TextAsset {

        get { return _textAsset; }
     //   set { _textAsset = value; }
    }

    public Levels(TextAsset csv )
    {
        _textAsset = csv;
        CsvReader reader = new CsvReader(csv.text);
    }

    /// <summary>
    /// Shows All Levels
    /// </summary>
    /// <param name="csv">It shows for csv files that contains all levels</param>
    /// <param name="_allStage">It shows allStage that is used to in game</param>
    public Levels(TextAsset csv, ref List<Stage> _allStage) : this(csv)
    {
        this._allStage = _allStage;
        CsvReader reader = new CsvReader(csv.text);

        foreach (string[] line in reader) {

            string[] lineSub = Regex.Split(line[0], ",");

            Stage stage = new Stage();
            stage.stageıd = int.Parse(lineSub[0]);
            stage.stageLength = int.Parse(lineSub[1]);
            stage.hearts = int.Parse(lineSub[2]);
            stage.binaryOption = int.Parse(lineSub[3]);
            stage.tripleOption = int.Parse(lineSub[4]);
            stage.quadOption = int.Parse(lineSub[5]);
            stage.specialStep = float.Parse(lineSub[6]);
            stage.checkPoint = float.Parse(lineSub[7]);
            stage.doubleRight = float.Parse(lineSub[8]);
            stage.doRight = float.Parse(lineSub[9]);
            stage.devoted = float.Parse(lineSub[10]);
            stage.dontMistake = float.Parse(lineSub[11]);
            stage.beFast = float.Parse(lineSub[12]);
            stage.signOption = float.Parse(lineSub[13]);
            stage.dameOption = float.Parse(lineSub[14]);
            stage.pin5 = float.Parse(lineSub[15]);
            stage.pin6 = float.Parse(lineSub[16]);
            stage.pin7 = float.Parse(lineSub[17]);
            stage.pin8 = float.Parse(lineSub[18]);
            stage.pin9 = float.Parse(lineSub[19]);
            stage.sortbyItems = float.Parse(lineSub[20]);
            stage.posGoods = float.Parse(lineSub[21]);
            stage.showWrongDoor1 = float.Parse(lineSub[22]);
            stage.showWrongDoor2 = float.Parse(lineSub[23]);
            stage.tripleHearts = float.Parse(lineSub[24]);
            stage.keyItem = float.Parse(lineSub[25]);
            stage.goOtherRoom = float.Parse(lineSub[26]);
            stage.checkPointItem = float.Parse(lineSub[27]);
            stage.neverGoBack = float.Parse(lineSub[28]);
            stage.jumpintolastRoom = float.Parse(lineSub[29]);

            stage.m_setProperties();

            _allStage.Add(stage);

       }
    }
    public Stage m_PushLevel(int index) {

        Stage stage = new Stage();

        stage = _allStage[index];

        return stage;
    }

    public void m_PrepareLevel(Stage stage) {

        

    }
}
