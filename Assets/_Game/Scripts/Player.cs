using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private LayerMask brickLayer;
    [SerializeField] private float speed = 2f;
    [SerializeField] private float raycastLenght = 1.1f;
    [SerializeField] private GameObject playerAnim;
    [SerializeField] private GameObject brickPrefabs;
    //[SerializeField] private GameObject brickHolders;
    [SerializeField] private Transform brickPoint;
    [SerializeField] private GameObject brickStack;


    private GameObject newBrick;
    private int brickNum = 0;
    private Vector3 brickHeight = new Vector3(0, 0.3f, 0);
    private Vector3 mousePosDown;
    private Vector3 mousePosUp;
    private Vector3 direction;
    //private Vector3 destinyPoint;

    //private bool isBrick = false;
    private bool isMoving = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isMoving == false)
        {
            //neu nhan xuong thi se ghi lai toa do diem nhan
            if (Input.GetMouseButtonDown(0))
            {
                mousePosDown = Input.mousePosition;
            }
            //neu nhac len thi se ghi lai toa do diem nhac
            if (Input.GetMouseButtonUp(0))
            {
                mousePosUp = Input.mousePosition;
                direction = new Vector3(mousePosUp.x - mousePosDown.x, mousePosUp.y - mousePosDown.y, mousePosUp.z - mousePosDown.z);
                direction = GetTargetPoint(direction);
                isMoving = true;
            }
        }
        else
        {
            //ke 1 tia raycast check + Moving
            Debug.DrawLine(transform.position + direction / 2 + Vector3.up, transform.position + direction / 2 + Vector3.down * raycastLenght, Color.red);
            RaycastHit hit;
            if (Physics.Raycast(transform.position + direction / 2 + Vector3.up, Vector3.down, out hit, raycastLenght, brickLayer))
            {
                transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);
            }
            else
            {
                isMoving = false;
            }
        }
        
        //destinyPoint = Moving(direction) + new Vector3(0, 0.5f, 0);
        //Debug.Log("destiny" + destinyPoint);
        //transform.position = Vector3.MoveTowards(transform.position, destinyPoint, speed * Time.deltaTime);

    }

    private Vector3 GetTargetPoint(Vector3 direction)
    {
        //Xac dinh huong vuot cua ngon tay tren man hinh de ap dung vao raycast
        if(Mathf.Abs(direction.x) < direction.y)
        {
            direction = Vector3.right;
        }
        else if (direction.x > direction.y && direction.x > 0)
        {
            direction = -Vector3.forward;
        }
        else if(Mathf.Abs(direction.x) > Mathf.Abs(direction.y) && direction.x < 0) 
        {
            direction = Vector3.forward;
        }
        else if (Mathf.Abs(direction.x) < Mathf.Abs(direction.y) && direction.y < 0)
        {
            direction = Vector3.left;
        }
        else
        {
            direction = Vector3.zero;
        }
        return direction;
    }

    private void OnTriggerEnter(Collider other)
    {
        //neu player va cham vs brick thi se xoa cai brick cu di va them brick moi duoi chan + nang chieu cao cua anim player
        if (other.tag == "BrickDelete")
        {
            //xoa nhan vat
            Destroy(other.gameObject);

            //chieu cao = chieu cao anim + chieu cao vien gach
            playerAnim.transform.position = playerAnim.transform.position + brickHeight;

            //set diem tao vien gach moi = vi tri cua anim 
            brickPoint.position = playerAnim.transform.position - brickHeight;

            //tao vien gach moi
            newBrick = Instantiate(brickPrefabs, brickPoint.position, brickPoint.rotation);

            //cho cac vien gach vao stack
            newBrick.transform.SetParent(brickStack.transform);
            brickNum++;
            Debug.Log(brickNum);
        }

        else if (other.tag == "BoxDelete" && brickNum > 0)
        {
            //neu di vao vung nay thi gach duoi chan player se bi tru di
            playerAnim.transform.position = playerAnim.transform.position - brickHeight;
            Destroy(brickStack.transform.GetChild(brickStack.transform.childCount - 1).gameObject);
            brickNum--;
            Debug.Log(brickNum);

        }
    }

    //public Vector3 Moving(Vector3 direction)
    //{
    //    Debug.DrawLine(transform.position + direction  + Vector3.up, transform.position + direction  + Vector3.down * raycastLenght, Color.red);
    //    int i = 1;
    //    Vector3 pos = new Vector3(0, 0, 0);
    //    RaycastHit hit;
    //    RaycastHit result;
    //    //while (Physics.Raycast(transform.position + direction * i + Vector3.up, Vector3.down, out hit, raycastLenght, brickLayer))
    //    //{
    //    //    result = hit;
    //    //    i++;
    //    //    if (result.collider == null)
    //    //    {
    //    //        pos = transform.position;
    //    //    }
    //    //    else
    //    //    {
    //    //        pos = result.collider.transform.position;
    //    //    }
    //    //}

    //    for(i = 1; i < 1000; i++)
    //    {
    //        if(Physics.Raycast(transform.position + direction * i + Vector3.up, Vector3.down, out hit, raycastLenght, brickLayer))
    //        {
    //            result = hit;
    //            if (result.collider == null)
    //            {
    //                pos = transform.position;
    //            }
    //            else
    //            {
    //                pos = result.collider.transform.position;
    //            }
    //        }
    //        else
    //        {
    //            break;
    //        }

    //    }
    //    return pos;
    //}

    //private bool checkBrick()
    //{
    //    //ham kiem tra xem huong di nv co gap brick ko bang cach dung raycast check o tiep theo

    //    Debug.DrawLine(transform.position,transform.position + Vector3.down * raycastLenght, Color.red);
    //    RaycastHit hit;

    //    //kiem tra xem tia co trung collider ko
    //    if (Physics.Raycast(transform.position, Vector3.down, raycastLenght, brickLayer))
    //    {
    //        isBrick = true;
    //    }
    //    else
    //    {
    //        isBrick = false;
    //    }

    //    return isBrick;
    //}


    public void OnInit()
    {
        //ham khoi tao
        transform.position = Vector3.zero;
        brickNum = 0;
    }

    public void AddBrick()
    {
        //ham tang so gach duoi chan + tang chieu cao player
        brickNum++;
        

    }
    public void RemoveBrick()
    {

    }

    public void ClearBrick()
    {

    }

    
}
