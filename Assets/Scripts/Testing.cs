/* 
    ------------------- Code Monkey -------------------
    Code here modified from code monkey tutorial :)
            credits below to my king
               unitycodemonkey.com
    --------------------------------------------------
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class Testing : MonoBehaviour {

    private Grid grid;

    private void Start() {
        grid = new Grid(8, 6, 1.0f, new Vector3(-5, -5));

    }

    private void Update() {
        //HandleClickToModifyGrid();
        //HandleHeatMapMouseMove();

        if (Input.GetMouseButtonDown(1)) {
            Debug.Log(grid.GetValue(UtilsClass.GetMouseWorldPosition()));
        }
    }

    private void HandleClickToModifyGrid() {
        if (Input.GetMouseButtonDown(0)) {
            grid.SetValue(UtilsClass.GetMouseWorldPosition(), 1);
        }
    }


}
