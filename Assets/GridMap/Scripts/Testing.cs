/* 
    ------------------- Code Monkey -------------------

    Thank you for downloading this package
    I hope you find it useful in your projects
    If you have any questions let me know
    Cheers!

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
        grid = new Grid(8, 6, 5f, new Vector3(-5, -5));

    }

    private void Update() {
        //HandleClickToModifyGrid();

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
