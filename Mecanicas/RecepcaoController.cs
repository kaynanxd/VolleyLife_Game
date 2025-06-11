using UnityEngine;

public class RecepcaoController : MonoBehaviour
{
    private Transform posicaoLevantador;
    public MovimentoBola movimentoBola;

    void Start()
    {
        GameObject levantador = GameObject.FindGameObjectWithTag("Levantador");
        if (levantador != null)
        {
            posicaoLevantador = levantador.transform;
        }
        else
        {
            Debug.LogWarning("Objeto com a tag 'Levantador' não foi encontrado na cena.");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bola"))
        {
            passarLevantador();
        }
    }

    public void passarLevantador()
    {
        if (posicaoLevantador != null && movimentoBola != null)
        {
            movimentoBola.IniciarMovimento(posicaoLevantador.position);
        }
        else
        {
            Debug.LogWarning("posicaoLevantador ou movimentoBola não está atribuído.");
        }
    }
}
