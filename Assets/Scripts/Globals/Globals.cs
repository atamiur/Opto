/* 
    Class Globals
    Describes global data (structs, types, variables, consts) to be used accorss the platform

    Inludes general purpose methods to be invoked anywhere in the code (utils) 
    
*/

using UnityEngine;
using UnityEngine.SceneManagement;

public static class Globals // : MonoBehaviour  // static -> doesn't derive from MonoBehaviour
{
    // global data definition
     static int currentScene = 0;

    // methods --------

    // method to decide next level and load corresponding scene
    // TODO: currently, the platform moves secuencially from a level to the next one (based on build sequence)
    //       the method must evolve to correspond to the patent's evolution
    public static void NextLevel(){
        int nextLevel = ( SceneManager.GetActiveScene().buildIndex + 1 ) 
                        % SceneManager.sceneCountInBuildSettings;               // get next scene sequencially in a circular approach (from last back to firt level) 
        currentScene = nextLevel;
        SceneManager.LoadScene(nextLevel);
    }
    public static void ReloadLevel(){
        int thisScene = SceneManager.GetActiveScene().buildIndex;               // get current scene build index
        currentScene = thisScene;                                               // overkill...
        SceneManager.LoadScene(thisScene);                                      // reload current scene
    }
}
