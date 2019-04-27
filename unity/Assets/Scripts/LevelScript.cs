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
					Instantiate (walkableTile, new Vector3 (x, y), Quaternion.identity);	
				}

				if (tileColors [x + y * levelWidth] == notWalkableColor) 
				{	
					Instantiate (notWalkableTile, new Vector3 (x, y), Quaternion.identity);
				}			
			}
		}
	}
}