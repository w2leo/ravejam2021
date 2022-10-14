using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Tile
{
    [SerializeField] private LandscapeCode landCode;
    [SerializeField] private int x, y;

    public Sprite tileSprite;
    public Sprite landscapeModificator;
    public Sprite resourceSprite;

    /*
    [SerializeField] private BuildingSlot buildingSlot;
    private Buildings BuiltGameObject = null;
    private bool canBuild;
    private TradeGoodsTypes good = TradeGoodsTypes.Null;
    [Range(0, 1)]
    private float remainingResourses = 1;
    */

    public int X => x;
    public int Y => y;
    public LandscapeCode Height => landCode;
    public HeightLevel R => landCode.R;
    public TemperatureLevel G => landCode.G;
    public HumidityLevel B => landCode.B;

    public Tile(int x, int y)
    {
        this.x = x;
        this.y = y;
        landCode = new LandscapeCode(-1, 0, 0); // Add Undefined to All fields
    }

    public Tile(int _x, int _y, LandscapeCode _h)
    {
        x = _x;
        y = _y;
        landCode = _h;
    }

    public void SetLandscape(int heightR, int heightG, int heightB)
    {
        landCode = new LandscapeCode(heightR, heightG, heightB);
    }

    public void SetLandscape(LandscapeCode landCode)
    {
        this.landCode = landCode;
    }

    public void SetHeight(HeightLevel height)
    {
        this.landCode.R = height;
    }

    public void AddLandscape(LandscapeCode addValue)
    {
        landCode += addValue;
    }



}
