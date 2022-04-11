using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillYourself : MonoBehaviour
{
     private void OnDestroy()
    {
        FindObjectOfType<HoleScript>().Restart();
    }
}
