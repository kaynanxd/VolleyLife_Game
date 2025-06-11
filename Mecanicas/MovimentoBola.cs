using UnityEngine;

public class MovimentoBola : MonoBehaviour
{
    [SerializeField] private float forcaHorizontal = 10f;
    [SerializeField] private float forcaVertical = 5f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    public void IrParaLocal(Vector3 novoLocal)
    {
        transform.position = novoLocal;
    }

    public void IniciarMovimento(Vector3 destino)
    {
        Vector3 forca = (destino - transform.position).normalized * forcaHorizontal + Vector3.up * forcaVertical;
        rb.velocity = Vector3.zero;
        rb.AddForce(forca, ForceMode.VelocityChange);
    }
}
