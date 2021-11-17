using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] public class ParalaxLayerGroup
{
    public List<ParalaxLayer> m_layers;
}
// FrIcKeN UnItY... will let you make a list of lists with the inner list editable in the editor if you make it with a seprate class, but wont let you just make a list of lists. So I had to make a new class with just a list that
// I can put in a list instead of just having a list of lists of layers