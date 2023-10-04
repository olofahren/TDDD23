using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayInside1 : MonoBehaviour {

	public Transform MinimapCam;
	public float MinimapSize = 44.0f;
	Vector2 enemyLocation;
	Vector2 pointTransform;
    SpriteRenderer spriteRenderer;
    public Sprite farAwaySprite;
    public Sprite closeSprite;
    private List<int> completedBattles;
    private int battleNumber;



    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        battleNumber = transform.parent.GetComponentInChildren<detectPlayer>().battleNumber;
    }

    void FixedUpdate () {

        completedBattles = PlayerPrefsExtra.GetList<int>("completedBattles");
        //Debug.Log(battleNumber);

        if (completedBattles[battleNumber] == 1)
        {
            spriteRenderer.enabled = false;
        }


        enemyLocation = transform.parent.transform.position;
		pointTransform = new Vector2(transform.position.x, transform.position.y);
        // Center of Minimap
        Vector2 camPosition = MinimapCam.transform.position;

		// Distance from the gameObject to Minimap
		float Distance = Vector2.Distance(enemyLocation, camPosition);
		Vector2 heading = enemyLocation - camPosition;
        float distance = heading.magnitude;
        Vector2 direction = heading / distance;


        // If the Distance is less than MinimapSize, it is within the Minimap view and we don't need to do anything
        // But if the Distance is greater than the MinimapSize, then do this
        if (Distance > MinimapSize)
		{

			transform.position = new Vector3(camPosition.x+(direction.x*MinimapSize), camPosition.y+(direction.y*MinimapSize), -28.66f);
            spriteRenderer.sprite = farAwaySprite;

        }
		else
		{
            transform.position = new Vector3(enemyLocation.x, enemyLocation.y, -28.66f);
            spriteRenderer.sprite = closeSprite;


        }
    }
}
