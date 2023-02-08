using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBehaviour : MonoBehaviour
{
    public GameObject obj;
    public IInteractable activate;

    private void Start()
    {
        activate = obj.GetComponent<IInteractable>();
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            activate.Interact();
        }
    }
}
