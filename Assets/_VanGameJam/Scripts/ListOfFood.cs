using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "List Of Food", menuName = "MPHT/ListOfFood", order = 1)]
public class ListOfFood : ScriptableObject
{
    [SerializeField] private List<Food> _foodContainer = null;

    public List<Food> FoodContainer => _foodContainer ?? (_foodContainer = new List<Food>());
}

[Serializable]
public struct Food
{
    public Sprite FoodImage;
    public GameObject FoodObject;
}
