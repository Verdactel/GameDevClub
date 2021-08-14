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
        screenWidth = 20.0f / ((float)Screen.height / (float)Screen.width); // 20 = vertical size * 2

        foreach(ParalaxLayer layer in m_layers)
        {
            GameObject parent = new GameObject();
            parent.transform.parent = this.transform;
            parent.name = "Paralax Layer Group";

            m_layerImageGroups.Add(new List<GameObject>());

            if (!layer.m_isActive)
            {
                parent.name += " - Inactive";
                continue;
            }

            int imageCountNeeded = (int)(screenWidth / ((layer.m_sprite.bounds.size.x) * Mathf.Abs(layer.m_scale.x) + layer.m_gap)) + 2;
            for (int f = 0; f < imageCountNeeded; f++)
            {
                GameObject layerItem = new GameObject();
                layerItem.transform.parent = parent.transform;
                layerItem.name = "Layer Item " + f;
                m_layerImageGroups[m_layerImageGroups.Count - 1].Add(layerItem);
                SpriteRenderer spriteR = m_layerImageGroups[m_layerImageGroups.Count - 1][f].AddComponent<SpriteRenderer>();
                spriteR.sprite = layer.m_sprite;
                spriteR.transform.localScale *= layer.m_scale;
                if(!layer.m_doesTile && f % 2 == 1)
                {
                    spriteR.flipX = true;
                }


                float z = 10.0f + ((m_layerImageGroups.Count - 1) * 0.1f);
                if (layer.m_isForeground) z -= 20.0f;
                if (f == 0)
                {
                    m_layerImageGroups[m_layerImageGroups.Count - 1][0].transform.position = new Vector3(layer.m_positionOffSet.x, layer.m_positionOffSet.y, z);
                }
                else
                {
                    m_layerImageGroups[m_layerImageGroups.Count - 1][f].transform.position = m_layerImageGroups[m_layerImageGroups.Count - 1][0].transform.position
                            + (Vector3.right * (spriteR.sprite.bounds.size.x * Mathf.Abs(layer.m_scale.x) + layer.m_gap) * f);
                }
            }
        }
    }


    void Update()
    {
        float distance = 1 * Time.deltaTime;

        for(int f = 0; f < m_layerImageGroups.Count; f++)
        {
            if (!m_layers[f].m_isActive)
            {
                continue;
            }

            for (int n = 0; n < m_layerImageGroups[f].Count; n++)
            {
                SpriteRenderer spriteR = m_layerImageGroups[f][n].GetComponent<SpriteRenderer>();

                //Move background
                m_layerImageGroups[f][n].transform.position += Vector3.right * m_layers[f].m_scrollSpeed * distance;

                //If scrolled off left, move to right, and vice versa
                if (m_layerImageGroups[f][n].transform.position.x < (-screenWidth / 2) - (spriteR.sprite.bounds.extents.x * Mathf.Abs(m_layers[f].m_scale.x) + (m_layers[f].m_gap / 2)))
                {
                    m_layerImageGroups[f][n].transform.position += Vector3.right * ((spriteR.sprite.bounds.size.x * Mathf.Abs(m_layers[f].m_scale.x) + m_layers[f].m_gap) * m_layerImageGroups[f].Count);
                    if (!m_layers[f].m_doesTile && m_layerImageGroups[f].Count % 2 == 1)
                    {
                        spriteR.flipX = !spriteR.flipX;
                    }
                }
                else if (m_layerImageGroups[f][n].transform.position.x > (screenWidth / 2) + (spriteR.sprite.bounds.extents.x * Mathf.Abs(m_layers[f].m_scale.x) + (m_layers[f].m_gap / 2)))
                {
                    m_layerImageGroups[f][n].transform.position -= Vector3.right * ((spriteR.sprite.bounds.size.x * Mathf.Abs(m_layers[f].m_scale.x) + m_layers[f].m_gap) * m_layerImageGroups[f].Count);
                    if (!m_layers[f].m_doesTile && m_layerImageGroups[f].Count % 2 == 1)
                    {
                        spriteR.flipX = !spriteR.flipX;
                    }
                }
            }
        }
    }
}
