using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickGenerator : SingletonMonoBehaviour<BrickGenerator>
{
    [SerializeField] private BrickSettings[]    brickSettings;
    [SerializeField] private Brick              brickPrefab;


    public static Brick GenerateNext()
    {        
        return Instance._GenerateNext();
    }

    private Brick _GenerateNext()
    {
        var brick = Instantiate(brickPrefab);

        BrickSettings settings = GetRandomSettings();
        int indexMask = RandomMaskIndex(settings);
        brick.Apply(settings, indexMask);

        return brick;
    }

    private static int RandomMaskIndex(BrickSettings settings)
    {
        return Random.Range(0, settings.rotationMasks.Length);
    }

    private BrickSettings GetRandomSettings()
    {
        return brickSettings[UnityEngine.Random.Range(0, brickSettings.Length)];
    }


}
