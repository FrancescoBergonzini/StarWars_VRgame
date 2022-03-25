using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Taglio_Ascolta : MonoBehaviour
{
    public Taglio[] tagli = new Taglio[2];
  
    private void OnTriggerEnter(Collider other)
    {
        if(tagli != null)
        {
            tagli[0].isCut = true;
            tagli[1].isCut = true;
        }      

    }
    // Start is called before the first frame update

}
