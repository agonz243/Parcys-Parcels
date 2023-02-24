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

    //float for where to snap for grid squares
    public float snapRange = 1.0f;

    //list of coordinates for each tile on grid to snap to
    //i think a vector3 here
    public List<GameObject> tilesnapPoints = new List<GameObject>();

    //list of the tilepoints vector3s for xy values
    public List<Transform> tilesnapXY = new List<Transform>();

    //to fetch tiles colliders
    public List<Collider2D> tileColliders = new List<Collider2D>();

    //start method here
    void Start()
    {
        MakeaDaGrid();
        //testing here for now
        SolveaDaPuzzle();
    }



    //function for gridd(y)ing

    //note: tiles populate from left to right, bottom to top
    public void MakeaDaGrid()
    {
        //loop over width
        for(int x = 0; x < _width; x = x+10) //added 15 to x and y for increased size of tiles
        {
            for(int y = 0; y < _height; y = y+10)
            {
                //these numbers are tweaked for tile and board size, ask graham if changes needed :)
                var spawnTile = Instantiate(tilePrefab, new Vector3(x-50, y), Quaternion.identity); //need to watch the Quarternion HERE for rotation
               
                //snapPoints.Add(spawnTile);
                
                spawnTile.name = $"Tile {x} {y}";
                
                //have the tile objects in a list, do not think needed?
                tilesnapPoints.Add(spawnTile.gameObject);

                //here we have the transform (vector 3) of each tile's position, want to use for snapping
                tilesnapXY.Add(spawnTile.transform);

                //adding the colliders to a separate list for endgame checks
                Collider2D currTile = spawnTile.bonk;

                //add in tiles colliders to list for win condition
                tileColliders.Add(currTile);

                //math here using mods to determine checkerboard coloring for tiles
                //var isOffset = (x + y) / 10;
                //lil magic math of my own design
                var isOffset = ((x + y) / 10) % 2 == 0;
                spawnTile.Init(isOffset);
            }
        }


        //put the coordinates in the bag and nobody gets hurt
        foreach (Transform pair in tilesnapXY)
        {
            //Debug.Log(pair);
        }


        //actually moving the cam here using reference from above
        camcorder.transform.position = new Vector3((float)_width / 2 - 0.5f -10, (float)_height / 2 - 0.5f, -10);
    }



    public void SolveaDaPuzzle()
    {
        foreach(var spawnTile in tilesnapPoints)
        {
            //want to see if colliding here
            //TileColl = spawnTile.GetComponent<Collider>();
            //if()
            HandleCollision(spawnTile);
        }
    }


    public void OnTriggerEnter(Collider col)
    {
        Debug.Log("Package HIT ME");
    }


    public void HandleCollision(GameObject obj)
    {
        if (obj.tag == "Packages")
        {
            Debug.Log("Package HIT ME");
        }
    }




    public void OnCollisionEnter2D(Collision2D collision)
    {
        HandleCollision(collision.gameObject);
    }
}
