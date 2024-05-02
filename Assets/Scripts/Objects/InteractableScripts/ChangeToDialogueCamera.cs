using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;

public class ChangeToDialogueCamera : MonoBehaviour
{
    [Header("Cameras")]
    [SerializeField] private CinemachineVirtualCamera dialogueCamera;
    [SerializeField] private CinemachineFreeLook thirdPersonCamera;

    private const int dialogueCameraPriority = 20;
    private const int thirdPersonCameraPriority = 20;

    public event Action OnStartDialogueEvent;
    public event Action OnEndDialogueEvent;

    private void OnEnable()
    {
        NPC.OnStartDialouge += SwitchToDialogueCamera;
        NPC.OnEndDialogue += SwitchToThirdPerson;
    }
    private void OnDisable()
    {
        NPC.OnStartDialouge -= SwitchToDialogueCamera;
        NPC.OnEndDialogue -= SwitchToThirdPerson;
    }
    public void SwitchToDialogueCamera()
    {
        dialogueCamera.Priority = dialogueCameraPriority;
        thirdPersonCamera.Priority = 0;
    }
    public void SwitchToThirdPerson()
    {
        thirdPersonCamera.Priority = thirdPersonCameraPriority;
        dialogueCamera.Priority = 0;
    }
}