using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//code gotten from https://thiscodedoesthis.com/moving-platform-unity/

public class PlatformMoving : MonoBehaviour
{
    [SerializeField]
    private Transform position1, position2;
    private float _speed = 5.0f;
    private bool _switch = false;

    private void FixedUpdate()
    {
       if(_switch == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, position1.position, _speed * Time.deltaTime);
        }
       else if(_switch == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, position2.position, _speed * Time.deltaTime);
        }

       if(transform.position == position1.position)
        {
            _switch = true;
        }
       else if(transform.position == position2.position)
        {
            _switch = false;
        }
    }
}
