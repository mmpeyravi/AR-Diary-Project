using System.Collections;
using System.Collections.Generic;
using Polyperfect.Animals;
using TMPro;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARCursor : MonoBehaviour
{
    public GameObject cursorChildObject;

    public GameObject objectToPlace;
    public GameObject environment;
    public ARRaycastManager raycastManager;
    public Camera cam;
    public bool useCursor = true;
    public TextMeshProUGUI text;
    public GameObject ball;
    RaycastHit hit;
    public GameObject plane;
    private bool cowDeath = false;

    public bool firstTime = true;
    public bool b=false;
    public GameObject corner;

    public int numOfCorners = 0;
    public Material mat;

    public GameObject floor;
    public GameObject textSetCorners;
    public GameObject arrow;
    public Transform[] transforms;
    public Vector3[] corners;
    public Vector3[] vertices;

    public GameObject cowsFarm;

    public GameObject farmTerrain;
    /*public NavMeshBuilder navMeshBuilder;
    public NavMeshSurface */
    //private Mesh mesh;

    //private MeshFilter mf;
    // Start is called before the first frame update
    void Start()
    {
        cursorChildObject.SetActive(useCursor);
        //farmTerrain.GetComponent<NavMeshSurface>().BuildNavMesh();

        /*mf = floor.GetComponent<MeshFilter>();
        mesh = new Mesh();
        //mf.mesh = mesh;
        Renderer rend = floor.GetComponent<MeshRenderer>();
        rend.material = mat;*/
        vertices = new Vector3[4];
        corners = new Vector3[4];
        //Instantiate(floor, transform.position, transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        //CreateMesh();
        if (useCursor)
        {
            UpdateCursor();
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Ray ray = cam.ScreenPointToRay(Input.GetTouch(0).position);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider != null)
                {
                    text.text = hit.collider.gameObject.name;
                    /*RaycastReturn = hit.collider.gameObject.name;
                    FoundObject = GameObject.Find(RaycastReturn);
                    Destroy(FoundObject);
                    Debug.Log("did hit");*/
                    if (hit.collider.gameObject.CompareTag("cow"))
                    {
                        cowDeath = true;
                        text.text = "cow!";
                        var animator = hit.collider.gameObject.GetComponent<Animator>();
                        animator.SetBool("isDead", true);
                        GameObject o;
                        (o = hit.collider.gameObject).GetComponent<Animal_WanderScript>().Die();
                        Destroy(o, 5f);
                        
                    }
                }
            }

            if (!cowDeath)
            {
                if (useCursor)
                {
                    Instantiate(objectToPlace, transform.position, transform.rotation);
                    if (firstTime)
                    {


                        //Instantiate(ball, transform.position, transform.rotation);
                        cowsFarm.transform.SetPositionAndRotation(transform.position, transform.rotation);
                        //GameObject model=Instantiate(cowsFarm, transform.position, transform.rotation);
                        //cowsFarm.transform.Translate(-2*cowsFarm.transform.up);
                        //cowsFarm.transform.Translate(-2*cowsFarm.transform.forward);
                        //farmTerrain.GetComponent<NavMeshSurface>().BuildNavMesh();
                        //cowsFarm.SetActive(true);
                        firstTime = false;
                        b = true;
                        //model.transform.Rotate(model.transform.up,180f);
                        //ThrowBall();
                    }
                }
                else
                {
                    List<ARRaycastHit> hits = new List<ARRaycastHit>();
                    raycastManager.Raycast(Input.GetTouch(0).position, hits, TrackableType.Planes);
                    if (hits.Count > 0)
                    {
                        GameObject.Instantiate(objectToPlace, hits[0].pose.position, hits[0].pose.rotation);
                    }
                }
            }
        }

        cowDeath = false;
    }

    void UpdateCursor()
    {
        Vector2 screenPosition = cam.ViewportToScreenPoint(new Vector2(0.5f, 0.5f));
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        raycastManager.Raycast(screenPosition, hits, TrackableType.Planes);
        if (hits.Count > 0)
        {
            transform.position = hits[0].pose.position;
            transform.rotation = hits[0].pose.rotation;
        }
    }

    public void ThrowBall()
    {
        GameObject b = Instantiate(ball, transform.position, transform.rotation);
        b.GetComponent<Rigidbody>().AddForce(0, 20, 0);
    }

    public void CreateMesh()
    {
        GameObject pl = Instantiate(floor, transform.position, transform.rotation);
        pl.transform.position = new Vector3(0, -0.45f, 0);
        pl.transform.eulerAngles = new Vector3(0, 0, 0);
        MeshFilter mf = pl.GetComponent<MeshFilter>();
        Mesh mesh = new Mesh();
        Renderer rend = pl.GetComponent<MeshRenderer>();
        rend.material = mat;

        mesh.vertices = vertices;
        mesh.uv = new Vector2[]
        {
            new Vector2(0, 0),
            new Vector2(0, 1),
            new Vector2(1, 1),
            new Vector2(1, 0)
        };
        mesh.triangles = new int[] {0, 1, 2, 0, 2, 3};
        mf.mesh = mesh;
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
        pl.GetComponent<MeshCollider>().sharedMesh = mesh;
    }

    public void CreateFarm()
    {
        //create floor
        for (int i = 0; i < 4; i++)
        {
            vertices[i] = corners[i];
        }

        CreateMesh();

        //create wall 0-1
        CreateWall(0);
        /*vertices[0] = corners[0] + new Vector3(0, 1, 0);
        vertices[1] = corners[1] + new Vector3(0, 1, 0);
        vertices[2] = corners[1];
        vertices[3] = corners[0];
        CreateMesh();*/


        //create wall 1-2
        CreateWall(1);
        /*vertices[0] = corners[1] + new Vector3(0, 1, 0);
        vertices[1] = corners[2] + new Vector3(0, 1, 0);
        vertices[2] = corners[2];
        vertices[3] = corners[1];
        CreateMesh();*/

        //create wall 0-3
        vertices[0] = corners[3] + new Vector3(0, 1, 0);
        vertices[1] = corners[0] + new Vector3(0, 1, 0);
        vertices[2] = corners[0];
        vertices[3] = corners[3];
        CreateMesh();
    }

    public void CreateWall(int i)
    {
        //create wall i-i+1
        vertices[0] = corners[i] + new Vector3(0, 1, 0);
        vertices[1] = corners[i + 1] + new Vector3(0, 1, 0);
        vertices[2] = corners[i + 1];
        vertices[3] = corners[i];
        CreateMesh();
    }

    public void CreateBallsInFarm()
    {
        if (numOfCorners <= 3)
        {
            if (numOfCorners == 0)
            {
                arrow.SetActive(true);
            }

            //ThrowBall();
            GameObject p = Instantiate(corner, transform.position, transform.rotation);
            p.transform.Translate(0.5f * Vector3.up);
            p.transform.Rotate(Vector3.up, 40f);
            text.text = transform.position + "";
            //p.GetComponent<MeshRenderer>().enabled = false;
            //firstTime = false;
            corners[numOfCorners] = transform.position;
            //vertices[numOfCorners] = transform.position;
            numOfCorners++;
            if (numOfCorners == 4)
            {
                CreateFarm();
                arrow.SetActive(false);
                textSetCorners.SetActive(false);
                GameObject[] cs = GameObject.FindGameObjectsWithTag("corner");
                foreach (var c in cs)
                {
                    Destroy(c);
                }
            }
        }
        else
        {
            GameObject.Instantiate(ball, transform.position, transform.rotation);
            
            //   firstTime = false;
        }
    }
}