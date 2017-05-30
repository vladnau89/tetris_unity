using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickGenerator : SingletonMonoBehaviour<BrickGenerator>
{
    [SerializeField] private Brick[] brickPrefabs;


    public static Brick GenerateNext()
    {        
        return Instance._GenerateNext();
    }

    private Brick _GenerateNext()
    {
        var brick = Instantiate(brickPrefabs[UnityEngine.Random.Range(0, brickPrefabs.Length)]);
        Debug.LogError("Generate " + brick.name);
        return brick;
    }

}
