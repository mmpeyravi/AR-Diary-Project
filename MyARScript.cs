using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.XR.ARFoundation;
using Random = UnityEngine.Random;

public class MyARScript : MonoBehaviour
{
    public static Text text;

    private ARTrackedImageManager _arTrackedImageManager;

    private ARTrackedImage _arTrackedImage;

    private GameObject _prefab = null;

    private bool firstTime = true;

    public GameObject handle;
    public GameObject characterManager;
    public GameObject dastopaha;
    public GameObject pelkl;
    public GameObject pelkr;
    public GameObject eyel;
    public GameObject eyer;
    public GameObject sphere;
    public GameObject spongeBob;
    public GameObject effect;

    public GameObject ketfLH;
    public GameObject ketfRH;
    public GameObject ketfLL;
    public GameObject ketfRL;
    public GameObject rightHand;
    [FormerlySerializedAs("saedL")] public GameObject forearmL;
    [FormerlySerializedAs("saedR")] public GameObject forearmR;
    public GameObject handL;
    public GameObject handR;
    public GameObject legL;
    public GameObject legR;
    public GameObject footL;
    public GameObject footR;
    [FormerlySerializedAs("mardomakL")] public GameObject pupilL;
    [FormerlySerializedAs("mardomakR")] public GameObject pupilR;

    public GameObject leftHand;
    public GameObject rightLeg;
    public GameObject leftLeg;
    public InputField px;
    public InputField py;
    public InputField pz;
    public InputField rx;
    public InputField ry;
    public InputField rz;

    public AudioClip music;
    private Touch _touch;

    private Vector3 _prefabLocalPosition;
    private Vector3 _prefabLocalEulerAngles;

    // Start is called before the first frame update
    void Start()
    {
        _arTrackedImageManager = GetComponent<ARTrackedImageManager>();
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        ExecuteClient();
    }

    // Update is called once per frame
    void Update()
    {
        //CheckDrag();
        //pelk.transform.localPositio.Rotate(pelk.transform.forward,pelk.transform.position-oldPositon);
        if (_prefab == null)
        {
            _prefab = GameObject.FindWithTag("Prefab");
        }
        else
        {
            if (firstTime)
            {
                //_arTrackedImage = FindObjectOfType<ARTrackedImage>();
                //sphere.SetActive(true);
                //_arTrackedImage.transform.localEulerAngles = new Vector3(0, 0, 0);
                //var transform3 = _arTrackedImage.transform;

                /*_faucet.transform.localRotation = Quaternion.Euler(90, 0, 0);
                _faucet.transform.localPosition = new Vector3(0, 0.06f, 0);*/
                //_faucet.transform.localEulerAngles += new Vector3(90, 0, 0);
                _prefabLocalPosition = _prefab.transform.localPosition;
                _prefabLocalEulerAngles = _prefab.transform.localEulerAngles;

                /*characterManager.transform.localPosition = localPosition;
                characterManager.transform.localRotation = localRotation;
                /*characterManager.transform.localEulerAngles += new Vector3(90, 180, 0);
                characterManager.transform.position += new Vector3(0, 0, 0);#1#
                characterManager.SetActive(true);
                characterManager.transform.SetParent(_faucet.transform);*/

                Spawn(spongeBob, 0, -0.06f, 0);
                Spawn(effect, 0, 0, -0.06f);
                StartCoroutine(Effecting(0.01f, 5));

                /*sphere.transform.localPosition = _prefabLocalPosition + new Vector3(0, -0.06f, 0);
                sphere.transform.localEulerAngles = _prefabLocalEulerAngles;
                //sphere.transform.localEulerAngles += new Vector3(90, 180, 0);
                //sphere.transform.SetParent(_faucet.transform);
                sphere.transform.parent = _prefab.transform;
                sphere.SetActive(true);*/

                /*effect.transform.localPosition = localPosition + new Vector3(-0.06f, 0, 0);
                effect.transform.localRotation = localRotation;
                effect.transform.SetParent(_faucet.transform);*/
                //effect.SetActive(true);
                /*var position = characterManager.transform.localPosition;
                px.text = position.x+"";
                py.text = position.y+"";
                pz.text = position.z+"";
                var rotation = characterManager.transform.localEulerAngles;
                rx.text = rotation.x+"";
                ry.text = rotation.y+"";
                rz.text = rotation.z+"";*/
                //characterManager.GetComponent<CJ>()
                dastopaha.transform.localPosition = _prefabLocalPosition;
                dastopaha.transform.localEulerAngles = _prefabLocalEulerAngles;
                //dastopaha.transform.localEulerAngles += new Vector3(90, 180, 0);
                dastopaha.transform.position += new Vector3(0, 0, 0);
                //dastopaha.SetActive(true);


                //StartCoroutine(Born());
                StartCoroutine(Pelk());
                StartCoroutine(Cheshm());
                //StartCoroutine(Cheshm());
                //characterManager.transform.SetPositionAndRotation(_faucet.transform.losition, _faucet.transform.localRotation);
                //dastopaha.SetActive(true);
                //dastopaha.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
                //Instantiate(characterManager, Vector3.zero, Quaternion.identity);
                //Instantiate(dastopaha, Vector3.zero, Quaternion.identity);
                firstTime = false;
            }

            effect.transform.RotateAround(_prefab.transform.position, -Vector3.forward, Time.deltaTime * 160);

            //var position = _faucet.transform.localPosition;
            //var rotation = _faucet.transform.localEulerAngles;
            //text.text = _arTrackedImage.transform.parent.parent.parent.name+"\n"+position.x + " " + position.y + " " + position.z
            //  +"\n"+rotation.x+" "+rotation.y+" "+rotation.z;
            /*var transform2 = characterManager.transform;
            var transform1 = transform2.localPosition;
            var angles = transform2.localEulerAngles;
            text.text ="character Manager:\n" +transform1.x+" "+transform1.y+" "+transform1.z+"\n"+
                        angles.x+" "+angles.y+" "+angles.z;

            var position2 = characterManager.transform.localPosition;
            px.text = position2.x+"";
            py.text = position2.y+"";
            pz.text = position2.z+"";
            var rotation2 = characterManager.transform.localEulerAngles;
            rx.text = rotation2.x+"";
            ry.text = rotation2.y+"";
            rz.text = rotation2.z+"";
            characterManager.transform.localPosition = new Vector3(float.Parse(px.text), float.Parse(py.text), float.Parse(pz.text));
            characterManager.transform.localEulerAngles = new Vector3(float.Parse(rx.text), float.Parse(ry.text), float.Parse(rz.text));*/
            //characterManager.transform.SetParent(_faucet.transform);
            //characterManager.transform.SetPositionAndRotation(_faucet.transform.position,_faucet.transform.localRotation);
            //dastopaha.transform.SetPositionAndRotation(_faucet.transform.position,_faucet.transform.localRotation);
            //_arTrackedImage.
            //text.text = _faucet.transform.localPosition + "\n" + _faucet.transform.localRotation;
        }
    }

    private void Spawn(GameObject g, float x, float y, float z)
    {
        g.transform.parent = _prefab.transform;
        g.transform.localPosition = new Vector3(x, y, z);
        g.transform.localRotation = Quaternion.Euler(-90, -180, 0);
        g.SetActive(true);
    }

    void CheckDrag()
    {
        /*DateTime.Now.Second
        if (DateTime.Now.Second-_dateTime.Second==15 && player.rotation.Equals(transform.rotation))
        {
            while (player.transform.right.normalized==transform.right.normalized)
            {
                _dragYawDegrees=p
            }
            
        }*/
        if (Input.touchCount != 1)
        {
            return;
        }

        _touch = Input.GetTouch(0);
        if (_touch.phase != TouchPhase.Moved)
        {
            return;
        }

        var t = handle.transform.Find("Faucet").Find("handle");
        if (t != null)
        {
            t.Rotate(Vector3.up, _touch.deltaPosition.x);
        }
    }

    IEnumerator Pelk()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.value * 5f);
            pelkl.GetComponent<MeshRenderer>().enabled = true; //.SetActive(true);
            pelkr.GetComponent<MeshRenderer>().enabled = true; //.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            pelkl.GetComponent<MeshRenderer>().enabled = false; //.SetActive(false);
            pelkr.GetComponent<MeshRenderer>().enabled = false; //.SetActive(false);
            //code here will execute after 5 seconds
        }
    }

    IEnumerator Born()
    {
        yield return new WaitForSeconds(3f);

        GetComponent<AudioSource>().PlayOneShot(music);
        pelkr.GetComponent<MeshRenderer>().enabled = true; //.SetActive(true);
        eyer.GetComponent<MeshRenderer>().enabled = true; //.SetActive(true);
        pupilR.GetComponent<MeshRenderer>().enabled = true;
        yield return new WaitForSeconds(1.5f);

        pelkl.GetComponent<MeshRenderer>().enabled = true; //.SetActive(true);
        eyel.GetComponent<MeshRenderer>().enabled = true; //.SetActive(true);
        pupilL.GetComponent<MeshRenderer>().enabled = true;
        yield return new WaitForSeconds(1.5f);

        ketfRH.GetComponent<MeshRenderer>().enabled = true; //.SetActive(true);
        //rightHand.GetComponent<MeshRenderer>().enabled=true;//.SetActive(true);

        forearmR.GetComponent<MeshRenderer>().enabled = true;
        handR.GetComponent<MeshRenderer>().enabled = true;
        //Ding(characterManager.transform.Find("ketf rh"), dastopaha.transform.Find("Right hand"));
        yield return new WaitForSeconds(1.5f);

        ketfLH.GetComponent<MeshRenderer>().enabled = true; //.SetActive(true);
        //leftHand.GetComponent<MeshRenderer>().enabled=true;//.SetActive(true);

        forearmL.GetComponent<MeshRenderer>().enabled = true;
        handL.GetComponent<MeshRenderer>().enabled = true;
        //Ding(characterManager.transform.Find("ketf lh"), dastopaha.transform.Find("Left hand"));
        yield return new WaitForSeconds(1.5f);

        ketfRL.GetComponent<MeshRenderer>().enabled = true; //.SetActive(true);
        //rightLeg.GetComponent<MeshRenderer>().enabled=true;//.SetActive(true);

        legR.GetComponent<MeshRenderer>().enabled = true;
        footR.GetComponent<MeshRenderer>().enabled = true;
        //Ding(characterManager.transform.Find("ketf rl"), dastopaha.transform.Find("Right leg"));
        yield return new WaitForSeconds(1.5f);

        ketfLL.GetComponent<MeshRenderer>().enabled = true; //.SetActive(true);
        //leftLeg.GetComponent<MeshRenderer>().enabled=true;//.SetActive(true);
        legL.GetComponent<MeshRenderer>().enabled = true;
        footL.GetComponent<MeshRenderer>().enabled = true;
        //Ding(characterManager.transform.Find("ketf ll"), dastopaha.transform.Find("Left leg"));
        yield return new WaitForSeconds(1.5f);

        pelkr.GetComponent<MeshRenderer>().enabled = false; //.SetActive(false);
        StartCoroutine(Cheshm());

        yield return new WaitForSeconds(3f);

        pelkl.GetComponent<MeshRenderer>().enabled = false; //.SetActive(false);
        yield return new WaitForSeconds(3f);

        StartCoroutine(Pelk());
    }

    IEnumerator Cheshm()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.value * 5f);
            float x = Random.Range(-60, 60);
            float y = Random.Range(60, 120);
            float z = Random.Range(-30, 30);
            eyel.transform.localEulerAngles = new Vector3(0, y, z);
            eyer.transform.localEulerAngles = new Vector3(0, y, z);
            //code here will execute after 5 seconds
        }
    }

    IEnumerator Effecting(float maxSize, float duration)
    {
        //float div = maxSize/100;
        effect.transform.localScale = new Vector3(0, 0, 0);
        effect.GetComponent<AudioSource>().volume = 0;
        //AudioSource audioSource = GetComponent<AudioSource>();
        while (effect.transform.localScale.x < maxSize) //max=0.1  dur=1
        {
            effect.transform.localScale += new Vector3(maxSize / 100, maxSize / 100, maxSize / 100);
            effect.GetComponent<AudioSource>().volume += 0.01f;
            yield return new WaitForSeconds(duration / 100);
        }

        yield return new WaitForSeconds(4f);
        while (effect.transform.localScale.x > 0)
        {
            effect.transform.localScale -= new Vector3(maxSize / 100, maxSize / 100, maxSize / 100);
            effect.GetComponent<AudioSource>().volume -= 0.01f;
            yield return new WaitForSeconds(duration / 100);
        }

        effect.SetActive(false);
    }

    public void Ding(Transform t1, Transform t2)
    {
        //var t = characterManager.transform.Find("ketf rh");
        if (t1 != null)
        {
            t1.gameObject.SetActive(true);
        }

        //var t = dastopaha.transform.Find("Right hand");
        if (t2 != null)
        {
            t2.gameObject.SetActive(true);
        }

        //GetComponent<AudioSource>().Stop();
        //GetComponent<AudioSource>().PlayOneShot(musicDatabase[music]);
    }

    // ExecuteClient() Method
    static void ExecuteClient()
    {
        try
        {
            // Establish the remote endpoint
            // for the socket. This example
            // uses port 11111 on the local
            // computer.
           // IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddr =IPAddress.Parse("172.28.0.12"); //ipHost.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddr, 12345);

            // Creation TCP/IP Socket using
            // Socket Class Constructor
            Socket sender = new Socket(ipAddr.AddressFamily,
                SocketType.Stream, ProtocolType.Tcp);

            try
            {
                // Connect Socket to the remote
                // endpoint using method Connect()
                sender.Connect(localEndPoint);

                // We print EndPoint information
                // that we are connected
                //Console.WriteLine("Socket connected to -> {0} ",
                //            sender.RemoteEndPoint.ToString());
                text.text = "Socket connected to -> {0} " + sender.RemoteEndPoint.ToString();
                // Creation of message that
                // we will send to Server
                byte[] messageSent = Encoding.ASCII.GetBytes("Test Client<EOF>");
                int byteSent = sender.Send(messageSent);

                // Data buffer
                byte[] messageReceived = new byte[1024];

                // We receive the message using
                // the method Receive(). This
                // method returns number of bytes
                // received, that we'll use to
                // convert them to string
                int byteRecv = sender.Receive(messageReceived);
                /*Console.WriteLine("Message from Server -> {0}",
                    Encoding.ASCII.GetString(messageReceived,
                        0, byteRecv));*/
                text.text = "Message from Server -> {0}" + Encoding.ASCII.GetString(messageReceived, 0, byteRecv);
                // Close Socket using
                // the method Close()
                sender.Shutdown(SocketShutdown.Both);
                sender.Close();
            }

            // Manage of Socket's Exceptions
            catch (ArgumentNullException ane)
            {
                Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
            }

            catch (SocketException se)
            {
                Console.WriteLine("SocketException : {0}", se.ToString());
            }

            catch (Exception e)
            {
                Console.WriteLine("Unexpected exception : {0}", e.ToString());
            }
        }

        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
    }
}