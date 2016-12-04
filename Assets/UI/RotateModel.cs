using UnityEngine;
using System.Collections;

public class RotateModel : MonoBehaviour
{
    private bool shouldRotate = false; //If the model should rotate

    public bool ShouldRotate //ShouldRotate property
    {
        set
        {
            shouldRotate = value; //Set the value of shouldRotate
        }
    }

    void Update() //Update is called once per frame
    {
        if (shouldRotate) //If the model should rotate
        {
            gameObject.transform.Rotate(0, .25f, 0); //Rotate the model
        }
	}
}