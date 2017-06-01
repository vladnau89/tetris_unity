using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "brick", menuName = "Brick/Create", order = 1)]
public class BrickSettings : ScriptableObject
{
    const int ROTATIONS_COUNT = 4;

    public ushort[] rotationMasks = new ushort[ROTATIONS_COUNT];

}
