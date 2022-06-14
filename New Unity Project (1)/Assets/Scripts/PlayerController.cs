using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    
    
    private Controles _Controles;
    private PlayerInput _playerinput;
    private Camera _mainCamera;
    private Rigidbody _rigidbody;

    private Vector2 _moveInput;
    
    
    private void OnEnable()
    {
        //inicializacao de variavel
        _Controles = new Controles();
        
        //referencias dos componentes no mesmo objeto do unity
        _playerinput = GetComponent<PlayerInput>();
        _rigidbody = GetComponent<Rigidbody>();
        
        //referencias para a camera main guardada no class input
        _mainCamera = Camera.main;
        
        //atribuido ao delegate do action triggered no player input
        _playerinput.onActionTriggered += onActionTriggered;
    }

    private void onDisable()
    {
      //retirando a atribuicao ao delegate 
      _playerinput.onActionTriggered += onActionTriggered;
    }

    private void onActionTriggered(InputAction.CallbackContext obj)
    {
        //comparando o nome do action que esta chegando com o nome da actions de mover
        if (obj.action.name.CompareTo(_Controles.Newactionmap.moviment.name) == 0)
        {
           //atribuir ao moveInput o valor proveniente do Input como um vector2
           _moveInput = obj.ReadValue<Vector2>();
        }
        
    }

    private void Move()
    {
        //calcula o movimento do eixo da camera para o movimento frente/tras
        Vector3 moveVertical = _mainCamera.transform.forward * _moveInput.y;
        
        //calcula o movimento no eixo da camera para o movimento esquerda/direita
        Vector3 moveHorizontal = _mainCamera.transform.right * _moveInput.x;
        
        //adciona a força no objeto atravez do rigidbody, com intensidade definida por move speed
        _rigidbody.AddForce((moveVertical + moveHorizontal) * moveSpeed * Time.fixedDeltaTime);
    }


    private void FixedUpdate()
    {
        Move();
    }
}
