using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Linq;

public class Interacao : MonoBehaviour
{

    [Header("Opções Interação")]
    public Text textoOpcaoEntrarInteracao;
    public Text textoInteracao;

    [Header("Configurações Personagens")]

    public string[] nomesPersonagens;
    public Sprite[] imagensPersonagens;
    public Text nomePersonagemInteracao;
    public Image imagemInteracaoFundo;
    public Image imagemInteracaoPersonagem;


    [Header("Configurações Diálogos")]

    private string nomeJogador = "Eu";
    public string nomeArquivoDialogo;
    private LinhaDialogo[] dialogos;
    private int indiceDialogo;
    private bool podeInteragir, interacaoAberta, digitando;
    private Coroutine corrotinaTexto;


    private struct LinhaDialogo
    {
        public string tagPersonagem;
        public string personagemFala;

        public LinhaDialogo(string tagPersonagem, string personagemFala)
        {
            this.tagPersonagem = tagPersonagem;
            this.personagemFala = personagemFala;
        }
    }

    void EstadoObjetos(bool estado, params GameObject[] objetos)
    {
        foreach (GameObject obj in objetos)
        {
            if (obj != null)
                obj.SetActive(estado);
        }
    }

    private void CarregarDialogosDoArquivo(string nomeArquivo)
    {
        TextAsset arquivo = Resources.Load<TextAsset>(nomeArquivo);
        if (arquivo == null)
        {
            Debug.LogError($"Arquivo '{nomeArquivo}' não encontrado!");
            return;
        }

        dialogos = arquivo.text
            .Split('\n')
            .Where(linha => linha.StartsWith("J: ") || linha.StartsWith("N"))
            .Select(linha =>
            {
                string tag = linha.Substring(0, linha.IndexOf(':')).Trim();
                string fala = linha.Substring(linha.IndexOf(':') + 1).Trim();
                return new LinhaDialogo(tag, fala);
            })
            .ToArray();
    }


    private void Start()
    {
        podeInteragir = false;
        EstadoObjetos(false,
        nomePersonagemInteracao.gameObject,
        textoOpcaoEntrarInteracao.gameObject,
        textoInteracao.gameObject,
        imagemInteracaoFundo.gameObject,
        imagemInteracaoPersonagem.gameObject);

        CarregarDialogosDoArquivo(nomeArquivoDialogo);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
    {
        textoOpcaoEntrarInteracao.gameObject.SetActive(true);
        podeInteragir = true;
    }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
    {
        textoOpcaoEntrarInteracao.gameObject.SetActive(false);
        podeInteragir = false;
        interacaoAberta = false;
        FecharInteracao();
    }

    }

    private void Update()
    {
        if (podeInteragir && Input.GetKeyDown(KeyCode.Return) && !interacaoAberta)
        {
            IniciarDialogo();
        }
        else if (interacaoAberta && Input.GetKeyDown(KeyCode.Return) && !digitando)
        {
            ProximoDialogo();
        }
        if (Input.GetKeyDown(KeyCode.Escape)) FecharInteracao();
    }

    private void IniciarDialogo()
    {
        interacaoAberta = true;
        indiceDialogo = 0;
        EstadoObjetos(true,
        nomePersonagemInteracao.gameObject,
        textoInteracao.gameObject,
        imagemInteracaoFundo.gameObject,
        imagemInteracaoPersonagem.gameObject);

        textoOpcaoEntrarInteracao.text = "Pressione Enter para continuar";
        MostrarDialogo();
    }

    private void FecharInteracao()
    {
        interacaoAberta = false;
        EstadoObjetos(false,
        nomePersonagemInteracao.gameObject,
        textoInteracao.gameObject,
        imagemInteracaoFundo.gameObject,
        imagemInteracaoPersonagem.gameObject);

        textoOpcaoEntrarInteracao.gameObject.SetActive(podeInteragir);

    }

    private void MostrarDialogo()
    {
        if (indiceDialogo >= dialogos.Length)
        {
            FecharInteracao();
            return;
        }

        string tag = dialogos[indiceDialogo].tagPersonagem;
        string fala = dialogos[indiceDialogo].personagemFala;

        if (tag == "J")
        {
            nomePersonagemInteracao.text = nomesPersonagens[0];
            imagemInteracaoPersonagem.sprite = imagensPersonagens[0];
        }
        else if (tag.StartsWith("N"))
        {
            int idNPC;
            if (int.TryParse(tag.Substring(1), out idNPC) && idNPC < nomesPersonagens.Length)
            {
                nomePersonagemInteracao.text = nomesPersonagens[idNPC];
                imagemInteracaoPersonagem.sprite = imagensPersonagens[idNPC];
            }
        }

        if (corrotinaTexto != null) StopCoroutine(corrotinaTexto);
        corrotinaTexto = StartCoroutine(DigitarTexto(fala));
    }

    private IEnumerator DigitarTexto(string frase)
    {
        digitando = true;
        textoInteracao.text = "";

        foreach (char letra in frase)
        {
            textoInteracao.text += letra;
            yield return new WaitForSeconds(0.05f);
        }

        digitando = false;


    }

    private void ProximoDialogo()
    {
        if (!digitando)
        {
            indiceDialogo++;
            MostrarDialogo();
        }
    }
}
