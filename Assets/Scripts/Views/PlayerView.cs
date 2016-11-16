using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Unity script for Player object in context of MVVM pattern.
/// </summary>
public class PlayerView : CharacterView
{
    // flag which shows that current player is multipayer (not main player who manage game instance).
    public bool IsMultiplayer = false;

    /// <summary>
    /// Touch screen movement.
    /// </summary>
    private enum Movement
    {
        None = 0,
        Left = 1,
        Right = 2,
        Up = 3,
        Down = 4
    };

    // Vectors for start and end touch movement
    private Vector2 fingerStart;
    private Vector2 fingerEnd;

    // Last detected touch movement
    private Movement lastTouchMovement = Movement.None;

    public override void Start()
    {
        base.Start();

        if (IsMultiplayer)
        {
            var camera = gameObject.GetComponentInChildren<Camera>();
            camera.gameObject.SetActive(false);

            var audio = camera.GetComponent<AudioListener>();
            audio.gameObject.SetActive(false);
        }
        else
        {
            IsPositionUpdated = true;
        }
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();

        if (!IsMultiplayer && IsPositionUpdated)
        {
            SendPlayerPositionToGameServer();
        }

        DetectTouchMovement();
    }

    public override void Notify(string eventPath, object source, params object[] data)
    {
        base.Notify(eventPath, source, data);
    }

    public override void OnCharacterStatisticChanged(CharacterStatisticsModel statistics)
    {
        if (IsMultiplayer)
            return;

        var healthObject = GameObject.FindGameObjectWithTag(UnityObjectTagName.HealthBar);
        var staminaObject = GameObject.FindGameObjectWithTag(UnityObjectTagName.StaminaBar);

        if (healthObject != null)
            SetSliderValue(healthObject, statistics.CurrentHealth);

        if (staminaObject)
            SetSliderValue(staminaObject, statistics.CurrentStamina);
    }

    private void SetSliderValue(GameObject gameObject, int value)
    {
        var slider = gameObject.GetComponent<Slider>();
        if (slider != null)
            slider.value = value;
    }

    /// <summary>
    /// Send current position of Player to the server.
    /// </summary>
    private void SendPlayerPositionToGameServer()
    {
        // rbody could be null if view just created and has not been intialize
        if (rbody == null)
            return;

        // Send player info to Game server
        var gameManager = GameStateManager.Instance;
        var characterDto = gameManager.Player.ConvertToDTO<CharacterDTO>();

        // Upadte current position with actual data 
        characterDto.LocationX = rbody.position.x;
        characterDto.LocationY = rbody.position.y;
        characterDto.TimeStamp = DateTime.UtcNow.ToString();

        StartCoroutine(gameManager.GameServer.SyncPlayerState(characterDto));

        print("Send player state to Game server...");
    }

    /// <summary>
    /// Detect peformed touch screen gestures.
    /// base on http://stackoverflow.com/questions/27712233/swipe-gestures-on-android-in-unity#answer-27718966
    /// </summary>
    private void DetectTouchMovement()
    {
        foreach (Touch touch in Input.touches)
        {

            if (touch.phase == TouchPhase.Began)
            {
                fingerStart = touch.position;
                fingerEnd = touch.position;

                Debug.Log(string.Format("Begin of touch movement x:{0}, y:{1}",
                    fingerStart.x, fingerStart.y));
            }

            if (touch.phase == TouchPhase.Moved)
            {
                fingerEnd = touch.position;

                Debug.Log(string.Format("On touch movement x:{0}, y:{1}",
                    fingerEnd.x, fingerEnd.y));

                //There is more movement on the X axis than the Y axis
                if (Mathf.Abs(fingerStart.x - fingerEnd.x) > Mathf.Abs(fingerStart.y - fingerEnd.y))
                {

                    //Right Swipe
                    if ((fingerEnd.x - fingerStart.x) > 0)
                        lastTouchMovement = Movement.Right;
                    //Left Swipe
                    else
                        lastTouchMovement = Movement.Left;

                }

                //More movement along the Y axis than the X axis
                else
                {
                    //Upward Swipe
                    if ((fingerEnd.y - fingerStart.y) > 0)
                        lastTouchMovement = Movement.Up;
                    //Downward Swipe
                    else
                        lastTouchMovement = Movement.Down;
                }
                //After the checks are performed, set the fingerStart & fingerEnd to be the same
                fingerStart = touch.position;

                //Now let's check if the Movement pattern is what we want
                SetMovementVector();
            }


            if (touch.phase == TouchPhase.Ended)
            {
                Debug.Log("End of touch movement");

                fingerStart = Vector2.zero;
                fingerEnd = Vector2.zero;
                lastTouchMovement = Movement.None;

                // reset movement vector
                SetMovementVector();

                Notify(NotificationName.CharacterMovementHaulted, this);
            }
        }
    }

    private void SetMovementVector()
    {
        speed = SPEED_WALK;
        inputX = inputY = 0f;

        Debug.Log(string.Format("Set movement vector base on Touch screen detection: {0}",
            lastTouchMovement));

        if (lastTouchMovement == Movement.None)
            return;

        if (lastTouchMovement == Movement.Left)
            inputX = -1f;
        else if (lastTouchMovement == Movement.Right)
            inputX = 1f;
        else if (lastTouchMovement == Movement.Up)
            inputY = 1f;
        else if (lastTouchMovement == Movement.Down)
            inputY = -1f;
    }
}
