using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinningLevel : MonoBehaviour
{
    [SerializeField] private Material winningMaterial;

    [SerializeField] private GameObject winningUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(WinningRoutine());
        }
    }

    IEnumerator WinningRoutine()
    {
        GetComponent<MeshRenderer>().material = winningMaterial;
        winningUI.SetActive(true);
        Time.timeScale = 0.25f;
        yield return new WaitForSeconds(1f);
        Time.timeScale = 1f;
        int currentSceneID = SceneManager.GetActiveScene().buildIndex;
       
            // TODO work out index max and rotate around scenes
            SceneManager.LoadSceneAsync(currentSceneID == 0 ? 1 : currentSceneID == 1 ? 2 : 0);  
        
        
    }
}
