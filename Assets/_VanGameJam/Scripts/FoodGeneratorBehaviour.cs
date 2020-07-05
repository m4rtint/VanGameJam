using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using Random = UnityEngine.Random;

public class FoodGeneratorBehaviour : MonoBehaviour
{
    [SerializeField] private ListOfFood _containerOfAllFood = null;
    [SerializeField] private List<ContainerBehaviour> _listOfContainers = null;
    private FoodGenerator _generator = null;

    public event Action<float> OnFoodWeightCounted;
    
    public void Initialize()
    {
        _generator = new FoodGenerator(_containerOfAllFood);
        FillContainers();
        InitializeContainers();
        AddDelegate();
    }

    public void Reset()
    {
        _generator = null;
        RemoveDelegate();
        ResetContainers();
    }

    private void AddDelegate()
    {
        foreach (var container in _listOfContainers)
        {
            container.OnFoodStoppedMoving += FillContainersIfNeeded;
            container.OnFoodStoppedMoving += AddWeightToScale;
        }
    }

    private void RemoveDelegate()
    {
        foreach (var container in _listOfContainers)
        {
            container.OnFoodStoppedMoving -= FillContainersIfNeeded;
            container.OnFoodStoppedMoving -= AddWeightToScale;
        }
    }

    private void InitializeContainers()
    {
        foreach (var container in _listOfContainers)
        {
            container.Initialize();
        }
    }

    private void ResetContainers()
    {
        foreach (var container in _listOfContainers)
        {
            container.Reset();
        }
    }

    private void FillContainersIfNeeded(float weight)
    {
        var amountEmpty = 0;
        foreach (var container in _listOfContainers)
        {
            if (container.IsFoodEmpty())
            {
                amountEmpty++;
            }
        }
        
        if (amountEmpty == _listOfContainers.Count)
        {
            FillContainers();
        }
    }

    private void AddWeightToScale(float weight)
    {
        if (OnFoodWeightCounted != null)
        {
            OnFoodWeightCounted(weight);
        }
    }

    private void FillContainers()
    {
        if (_listOfContainers != null)
        {
            foreach (var container in _listOfContainers)
            {
                container.SetFood(_generator.NextFoodItem());
            }
        }
    }

    private void OnDisable()
    {
        _generator = null;
    }

    public void LoseGame()
    {
        _generator = null;
        foreach (var container in _listOfContainers)
        {
            container.OnGameLost();
        }
    }
}

public class FoodGenerator
{
    private const int NumberOfFoodToGenerator = 50;
    [CanBeNull] private ListOfFood _allFood = null;
    private Queue<Food> queueOfFood = new Queue<Food>();

    public FoodGenerator(ListOfFood container)
    {
        _allFood = container;
        FillInQueueOfFood();
    }

    public Food NextFoodItem()
    {
        return queueOfFood.Dequeue();
    }

    private void FillInQueueOfFood()
    {
        if (_allFood != null)
        {
            for (int i = 0; i < NumberOfFoodToGenerator; i++)
            {
                queueOfFood.Enqueue(GetRandomFoodItem());
            }
        }
    }

    private Food GetRandomFoodItem()
    {
        var numberOfFoods = _allFood.FoodContainer.Count;
        var index = Random.Range(0, numberOfFoods);
        return _allFood.FoodContainer[index];
    }
    
}
