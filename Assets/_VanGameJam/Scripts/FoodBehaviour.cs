using System;
using DG.Tweening;
using Lean.Touch;
using UnityEngine;

public class FoodBehaviour : MonoBehaviour
{
    private const int NumberOfTimesToCheckZeroMovement = 3;
    private const float RemoveAnimationDuration = 1.0f;
    private const float AnimationTime = 0.5f;
    
    private Rigidbody2D _foodRigidBody = null;
    private LeanSelectable _foodLeanSelectable = null;
    private LeanDragTranslate _foodLeanDragTranslate = null;

    private bool _needToCheckMovement = true;
    private int _numberOfTimesCheckedZeroMovement = 0;
    
    public event Action OnFoodStoppedMoving;

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

    public void OnRemove()
    {
        FoodRigidBody.bodyType = RigidbodyType2D.Kinematic;
        transform.DOScale(Vector3.zero, RemoveAnimationDuration).SetEase(Ease.InBack).OnComplete(() =>
        {
            Destroy(gameObject);
        });
    }

    private void OnEnable()
    {   
        FoodLeanSelectable.OnSelectUp.RemoveAllListeners();
        FoodLeanSelectable.OnSelectUp.AddListener(Released);
        SetFoodState(true);
        _numberOfTimesCheckedZeroMovement = 0;
        AnimateStart();
    }

    private void AnimateStart()
    {
        transform.localScale = Vector3.zero;
        transform.DOScale(Vector3.one, AnimationTime).SetEase(Ease.OutQuart).SetDelay(AnimationTime);
    }

    private void OnDisable()
    {
        FoodLeanSelectable.OnSelectUp.RemoveAllListeners();
        Destroy(gameObject);
    }

    private void Released(LeanFinger finger)
    {
        SetFoodState(false);
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
        
        FoodLeanDragTranslate.enabled = isHeld;
    }

    private void FixedUpdate()
    {
        if (HasFoodStoppedMoving() && _needToCheckMovement)
        {
            _numberOfTimesCheckedZeroMovement++;
            if (_numberOfTimesCheckedZeroMovement < NumberOfTimesToCheckZeroMovement)
            {
                return;
            }
            
            _needToCheckMovement = false;
            if (OnFoodStoppedMoving != null)
            {
                OnFoodStoppedMoving();
            }
        }
    }

    private bool HasFoodStoppedMoving()
    {
        return FoodRigidBody.bodyType == RigidbodyType2D.Dynamic && 
               FoodRigidBody.velocity == Vector2.zero &&
               Math.Abs(FoodRigidBody.angularVelocity) < 0.05;
    }
}
