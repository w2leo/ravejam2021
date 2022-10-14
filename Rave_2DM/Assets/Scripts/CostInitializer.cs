using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum TilePosition
{
    leftPosition, 
    rightPosition,
    upPosition, 
    downPosition,
    leftDownPosition, 
    leftUpPosition,
    rightDownPosition, 
    rightUpPosition
}
public class CoastInitializer : MonoBehaviour
{
    public Dictionary<TilePosition, Transform> coastPositions;


    public void AddDictionaryValues()
    {
        var values = TilePosition.GetValues(typeof(TilePosition));
        coastPositions.Clear();

        foreach (TilePosition item in values)
        {
            coastPositions.Add(item, transform.Find(item.ToString()));
        }              
    }

    public void SetSpriteSoPosition(Sprite sprite, TilePosition position)
    {
        if (coastPositions.ContainsKey(position))
        {
            Transform pos = coastPositions[position];
            SpriteRenderer sr;

            if (!pos.TryGetComponent<SpriteRenderer>(out sr))
                sr = transform.gameObject.AddComponent<SpriteRenderer>();

            sr.sprite = sprite;
            sr.sortingOrder = 10;
        }
    }
}
