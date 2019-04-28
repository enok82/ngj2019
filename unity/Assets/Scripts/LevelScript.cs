using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelScript : MonoBehaviour
{

	public int levelWidth;
	public int levelHeight;

	public GameObject walkableTile;
	public GameObject notWalkableTile;
    public GameObject finishTile;

	private Color[] tileColors;

	public Color walkableColor;
	public Color notWalkableColor;
    public Color finishColor;

	public Texture2D levelTexture;

    public Texture2D[] levelTextures;

	public float waitTime;

    public List<GameObject> walkableTiles;

    private Animator anim;
						  

    private void OnEnable()
    {
	    //GameManager.Instance.startGameEvent += LightUpTiles;
    }

    private void OnDisable()
    {
	   // GameManager.Instance.startGameEvent -= LightUpTiles;

    }

    void Awake ()
	{
		levelWidth = levelTexture.width;
		levelHeight = levelTexture.height;
		LoadLevel ();
        anim = GetComponent<Animator>();

	}
	

	void LoadLevel ()
	{
        levelTexture = levelTextures[Random.Range(0,levelTextures.Length)];
        levelWidth = levelTexture.width;
        levelHeight = levelTexture.height;

        tileColors = new Color[levelWidth * levelHeight];
		tileColors = levelTexture.GetPixels ();

		for (int z = 0; z < levelHeight; z++) 
		{
			for (int x = 0; x < levelWidth; x++) 
			{
				if (tileColors [x + z * levelWidth] == walkableColor) 
				{	
					GameObject clone = Instantiate (notWalkableTile, new Vector3 (transform.position.x + x, 0, transform.position.z + z), Quaternion.identity);
                    clone.tag = "walkable";
                    clone.name = "Walkable Tile" + x + z;
                    clone.transform.parent = this.transform;
                    walkableTiles.Add(clone);

				}

				if (tileColors [x + z * levelWidth] == notWalkableColor) 
				{
                    GameObject clone = Instantiate(notWalkableTile, new Vector3 (transform.position.x + x, 0, transform.position.z + z), Quaternion.identity);
                    clone.tag = "notWalkable";
                    clone.name = "Not Walkable Tile" + x + z;
                    clone.transform.parent = this.transform;
                }

                if (tileColors[x + z * levelWidth] == finishColor)
                {
                    GameObject clone = Instantiate(notWalkableTile, new Vector3(transform.position.x + x, 0, transform.position.z + z), Quaternion.identity);
                    clone.tag = "finishTile";
                    clone.name = "Finish Tile" + x + z;
                    clone.transform.parent = this.transform;
                    walkableTiles.Add(clone);
                }
            }
		}
	}

































   

}