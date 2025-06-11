using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float velocidade;
    public float velocidade_direcao;
    public float pulo;
    public float rotacaoHorizontal;
    private Animator animator;
    private Rigidbody rg;

    private float movimento_andar_lados;
    private float movimento_andar_direcao;

    private bool ta_pulando;
    private bool pulo_duplo;

    void Start()
    {
        rg = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        rg.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

    }

    void Update()
    {
        Movimentacao_lados();
        Pular();
    }

    void Movimentacao_lados()
    {
        movimento_andar_lados = Input.GetAxisRaw("Horizontal");
        movimento_andar_direcao = Input.GetAxisRaw("Vertical");
        Vector3 movimento = new Vector3(movimento_andar_lados, 0f, movimento_andar_direcao);

        if (ta_pulando)
        {
            movimento_andar_lados = 0;
            movimento_andar_direcao = 0;
        }


        rg.linearVelocity = new Vector3(movimento.x * velocidade, rg.linearVelocity.y, movimento.z * velocidade_direcao);


        if (movimento_andar_lados != 0 || movimento_andar_direcao != 0)
        {
            animator.SetBool("Correndo", true);
        }
        else
        {
            animator.SetBool("Correndo", false);
        }

	if (movimento_andar_lados > 0f)
	{
   	 transform.eulerAngles = new Vector3(rotacaoHorizontal, 0f, transform.eulerAngles.z);
	}
	else if (movimento_andar_lados < 0f)
	{
  	  transform.eulerAngles = new Vector3(-rotacaoHorizontal, 180f, transform.eulerAngles.z);
	}

    }

    void Pular()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (!ta_pulando)
            {
                rg.linearVelocity = new Vector3(rg.linearVelocity.x, pulo, rg.linearVelocity.z);
                pulo_duplo = true;
                animator.SetBool("Pulando", true);

            }
            if (pulo_duplo)
            {
                rg.linearVelocity = new Vector3(rg.linearVelocity.x, pulo, rg.linearVelocity.z);
                pulo_duplo = false;

            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 6)
        {
            ta_pulando = false;
            animator.SetBool("Pulando", false);
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == 6)
        {
            ta_pulando = true;
        }
    }

}