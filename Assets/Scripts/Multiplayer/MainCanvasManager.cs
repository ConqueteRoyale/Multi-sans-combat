using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//2018-13-21
//Kevin Langlois
//Script qui  ajoute un accesseur au canvas qui contient tout le UI du lobbby multijoueur
public class MainCanvasManager : MonoBehaviour
{
    public static MainCanvasManager Instance;


    [SerializeField]
    private LobbyCanvas _lobbyCanvas;
    public LobbyCanvas LobbyCanvas
        {
            get { return _lobbyCanvas;}
        }

    [SerializeField]
    private CurrentRoomCanvas _currentRoomCanvas;
    public CurrentRoomCanvas CurrentRoomCanvas
    {
        get { return _currentRoomCanvas; }
    }


    private void Awake()
    {
        Instance = this;
    }
}
