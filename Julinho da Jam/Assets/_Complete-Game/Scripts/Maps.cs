using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maps : MonoBehaviour {

    //fase 1.1
    private static float[] m11CameraSetup = { 1.0f, 1.0f, -10.0f, 5.0f };
    private static char[,] m11LevelSetup =
    {
        { 'P', '_', 'A', '_', 'L' },
        { '_', '_', '_', '_', '_' },
        { '_', '_', '_', '_', '_' },
        { 'F', 'W', '_', '_', '_' },
    };

    //fase 1.2
    private static float[] m12CameraSetup = { 2.0f, 1.0f, -10.0f, 3.0f };
    private static char[,] m12LevelSetup =
    {
        { 'P', '_', '_' },
        { 'W', '_', 'F' },
    };

    public static int[] BoardSettings(int level)
    {
        switch (level)
        {
            case 1:
                return new int[] { m11LevelSetup.GetLength(0), m11LevelSetup.GetLength(1) };
            case 2:
                return new int[] { m12LevelSetup.GetLength(0), m12LevelSetup.GetLength(1) };
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
        }
        return null;
    }
}
