using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "brick", menuName = "Brick/Create", order = 1)]
public class BrickSettings : ScriptableObject
{
    public ushort pos1;

    public ushort[] rotationMasks = new ushort[4];

}
