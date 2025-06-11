using UnityEngine;
using System.Collections.Generic;

public class LevantamentoController : MonoBehaviour
{
    public enum ModoControle
    {
        Jogador,
        IA
    }

    [Header("Configuração Geral")]
    [SerializeField] private ModoControle modo = ModoControle.Jogador;
    [SerializeField] private MovimentoBola movimentoBola;

    [Header("IA - Locais de ataque")]
    [SerializeField] private List<Transform> locaisDeLevantamentoIA;


    private Transform posicaoAtaque;

    void Start()
    {
        if (modo == ModoControle.Jogador)
        {
            GameObject jogadorAtaque = GameObject.FindGameObjectWithTag("Ataque");
            if (jogadorAtaque != null)
            {
                posicaoAtaque = jogadorAtaque.transform;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bola"))
        {
            levantamentoBola();
        }
    }

    public void levantamentoBola()
    {
        if (modo == ModoControle.IA)
        {
            if (locaisDeLevantamentoIA.Count > 0)
            {
                posicaoAtaque = locaisDeLevantamentoIA[Random.Range(0, locaisDeLevantamentoIA.Count)];
            }

        }

        if (posicaoAtaque != null && movimentoBola != null)
        {
            movimentoBola.IniciarMovimento(posicaoAtaque.position);
        }
    }
}
