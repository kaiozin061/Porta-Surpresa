using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class MovimentoJogador : MonoBehaviour
{

    [Header("Movimento")]

    public float velocidade = 3f;
    public float gravidade = -9.8f;

    [Header("Mouse")]
    public float sensibilidadeMouse = 1.5f;

    [Header("Animação")]
    public Animator animator;

    CharacterController controle;
    float velocidadeQueda;
    
    void Start()
    {
        controle = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensibilidadeMouse;
        transform.Rotate(0f, mouseX, 0f);

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 direcao = transform.right * horizontal + transform.forward * vertical;

        if (controle.isGrounded && velocidadeQueda < 0f)
            velocidadeQueda = -2f;
        velocidadeQueda += gravidade * Time.deltaTime;

        Vector3 movimento = direcao * velocidade;
        movimento.y = velocidadeQueda;
        controle.Move(movimento * Time.deltaTime);

        if (animator != null)
        {
            animator.SetFloat("Velocidade", direcao.magnitude);
        }

    }
}
