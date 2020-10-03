using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerScript : MonoBehaviour
{
    public Animator animator;
    public Collider triggerZone;
    private bool triggered;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(triggered)
        {

            if (Input.GetKeyDown(KeyCode.E))
            {
                animator.enabled = true;

                if (animator.GetBool("isOpening") == false)
                {
                   animator.SetBool("isOpening", true);
                }
                else
                {
                    animator.SetBool("isOpening", false);
                }
            }


            if (Input.GetKeyDown(KeyCode.Q))
            {

                if (animator.enabled == true)
                {
                    animator.enabled = false;
                }
                else
                {
                    animator.enabled = true;
                }
            }
        }

    }

    private void OnTriggerEnter(Collider triggerZone)
    {
        triggered = true;
    }

    private void OnTriggerExit(Collider other)
    {
        triggered = false;
    }
}
