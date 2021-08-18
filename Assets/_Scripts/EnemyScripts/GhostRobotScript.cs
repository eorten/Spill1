using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostRobotScript : MonoBehaviour
{
    [SerializeField]
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        float speed = Random.Range(0.5f, 3f);
        anim.speed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
