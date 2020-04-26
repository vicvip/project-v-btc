using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;

public class HighlightMaterial : MonoBehaviour
{
    public Material highlightMaterial;
    public Material originalMaterial;
    public Material tilledMaterial;
    public GameObject character;
    public GameObject mainCamera;
    public GameObject placeHolderTile;
    public GameObject tileHitFXObj;
    public GameObject poofFXObj;
    public Vector3 offset;
    private GameObject[] generatedTiles = null;
    private GameObject tempTile = null;
    private GameObject selectedTile;
    public List<ObjectPosition> objectPositionList;

    // Start is called before the first frame update
    void Start()
    {
        offset = placeHolderTile.transform.position - character.transform.position;
        objectPositionList = mainCamera.GetComponent<TilesGenerator>().objectPositionList;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(generatedTiles == null)
        {
            generatedTiles = GameObject.FindGameObjectsWithTag("RandomTiles");
        }

        if(tempTile != null) 
        {
            var tempRenderer = tempTile.transform.GetComponent<Renderer>();
            var tempTileObject = objectPositionList.Where(o => o.X == tempTile.transform.position.x && o.Z == tempTile.transform.position.z).First();
            if(!tempTileObject.IsTilled)
            {
                tempRenderer.material = originalMaterial;
            }
        }
        
        var newPos = character.transform.position - offset + character.transform.forward;
        placeHolderTile.transform.position = new Vector3(newPos.x, 0, newPos.z);
        var estPos = estimatePosition(placeHolderTile.transform.position);
        
        selectedTile = generatedTiles.Where(t => t.transform.position == estPos).FirstOrDefault();
        if(selectedTile != null)
        {
            tileHitFXObj.transform.position = selectedTile.transform.position + new Vector3(0, 0.25f, 0);
            poofFXObj.transform.position = selectedTile.transform.position + new Vector3(0, -0.25f, 0);
            var tileRenderer = selectedTile.transform.GetComponent<Renderer>();
            var tileObject = objectPositionList.Where(o => o.X == selectedTile.transform.position.x && o.Z == selectedTile.transform.position.z).First();
            if(!tileObject.IsTilled)
            {
                tileRenderer.material = highlightMaterial;
            }
        }

        tempTile = selectedTile;
    }

    public void TriggerTileHitFX()
    {
        var tileObject = objectPositionList.Where(o => o.X == selectedTile.transform.position.x && o.Z == selectedTile.transform.position.z).First();
        var tileRenderer = selectedTile.transform.GetComponent<Renderer>();
        var TileHitFX = tileHitFXObj.GetComponent<ParticleSystem>();
        var poofFX = poofFXObj.GetComponent<ParticleSystem>();
        tileRenderer.material = tilledMaterial;
        tileObject.IsTilled = true;
        tileHitFXObj.SetActive(true);
        poofFXObj.SetActive(true);
        TileHitFX.Play();
        poofFX.Play();
    }

    private Vector3 estimatePosition(Vector3 actualPosition) 
    {
        float estX = (float)Math.Round(actualPosition.x);
        float estZ = (float)Math.Round(actualPosition.z);
        return new Vector3(estX, 0, estZ);
    }
}
