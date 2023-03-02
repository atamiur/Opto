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
    public static int currentScene                      = 0;                    // current scene's build index
    public static int amblyopicEye {get; internal set;} = 0;                    // 0 = Left | 1 = right

    // enums 
    public enum Side       { Left     , Right     }
    public enum EyeStatus  { Dominant , Weaker    }

    // Culling Masks
    public const int CullingMaskDominantEyeLayerBit     = 10 ;                  // bitwize value of dominant eye's layer
    public const int CullingMaskWeakerEyeLayerBit       = 11 ;                  // bitwize value of weaker   eye's layer
    public const int CullingMaskDefaultLayerBit         =  0 ;                  // bitwize value of default  eye's layer

    public const int CullingMaskDominantEye             =  (1 << CullingMaskDefaultLayerBit) | (1 << CullingMaskDominantEyeLayerBit)  ; // Dominant Eye's culling mask
    public const int CullingMaskWeakerEye               =  (1 << CullingMaskDefaultLayerBit) | (1 << CullingMaskWeakerEyeLayerBit)    ; // Weaker Eye's culling mask

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

    public static void SetAmblyopicEye(int eye){
        amblyopicEye = eye;
    }

    public static int GetCullingMask(Side side, Side eye)
    {
        /* XOR
        0 0 = 0  Weak
        0 1 = 1  Dominante
        1 0 = 1  Dominante
        1 1 = 0  weak
        */

        if ( (int)side == 0 ^ (int)eye == 0){                                   
            return CullingMaskDominantEye;                                      // 0 0  and 1 1 return weak eye
        } else {
            return CullingMaskWeakerEye;                                        // 0 1  and 1 0 return weak eye
        }
    }
}
