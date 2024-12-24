using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Player player;
    private PlayerControlsManager playerControls;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();
        playerControls = PlayerControlsManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        player.HandleBooleans(playerControls.IsMovementPressed, playerControls.IsRunPressed, playerControls.IsJumpPressed, playerControls.IsCrouchPressed);
        player.currMovement = new Vector3(playerControls.CurrentMovement.x, player.currMovement.y, playerControls.CurrentMovement.z);
    }
}
