using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct BuildingPlan
{
    TradeGoods tradeGoods;
    BuildingSlot slot;
    LandscapeCode landscape; // реализовать больще - меньше. 
}


public enum BuildingTypes
{
    ExploitWood,
    ExploitMetal,
    ExploitStone,
    ExploitFish,
    ExploitAnimals,
    ExploitSoil,
    FactoryMountain,
    FactoryJungle,
    FactoryOcean,
    FactoryDesert,
    InfraColonyCenter,
    InfraPowerSolar,
    InfraPowerWind,
    InfraPowerThermal,
    InfraPowerBurn,
    CivilHouse,
    CivilTower,
    CivilCosmodrome,
    CivilArena,
    CivilTavern,
    CivilHotel,
    CivilCasino,
    CivilLibrary,
    NativeRuinMountain,
    NativeRuinJungle,
    NativeRuinOcean,
    NativeRuinDesert
}

public class Buildings // обычный класс, каждое здание - инстанс класса. 
{
    protected Tile tile; // Where is builded
    protected SpriteRenderer sprite; // sprite for render
    protected BuildingPlan buildingPlan;
    // can Enter the building& OnEnter()

    protected Dictionary<BuildingTypes, float> powerUsage;
    public virtual Dictionary<BuildingTypes, float> PowerUsage
    {
        get
        {
            return powerUsage;
        }
        set
        {
            powerUsage = value;
        }
    }
}


public class CivilBuildings : Buildings
{
    public override Dictionary<BuildingTypes, float> PowerUsage { get; set; }
    // Check type only Civil;
}
public class NativeBuildings : Buildings
{
    public override Dictionary<BuildingTypes, float> PowerUsage { get; set; }
    // Check type only Civil;
}

