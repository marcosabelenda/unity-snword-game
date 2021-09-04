using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickScript : MonoBehaviour
{
   public AudioSource audioSource;
   public AudioClip click;
   
   public void PlayClickSound()
   { 
       audioSource.PlayOneShot(click);
   }
}
