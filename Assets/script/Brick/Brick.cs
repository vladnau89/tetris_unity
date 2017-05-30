using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void Rotate()
    {

    }

}
