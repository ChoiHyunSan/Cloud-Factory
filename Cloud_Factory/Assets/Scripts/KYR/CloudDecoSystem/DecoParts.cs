using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DecoParts : MonoBehaviour
{
    // Start is called before the first frame update
    
    public bool canAttached; //������ �� �ִ°�.
    public bool isEditActive; //�̵� ȸ�� ��尡 Ȱ��ȭ �Ǿ��°�.
    public bool canEdit; //�̵� �� ȸ���� �����Ѱ�.UI On(�����Ӱ� ��ư)


    public Vector2 mouseWorld;
    private Vector2 top_right_corner;
    private Vector2 bottom_left_corner;
    public void init(Vector2 _top_right_corner, Vector2 _bottom_left_corner)
    {
        canAttached = true;// ���߿� false�� �ٲ������.
        isEditActive = false;
        canEdit = false;
        top_right_corner = _top_right_corner;
        bottom_left_corner = _bottom_left_corner;
    }

    //private void Update()
    //{
    //    mouseWorld = Camera.main.ScreenToWorldPoint(gameObject.transform.position);
    //    Collider2D _collider = Physics2D.OverlapArea(bottom_left_corner, top_right_corner,LayerMask.GetMask());
    //    if (_collider != null)
    //    {
    //        Debug.Log(_collider.name);
    //        canAttached = true;
    //    }
    //    else
    //        canAttached = false;
    //}

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other != null)
    //    {
    //        Debug.Log(other.name);
    //        canAttached = true;
    //    }
    //    else
    //        canAttached = false;
    //}

    public void ReSettingDecoParts()
    {
        if (canAttached)
            isEditActive = true;
        else
            isEditActive = false;
    }

    public void ActiveCanEdit()
    {
        canEdit = true;
    }

    public void UnactiveCanEdit()
    {
        canEdit = false;
    }
}
