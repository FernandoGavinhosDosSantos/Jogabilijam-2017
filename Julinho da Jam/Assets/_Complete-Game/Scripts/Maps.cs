using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maps : MonoBehaviour {

    //fase 1.1
    private static float[] m11CameraSetup = { 2.5f, 1.1f, -10.0f, 3.5f };
    private static char[,] m11LevelSetup =
    {
        {'W', 'W', '_', 'P'},
        {'W', '_', '_', '_'},
        {'W', '_', '_', 'W'},
        {'W', 'L', 'W', '_'},
        {'W', '_', '_', '_'},
        {'F', '_', '_', 'L'},

    };

    //fase 1.2
    private static float[] m12CameraSetup = { 3f, 1.7f, -10.0f, 4.0f };
    private static char[,] m12LevelSetup =
    {
        { 'W', 'W', '_', '_', 'P' },
        { 'L', '_', '_', '_', '_' },
        { 'W', '_', '_', '_', 'W' },
        { 'W', '_', 'L', 'W', 'W' },
        { 'W', '_', '_', 'W', 'W' },
        { 'F', '_', 'W', 'W', 'W' },
        { 'W', 'L', 'W', 'W', 'W' },
    };
    private static float[] m21CameraSetup = { 2.5f, 2.2f, -10.0f, 4.5f };
    private static char[,] m21LevelSetup =
    {
        { 'W', 'W', 'W', '_', '_', 'P'},
        { 'W', 'W', 'W', '_', 'W', '_'},
        { 'L', '_', '_', '_', '_', '_'},
        { 'W', 'W', 'W', 'L', 'W', 'W'},
        { 'L', '_', 'L', '_', 'W', 'W'},
        { 'W', 'F', 'W', 'W', 'W', 'W'},
    };

    private static float[] m22CameraSetup = { 3f, 2.3f, -10.0f, 5.0f };
    private static char[,] m22LevelSetup =
    {
        {'W', 'W', 'W', '_', '_', '_', 'P'},
        {'W', 'W', 'W', '_', 'W', '_', '_'},
        {'W', 'W', 'W', 'L', 'W', '_', '_'},
        {'W', 'W', 'W', '_', '_', 'W', '_'},
        {'T', '_', '_', '_', '_', '_', '_'},
        {'W', 'W', 'W', '_', 'L', 'W', 'L'},
        {'W', 'W', 'W', 'F', 'W', 'W', 'W'},
    };

    private static float[] m31CameraSetup = { 2f, 2.5f, -10.0f, 5.0f };
    private static char[,] m31LevelSetup =
    {
        {'W', 'W', 'T', '_', '_', '_', 'P'},
        {'W', 'W', '_', '_', '_', '_', '_'},
        {'T', '_', '_', '_', '_', '_', '_'},
        {'F', 'W', '_', '_', '_', '_', '_'},
        {'W', 'W', '_', '_', '_', 'T', 'W'},
    };

    private static float[] m32CameraSetup = { 3.5f, 2.3f, -10.0f, 5.0f };
    private static char[,] m32LevelSetup =
    {
        {'W', '_', '_', '_', '_', '_', 'P'},
        {'_', '_', 'W', 'W', '_', '_', '_'},
        {'L', '_', '_', '_', '_', '_', '_'},
        {'_', '_', '_', '_', 'L', 'W', '_'},
        {'_', 'T', '_', 'W', '_', '_', '_'},
        {'W', '_', '_', 'W', '_', '_', 'W'},
        {'W', '_', '_', '_', '_', 'W', 'W'},
        {'W', 'F', 'T', '_', 'L', 'W', 'W'},
    };

    private static float[] m41CameraSetup = { 3f, 2.3f, -10.0f, 5.0f };
    private static char[,] m41LevelSetup =
    {
        { 'W', 'W', 'W', 'P', '_', '_'},
        { 'L', '_', '_', '_', '_', 'W'},
        { '_', '_', '_', '_', '_', 'A'},
        { 'W', 'W', '_', '_', 'L', '_'},
        { 'W', 'W', '_', '_', '_', 'A'},
        { 'W', 'A', '_', '_', '_', '_'},
        { 'F', '_', '_', 'W', 'W', 'W'},
    };

    private static float[] m42CameraSetup = { 3.5f, 2.3f, -10.0f, 5.0f };
    private static char[,] m42LevelSetup =
    {
        { 'W', '_', '_', '_', 'P'},
        { 'W', 'W', '_', '_', '_'},
        { '_', 'T', '_', '_', 'W'},
        { '_', '_', '_', 'L', '_'},
        { 'L', '_', '_', '_', '_'},
        { 'W', 'W', '_', '_', 'T'},
        { 'W', 'W', '_', '_', 'A'},
        { 'W', 'W', 'A', 'F', '_'},
    };

    private static float[] m51CameraSetup = { 3.5f, 2.3f, -10.0f, 5.0f };
    private static char[,] m51LevelSetup =
    {
        { 'W', 'W', '_', '_', '_', 'P'},
        { 'W', 'W', '_', '_', '_', '_'},
        { 'W', 'W', '_', '_', 'W', 'W'},
        { '_', '_', '_', '_', '_', '_'},
        { 'L', '_', '_', '_', '_', 'A'},
        { 'F', 'L', '_', '_', 'W', 'W'},
        { 'T', 'T', '_', '_', 'W', 'W'},
    };

    private static float[] m521CameraSetup = { 3.5f, 2.3f, -10.0f, 5.0f };
    private static char[,] m521LevelSetup =
    {
        { 'L', '_', '_', '_', '_', 'P'},
        { '_', 'W', 'W', '_', '_', 'W'},
        { '_', 'W', 'W', '_', '_', 'W'},
        { '_', 'W', 'W', '_', '_', 'W'},
        { 'L', 'W', 'W', 'L', 'L', 'W'},
        { '_', '_', '_', '_', '_', 'W'},
        { 'F', 'W', 'W', 'W', 'W', 'W'},
    };

    private static float[] m522CameraSetup = { 3.5f, 2.3f, -10.0f, 5.0f };
    private static char[,] m522LevelSetup =
    {
        { 'L', '_', '_', '_', '_', 'P'},
        { '_', 'W', 'W', '_', '_', 'W'},
        { '_', 'W', 'W', '_', '_', 'W'},
        { '_', 'W', 'W', '_', '_', 'W'},
        { 'L', 'W', 'W', 'L', 'L', 'W'},
        { '_', '_', '_', '_', '_', 'W'},
        { 'F', 'W', 'W', 'W', 'W', 'W'},
    };
    public static int[] BoardSettings(int level)
    {
        switch (level)
        {
            case 1:
                return new int[] { m11LevelSetup.GetLength(0), m11LevelSetup.GetLength(1) };
            case 2:
                return new int[] { m12LevelSetup.GetLength(0), m12LevelSetup.GetLength(1) };
            case 3:
                return new int[] { m21LevelSetup.GetLength(0), m21LevelSetup.GetLength(1) };
            case 4:
                return new int[] { m22LevelSetup.GetLength(0), m22LevelSetup.GetLength(1) };
            case 5:
                return new int[] { m31LevelSetup.GetLength(0), m31LevelSetup.GetLength(1) };
            case 6:
                return new int[] { m32LevelSetup.GetLength(0), m32LevelSetup.GetLength(1) };
            case 7:
                return new int[] { m41LevelSetup.GetLength(0), m41LevelSetup.GetLength(1) };
            case 8:
                return new int[] { m42LevelSetup.GetLength(0), m42LevelSetup.GetLength(1) };
            case 9:
                return new int[] { m51LevelSetup.GetLength(0), m51LevelSetup.GetLength(1) };
            case 10:
                return new int[] { m521LevelSetup.GetLength(0), m521LevelSetup.GetLength(1) };
            case 11:
                return new int[] { m522LevelSetup.GetLength(0), m522LevelSetup.GetLength(1) };
        }
        return null;
    }

    public static float[] CameraSettings(int level)
    {
        switch (level)
        {
            case 1:
                return m11CameraSetup;
            case 2:
                return m12CameraSetup;
            case 3:
                return m21CameraSetup;
            case 4:
                return m22CameraSetup;
            case 5:
                return m31CameraSetup;
            case 6:
                return m32CameraSetup;
            case 7:
                return m41CameraSetup;
            case 8:
                return m42CameraSetup;
            case 9:
                return m51CameraSetup;
            case 10:
                return m521CameraSetup;
            case 11:
                return m522CameraSetup;
        }
        return null;
    }

    public static char[,] LevelSettings(int level)
    {
        switch (level)
        {
            case 1:
                return m11LevelSetup;
            case 2:
                return m12LevelSetup;
            case 3:
                return m21LevelSetup;
            case 4:
                return m22LevelSetup;
            case 5:
                return m31LevelSetup;
            case 6:
                return m32LevelSetup;
            case 7:
                return m41LevelSetup;
            case 8:
                return m42LevelSetup;
            case 9:
                return m51LevelSetup;
            case 10:
                return m521LevelSetup;
            case 11:
                return m522LevelSetup;
        }
        return null;
    }
}
