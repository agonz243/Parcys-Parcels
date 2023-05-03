using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class sortMode : MonoBehaviour
{
    public void customAxisOn()
    {
        GraphicsSettings.transparencySortMode = TransparencySortMode.CustomAxis;
        GraphicsSettings.transparencySortAxis = new Vector3(0.0f, 1.0f, 0.0f);
    }

    public void customAxisOff()
    {
        GraphicsSettings.transparencySortMode = TransparencySortMode.Default;
    }
}
