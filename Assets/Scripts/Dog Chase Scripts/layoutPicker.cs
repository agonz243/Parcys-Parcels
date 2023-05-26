using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class layoutPicker : MonoBehaviour
{
    [SerializeField] GameObject[] layouts;
    [SerializeField] GameObject astar;

    // Start is called before the first frame update
    void Awake()
    {
        GameObject randomLayout = layouts[Random.Range(0, layouts.Length)];
        randomLayout.SetActive(true);

        // Set A* algorithm to active AFTER activating layout
        // so it is included in the navigation graph
        astar.SetActive(true);
    }
}
