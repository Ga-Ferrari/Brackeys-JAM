using UnityEngine;

public class InteragivelTeste : ObjetoInteragivel
{

    public override bool Interagir(GameObject gameObject)
    {
        Debug.Log("Você abriu o baú!");
        return true;
    }

}