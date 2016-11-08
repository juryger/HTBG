﻿using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Unity script for player object.
/// </summary>
public class PlayerView : MonoBehaviour, IView
{
    private const int SPEED_WALK = 50;
    private const int SPEED_RUN = 70;

    private Rigidbody2D rbody;
    private Animator anim;

    private float inputX, inputY;
    private int speed;

    // Use this for initialization
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 movementVector = new Vector2(inputX, inputY);
        if (movementVector != Vector2.zero)
        {
            if (Math.Abs(movementVector.x) != 0.1f && Math.Abs(movementVector.y) != 0.1f)
            {
                anim.SetBool("iswalking", true);
                rbody.MovePosition(rbody.position + movementVector * speed * Time.deltaTime);
            }
            else
            {
                anim.SetBool("iswalking", false);
            }

            anim.SetFloat("input_x", movementVector.x);
            anim.SetFloat("input_y", movementVector.y);
        }
        else
        {
            anim.SetBool("iswalking", false);
        }
    }

    public BaseViewModel ViewModel { get; private set; }

    public void Notify(string eventPath, object source, params object[] data)
    {
        switch (eventPath)
        {
            case NotificationName.PlayerPositionChanged:
                var x = (float)data[0];
                var y = (float)data[1];
                var z = (float)data[2];
                transform.position = new Vector3(x, y, z);
                break;
            case NotificationName.PlayerMovementVectorChanged:
                inputX = (float)data[0];
                inputY = (float)data[1];

                var isRun = (bool)data[2];
                speed = isRun ? SPEED_RUN : SPEED_WALK;

                break;
            case NotificationName.PlayerMovementHaulted:
                inputX = 0f;
                inputY = 0f;
                break;
            case NotificationName.PlayerStatisticsChanged:
                var healthObject = GameObject.FindGameObjectWithTag(UnityObjectTagName.HealthBar);
                var staminaObject = GameObject.FindGameObjectWithTag(UnityObjectTagName.StaminaBar);

                var statistics = data[0] as CharacterStatisticsModel;

                if (healthObject != null)
                    SetSliderValue(healthObject, statistics.CurrentHealth);

                if (staminaObject)
                    SetSliderValue(staminaObject, statistics.CurrentStamina);
                break;
            case NotificationName.RequestPlayerPosition:
                ViewModel.Notify(NotificationName.ResponsePlayerPosition, this, rbody.position.x, rbody.position.y);
                break;
            default:
                break;
        }
    }

    private void SetSliderValue(GameObject gameObject, int value)
    {
        var slider = gameObject.GetComponent<Slider>();
        if (slider != null)
            slider.value = value;
    }

    public void SetViewModel(BaseViewModel viewModel)
    {
        if (ViewModel != null)
            throw new ApplicationException("ViewModel has been already initialized.");

        ViewModel = viewModel;
    }

    public void Dispose()
    {
        ViewModel = null;
    }
}
