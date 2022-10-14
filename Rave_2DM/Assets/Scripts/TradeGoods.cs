using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TradeGoodsTypes
{
    Null = 0,
    GlobalMoney,
    ConvictIdle,
    ConvictEX,
    ConvictLimit,
    NanoFuel,
    NanoAlloy,
    NanoChip,
    NanoFood,
    NanoBots,
    MacroFuel,
    MacroAlloy,
    MacroChip,
    MacroFood,
    MacroPower,
    MicroFuel,
    MicroAlloy,
    MicroChip,
    MicroFood,
    MicroVirus    
}
public class TradeGoods
{
   
    private static Dictionary<TradeGoodsTypes, int> basePrice;
    private static double minSaleRatio = 0.25d;
    private static double maxSaleRatio = 4.0d;
   // private static double saleRatio, buyRatio; // в класс транспортного корабля у него свои коэффициенты етип double

    public TradeGoods()
    {
        int max = System.Enum.GetNames(typeof(TradeGoodsTypes)).Length;
       
        AddBasePrices();
        if (basePrice.Count != max)
        {
            basePrice.Clear();
            throw new System.Exception("Wrong Input data for TradeGoods Collections");
        }
    }

    private void AddBasePrices()
    {
        basePrice.Add(TradeGoodsTypes.ConvictIdle, 60000);
        basePrice.Add(TradeGoodsTypes.ConvictEX, 60000);
        basePrice.Add(TradeGoodsTypes.ConvictLimit, 5000);

        basePrice.Add(TradeGoodsTypes.NanoFuel, 100);  
        basePrice.Add(TradeGoodsTypes.NanoAlloy, 200);
        basePrice.Add(TradeGoodsTypes.NanoChip, 300);
        basePrice.Add(TradeGoodsTypes.NanoFood, 1000);
        basePrice.Add(TradeGoodsTypes.NanoBots, 1700);

        basePrice.Add(TradeGoodsTypes.MacroFuel, 100);
        basePrice.Add(TradeGoodsTypes.MacroAlloy, 200);
        basePrice.Add(TradeGoodsTypes.MacroChip, 300);
        basePrice.Add(TradeGoodsTypes.MacroFood, 1000);
        basePrice.Add(TradeGoodsTypes.MacroPower, 1700);

        basePrice.Add(TradeGoodsTypes.MicroFuel, 100);  
        basePrice.Add(TradeGoodsTypes.MicroAlloy, 200);
        basePrice.Add(TradeGoodsTypes.MicroChip, 300);
        basePrice.Add(TradeGoodsTypes.MicroFood, 1000);
        basePrice.Add(TradeGoodsTypes.MicroVirus, 1700);
    }
}

