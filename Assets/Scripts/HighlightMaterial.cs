using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;

public class HighlightMaterial : MonoBehaviour
{
    public Material highlightMaterial;
    public Material originalMaterial;
    public GameObject character;
    public GameObject placeHolderTile;
    public Vector3 offset;
    private GameObject[] generatedTiles = null;
    private GameObject tempTile = null;

    // Start is called before the first frame update
    void Start()
    {
        offset = placeHolderTile.transform.position - character.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(generatedTiles == null)
        {
             generatedTiles = GameObject.FindGameObjectsWithTag("RandomTiles");
        }

        if(tempTile != null) 
        {
            var tempRenderer = tempTile.transform.GetComponent<Renderer>();
            tempRenderer.material = originalMaterial;
        }
        
        var newPos = character.transform.position - offset + character.transform.forward;
        placeHolderTile.transform.position = new Vector3(newPos.x, 0, newPos.z);
        var estPos = estimatePosition(placeHolderTile.transform.position);
        
        var selectedTile = generatedTiles.Where(t => t.transform.position == estPos).FirstOrDefault();
        if(selectedTile != null)
        {
            var tileRenderer = selectedTile.transform.GetComponent<Renderer>();
            tileRenderer.material = highlightMaterial;
            Debug.Log(selectedTile.transform.position);
        }

        tempTile = selectedTile;
    }

    private Vector3 estimatePosition(Vector3 actualPosition) 
    {
        float estX = (float)Math.Round(actualPosition.x);
        float estZ = (float)Math.Round(actualPosition.z);
        return new Vector3(estX, 0, estZ);
    }
}
