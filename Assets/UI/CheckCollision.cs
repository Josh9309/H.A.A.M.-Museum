using UnityEngine;
using System.Collections;

public class CheckCollision : MonoBehaviour
{
    private Canvas itemCanvas; //The canvas displaying item information
    private UnityEngine.UI.Text itemTitle; //The item's title
    private UnityEngine.UI.Text itemDescription; //The item's description
    private MeshFilter model; //The model to render
    private Renderer modelTextureRenderer; //The model's texture renderer
    private GameObject modelIdentity; //The model's default position and rotation
    private RotateModel rotateModelScript; //The script for rotating models
    private UnityStandardAssets.Characters.FirstPerson.FirstPersonController fpsController; //The first person controller

    void Start() //Use this for initialization
    {
        itemCanvas = GetComponentInChildren<Canvas>(); //Get the item canvas

        UnityEngine.UI.Text[] textElements = itemCanvas.GetComponentsInChildren<UnityEngine.UI.Text>(); //Get the UI text

        for (int i = 0; i < textElements.Length; i++) //For all the text elements
        {
            if (textElements[i].name == "Title") //If this is the title
            {
                itemTitle = textElements[i]; //Get the title of the item            
            }
            else if (textElements[i].name == "Description") //If this is the description
            {
                itemDescription = textElements[i]; //Get the title of the item
            }
        }

        model = itemCanvas.GetComponentInChildren<MeshFilter>(); //Get the model
        modelTextureRenderer = model.GetComponent<Renderer>(); //Get the texture renderer

        GameObject []gameobjectSearch = FindObjectsOfType<GameObject>(); //Get all the gameobjects

        for (int i = 0; i < gameobjectSearch.Length; i++) //For all the gameobjects
        {
            if (gameobjectSearch[i].name == "ModelIdentity") //If the model identity has been found
            {
                modelIdentity = gameobjectSearch[i]; //Get the model's identity
            }
        }

        rotateModelScript = GetComponentInChildren<RotateModel>(); //Get the script
        fpsController = GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>(); //Get the first person controller

        model.transform.position = new Vector3(model.transform.position.x, -100, model.transform.position.z); //Move the model real far away
        itemCanvas.enabled = false; //Disable the item canvas
    }

    void OnTriggerStay(Collider coll) //Check for trigger stay
    {
        Trigger triggerScript = coll.GetComponent<Trigger>(); //Get the trigger script on the object
        itemTitle.text = triggerScript.ItemTitle; //Change the title text
        itemDescription.text = triggerScript.ItemDescription; //Change the description text

        model.transform.rotation = coll.gameObject.transform.rotation; //Rotate the painting accordingly

        model.mesh = coll.GetComponent<MeshFilter>().mesh; //Get the model on the object
        modelTextureRenderer.material = coll.GetComponent<Renderer>().material; //Get the material on the object's texture renderer

        rotateModelScript.ShouldRotate = triggerScript.ShouldRotate; //Set if the model should rotate

        rotateModelScript.RotateAroundX = triggerScript.RotateAroundX; //Set which axis to rotate around

        fpsController.M_MouseLook.YSensitivity = 0; //Stop the player from looking up or down

        model.transform.position = modelIdentity.transform.position; //Move the model back into view
        itemCanvas.enabled = true; //Enable the item canvas
    }

    void OnTriggerExit(Collider coll) //Check for trigger exit
    {
        fpsController.M_MouseLook.YSensitivity = 2; //Let the player look up and down again

        rotateModelScript.ShouldRotate = false; //Stop the model from rotating
        model.transform.rotation = modelIdentity.transform.rotation; //Reset the model

        model.transform.position = new Vector3(model.transform.position.x, -100, model.transform.position.z); //Move the model real far away
        itemCanvas.enabled = false; //Disable the item canvas
    }
}