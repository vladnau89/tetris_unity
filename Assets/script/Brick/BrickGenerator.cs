using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickGenerator : SingletonMonoBehaviour<BrickGenerator>
{
    [SerializeField] private BrickSettings[]    brickSettings;
    [SerializeField] private Brick              brickPrefab;


    public static Brick GenerateNext(int yPos)
    {        
        return Instance._GenerateNext(yPos);
    }

    private Brick _GenerateNext(int yPos)
    {
        var brick = Instantiate(brickPrefab, new Vector3(0, yPos, 0), Quaternion.identity);

        BrickSettings settings = GetRandomBrickSetting();
        int indexMask = RandomRotationMaskIndex(settings);
        brick.Apply(settings, indexMask);

        return brick;
    }

    private static int RandomRotationMaskIndex(BrickSettings settings)
    {
        // get random between index 0 or index 2
        // as I saw only 2 variation of rotation can be 
        // because "next brick" window has height = 2
        int randomIndex = Random.Range(0, 2) * 2;

        return randomIndex;
    }

    private BrickSettings GetRandomBrickSetting()
    {
        return brickSettings[UnityEngine.Random.Range(0, brickSettings.Length)];
    }


}
