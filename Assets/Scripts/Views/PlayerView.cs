using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// Unity script for player object.
/// </summary>
public class PlayerView : MonoBehaviour, IView
{
    private Rigidbody2D rbody;
    private Animator anim;

    private float inputX, inputY;

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
                rbody.MovePosition(rbody.position + movementVector * 50 * Time.deltaTime);
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

    public BaseController Controller { get; private set; }

    public object[] GetViewData(string viewDataPath)
    {
        throw new NotImplementedException();
    }

    public void Notify(string eventPath, object source, params object[] data)
    {
        switch (eventPath)
        {
            case ControllerNotification.GameObjectPositionChanged:
                var x = (float)data[0];
                var y = (float)data[1];
                var z = (float)data[2];
                transform.position = new Vector3(x, y, z);
                break;
            case ControllerNotification.GameObjectMovementVectorChanged:
                inputX = (float)data[0];
                inputY = (float)data[1];        
                break;
            case ControllerNotification.GameObjectMovementHalted:
                inputX = 0f;
                inputY = 0f;
                break;
            default:
                break;
        }
    }

    public void SetController(BaseController controller)
    {
        if (Controller != null)
            throw new ApplicationException("Controller has been already initialized.");

        Controller = controller;
    }

    public void SyncState()
    {
        throw new NotImplementedException();
    }
}
