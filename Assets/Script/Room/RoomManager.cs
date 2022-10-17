using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    #region Properties

    public static RoomManager Instance { get; private set; }
    private int planetIndex = 0;
    private int roomIndex = 0;
    private GameObject[][] rooms = null;
    public GameObject leftButton = null;
    public GameObject rightButton = null;
    public GameObject roomExchangePre = null;
    private GameObject roomExchangeCanvas = null;
    private bool isExchanging = false;
    private bool isAnimationPlay = false;

    #endregion

    #region Unity Methods
    void Start()
    {
        if (!Instance)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        rooms = new GameObject[1][];
        rooms[0] = new GameObject[2];

        rooms[0][0] = GameObject.Find("Room1");
        rooms[0][1] = GameObject.Find("Room2");
        
    }

    void Update()
    {
        if (isExchanging)
        {
            return;
        }

        if ((roomIndex == 0)||(!rooms[planetIndex][roomIndex - 1]))
        {
            leftButton.SetActive(false);
        }
        else
        {
            leftButton.SetActive(true);
        }

        if ((roomIndex == rooms[planetIndex].Length - 1)||(!rooms[planetIndex][roomIndex + 1]))
        {
            rightButton.SetActive(false);
        }
        else
        {
            rightButton.SetActive(true);
        }

        for (int i = 0; i < rooms[planetIndex].Length; i++)
        {
            if (rooms[planetIndex][i])
            {
                rooms[planetIndex][i].SetActive(i == roomIndex);
            }
        }
    }

    #endregion

    #region Room

    public void NextRoom()
    {
        if (isAnimationPlay)
        {
            return;
        }

        isExchanging = true;
        isAnimationPlay = true;
        roomIndex++;
        roomExchangeCanvas = Instantiate(roomExchangePre);
        roomExchangeCanvas.GetComponent<Animator>().SetBool("Left",true);
        StartCoroutine("FinishExchange");
        StartCoroutine("DestroyAnimation");
    }

    public void LastRoom()
    {
        if (isAnimationPlay)
        {
            return;
        }

        isExchanging = true;
        isAnimationPlay = true;
        roomIndex--;
        roomExchangeCanvas = Instantiate(roomExchangePre);
        roomExchangeCanvas.GetComponent<Animator>().SetBool("Left",false);
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

    public IEnumerator FinishExchange()
    {
        yield return new WaitForSeconds(1f);
        isExchanging = false;
    }

    public IEnumerator DestroyAnimation()
    {
        yield return new WaitForSeconds(2.5f);
        isAnimationPlay = false;
        Destroy(roomExchangeCanvas);
    }

    #endregion
}
