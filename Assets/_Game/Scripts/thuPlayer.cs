using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class thuPlayer : MonoBehaviour
{
    public enum Direct { Forward, Back, Right, Left, None};
    public Direct direct;
    private Vector3 mousePosDown;
    private Vector3 mousePosUp;
    public Vector3 direction;
    void Start()
    {
        
    }

    
    void Update()
    {
        //neu nhan xuong thi se ghi lai toa do diem nhan
        if (Input.GetMouseButtonDown(0))
        {
            mousePosDown = Input.mousePosition;
            Debug.Log("start" + mousePosDown);
        }
        //neu nhac len thi se ghi lai toa do diem nhac
        if (Input.GetMouseButtonUp(0))
        {
            mousePosUp = Input.mousePosition;
            Debug.Log("end" + mousePosUp);
            direction = new Vector3(mousePosUp.x - mousePosDown.x, mousePosUp.y - mousePosDown.y, mousePosUp.z - mousePosDown.z);
            Control(direction);
            Debug.Log(direct);
        }
        
    }

    public Direct Control(Vector3 direction)
    {

        if (Mathf.Abs(direction.x) < direction.y)
        {
            direct = Direct.Forward;
        }
        else if (direction.x > direction.y && direction.x > 0)
        {
            direct = Direct.Right;
        }
        else if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y) && direction.x < 0)
        {
            direct = Direct.Left;  
        }
        else if (Mathf.Abs(direction.x) < Mathf.Abs(direction.y) && direction.y < 0)
        {
            direct = Direct.Back;
        }
        else
        {
            direct = Direct.None;
        }
        return direct;
    }

}
