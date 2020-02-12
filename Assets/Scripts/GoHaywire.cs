using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoHaywire : MonoBehaviour
{
   private void OnTriggerEnter(Collider other)
   {
      if (other.CompareTag("Player"))
      {
         other.SendMessage("GoHaywire");
      }
   }
}
