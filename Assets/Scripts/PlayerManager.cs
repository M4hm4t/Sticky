using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public Material collectedObjMat;
    public PlayerState playerState;
    public LevelState levelState;
    public List<GameObject> collidedList;
    public Transform collectedPollTransform;
    public Transform particlePrefab;
    public enum PlayerState
    {
        Stop,
        Move
    }
     public enum LevelState
    {
        NotFinished,
        Finised
    }
    public void CallMakeSphere()
    {
        foreach (GameObject obj in collidedList)
        {
            obj.GetComponent<CollectedObjectController>().MakeSphere();
        }
    }
}
