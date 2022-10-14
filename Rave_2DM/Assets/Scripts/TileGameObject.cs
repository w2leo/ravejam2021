using UnityEngine;
using System;
using UnityEngine.UI;
using System.Collections.Generic;

public enum BuildingSlot 
{
    E, S, M, L, XL, XXL
}

[Serializable]
public class TileGameObject : MonoBehaviour
{
    [SerializeField] public Tile tile;
    private SpriteRenderer spriteRenderer;
    [SerializeField] public Transform LandscapeModificator;
    [SerializeField] public Transform Resources;
    [SerializeField] public Transform Building;

    public Transform[] coastPositions;

    private bool isCopy;

    private SpriteRenderer srCoast;
    public bool GetTileSprite()
    {
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();
        if (tile.tileSprite != null)
            return true;
        return false;
    }

    public void SetTileInfo(Tile tile, bool isCopy)
    {
        this.tile = tile;
        this.isCopy = isCopy;
    }

    public void PrintData()
    {
        Debug.Log($"R = {tile.R} / G = {tile.G} / B = {tile.B}");
    }

    public void SetSpriteToTile()
    {
        spriteRenderer.sprite = tile.tileSprite;
        LandscapeModificator.GetComponent<SpriteRenderer>().sprite = tile.landscapeModificator;   
    }

    public void DrawTile(bool r, bool g, bool b)
    {
        int maxHeight = 8;
        float R, G, B;
        R = G = B = 0;
        if (r) R = (float)tile.R / (float)maxHeight;
        if (g) G = (float)tile.G / (float)maxHeight;
        if (b) B = (float)tile.B / (float)maxHeight;
        spriteRenderer.color = new Color(R, G, B);
    }

    public void SetCoast(int position, Sprite sprite)
    {
        srCoast = coastPositions[position].gameObject.AddComponent<SpriteRenderer>();
        srCoast.sortingOrder = 10;
        srCoast.sprite = sprite;
    }
}


