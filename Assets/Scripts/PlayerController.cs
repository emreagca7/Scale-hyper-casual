using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera thirdPersonCam;
    [SerializeField] CinemachineVirtualCamera firstPersonCam;
    Rigidbody PlayerRB;
    public Scrollbar scrolbarX;
    public Scrollbar scrolbarY;
    public Scrollbar scrolbarZ;
    public float floatRange = 250f;
    public float playerDefultScale = 30f;
    public float speed;
    public Animator animatorPb;

    public void OnEnable()
    {
        CameraSwitcher.Register(thirdPersonCam);
        CameraSwitcher.Register(firstPersonCam);
        CameraSwitcher.SwitchCamera(thirdPersonCam);

    }
    private void OnDisable()
    {
        CameraSwitcher.UnRegister(thirdPersonCam);
        CameraSwitcher.UnRegister(firstPersonCam);
    }

    private void Awake()
    {
        PlayerRB = gameObject.GetComponent<Rigidbody>();
        animatorPb = gameObject.GetComponent<Animator>();
    }


    void Update()
    {

        scaleCalculator();
        Movement();
    }
    void scaleCalculator()
    {
        if (scrolbarY.value > 0.5)
        {
            gameObject.GetComponent<Transform>().localScale = new Vector3(playerDefultScale + scrolbarX.value * floatRange,
           playerDefultScale + scrolbarY.value * floatRange,
           playerDefultScale + scrolbarY.value * floatRange);
        }
        else if (scrolbarY.value < 0.5)
        {
            gameObject.GetComponent<Transform>().localScale = new Vector3(playerDefultScale + scrolbarX.value * floatRange,
           playerDefultScale + scrolbarY.value * floatRange,
           playerDefultScale + scrolbarY.value * floatRange);
        }
    }
    void Movement()
    {
        if (scrolbarX.value > 0.50 || scrolbarY.value > 0.50)
        {
            transform.Translate(Vector3.forward * speed / 4 * Time.deltaTime);
            animatorPb.SetBool("min", false);

            CameraSwitcher.SwitchCamera(thirdPersonCam);


        }
        else if (scrolbarX.value <= 0.50 || scrolbarY.value <= 0.50)
        {
            transform.Translate(Vector3.forward * speed * 4 * Time.deltaTime);
            animatorPb.SetBool("min", true);

            CameraSwitcher.SwitchCamera(firstPersonCam);


        }
        /*else
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        */
    }
}
