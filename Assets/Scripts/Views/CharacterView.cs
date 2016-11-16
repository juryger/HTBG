using System;
using UnityEngine;

/// <summary>
/// Unity script for Character View object in context of MVVM pattern.
/// </summary>
public class CharacterView : MonoBehaviour, IView
{
    private bool isPositionUpdated = false;
    protected const int SPEED_WALK = 50;
    protected const int SPEED_RUN = 70;

    protected Rigidbody2D rbody;
    protected Animator anim;

    protected float inputX, inputY;
    protected int speed;

    /// <summary>
    /// Flags shows that position of character is changed.
    /// </summary>
    protected bool IsPositionUpdated
    {
        get
        {
            var test = isPositionUpdated;

            isPositionUpdated = false;

            return test;
        }
        set
        {
            isPositionUpdated = value;
        }
    }
    // ViewModel object in context of MVVM pattern
    public BaseViewModel ViewModel { get; private set; }

    // Use this for initialization
    public virtual void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    public virtual void Update()
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
                IsPositionUpdated = true;
            }

            anim.SetFloat("input_x", movementVector.x);
            anim.SetFloat("input_y", movementVector.y);
        }
        else
        {
            anim.SetBool("iswalking", false);
        }
    }

    public virtual void Notify(string eventPath, object source, params object[] data)
    {
        switch (eventPath)
        {
            case NotificationName.RemoveCharacterFromScene:
                if (rbody != null)
                    DestroyObject(gameObject);
                break;
            case NotificationName.CharacterPositionChanged:
                var x = (float)data[0];
                var y = (float)data[1];
                var z = (float)data[2];
                transform.position = new Vector3(x, y, z);

                IsPositionUpdated = true;
                break;
            case NotificationName.CharacterMovementVectorChanged:
                inputX = (float)data[0];
                inputY = (float)data[1];

                var isRun = (bool)data[2];
                speed = isRun ? SPEED_RUN : SPEED_WALK;

                break;
            case NotificationName.CharacterMovementHaulted:
                inputX = 0f;
                inputY = 0f;

                IsPositionUpdated = true;
                break;
            case NotificationName.CharacterStatisticsChanged:
                OnCharacterStatisticChanged(data[0] as CharacterStatisticsModel);
                break;
            case NotificationName.RequestCharacterPosition:
                ViewModel.Notify(NotificationName.ResponseCharacterPosition, this, rbody.position.x, rbody.position.y);
                break;
            default:
                break;
        }
    }

    public void SetViewModel(BaseViewModel viewModel)
    {
        if (ViewModel != null)
            throw new ApplicationException("ViewModel has been already initialized.");

        ViewModel = viewModel;
    }

    public virtual void OnCharacterStatisticChanged(CharacterStatisticsModel statistics)
    {
    }

    public virtual void Dispose()
    {
        ViewModel = null;
    }
}
