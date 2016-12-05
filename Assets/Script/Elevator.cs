using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider))] //Require a box collider
public class Elevator : MonoBehaviour
{
    Animator anim; //The elevator's animator

    void Start() //Use this for initialization
    {
        anim = GetComponent<Animator>(); //Get the animator
        anim.Play("Open Door"); //Play the open animation
	}	
	
	void Update() //Update is called once per frame
    {
	
	}
}