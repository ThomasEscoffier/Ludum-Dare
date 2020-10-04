using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDisplay : MonoBehaviour
{

    public Items item;

    public MeshRenderer meshRenderer;

    // Start is called before the first frame update
    void Start()
    {
        meshRenderer.material = item.marterial;
    }

}
