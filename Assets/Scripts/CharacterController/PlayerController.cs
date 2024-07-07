using UnityEngine;
using UnityEngine.InputSystem;
using RadgarGames.Interface;

namespace RadgarGames.NavMesh.Character
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour, ICharacter, IInfluencer
    {
        private CharacterController _characterController;
        //private PlayerInput _playerInput;
        private Vector2 _movementInput;

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
        }
        
        private void Update()
        {
            Vector3 movement = new Vector3(_movementInput.x, 0.0f, _movementInput.y);
            _characterController.Move(movement * Time.deltaTime);
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            _movementInput = context.ReadValue<Vector2>();
        }

        public Transform GetTransform()
        {
            return transform;
        }
    }
}