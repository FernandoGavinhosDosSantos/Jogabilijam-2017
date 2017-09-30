using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maps : MonoBehaviour {

    //fase 1.1
    private static int[] m11CharacterSetup = { 0, 0, 0, 0, 0 };
    private static float[] m11CameraSetup = { 2.5f, 1.0f, -10.0f, 3.5f };
    private static char[,] m11LevelSetup =
    {
        { 'W', 'W', '_', 'P' },
        { 'W', '_', '_', 'W' },
        { 'W', '_', '_', 'W' },
        { 'W', 'L', 'W', 'W' },
        { 'W', '_', '_', '_' },
        { 'F', '_', '_', 'L' },
    };

    //fase 1.2
    private static int[] m12CharacterSetup = { 0, 0, 0, 0, 0 };
    private static float[] m12CameraSetup = { 3.0f, 1.5f, -10.0f, 4.0f };
    private static char[,] m12LevelSetup =
    {
        { 'W', 'W', '_', '_', 'P' },
        { 'L', '_', '_', '_', '_' },
        { 'W', '_', '_', '_', 'W' },
        { 'W', '_', 'L', '_', 'W' },
        { 'W', '_', '_', 'W', 'W' },
        { 'F', '_', 'W', 'W', 'W' },
        { 'W', 'L', 'W', 'W', 'W' },
    };

    //fase 2.1
    private static int[] m21CharacterSetup = { 1, 0, 0, 0, 2 };
    private static float[] m21CameraSetup = { 2.5f, 2.0f, -10.0f, 4.5f };
    private static char[,] m21LevelSetup =
    {
        { 'W', 'W', 'W', '_', '_', 'P' },
        { 'W', 'W', 'W', '_', 'W', '_' },
        { 'L', '_', '_', '_', '_', '_' },
        { 'W', 'W', 'W', 'L', 'W', 'W' },
        { '_', 'L', '_', '_', 'W', 'W' },
        { 'L', 'F', 'W', 'W', 'W', 'W' },
    };

    //fase 2.2
    private static int[] m22CharacterSetup = { 1, 0, 0, 0, 2 };
    private static float[] m22CameraSetup = { 2.5f, 2.5f, -10.0f, 5.0f };
    private static char[,] m22LevelSetup =
    {
        { 'W', 'W', 'W', '_', '_', '_', 'P' },
        { 'W', 'W', 'W', '_', 'W', '_', '_' },
        { 'W', 'W', 'W', 'L', 'W', '_', '_' },
        { 'W', 'W', 'W', '_', '_', 'W', '_' },
        { 'T', '_', '_', '_', '_', '_', '_' },
        { 'W', 'W', 'W', '_', 'L', 'W', 'L' },
        { 'W', 'W', 'W', 'F', 'W', 'W', 'W' },
    };

    //fase 3.1
    private static int[] m31CharacterSetup = { 1, 1, 0, 0, 2 };
    private static float[] m31CameraSetup = { 3.0f, 2.5f, -10.0f, 5.0f };
    private static char[,] m31LevelSetup =
    {
        { 'W', 'W', 'T', '_', '_', '_', 'P' },
        { 'W', 'W', '_', '_', '_', '_', '_' },
        { 'T', '_', '_', '_', '_', '_', '_' },
        { 'F', 'W', '_', '_', '_', '_', '_' },
        { 'W', 'W', '_', '_', '_', 'T', 'W' },
    };

    //fase 3.2
    private static int[] m32CharacterSetup = { 1, 1, 0, 0, 3 };
    private static float[] m32CameraSetup = { 3.5f, 2.5f, -10.0f, 5.0f };
    private static char[,] m32LevelSetup =
    {
        { 'W', '_', '_', '_', '_', '_', 'P' },
        { '_', '_', 'W', 'W', '_', '_', '_' },
        { 'L', '_', '_', '_', '_', '_', '_' },
        { '_', '_', '_', '_', 'L', 'W', '_' },
        { '_', 'T', '_', 'W', '_', '_', '_' },
        { 'W', '_', '_', 'W', '_', '_', 'W' },
        { 'W', '_', '_', '_', '_', 'W', 'W' },
        { 'W', 'F', 'T', '_', 'L', 'W', 'W' },
    };

    //fase 4.1
    private static int[] m41CharacterSetup = { 1, 1, 1, 0, 2 };
    private static float[] m41CameraSetup = { 3.0f, 2.0f, -10.0f, 4.5f };
    private static char[,] m41LevelSetup =
    {
        { 'W', 'W', 'W', 'P', '_', '_' },
        { 'L', '_', '_', '_', '_', 'W' },
        { '_', '_', '_', '_', '_', 'A' },
        { 'W', 'W', '_', '_', 'L', '_' },
        { 'W', 'W', '_', '_', '_', 'A' },
        { 'W', 'A', '_', '_', '_', '_' },
        { 'F', '_', '_', 'A', 'W', 'W' },
    };

    //fase 4.2
    private static int[] m42CharacterSetup = { 1, 1, 1, 0, 3 };
    private static float[] m42CameraSetup = { 3.5f, 1.5f, -10.0f, 4.0f };
    private static char[,] m42LevelSetup =
    {
        { 'W', '_', '_', '_', 'P' },
        { 'W', 'W', '_', '_', '_' },
        { '_', 'T', '_', '_', 'W' },
        { '_', '_', '_', 'L', '_' },
        { 'L', '_', '_', '_', '_' },
        { 'W', 'W', '_', '_', 'T' },
        { 'W', 'W', '_', '_', 'A' },
        { 'W', 'W', 'A', 'F', '_' },
    };

    //fase 5.1
    private static int[] m51CharacterSetup = { 1, 1, 1, 1, 2 };
    private static float[] m51CameraSetup = { 3.5f, 3.0f, -10.0f, 5.5f };
    private static char[,] m51LevelSetup =
    {
        { 'W', 'W', 'W', 'A', 'A', '_', 'A', 'A', 'W' },
        { 'W', 'W', 'W', '_', '_', '_', '_', '_', 'W' },
        { 'W', 'W', 'W', '_', 'T', '_', '_', '_', 'W' },
        { 'W', 'W', 'W', 'T', '_', 'T', '_', '_', 'W' },
        { 'P', '_', '_', '_', '_', '_', 'T', '_', 'W' },
        { '_', 'L', '_', 'W', '_', '_', '_', 'T', 'W' },
        { '_', '_', '_', '_', '_', '_', '_', '_', 'F' },
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
                /*
            case 10:
                return new int[] { m52LevelSetup.GetLength(0), m52LevelSetup.GetLength(1) };
                */
        }
        return null;
    }

    public static int[] CharacterSettings(int level)
    {
        switch (level)
        {
            case 1:
                return m11CharacterSetup;
            case 2:
                return m12CharacterSetup;
            case 3:
                return m21CharacterSetup;
            case 4:
                return m22CharacterSetup;
            case 5:
                return m31CharacterSetup;
            case 6:
                return m32CharacterSetup;
            case 7:
                return m41CharacterSetup;
            case 8:
                return m42CharacterSetup;
            case 9:
                return m51CharacterSetup;
                /*
            case 10:
                return m52CharacterSetup;
                */
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
                 /*
            case 10:
                return m52CameraSetup;
                */
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
                /*
            case 10:
                return m52LevelSetup;
                */
        }
        return null;
    }
}
