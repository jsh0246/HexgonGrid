using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GlobalGrid : MonoBehaviour
{
    public static GlobalGrid Instance { get; private set; }

    public Grid Grid { get; private set; }

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;

            Grid = GameObject.Find("Grid").GetComponent<Grid>();

            DontDestroyOnLoad(this.gameObject);
        }
    }
}