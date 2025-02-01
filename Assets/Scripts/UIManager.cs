using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
   public void MainMenu()
   {
      SceneManager.LoadScene("Main Menu");
      Time.timeScale = 1;
   }
   
   public void ContiniousLevelLoader()
   {
      int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
      SceneManager.LoadScene(currentSceneIndex + 1);
      Time.timeScale = 1;
   }
}
