using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class WinnerScript : MonoBehaviour
{
    private Animator anmCtrl;
    
    // Start is called before the first frame update
    void Start()
    {
        anmCtrl = GetComponent<Animator>();
    }
    
    void Update()
    {
        anmCtrl.SetBool("win", true);
        StartCoroutine( backToMenu() );
    }

    private IEnumerator backToMenu(){
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene("Menu");
    }
    
}
