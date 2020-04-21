using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    [SerializeField] Vector3 cameraOffset;
    [SerializeField]float damping;

    Transform cameraLookTarget;
    public Player localPlayer;

    void Awake()
    {
        GameManager.Instance.OnLocalPlayerJoined += HandLeLocalPlayerJoined;
    }
    void HandLeLocalPlayerJoined(Player player)
    {
        localPlayer = player;
        cameraLookTarget = localPlayer.transform.Find("cameraLookTarget");
        if (cameraLookTarget == null)
            cameraLookTarget = localPlayer.transform;
    }

    void Update()
    {
        Vector3 targetPosition = cameraLookTarget.position + localPlayer.transform.forward * cameraOffset.z +
            localPlayer.transform.up * cameraOffset.y +
            localPlayer.transform.right * cameraOffset.x;
        Quaternion targetRotation=Quaternion.LookRotation(cameraLookTarget.position-targetPosition,Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, damping * Time.deltaTime);

        transform.position = Vector3.Lerp(transform.position, targetPosition, damping * Time.deltaTime);
        
    }
}
