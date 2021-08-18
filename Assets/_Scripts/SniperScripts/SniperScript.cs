using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperScript : MonoBehaviour
{
    [SerializeField]
    private Camera playerCamera;
    [SerializeField]
    private Camera weaponCamera;
    [SerializeField]
    private ParticleSystem partSys;
    [SerializeField]
    private Animator anim;
    [SerializeField]
    private Animator camAnim;
    [SerializeField]
    private GameObject scopeCanvas;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private LayerMask Hit;
    [SerializeField]
    private Transform tracerStart;
    [SerializeField]
    private LineRenderer line;

    [SerializeField]
    private ParticleSystem partSysExplosionPrimary;
    [SerializeField]
    private GameObject explosionObjectPrimary;

    [SerializeField]
    private ParticleSystem partSysExplosionSecondary;
    [SerializeField]
    private GameObject explosionObjectSecondary;


    private bool primaryIsRunning = false;
    private bool secondaryIsRunning = false;

    public static bool runHudTimerSecondary = false;
    public static bool runHudTimerPrimary = false;

    private int force = 100;
    private float explosionRadius = 30;

    
    public float reloadPrimary = 0.8f; //ENDRE HUD OGSÅ
    public float reloadSecodary = 3f; //ENDRE HUD OGSÅ
    // Start is called before the first frame update
    void Start()
    {
        partSys.Stop();
        scopeCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //GameObject player = GameObject.FindWithTag("Player");
        if (!primaryIsRunning && Input.GetMouseButton(0))
        {
            StartCoroutine(ShootPrimary(player));
            runHudTimerPrimary = true;
        }
        if (!secondaryIsRunning && Input.GetMouseButtonDown(1))
        {
            runHudTimerSecondary = true;
            StartCoroutine(ShootSecondary(player));
        }
        
    }

    private IEnumerator ShootPrimary(GameObject player)
    {
        primaryIsRunning = true;        
        anim.SetTrigger("Shoot");
        RaycastHit hit;
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, Hit))
        {
            Color32 clr = new Color32(255, 255, 255, 255);
            StartCoroutine(DrawShot(hit.point, clr));

            GameObject objectHit = hit.transform.gameObject;

            explosionObjectPrimary.transform.position = hit.point;
            partSysExplosionPrimary.Emit(300);

            if (objectHit.tag == "Enemy")
            {
                Destroy(objectHit);
                print("hitEnemy");
            }

            float dist = Vector3.Distance(hit.point, player.transform.position);
            print("" + dist);
            if (dist < explosionRadius && objectHit.tag != "Player")
            {
                player.GetComponent<Rigidbody>().AddForce(-1 * ray.direction * force, ForceMode.Impulse);
            }


        }
        
        yield return new WaitForSeconds(reloadPrimary);
        runHudTimerPrimary = false;
        primaryIsRunning = false;
    }


    private IEnumerator ShootSecondary(GameObject player)
    {
        secondaryIsRunning = true;

        anim.SetTrigger("Shoot");
        RaycastHit hit;
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        

        if (Physics.Raycast(ray, out hit, Hit))
        {
            Color32 clr = new Color32(255, 0, 0, 255);
            StartCoroutine(DrawShot(hit.point, clr));

            GameObject objectHit = hit.transform.gameObject;
            print(objectHit.tag);
            float dist = Vector3.Distance(hit.point, player.transform.position);

            explosionObjectSecondary.transform.position = hit.point;
            partSysExplosionSecondary.Emit(300);

            if (dist < explosionRadius && objectHit.tag != "Player")
            {
                Rigidbody rb = player.GetComponent<Rigidbody>();
                Vector3 speedBefore = new Vector3(rb.velocity.x, 0, rb.velocity.z);
                rb.velocity = (-0.5f * ray.direction * force) + speedBefore; //Halvparten så sterk, *0.5
                
                
            }


        }
        
        yield return new WaitForSeconds(reloadSecodary);
        runHudTimerSecondary = false;
        secondaryIsRunning = false;
    }
    private IEnumerator DrawShot(Vector3 end, Color32 tracerColor)
    {
        line.positionCount = 2;
        line.SetPosition(0, tracerStart.position);
        line.SetPosition(1, end);
        line.startColor = (tracerColor);
        line.endColor = new Color32(0, 0, 0, 0);

        yield return new WaitForSeconds(0.15f); //Line duration
        line.positionCount = 0;
    }

}
