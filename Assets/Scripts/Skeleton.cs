/* 
    ------------------- Code Monkey -------------------
 Code here adapted from CodeMonkey Tutorials, website below!

               unitycodemonkey.com
    --------------------------------------------------
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class Skeleton : MonoBehaviour
{

    private Grid grid;

    private void Start()
    {
        grid = new Grid(10, 10, 10f, new Vector3(-40, -10));

    }

    private void Update()
    {
  

        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log(grid.GetValue(UtilsClass.GetMouseWorldPosition()));
        }
    }

    private void HandleClickToModifyGrid()
    {
        if (Input.GetMouseButtonDown(0))
        {
            grid.SetValue(UtilsClass.GetMouseWorldPosition(), 1);
        }
    }

}
