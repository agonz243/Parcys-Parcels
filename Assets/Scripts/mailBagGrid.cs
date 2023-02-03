using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mailBagGrid : MonoBehaviour
{
    //private variables for dimensions of grid
    [SerializeField] private int _width, _height;

    //making an individual tile
    [SerializeField] private Tile skware;

    //function for gridding
    void MakeaDaGrid()
    {
        //loop over width
        for(int x = 0; x < _width; x++)
        {
            for(int y = 0; y < _height; y++)
            {

            }
        }
    }
}
