using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    private static Settings instance;

    [SerializeField] private bool _wallKick = false;

    public static bool WallKick { get { return instance._wallKick; } }

    private void Awake()
    {
        instance = this;
    }

}
