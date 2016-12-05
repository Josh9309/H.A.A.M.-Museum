using UnityEngine;
using System.Collections;

public class RotateModel : MonoBehaviour
{
    private bool shouldRotate = false; //If the model should rotate
    [SerializeField] private bool rotateAroundX; //What axis to rotate around

    public bool ShouldRotate //ShouldRotate property
    {
        set
        {
            shouldRotate = value; //Set the value of shouldRotate
        }
    }

    public bool RotateAroundX //RotateAroundX property
    {
        set
        {
            rotateAroundX = value; //Set the value of rotateAroundX
        }
    }

    void Update() //Update is called once per frame
    {
        if (shouldRotate && rotateAroundX) //If the model should rotate around the X axis
        {
            gameObject.transform.Rotate(.5f, 0, 0); //Rotate the model
        }
        else if (shouldRotate) //If the model should rotate around the Y axis
        {
            gameObject.transform.Rotate(0, .5f, 0); //Rotate the model
        }
    }
}