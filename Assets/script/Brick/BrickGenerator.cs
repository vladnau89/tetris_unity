using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickGenerator : SingletonMonoBehaviour<BrickGenerator>
{
    [SerializeField] private Transform          pivot;
    [SerializeField] private BrickSettings[]    brickSettings;
    [SerializeField] private Material[]         brickMaterials;
    [SerializeField] private Brick              brickPrefab;


    public static Brick GenerateNext(int xPos, int yPos)
    {        
        return Instance._GenerateNext(xPos, yPos);
    }

    private Brick _GenerateNext(int xPos, int yPos)
    {
        var brick = Instantiate(brickPrefab);

        brick.transform.parent = pivot;

        brick.transform.localPosition = new Vector3(xPos, yPos, 0);
        brick.transform.localRotation = Quaternion.identity;

        BrickSettings settings = GetRandomBrickSetting();
        int indexMask = RandomRotationMaskIndex(settings);
        Material material = GetRandomMaterial();
        brick.Apply(settings, indexMask, material);

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


    private Material GetRandomMaterial()
    {
        return brickMaterials[UnityEngine.Random.Range(0, brickMaterials.Length)];
    }

}
