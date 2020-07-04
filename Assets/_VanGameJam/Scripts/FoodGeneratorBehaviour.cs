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

    private int NumberOfContainersToFill = 0;

    public event Action<float> OnFoodWeightCounted;
    
    public void Initialize()
    {
        _generator = new FoodGenerator(_containerOfAllFood);
        FillContainers();
        AddDelegate();
    }

    public void Reset()
    {
        _generator = null;
        NumberOfContainersToFill = 0;
        RemoveDelegate();
        ResetContainers();
    }

    private void AddDelegate()
    {
        foreach (var container in _listOfContainers)
        {
            container.OnFoodStoppedMoving += EmptyOneContainer;
            container.OnFoodStoppedMoving += AddWeightToScale;
        }
    }

    private void RemoveDelegate()
    {
        foreach (var container in _listOfContainers)
        {
            container.OnFoodStoppedMoving -= EmptyOneContainer;
            container.OnFoodStoppedMoving -= AddWeightToScale;
        }
    }

    private void ResetContainers()
    {
        foreach (var container in _listOfContainers)
        {
            container.Reset();
        }
    }

    private void EmptyOneContainer(float weight)
    {
        NumberOfContainersToFill++;
        if (NumberOfContainersToFill == _listOfContainers.Count)
        {
            Debug.Log("Fill In Containers");
            NumberOfContainersToFill = 0;
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
