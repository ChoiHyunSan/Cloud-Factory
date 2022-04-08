using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
/*
 ��ũ��Ʈ �۵� Ȯ���� ���� �̺�Ʈ �Լ��� �ӽ÷� ���� ��ũ��Ʈ�Դϴ�.
 */ 
public class DemoFunc : MonoBehaviour
{
    [SerializeField]
    CloudMakeSystem Cloudmakesystem;


    public void clicked() //matarial in inventory selected
    {   
        Cloudmakesystem.event_Selected(EventSystem.current.currentSelectedGameObject.name);
    }

    public void unclicked() //matarial in cloudmaker deselected
    {
        Cloudmakesystem.event_UnSelected(EventSystem.current.currentSelectedGameObject.name);
        Debug.Log("Ŭ��");
    }

    public void makeCloudBtn() //btn of cloudmaker clicked
    {
        Cloudmakesystem.event_createBtn(EventSystem.current.currentSelectedGameObject.name);
    }


    void Start()
    {
        //Load cloudSyetem with Finding obj tag
        Cloudmakesystem = GameObject.FindWithTag("CloudSystem").GetComponent<CloudMakeSystem>();
    }

}
