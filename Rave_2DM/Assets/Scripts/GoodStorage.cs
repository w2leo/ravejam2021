using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodStorage : TradeGoods
{
    private static Dictionary<TradeGoodsTypes, double> goodsList;

    public GoodStorage()
    {
        int max = System.Enum.GetNames(typeof(TradeGoodsTypes)).Length;
        for (int i = 0; i < max; i++)
        {
            goodsList.Add((TradeGoodsTypes)i, 0);
        }
        if (goodsList.Count != max)
        {
            goodsList.Clear();
            throw new System.Exception("Wrong Input data for TradeGoods Collections TradeStorage()");
        }
    }

    public double GetGoodValue(TradeGoodsTypes type)
    {
        return goodsList[type];
    }

    private void ChangeGoodValue(TradeGoodsTypes type, double value)
    {
        goodsList[type] += value;
    }

    public void IncomeGoodValue(TradeGoodsTypes type, double addValue)
    {
        if (addValue > 0)
        {
            ChangeGoodValue(type, addValue);
            if (type == TradeGoodsTypes.ConvictIdle && goodsList[type] > goodsList[TradeGoodsTypes.ConvictLimit])
            {
                goodsList[type] = goodsList[TradeGoodsTypes.ConvictLimit];
            }
        }
    }

    public bool SpendGoodValue(TradeGoodsTypes type, double cost)
    {
        double curValue = goodsList[type];
        if (curValue >= cost)
        {
            ChangeGoodValue(type, (-1.0d) * cost);
            return true;
        }
        else
        {
            Debug.Log($"Not enougth {type} ");
            return false;
        }
    }
}
