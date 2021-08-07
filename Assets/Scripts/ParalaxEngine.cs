using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParalaxEngine : MonoBehaviour
{
    // This system runs on the assumption of the camera being at 0 -500 0, orthographic, and vertical size 10

    [SerializeField] public ParalaxLayer[] m_layers;


    private List<List<GameObject>> m_layerImageGroups = new List<List<GameObject>>();
    private float screenWidth;

    private void Start()
    {
        screenWidth = 10.0f / ((float)Screen.height / (float)Screen.width);

        foreach(ParalaxLayer layer in m_layers)
        {
            GameObject parent = new GameObject();
            parent.transform.parent = this.transform;
            parent.name = "Paralax Layer Group";

            int imageCountNeeded = (int)(screenWidth / ((layer.m_sprite.bounds.size.x + layer.m_gap) * layer.m_scale.x)) + 2;
            m_layerImageGroups.Add(new List<GameObject>());
            for (int f = 0; f < imageCountNeeded; f++)
            {
                GameObject layerItem = new GameObject();
                layerItem.transform.parent = parent.transform;
                layerItem.name = "Layer Item " + f;
                m_layerImageGroups[m_layerImageGroups.Count - 1].Add(layerItem);
                SpriteRenderer m_sprite = m_layerImageGroups[m_layerImageGroups.Count - 1][f].AddComponent<SpriteRenderer>();
                m_sprite.sprite = layer.m_sprite;
                m_sprite.transform.localScale *= layer.m_scale;
                if(layer.m_doesTile && f % 2 == 1)
                {
                    m_sprite.flipX = true;
                }

                if (f == 0)
                {
                    float z = 100.0f + m_layerImageGroups.Count - 1;
                    if (layer.m_isForeground) z -= 200;
                    m_layerImageGroups[m_layerImageGroups.Count - 1][f].transform.position = new Vector3(layer.m_positionOffSet.x, layer.m_positionOffSet.y, 100.0f + m_layerImageGroups.Count - 1);
                }
            }
        }
    }


    void Update()
    {
        float distance = 1 * Time.deltaTime;

        for(int f = 0; f < m_layerImageGroups.Count; f++)
        {
            bool flip = m_layers[f].m_doesTile;

            Vector3 posCurrent = m_layerImageGroups[f][0].transform.position;
            Vector3 posNew = posCurrent + Vector3.right * m_layers[f].m_scrollSpeed * distance;
            m_layerImageGroups[f][0].transform.position = posNew;
            if (m_layerImageGroups[f][0].transform.position.x < (-screenWidth / 2) - (m_layerImageGroups[f][0].GetComponent<SpriteRenderer>().sprite.bounds.extents.x + (m_layers[f].m_gap / 2)))
            {
                m_layerImageGroups[f][0].transform.position += Vector3.right * (m_layerImageGroups[f][0].GetComponent<SpriteRenderer>().sprite.bounds.size.x + m_layers[f].m_gap);
                if (flip)
                {
                    m_layerImageGroups[f][0].GetComponent<SpriteRenderer>().flipX = !m_layerImageGroups[f][0].GetComponent<SpriteRenderer>().flipX;
                }
            }
            else if (m_layerImageGroups[f][0].transform.position.x > (-screenWidth / 2) + (m_layerImageGroups[f][0].GetComponent<SpriteRenderer>().sprite.bounds.extents.x + (m_layers[f].m_gap / 2)))
            {
                m_layerImageGroups[f][0].transform.position -= Vector3.right * (m_layerImageGroups[f][0].GetComponent<SpriteRenderer>().sprite.bounds.size.x + m_layers[f].m_gap);
                if (flip)
                {
                    m_layerImageGroups[f][0].GetComponent<SpriteRenderer>().flipX = !m_layerImageGroups[f][0].GetComponent<SpriteRenderer>().flipX;
                }
            }
            else
            {
                flip = false;
            }
            for (int n = 1; n < m_layerImageGroups[f].Count; n++)
            {
                m_layerImageGroups[f][n].transform.position = posNew + (Vector3.right * m_layerImageGroups[f][0].GetComponent<SpriteRenderer>().sprite.bounds.size.x * n);
                if (flip)
                {
                    m_layerImageGroups[f][n].GetComponent<SpriteRenderer>().flipX = !m_layerImageGroups[f][n].GetComponent<SpriteRenderer>().flipX;
                }
            }
        }
    }
}
