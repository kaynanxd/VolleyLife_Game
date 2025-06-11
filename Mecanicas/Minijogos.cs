using UnityEngine;
using UnityEngine.UI;

public class Minijogos : MonoBehaviour
{
    [Header("Menu Principal")]
    public Text tituloMenuMJ;
    public Image fundoMenuMJ;
    public Button btnMenuMJ1;
    public Button btnMenuMJ2;

    [Header("Sistema de Minijogos")]
    public Text tituloSysMJ;
    public Image fundoSysMJ;
    public Image imagemBandeira;
    public InputField inputResposta; // Campo onde a pessoa digita
    public Button btnVerificar; // Botão "Verificar"
    public Text textoResultado; // Mostra "Acertou!" ou "Errou!"

    [Header("Dados das Bandeiras")]
    public Bandeira[] bandeiras;

    private int indiceAtual = -1;

    void Start()
    {
        btnMenuMJ1.onClick.AddListener(IniciarMinijogo);
    }

    void EstadoObjetos(bool estado, params GameObject[] objetos)
    {
        foreach (GameObject obj in objetos)
        {
            if (obj != null)
                obj.SetActive(estado);
        }
    }

    public void IniciarMinijogo()
    {
        EstadoObjetos(true, tituloSysMJ.gameObject, fundoSysMJ.gameObject, imagemBandeira.gameObject, inputResposta.gameObject, btnVerificar.gameObject, textoResultado.gameObject);
        MostrarNovaBandeira();
    }

    void MostrarNovaBandeira()
    {
        inputResposta.text = "";
        textoResultado.text = "";

        indiceAtual = Random.Range(0, bandeiras.Length);
        imagemBandeira.sprite = bandeiras[indiceAtual].imagem;
    }
}