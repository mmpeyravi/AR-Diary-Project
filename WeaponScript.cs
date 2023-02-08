using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponScript : MonoBehaviour
{
    private Vector2 touchPosition;
    private Vector2 delta;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        delta=touchPosition-Touchscreen.current.primaryTouch.position.ReadValue();
        touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();
        Debug.Log(delta);
        transform.eulerAngles += new Vector3(0.01f*delta.x, 0.01f*delta.y, 0);
        //Vector3 worldPosition = Camera.main.ScreenToWorldPoint(touchPosition);

       // transform.position = worldPosition;
        //Debug.Log(Camera.main.gameObject.name+":"+worldPosition);
    }
}
