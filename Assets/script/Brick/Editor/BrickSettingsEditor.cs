using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

[CustomEditor(typeof(BrickSettings))]
public class BrickSettingsEditor : Editor
{
    public override void OnInspectorGUI()
    {
        BrickSettings brickSettings = base.target as BrickSettings;

        int length = 4;

        //DrawRotationMatrix("Rotation state 1", ref brickSettings.pos1, length);
        //GUILayout.Space(23);

        for (int i = 0; i < brickSettings.rotationMasks.Length; i++)
        {
            string label = string.Format("Rotation state {0}", i + 1);
            DrawRotationMatrix(label, ref brickSettings.rotationMasks[i], length);

            GUILayout.Space(10);
        }



        EditorUtility.SetDirty(target);


    }


    private void DrawRotationMatrix(string label, ref ushort rotationMask, int length)
    {
        EditorGUILayout.LabelField("value : " + rotationMask);
        EditorGUILayout.LabelField(label);
        for (int j = 0; j < length; j++)
        {
            DrawRaw(ref rotationMask, length, j);
        }
      
    }


    private void DrawRaw(ref ushort rowValue, int length, int rawIndex)
    {
        EditorGUILayout.BeginHorizontal();
        {
            for (int i = rawIndex * length; i < rawIndex * length + length; i++)
            {
                ushort bit = (ushort)(1 << i);

                DrawToggle(ref rowValue, bit);
            }
        }
        EditorGUILayout.EndHorizontal();
    }


    private void DrawToggle(ref ushort row, ushort bit)
    {
        bool toggle1 = Convert.ToBoolean(row & bit);
        bool editorToggle1 = EditorGUILayout.Toggle(toggle1, GUILayout.Width(20));

        if (editorToggle1)
        {
            row |= bit;
        }
        else
        {
            row &= (ushort)~bit;
        }
    }




}
