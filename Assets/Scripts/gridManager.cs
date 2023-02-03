using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gridManager : MonoBehaviour
{
    //private variables for dimensions of grid
    [SerializeField] private int _width, _height;

    //making an individual tile
    [SerializeField] private Tile tilePrefab;

    //want to center camera on our grid, solve problem of "origin"
    [SerializeField] private Transform camcorder;

    //start method here
    void Start()
    {
        MakeaDaGrid();
    }



    //function for gridding
    void MakeaDaGrid()
    {
        //loop over width
        for(int x = 0; x < _width; x = x+15) //added 15 to x and y for increased size of tiles
        {
            for(int y = 0; y < _height; y = y+15)
            {
                //REMEMEBER rotation in another script!!!!111!1!1!!!!!!
                //these numbers are tweaked for tile and board size, ask graham if changes needed :)
                var spawnTile = Instantiate(tilePrefab, new Vector3(x-40, y-2), Quaternion.identity); //need to watch the Quarternion HERE for rotation
                spawnTile.name = $"Tile {x} {y}";

                //math here using mods to determine checkerboard coloring for tiles
                var isOffset = (x + y) % 2 == 1;
                spawnTile.Init(isOffset);
            }
        }

        //actually moving the cam here using reference from above
        camcorder.transform.position = new Vector3((float)_width / 2 - 0.5f, (float)_height / 2 - 0.5f, -10);
    }
}
