using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Level_Generator : MonoBehaviour
{
    private const float PLAYER_DISTANCE_SPAWN_LEVEL_PART = 200f; 

    [SerializeField] private Transform levelPart_Start;
    [SerializeField] private List<Transform> ThunderPartList;
    [SerializeField] private List<Transform> EQPartList;
    [SerializeField] private List<Transform> TsunamiPartList;
    

    private List<Transform> randomTransform;
    [SerializeField] private Transform player;

    [SerializeField] private TMP_Text _thunderCollisionCount;
    [SerializeField] private TMP_Text _EQCollisionCount;
    [SerializeField] private TMP_Text _TsunamiCollisionCount;

    [SerializeField] private TMP_Text _thunderLevel;
    [SerializeField] private TMP_Text _EQLevel;
    [SerializeField] private TMP_Text _TsunamiLevel;

    private int prevThunderValue = 0;
    private int prevEQValue = 0;
    private int prevTsunamiValue = 0;
    private int prevLevelIndex = 0;
    private int curLevelIndex = 0;
    private string prevLevelName = "";

    private Vector3 lastEndPosition;


    private void Awake()
    {
        randomTransform = new List<Transform>();
        lastEndPosition = levelPart_Start.Find("EndPoint").position;
        lastEndPosition = new Vector3(lastEndPosition.x - 17.01f, 0f, 0);
        prevLevelName = levelPart_Start.name;

        randomTransform.Add(ThunderPartList[0]);
        randomTransform.Add(EQPartList[0]);
        randomTransform.Add(TsunamiPartList[0]);


        int startingSpawnLevelParts = 5;
        for (int i = 0; i < startingSpawnLevelParts; i++)
        {
           
            SpawnLevelPart();
        }  
    }


    private void Update()
    {

      if (Vector3.Distance(player.position, lastEndPosition) < PLAYER_DISTANCE_SPAWN_LEVEL_PART)
        {

            RefreshRandomizer();
            
            SpawnLevelPart();
        }
    }

    private void RefreshRandomizer()
    {
        if (int.Parse(_thunderCollisionCount.text) != 0 && int.Parse(_thunderCollisionCount.text) != prevThunderValue)
        {
            for (int i = 0; i <= int.Parse(_thunderCollisionCount.text); i++)
            {
                if (i < ThunderPartList.Count)
                    randomTransform.Add(ThunderPartList[i]);
            }
            prevThunderValue = int.Parse(_thunderCollisionCount.text);
        }

        if (int.Parse(_EQCollisionCount.text) != 0 && int.Parse(_EQCollisionCount.text) != prevEQValue)
        {
            for (int i = 0; i <= int.Parse(_EQCollisionCount.text); i++)
            {
                if (i < EQPartList.Count)
                    randomTransform.Add(EQPartList[i]);
            }
            prevEQValue = int.Parse(_EQCollisionCount.text);
        }

        if (int.Parse(_TsunamiCollisionCount.text) != 0 && int.Parse(_TsunamiCollisionCount.text) != prevTsunamiValue)
        {
            for (int i = 0; i <= int.Parse(_TsunamiCollisionCount.text); i++)
            {
                if (i < TsunamiPartList.Count)
                    randomTransform.Add(TsunamiPartList[i]);
            }
            prevTsunamiValue = int.Parse(_TsunamiCollisionCount.text);
        }
    }

    private void SpawnLevelPart()
    {
        //do
        //    curLevelIndex = Random.Range(0, randomTransform.Count);
        //while (curLevelIndex == prevLevelIndex);

        curLevelIndex = Random.Range(0, randomTransform.Count);

        prevLevelIndex = curLevelIndex;
        Transform chosenLevelPart = randomTransform[curLevelIndex];
        int levelCount = 0;

        if (randomTransform[curLevelIndex].name.Contains("Earthquake"))
        {
            levelCount = int.Parse(_EQLevel.text);
            _EQLevel.text = (levelCount + 1).ToString();
        }
        else if (randomTransform[curLevelIndex].name.Contains("Tsunami"))
        {
            levelCount = int.Parse(_TsunamiLevel.text);
            _TsunamiLevel.text = (levelCount + 1).ToString();
        }
        else if (randomTransform[curLevelIndex].name.Contains("Thunder"))
        {
            levelCount = int.Parse(_thunderLevel.text);
            _thunderLevel.text = (levelCount + 1).ToString();
        }

        Transform lastLevelPartTransform = SpawnNextStation(chosenLevelPart, lastEndPosition);
        lastEndPosition = lastLevelPartTransform.Find("EndPoint").position;
        lastEndPosition = new Vector3(lastEndPosition.x - 18.87f, 0f, 0);
        prevLevelName = randomTransform[curLevelIndex].name;
    }

    private Transform SpawnNextStation(Transform levelPart, Vector3 spawnPostion)
    {
        Transform levelPartTransform = Instantiate(levelPart, spawnPostion, Quaternion.identity);
        return levelPartTransform;
    }
}
