using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testBag
{
    //private variables for dimensions
    private int width;
    private int height;
    //2D array for x,y values
    private int [,] gridArray;

    //constructor for the grid(dy)
    public testBag(int width, int height)
    {
        this.width = width;
        this.height = height;

        gridArray = new int[width, height];

        for (int x = 0; x <gridArray.GetLength(0); x++)
        {
            for(int y = 0; y < gridArray.GetLength(1); y++)
            {

            }
        }
    }
}
