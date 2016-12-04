using UnityEngine;
using System.Collections;

[RequireComponent (typeof (SphereCollider))] //Require a sphere collider
public class Trigger : MonoBehaviour
{
    [SerializeField] private bool shouldRotate; //If the model should rotate
    [SerializeField] private bool rotateAroundX; //What axis to rotate around
    [SerializeField] private string itemTitle; //The item's title
    [SerializeField] private string itemDescription; //The item's 

    public bool ShouldRotate //ShouldRotate property
    {
        get
        {
            return shouldRotate; //Return shouldRotate
        }
    }

    public bool RotateAroundX //RotateAroundX property
    {
        get
        {
            return rotateAroundX; //Return rotateAroundX
        }
    }

    public string ItemTitle //ItemTitle property
    {
        get
        {
            return itemTitle; //Return itemTitle
        }
    }

    public string ItemDescription //ItemDescription property
    {
        get
        {
            return itemDescription; //Return itemDescription
        }
    }

    void Start() //Use this for initialization
    {
        GetComponent<SphereCollider>().radius *= 2; //Adjust the size of the trigger
    }
}