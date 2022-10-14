using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;


public class MapCreator : MonoBehaviour
{
    [SerializeField] private  int sizeX, sizeY;
    [SerializeField] private int mapCreatorSeed;
    [SerializeField] private Transform spritePrefab;

    public Sprite mainDotSprite;
    // Sprites Info
    [SerializeField] private Sprite[] groundSprites; // land, ocean, sea
    Dictionary<HeightLevel, Sprite> groundTiles;
    
    [SerializeField] private Sprite[] landTempSprites; //
    Dictionary<TemperatureLevel, Sprite> landTempTiles;

    [SerializeField] private Sprite[] coastListSprites; //

    // End Sprites Info

    [SerializeField] private Map map;
    private Transform mapCenter, mapLeft, mapRight;
    private readonly string mapName = "MapTiles";

    [SerializeField] private float orthogonalRatio = 1.65f;
    [SerializeField] private float diagonalRatio = 2.5f;
    [SerializeField] private int rareHumidityConst = 1;
    [SerializeField] private int [] R2R4R6Ratio;

    [SerializeField] private int mainTileRatio;

    [SerializeField] private AnimationCurve tempCurve;
    private (bool, bool, bool) RGB = (true, true, true);
    private bool rgbChanged = false;

    public Text R2text, R4text, R6text, MTtext, SeedText;

    private void Start()
    {
        R2R4R6Ratio = new int[3] { 1, 1, 1 };
        // Add sprite values to Dictionary (refactor later)
        groundTiles = new Dictionary<HeightLevel, Sprite>();
        landTempTiles = new Dictionary<TemperatureLevel, Sprite>();
        
        groundTiles.Add(HeightLevel.R0_DEEP_OCEAN, groundSprites[0]);
        groundTiles.Add(HeightLevel.R2_OCEAN, groundSprites[1]);
        groundTiles.Add(HeightLevel.R3_COAST, groundSprites[2]);
        groundTiles.Add(HeightLevel.R4_PLAIN, groundSprites[3]);
        groundTiles.Add(HeightLevel.R5_HILLS, groundSprites[4]);
        groundTiles.Add(HeightLevel.R6_MOUNTAINS, groundSprites[5]);
        groundTiles.Add(HeightLevel.R8_EVEREST, groundSprites[6]);

        landTempTiles.Add(TemperatureLevel.G0_DEATH_TEMP, landTempSprites[0]);
        landTempTiles.Add(TemperatureLevel.G2_COLD_LIFE_LOW, landTempSprites[1]);
              
        // End Add sprites;

        CreateMap(sizeX, sizeY, mapCreatorSeed);
       
    }

    private bool CheckSize()
    {
        return sizeX % 2 == 0 && sizeY % 2 != 0;
    }

    private void CreateMap(int x, int y, int seed)
    {
        if (CheckSize())
        {
            System.DateTime time = System.DateTime.Now;
            if (map != null)
                DeleteMap();

            CreateMapTileObject(out mapCenter);
            CreateMapTileObject(out mapLeft);
            CreateMapTileObject(out mapRight);


            map = new Map(x, y, R2R4R6Ratio, seed, tempCurve, orthogonalRatio, diagonalRatio, rareHumidityConst, mainTileRatio);
            
            Debug.Log($"Map generation time = {System.DateTime.Now - time}");
            time = System.DateTime.Now;

            map.mainDotSprite = mainDotSprite;
            map.CreateGameObjects(spritePrefab, mapCenter, isCopy: false);
            map.CreateGameObjects(spritePrefab, mapLeft, isCopy: true);
            map.CreateGameObjects(spritePrefab, mapRight, isCopy: true);

            mapCenter.position = new Vector2(0.5f, 0);
            mapLeft.position = new Vector2(mapCenter.position.x - sizeX, mapCenter.position.y);
            mapRight.position = new Vector2(mapCenter.position.x + sizeX, mapCenter.position.y);

            // delete from here (Deactivated mapCopies)
            //mapLeft.gameObject.SetActive(false);
            //mapRight.gameObject.SetActive(false);
            // to here 

            map.SetSprites(groundTiles);
            map.SetSprites(landTempTiles);


            map.SetSpritesToObjects();

           // map.SetCoasts(coastListSprites);

            CenterMap();
            Debug.Log($"Map create time = {System.DateTime.Now - time}");
        }
        else
            Debug.Log("Wrong sizeX, sizeY");
    }

    private void CreateMapTileObject(out Transform mapTransform)
    {       
        mapTransform = new GameObject().transform;
        mapTransform.SetParent(transform);
        mapTransform.name = mapName;
    }

    private void DeleteMap()
    {
        if (map!=null)
        {
            for (int i = transform.childCount - 1; i >= 0; i--)
                Destroy(transform.GetChild(i).gameObject);
            map = null;
        }
        transform.position = new Vector3(0, 0, 0);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CreateMap(sizeX, sizeY, mapCreatorSeed);
           // mapCreatorSeed++;
            rgbChanged = false;
            RGB.Item1 = RGB.Item2 = RGB.Item3 = true;
        }
        if (Input.GetKeyDown(KeyCode.D))
            DeleteMap();

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            RGB.Item1 = !RGB.Item1;
            rgbChanged = true;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            RGB.Item2 = !RGB.Item2;
            rgbChanged = true;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            RGB.Item3 = !RGB.Item3;
            rgbChanged = true;
        }

        if (rgbChanged)
        {
            map.DrawTilesAll(RGB.Item1, RGB.Item2, RGB.Item3);
            rgbChanged = false;
        }


        ChangeTextOverlay();
    }

    private void ChangeTextOverlay()
    {
        R2text.text = "R2 =" + R2R4R6Ratio[0].ToString();
        R4text.text = "R4 =" + R2R4R6Ratio[1].ToString();
        R6text.text = "R6 =" + R2R4R6Ratio[2].ToString();
        MTtext.text = "MT =" + mainTileRatio.ToString();
        SeedText.text = "Seed =" + mapCreatorSeed.ToString();
    }

    private void CenterMap()
    {
        transform.position =new Vector3(((float)sizeX) / -2.0f, ((float)sizeY) / -2.0f, transform.position.z);
    }


    public void ChangeR2()
    {
        R2R4R6Ratio[0] ++;
    }
    public void ChangeR4()
    {
        R2R4R6Ratio[1]++;
    }
    public void ChangeR6()
    {
        R2R4R6Ratio[2]++;
    }

    public void ChangeMainTileRatio(int addMT)
    {
        mainTileRatio += addMT;
    }

    public void ChangeSeed(int x)
    {
        mapCreatorSeed += x;
    }

    public void GenerateMap()
    {
        CreateMap(sizeX, sizeY, mapCreatorSeed);
        rgbChanged = false;
        RGB.Item1 = RGB.Item2 = RGB.Item3 = true;
    }

    public void ResetMap()
    {
        R2R4R6Ratio[0] = 1;
        R2R4R6Ratio[1] = 1;
        R2R4R6Ratio[2] =1;
        mainTileRatio = 1;
        mapCreatorSeed = 1;
        GenerateMap();
    }
}
