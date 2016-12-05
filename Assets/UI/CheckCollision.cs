using UnityEngine;
using System.Collections;

public class CheckCollision : MonoBehaviour
{
    private Canvas interactionCanvas; //The canvas displaying interaction information
    private Canvas itemCanvas; //The canvas displaying item information
    private UnityEngine.UI.Text interactionMessage; //The interaction message
    private UnityEngine.UI.Text itemTitle; //The item's title
    private UnityEngine.UI.Text itemDescription; //The item's description
    private MeshFilter model; //The model to render
    private Renderer modelTextureRenderer; //The model's texture renderer
    private GameObject modelIdentity; //The model's default position and rotation
    private RotateModel rotateModelScript; //The script for rotating models
    private UnityStandardAssets.Characters.FirstPerson.FirstPersonController fpsController; //The first person controller

    void Start() //Use this for initialization
    {
        Canvas []canvases = GetComponentsInChildren<Canvas>(); //Get the canvases

        for (int i = 0; i < canvases.Length; i++) //For all the text elements
        {
            if (canvases[i].name == "Interaction")
            {
                interactionCanvas = canvases[i]; //Get the interaction canvas 
            }
            else if (canvases[i].name == "Item")
            {
                itemCanvas = canvases[i]; //Get the item canvas
            }
        }

        interactionMessage = interactionCanvas.GetComponentInChildren<UnityEngine.UI.Text>(); //Get the message text

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
        interactionCanvas.enabled = false; //Disable the interaction canvas
        itemCanvas.enabled = false; //Disable the item canvas
    }

    void OnTriggerStay(Collider coll) //Check for trigger stay
    {
        if (coll.gameObject.name.Contains("Elevator")) //If the trigger is for the elevator
        {
            if (!interactionCanvas.enabled) //If the interaction canvas is disabled
            {
                interactionCanvas.enabled = true; //Enable the interaction canvas
                interactionMessage.text = "Press 1 for floor 1\nPress 2 for floor 2\nPress 3 for floor 3"; //Set the message text
            }
        }
        else //If the trigger is not an elevator
        {
            if (!interactionCanvas.enabled) //If the interaction canvas is disabled
            {
                interactionCanvas.enabled = true; //Enable the interaction canvas
                interactionMessage.text = "Left click to examine"; //Set the message text

                Trigger triggerScript = coll.GetComponent<Trigger>(); //Get the trigger script on the object
                itemTitle.text = triggerScript.ItemTitle; //Change the title text
                itemDescription.text = triggerScript.ItemDescription; //Change the description text

                model.transform.rotation = coll.gameObject.transform.rotation; //Rotate the painting accordingly

                model.mesh = coll.GetComponent<MeshFilter>().mesh; //Get the model on the object
                modelTextureRenderer.material = coll.GetComponent<Renderer>().material; //Get the material on the object's texture renderer

                rotateModelScript.ShouldRotate = triggerScript.ShouldRotate; //Set if the model should rotate

                rotateModelScript.RotateAroundX = triggerScript.RotateAroundX; //Set which axis to rotate around
            }
            else if (interactionCanvas.enabled && Input.GetButton("Fire1")) //If the interaction canvas is enabled and the player left clicks
            {
                fpsController.enabled = false; //Disable the player

                model.transform.position = modelIdentity.transform.position; //Move the model back into view
                itemCanvas.enabled = true; //Enable the item canvas
            }
            else if (itemCanvas.enabled && Input.GetButton("Fire2")) //If the item canvas is enabled and the player right clicks
            {
                fpsController.enabled = true; //Enable the player

                interactionCanvas.enabled = false; //Disable the interaction canvas

                rotateModelScript.ShouldRotate = false; //Stop the model from rotating
                model.transform.rotation = modelIdentity.transform.rotation; //Reset the model

                model.transform.position = new Vector3(model.transform.position.x, -100, model.transform.position.z); //Move the model real far away
                itemCanvas.enabled = false; //Disable the item canvas
            }
        }
    }

    void OnTriggerExit(Collider coll) //Check for trigger exit
    {
        interactionCanvas.enabled = false; //Disable the interaction canvas
    }
}