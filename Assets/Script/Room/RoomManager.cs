using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    #region Properties

    public static RoomManager Instance { get; private set; }
    public int planetIndex = 0;
    public int roomIndex = 0;
    public GameObject[][] rooms = null;
    public GameObject leftButton = null;
    public GameObject rightButton = null;
    public GameObject roomExchangePre = null;
    private GameObject roomExchangeCanvas = null;
    private bool isExchanging = false;
    private bool isAnimationPlay = false;
    public bool canChangeRoom = true;

    #endregion

    #region Unity Methods
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        canChangeRoom = true;

        rooms = new GameObject[4][];
        rooms[0] = new GameObject[2];
        rooms[1] = new GameObject[2];
        rooms[2] = new GameObject[2];
        rooms[3] = new GameObject[2];

        rooms[0][0] = GameObject.Find("Planet1/Room1");
        rooms[0][1] = GameObject.Find("Planet1/Room2");
        rooms[1][0] = GameObject.Find("Planet2/Room1");
        rooms[1][1] = GameObject.Find("Planet1/Room2");
        rooms[2][0] = GameObject.Find("Planet3/Room1");
        rooms[2][1] = GameObject.Find("Planet1/Room2");
        rooms[3][0] = GameObject.Find("Planet4/Room1");
        rooms[3][1] = GameObject.Find("Planet1/Room2");

        leftButton = GameObject.Find("LeftButton");
        rightButton = GameObject.Find("RightButton");

    }

    void Start()
    {

        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                rooms[i][j].SetActive(false);
            }
        }

        rooms[0][0].SetActive(true);
    }

    void Update()
    {
        if (isExchanging)
        {
            return;
        }

        if ((roomIndex == 0) || (!rooms[planetIndex][roomIndex - 1]) || (!canChangeRoom))
        {
            leftButton.SetActive(false);
        }
        else
        {
            leftButton.SetActive(true);
        }

        if ((roomIndex == rooms[planetIndex].Length - 1) || (!rooms[planetIndex][roomIndex + 1]) || (!canChangeRoom))
        {
            rightButton.SetActive(false);
        }
        else
        {
            rightButton.SetActive(true);
        }


        if (roomIndex == 0)
        {
            for (int i = 0; i < rooms.Length; i++)
            {
                rooms[i][0].SetActive(i == planetIndex);
            }
            rooms[0][1].SetActive(false);
        }
        else
        {
            for (int i = 0; i < rooms.Length; i++)
            {
                rooms[i][0].SetActive(false);
            }
            rooms[0][1].SetActive(true);
        }
    }

    #endregion

    #region Room

    public void NextRoom()
    {
        if (InputManager.Instance.sceneState != SceneState.MainScene) { return; }
        if (isAnimationPlay)
        {
            return;
        }

        InputManager.Instance.sceneState = SceneState.Animation;
        isExchanging = true;
        isAnimationPlay = true;
        roomIndex++;
        roomExchangeCanvas = Instantiate(roomExchangePre);
        roomExchangeCanvas.GetComponent<Animator>().SetBool("Left", true);
        StartCoroutine("FinishExchange");
        StartCoroutine("DestroyAnimation");
    }

    public void LastRoom()
    {
        if (InputManager.Instance.sceneState != SceneState.MainScene) { return; }
        if (isAnimationPlay)
        {
            return;
        }

        InputManager.Instance.sceneState = SceneState.Animation;
        isExchanging = true;
        isAnimationPlay = true;
        roomIndex--;
        roomExchangeCanvas = Instantiate(roomExchangePre);
        roomExchangeCanvas.GetComponent<Animator>().SetBool("Left", false);
        StartCoroutine("FinishExchange");
        StartCoroutine("DestroyAnimation");
    }

    public void NextPlanet()
    {
        planetIndex++;
    }

    public void LastPlanet()
    {
        planetIndex--;
    }

    public void ChangePlanet(int planetIndex)
    {
        if (InputManager.Instance.sceneState != SceneState.MainScene) { return; }
        if (isAnimationPlay)
        {
            return;
        }

        InputManager.Instance.sceneState = SceneState.Animation;
        isExchanging = true;
        isAnimationPlay = true;
        this.planetIndex = planetIndex;
        roomExchangeCanvas = Instantiate(roomExchangePre);
        roomExchangeCanvas.GetComponent<Animator>().SetBool("Left", false);
        StartCoroutine("FinishExchange");
        StartCoroutine("DestroyAnimation");
    }

    public IEnumerator FinishExchange()
    {
        yield return new WaitForSeconds(1f);
        CeController.Instance.ControlCEs();
        isExchanging = false;
    }

    public IEnumerator DestroyAnimation()
    {
        yield return new WaitForSeconds(2.5f);
        isAnimationPlay = false;
        InputManager.Instance.sceneState = SceneState.MainScene;
        Destroy(roomExchangeCanvas);
    }

    #endregion
}
