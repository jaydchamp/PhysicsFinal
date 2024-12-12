using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.UI;  // Use TMP_Text for TextMeshPro
using UnityEditor.Rendering;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;
using System.Linq;



#if UNITY_EDITOR
using UnityEditor;
#endif

public enum SliderType
{
    GravConstant,
    EccenScale
}

public class UI : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private GameObject content;    //UI Panel with content inside of it
    [SerializeField] private RectTransform window;  //entire UI Window
    [SerializeField] private Button toggleButton;   //arrow button to hide UI
    [SerializeField] private Camera innerCam;       //inner orbit cam
    [SerializeField] private Camera outerCam;       //outer orbit cam
    [SerializeField] private Camera followerCam;    //camera to follow planets
    [SerializeField] private GameObject viewSun;    //sun

    [SerializeField] private Planet follow; //planet to follow

    [SerializeField] public List<Planet> allPlanets = new List<Planet>();   //list holding all 8 planets and sun

    [SerializeField] private Slider gravConstantSlider; //slider to change grav constant
    [SerializeField] private TMP_Text gravConstValue;

    [SerializeField] private Slider eccenScaleSlider;   //slider to change eccentricity scaler
    [SerializeField] private TMP_Text eccenScaleValue;

    private bool inRealSim = false;
    private bool inFakeSim = true;

    private bool followingEarth = false;
    private bool settingsOpen = true;
    private Vector2 dragOffset;    //offset for moving window

    void Start()
    {
        innerCam = GameObject.Find("InnerOrbCam").GetComponent<Camera>();
        outerCam = GameObject.Find("OuterOrbCam").GetComponent<Camera>();
        followerCam = GameObject.Find("MovingCam").GetComponent<Camera>();

        gravConstantSlider.value = Planet.gravConstant;
        gravConstantSlider.onValueChanged.AddListener((value) => OnSliderValueChanged(value, SliderType.GravConstant));

        eccenScaleSlider.value = Planet.eccScaler;
        eccenScaleSlider.onValueChanged.AddListener((value) => OnSliderValueChanged(value, SliderType.EccenScale));

        innerCam.enabled = true;
        outerCam.enabled = false;
        followerCam.enabled = false;
    }

    void Update()
    {
        gravConstValue.text = Planet.gravConstant.ToString("000.00");
        eccenScaleValue.text = Planet.eccScaler.ToString("00.00");
    }
    public void TogglePanel()
    {
        settingsOpen = !settingsOpen;
        content.SetActive(settingsOpen);
        toggleButton.transform.rotation = Quaternion.Euler(0, 0, settingsOpen ? 0 : 180);
    }
    public void ToggleInnerOrbitCam()
    {
        //switch to InnerOrbitCam
        followerCam.enabled = false;
        innerCam.enabled = true;
        outerCam.enabled = false;
        viewSun.transform.localScale = new Vector2(25f, 25f);
    }
    public void ToggleOuterOrbitCam()
    {
        //switch to OuterOrbitCam
        followerCam.enabled = false;
        innerCam.enabled = false;
        outerCam.enabled = true;
        viewSun.transform.localScale = new Vector2(75f, 75f);
    }
    void OnSliderValueChanged(float value, SliderType sliderType)
    {
        switch (sliderType)
        {
            case SliderType.GravConstant:
                {
                    Planet.gravConstant = value;
                    ClearLines();
                    break;
                }
            case SliderType.EccenScale:
                {
                    Planet.eccScaler = value;
                    ClearLines();
                    break;
                }
        }
    }
    public void QuitSim()
    {
        Debug.Log("quitting game");
        EditorApplication.isPlaying = false;
        Application.Quit();
    }
    public void RestartSim()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentScene);
        innerCam.enabled = true;
        outerCam.enabled = false;
        followerCam.enabled = false;
    }
    public void ShowFakeScale()
    {
        if (!inFakeSim)
        {
            inFakeSim = true;
            inRealSim = false;

            innerCam.orthographicSize *= 4;
            foreach (Planet planet in allPlanets)
            {
                if (!planet.transform.CompareTag("sun"))
                {
                    planet.lineRenderer.startWidth = 50;
                    planet.lineRenderer.endWidth = 50;
                }

                Transform planetTransform = planet.transform;

                //foreach (Transform child in planetTransform)
                //{
                planet.transform.GetChild(0).gameObject.SetActive(true);
                //}
            }
        }
    }
    public void ShowRealScale()
    {
        if (!inRealSim)
        {
            inRealSim = true;
            inFakeSim = false;

            innerCam.orthographicSize /= 4;
            foreach (Planet planet in allPlanets)
            {
                if (!planet.transform.CompareTag("sun"))
                {
                    planet.lineRenderer.startWidth = 5;
                    planet.lineRenderer.endWidth = 5;
                }

                Transform planetTransform = planet.transform;

                //foreach (Transform child in planetTransform)
                //{
                //turn off child viewer planets
                planet.transform.GetChild(0).gameObject.SetActive(false);
                //}
            }
        }
    }
    public void ClearLines()
    {
        foreach (Planet planet in allPlanets)
        {
            LineRenderer lineRenderer = planet.GetComponent<LineRenderer>();
            if (lineRenderer != null)
            {
                lineRenderer.positionCount = 0;
            }
        }
    }
    public void FollowPlanet()
    {
        followingEarth = !followingEarth;

        if (followingEarth)
        {
            followerCam.enabled = true;
            innerCam.enabled = false;
            outerCam.enabled = false;
            follow = allPlanets.First();

            followerCam.transform.position = follow.transform.position + new Vector3(0, 0, -5);

            followerCam.transform.position = follow.transform.position;
            followerCam.transform.LookAt(follow.transform);
            StartCoroutine(FollowPlanetCoroutine());
        }
        else
        {
            ToggleInnerOrbitCam();
        }
    }
    public void OnBeginDrag()
    {
        dragOffset = Input.mousePosition - window.position;
    }
    public void OnDrag()
    {
        window.position = (Vector2)Input.mousePosition - dragOffset;
    }

    private IEnumerator FollowPlanetCoroutine()
    {
        while (true)
        {
            followerCam.transform.position = follow.transform.position + new Vector3(0, 0, -5);
            followerCam.transform.LookAt(follow.transform);
            yield return null;
        }
    }
}
