using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public int moveSpeed = 30;
    [HideInInspector] public Vector2 screenBounds;

    private void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));
    }

    private void Update()
    {
        Move();
    }

    public void Move()
    {
#if UNITY_EDITOR
        if(Input.GetMouseButton(0))
        {
            Vector3 destinationPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            destinationPos.z = transform.position.z;

            destinationPos.x = Mathf.Clamp(destinationPos.x, -screenBounds.x, screenBounds.x);
            destinationPos.y = Mathf.Clamp(destinationPos.y, -screenBounds.y, screenBounds.y);
            transform.position = Vector3.MoveTowards(transform.position, destinationPos, moveSpeed * Time.deltaTime);
        }

#endif
#if UNITY_ANDROID || UNITY_IOS
        if(Input.touchCount > 0)
        {
            Touch touchPos = Input.touches[0];
            Vector3 destinationPos = Camera.main.ScreenToWorldPoint(touchPos.position);
            destinationPos.z = transform.position.z;

            destinationPos.x = Mathf.Clamp(destinationPos.x, -screenBounds.x, screenBounds.x);
            destinationPos.y = Mathf.Clamp(destinationPos.y, -screenBounds.y, screenBounds.y);
            transform.position = Vector3.MoveTowards(transform.position, destinationPos, moveSpeed * Time.deltaTime);
        }
#endif
    }
}
