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
        rooms[0] = new GameObject[4];

        rooms[0][0] = GameObject.Find("Rooms/Room1");
        rooms[0][1] = GameObject.Find("Rooms/Room2");
        rooms[0][2] = GameObject.Find("Rooms/Room3");
        rooms[0][3] = GameObject.Find("Rooms/Room4");
        
    }

    void Update()
    {
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
        roomIndex++;
    }

    public void LastRoom()
    {
        roomIndex--;
    }

    public void NextPlanet()
    {
        planetIndex++;
    }

    public void LastPlanet()
    {
        planetIndex--;
    }

    #endregion
}
