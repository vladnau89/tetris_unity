using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Generator
{
    private delegate float GenerateRandomFunc();

    private static System.Random r = new System.Random();

    public enum Type
    {
        Unity,
        DotNet,
    }

    public static float[] GenerateRandomArray01(int count, Type type)
    {
        GenerateRandomFunc func = null;

        switch (type)
        {   
            case Type.Unity:
                func = () => UnityEngine.Random.Range(0f, 1.0f);
                break;
            case Type.DotNet:
                func = GetRandomDotnetFloat;
                break;
            default:
                throw new System.Exception("undefined type = " + type);
        }

        return GenerateRandomArray(count, func);
    }

    private static float[] GenerateRandomArray(int count , GenerateRandomFunc func)
    {
        float[] nums = new float[count];

        for (int i = 0; i < count; i++)
        {
            nums[i] = func();
        }

        return nums;
    }

    private static float GetRandomDotnetFloat()
    {
        int randomInt = r.Next(0, int.MaxValue);
        return (float)randomInt / int.MaxValue;
    }

}
