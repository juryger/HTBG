using UnityEngine;

//[RequireComponent(typeof(Camera))]
public class CameraFollow : MonoBehaviour
{
    public Transform target;
    //private Camera myCamera;

    // Use this for initialization
    void Start()
    {
        //myCamera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        //myCamera.orthographicSize = (Screen.height / 100f) / 4f;

        if (target)
        {
            transform.position = Vector3.Lerp(transform.position, target.position, 0.1f) + new Vector3(0, 0, -0.9f);
        }
    }
}
