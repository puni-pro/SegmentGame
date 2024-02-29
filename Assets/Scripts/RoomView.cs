using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.UI;

public class RoomView : MonoBehaviourPunCallbacks
{

    [SerializeField]
    private TextMeshProUGUI roomname;

    [SerializeField]
    private List<TextMeshProUGUI> playerTexts; // �v���C���[�̃e�L�X�g�����X�g�ŊǗ�
    private List<Player> playersInRoom = new List<Player>(); // ���[�����̃v���C���[���X�g
    public GameObject RoomViewer;
    public GameObject MatchmakingViewer;
    public Button startbutton;



    private void Start()
    {
        startbutton.interactable = false;
        roomname.text = "---";
        foreach (var playerText in playerTexts)
        {
            playerText.text = "nan";
        }
    }


    public void InactivateRoomView()//�Q�[���J�n�{�^��
    {
        RoomViewer.SetActive(false);
    }

    public override void OnJoinedRoom()
    {
        roomname.text = PhotonNetwork.CurrentRoom.Name;
        UpdatePlayerList();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log($"{newPlayer.NickName}���Q�����܂���");
        UpdatePlayerList();
    }

    // ���v���C���[�����[������ޏo�������ɌĂ΂��R�[���o�b�N
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log($"{otherPlayer.NickName}���ޏo���܂���");
        UpdatePlayerList();
    }
    private void UpdatePlayerList()
    {
        playersInRoom.Clear(); // �v���C���[���X�g���N���A���Ă���X�V

        foreach (var player in PhotonNetwork.PlayerList)
        {
            playersInRoom.Add(player);
        }


        // �v���C���[�e�L�X�g���X�V
        for (int i = 0; i < playerTexts.Count; i++)
        {
            if (i < playersInRoom.Count)
            {
                playerTexts[i].text = $"player{i}: {playersInRoom[i].NickName}";
            }
            else
            {
                playerTexts[i].text = $"player{i}: nan";
            }
        }

        //��l�ȏ�̂Ƃ��̓Q�[���J�n�\�ɂȂ�
        //�J�n�{�^����master�N���C�A���g�̂݉�����悤�ɂ���
        if (playersInRoom.Count >= 2 && PhotonNetwork.IsMasterClient)
        {
            startbutton.interactable = true; 
        }
        else
        {
            startbutton.interactable = false;
        }

    }



    public void RoomExit()//�ޏo�{�^��
    {
        PhotonNetwork.LeaveRoom();
        MatchmakingViewer.SetActive(true);

    }
}
