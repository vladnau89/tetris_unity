using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Brick : MonoBehaviour
{
    private string _name;
    public new string name
    {
        get
        {
            if (string.IsNullOrEmpty(_name))
            {
                _name = base.name = base.name.Replace("(Clone)", "");
            }
            return _name;
        }
    }


    
    [SerializeField] private GameObject[] bricks = new GameObject[16];

    private BrickSettings _settings;
    private int _maskIndex;


    public void Apply(BrickSettings settings, int maskIndex)
    {
        _settings = settings;
        _maskIndex = maskIndex;

        ushort mask = settings.rotationMasks[maskIndex];

        Repaint(mask);
    }

    private void Repaint(ushort mask)
    {
        for (int i = 0; i < bricks.Length; i++)
        {
            bool active = Convert.ToBoolean(mask & 1 << i);
            bricks[i].SetActive(active);
        }
    }

    public void Rotate()
    {
        ++_maskIndex;
        ushort maskNext = _settings.rotationMasks[_maskIndex % _settings.rotationMasks.Length];
        Repaint(maskNext);
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space))
        {
            Rotate();
        }
    }


    private void OnDrawGizmos()
    {
        for (int i = 0; i < bricks.Length; i++)
        {
            Gizmos.DrawWireCube(bricks[i].GetComponent<Renderer>().bounds.center, bricks[i].transform.localScale);
        }
    }

}
