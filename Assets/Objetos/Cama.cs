using UnityEngine;

public class Cama : ObjetoInteragivel
{
    public override bool Interagir(GameObject gameObject)
    {
        EventBus.DispararOnDormirCama();
        return true;
    }
}