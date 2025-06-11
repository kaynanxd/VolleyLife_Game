using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class SaqueController : MonoBehaviour
{
    [Header("Configuração Geral")]
    [SerializeField] private float velocidade = 1f, pulo = 100f;
    [SerializeField] private Text textoOpcaoSaque;
    [SerializeField] private Canvas locaisSaque;
    [SerializeField] private Transform posicaoSaque;

    [Header("Barra de Força")]
    [SerializeField] private Canvas barraForca;
    [SerializeField] private RectTransform barraAcerto;
    [SerializeField] private float velocidadeBarraAcerto = 5f, esquerdaLimite = -150f, direitaLimite = 150f;
    [SerializeField] private Text textoResultadoBarra;

    private Rigidbody rg;
    private Animator animator;

    private bool chegouNaPosicaoSaque = false;
    private bool barraAtiva = false;
    private bool saqueFinalizado = false;
    private MovimentoBola movimentoBola;
    private bool localJaSelecionado = false;

    private Transform localEscolhido;

    void Start()
    {
        rg = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        movimentoBola = FindObjectOfType<MovimentoBola>();
        Physics.gravity = new Vector3(0, -120f, 0);
    }

    void Update()
    {
        if (!chegouNaPosicaoSaque)
        {
            irPontoSaque();
            return;
        }

if (chegouNaPosicaoSaque && !localJaSelecionado && Input.GetKeyDown(KeyCode.Return)) {
    locaisSaque.gameObject.SetActive(true);
    textoOpcaoSaque.text = "Clique no local que deseja sacar!";
}

if (!barraAtiva && localJaSelecionado)
{
    textoOpcaoSaque.text = "⌈ESPAÇO⌉ para selecionar a força!";
    barraForca.gameObject.SetActive(true);
    locaisSaque.gameObject.SetActive(false);
    barraAtiva = true;
}


if (barraAtiva && !saqueFinalizado)
{
    MoverBarra();

    if (Input.GetKeyDown(KeyCode.Space))
    {
        AvaliarBarra();
        textoResultadoBarra.gameObject.SetActive(true);
        saqueFinalizado = true;

        StartCoroutine(PularEIniciarMovimento());
    }
}
    }

    void irPontoSaque()
{
    Vector3 direcao = posicaoSaque.position - transform.position;
    float distanciaPosicaoSaque = direcao.magnitude;

    if (distanciaPosicaoSaque > 50f)
    {
        direcao.Normalize();
        rg.velocity = new Vector3(direcao.x * velocidade, rg.velocity.y, direcao.z * velocidade);
    }
    else
    {
        rg.velocity = Vector3.zero;
        textoOpcaoSaque.gameObject.SetActive(true);
        chegouNaPosicaoSaque = true;
    }
}

void MoverBarra()
{
    float t = Mathf.PingPong(Time.time * velocidadeBarraAcerto * 0.001f, 1f);
    float posX = Mathf.Lerp(esquerdaLimite, direitaLimite, t);
    barraAcerto.anchoredPosition = new Vector2(posX, barraAcerto.anchoredPosition.y);
}

void AvaliarBarra()
{
    float posX = barraAcerto.anchoredPosition.x;
    textoResultadoBarra.text = (posX >= -30f && posX <= 30f)
        ? "Saque perfeito!"
        : "Fora da zona!";
}

private IEnumerator PularEIniciarMovimento()
{
    rg.velocity = new Vector3(rg.velocity.x, pulo, rg.velocity.z);
    rg.AddForce(Vector3.up * pulo, ForceMode.VelocityChange);

    yield return new WaitForSeconds(0.3f);

    if (localEscolhido != null)
    {
        barraForca.gameObject.SetActive(false);
        movimentoBola.IniciarMovimento(localEscolhido.position);
    }

    this.enabled = false;
}

public void SelecionarLocal(Transform novoLocal)
{
    textoOpcaoSaque.gameObject.SetActive(false);
    localEscolhido = novoLocal;
    localJaSelecionado = true;
}
}
