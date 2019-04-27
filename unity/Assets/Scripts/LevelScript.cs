using UnityEngine;
using System.Collections;

public class LevelScript : MonoBehaviour
{

	public int levelWidth;
	public int levelHeight;

	public GameObject walkableTile;
	public GameObject notWalkableTile;

	private Color[] tileColors;

	public Color walkableColor;
	public Color notWalkableColor;

	public Texture2D levelTexture;


	void Start ()
	{
		levelWidth = levelTexture.width;
		levelHeight = levelTexture.height;
		LoadLevel ();	
	}
	

	void LoadLevel ()
	{
		tileColors = new Color[levelWidth * levelHeight];
		tileColors = levelTexture.GetPixels ();

		for (int y = 0; y < levelHeight; y++) 
		{
			for (int x = 0; x < levelWidth; x++) 
			{
				if (tileColors [x + y * levelWidth] == walkableColor) 
				{	
					GameObject clone = Instantiate (walkableTile, new Vector3 (x, 0, y), Quaternion.identity);
                    clone.tag = "walkable";
                    clone.name = "Walkable Tile" + x + y;

				}

				if (tileColors [x + y * levelWidth] == notWalkableColor) 
				{
                    GameObject clone = Instantiate(notWalkableTile, new Vector3 (x, 0, y), Quaternion.identity);
                    clone.tag = "notWalkable";
                    clone.name = "Not Walkable Tile" + x + y;
                }			
			}
		}
	}
    
}