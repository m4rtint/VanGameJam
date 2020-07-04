﻿using System;
using Lean.Touch;
using UnityEngine;

public class FoodBehaviour : MonoBehaviour
{
    private Rigidbody2D _foodRigidBody = null;
    private LeanSelectable _foodLeanSelectable = null;
    private LeanDragTranslate _foodLeanDragTranslate = null;

    private LeanDragTranslate FoodLeanDragTranslate
    {
        get
        {
            if (_foodLeanDragTranslate == null)
            {
                _foodLeanDragTranslate = GetComponent<LeanDragTranslate>();
            }

            return _foodLeanDragTranslate;
        }
    }

    private LeanSelectable FoodLeanSelectable
    {
        get
        {
            if (_foodLeanSelectable == null)
            {
                _foodLeanSelectable = GetComponent<LeanSelectable>();
            }

            return _foodLeanSelectable;
        } 
    }

    private Rigidbody2D FoodRigidBody
    {
        get
        {
            if (_foodRigidBody == null)
            {
                _foodRigidBody = GetComponent<Rigidbody2D>();
            }

            return _foodRigidBody;
        }
    }

    private void OnEnable()
    {   
        FoodLeanSelectable.OnSelectUp.RemoveAllListeners();
        FoodLeanSelectable.OnSelectUp.AddListener(Released);
        SetFoodState(true);
        FoodLeanDragTranslate.enabled = true;
    }

    private void OnDisable()
    {
        FoodLeanSelectable.OnSelectUp.RemoveAllListeners();
    }

    private void Released(LeanFinger finger)
    {
        SetFoodState(false);
        FoodLeanDragTranslate.enabled = false;
    }

    private void SetFoodState(bool isHeld)
    {
        if (isHeld)
        {
            FoodRigidBody.bodyType = RigidbodyType2D.Kinematic;
        }
        else
        {
            FoodRigidBody.bodyType = RigidbodyType2D.Dynamic;
        }
    }
}
