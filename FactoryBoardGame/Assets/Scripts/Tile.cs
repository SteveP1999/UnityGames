using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private string tileName;
    [SerializeField] private Color highLightColor = Color.cyan;
    
    public void setTileName(string name)
    {
        tileName = name;
    }

    public string getTileName()
    {
        return tileName;
    }

    public void OnMouseEnter()
    {
        
    }

    public void OnMouseExit()
    {
        
    }
}
