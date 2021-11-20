using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParalaxEngine : MonoBehaviour // This system runs on the assumption of the camera being at 0 -500 0, orthographic, and vertical size 10
{
    [Header("Fine Toon")]
    public float m_fadeOutTime;
    public List<ParalaxLayerGroup> m_levelScene;

    [Header("Touch with Cution")]
    public float m_speedMult;

    private int m_sceneIndex;
    private List<List<GameObject>> m_layerImageGroups;
    private float screenWidth = 20.0f / ((float)Screen.height / (float)Screen.width); // 20 = vertical size * 2


    void Update()
    {
        float distance = 1 * Time.deltaTime;

        for (int f = 0; f < m_layerImageGroups.Count; f++)
        {
            //A layer is made of multiple of the same image. This for loop goes through those images and moves them together
            float alpha = 0.0f;
            for (int n = 0; n < m_layerImageGroups[f].Count; n++)
            {
                SpriteRenderer spriteR = m_layerImageGroups[f][n].GetComponent<SpriteRenderer>();

                //if layer is inactive/active, fade it out/in
                if (n == 0)
                {
                    if (!m_levelScene[m_sceneIndex].m_layers[f].m_isActive)
                    {
                        if (spriteR.color.a > 0.0f)
                        {
                            alpha = -(1 / m_fadeOutTime) * Time.deltaTime;
                        }
                        else
                        {
                            //break; Invisable layers move still so we can keep them lined up with things. If we decide that's not nessasary add this line back in for a micro scopic performance boost
                        }
                    }
                    else
                    {
                        if (spriteR.color.a < 1.0f)
                        {
                            alpha = (1 / m_fadeOutTime) * Time.deltaTime;
                        }
                    }
                }
                spriteR.color = new Color(spriteR.color.r, spriteR.color.g, spriteR.color.b, spriteR.color.a + alpha);

                //Simply move the image
                m_layerImageGroups[f][n].transform.position += Vector3.right * m_levelScene[m_sceneIndex].m_layers[f].m_scrollSpeed * distance * m_speedMult;

                //If the image has scrolled off the left side of the screen...
                if (m_layerImageGroups[f][n].transform.position.x < (-screenWidth / 2) - (spriteR.sprite.bounds.extents.x * Mathf.Abs(m_levelScene[m_sceneIndex].m_layers[f].m_scale.x) + (m_levelScene[m_sceneIndex].m_layers[f].m_gap / 2)))
                {
                    //...Move it to the right side of the screen, but relative to its size so it stays in line with its layer
                    m_layerImageGroups[f][n].transform.position += Vector3.right * ((spriteR.sprite.bounds.size.x * Mathf.Abs(m_levelScene[m_sceneIndex].m_layers[f].m_scale.x) + m_levelScene[m_sceneIndex].m_layers[f].m_gap) * m_layerImageGroups[f].Count);
                    //If the image isn't tialable (must be set manualy) and an odd number of images was generated for this layer, flip the image every time it changes sides so that the pixles transition seemlesly
                    if (!m_levelScene[m_sceneIndex].m_layers[f].m_doesTile && m_layerImageGroups[f].Count % 2 == 1)
                    {
                        spriteR.flipX = !spriteR.flipX;
                    }
                }
                //The same but for the right side of the screen
                else if (m_layerImageGroups[f][n].transform.position.x > (screenWidth / 2) + (spriteR.sprite.bounds.extents.x * Mathf.Abs(m_levelScene[m_sceneIndex].m_layers[f].m_scale.x) + (m_levelScene[m_sceneIndex].m_layers[f].m_gap / 2)))
                {
                    m_layerImageGroups[f][n].transform.position -= Vector3.right * ((spriteR.sprite.bounds.size.x * Mathf.Abs(m_levelScene[m_sceneIndex].m_layers[f].m_scale.x) + m_levelScene[m_sceneIndex].m_layers[f].m_gap) * m_layerImageGroups[f].Count);
                    if (!m_levelScene[m_sceneIndex].m_layers[f].m_doesTile && m_layerImageGroups[f].Count % 2 == 1)
                    {
                        spriteR.flipX = !spriteR.flipX;
                    }
                }
            }
        }
    }

    public void LoadNewScene(int sceneIndex)
    {
        //Destroy the current group of layers (level scene)
        for (int f = transform.childCount - 1; f >= 0; f--)
        {
            Destroy(this.transform.GetChild(f).gameObject);
        }

        m_sceneIndex = sceneIndex;
        m_layerImageGroups = new List<List<GameObject>>();

        //create each layer for the new scene
        foreach (ParalaxLayer layer in m_levelScene[sceneIndex].m_layers)
        {
            //Create a new empty game object to hold the group, move it to be under this engine, and name it
            GameObject parent = new GameObject();
            parent.transform.parent = this.transform;
            parent.name = "Paralax Layer Group";


            m_layerImageGroups.Add(new List<GameObject>());

            //Unity go brrr. Which is gamer for, images that should move seemlessly were getting an little line between them for no reason, this is an atempt to fix that.
            if (layer.m_gap == 0) layer.m_gap = -0.01f;

            //How many images are needed to span the screen
            int imageCountNeeded = (int)(screenWidth / ((layer.m_sprite.bounds.size.x) * Mathf.Abs(layer.m_scale.x) + layer.m_gap)) + 2;
            for (int f = 0; f < imageCountNeeded; f++)
            {
                //Create image object
                GameObject layerItem = new GameObject();
                layerItem.transform.parent = parent.transform;
                layerItem.name = "Layer Item " + f;
                m_layerImageGroups[m_layerImageGroups.Count - 1].Add(layerItem);
                //Make image object an image
                SpriteRenderer spriteR = m_layerImageGroups[m_layerImageGroups.Count - 1][f].AddComponent<SpriteRenderer>();
                spriteR.sprite = layer.m_sprite;
                spriteR.transform.localScale *= layer.m_scale;
                if (!layer.m_doesTile && f % 2 == 1)
                {
                    spriteR.flipX = true;
                }

                //No one cares about the z axis nerd. so like, just put it not 0.0 and then move them little by little so that they don't clip (and so that they apear in order). And put forground closer isntead of further
                float z = 10.0f + ((m_layerImageGroups.Count - 1) * 0.1f);
                if (layer.m_isForeground) z -= 20.0f;
                //The very first image gets placed specificaly where told to by editor. Large values will put it off the top/bottom, but left and right will fix them self
                if (f == 0)
                {
                    m_layerImageGroups[m_layerImageGroups.Count - 1][0].transform.position = new Vector3(layer.m_positionOffSet.x, layer.m_positionOffSet.y, z);
                }
                //the rest of the images get placed in line relative to the first, and the update function will take care of moving them to the other side of the first if needed
                else
                {
                    m_layerImageGroups[m_layerImageGroups.Count - 1][f].transform.position = m_layerImageGroups[m_layerImageGroups.Count - 1][0].transform.position
                            + (Vector3.right * (spriteR.sprite.bounds.size.x * Mathf.Abs(layer.m_scale.x) + layer.m_gap) * f);
                }

                //If layer inactive, make it invisable
                if (!layer.m_isActive)
                {
                    spriteR.color = new Color(spriteR.color.r, spriteR.color.g, spriteR.color.b, 0.0f);
                }
            }
        }
    }
}
