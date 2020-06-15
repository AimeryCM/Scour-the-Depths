using UnityEngine;
using TMPro;

public class DamageTextScript : MonoBehaviour
{
	private TextMeshPro tmpro;
	private Color color;
	private float timeSinceCreation = 0;
	[SerializeField] private float textSpeed;
	[SerializeField] private float timeToFade;
	[SerializeField] private float fadeSpeed;

	public void Setup(int damage, bool crit)
	{
		tmpro = GetComponent<TextMeshPro>();
		tmpro.SetText(damage.ToString());
		if(crit)
			color = Color.red;
		else
			color = Color.green;
		tmpro.color = color;
	}

	void FixedUpdate()
	{
		transform.position += new Vector3(0, textSpeed * Time.fixedDeltaTime, 0);
		timeSinceCreation += Time.fixedDeltaTime;
		if(timeSinceCreation > timeToFade)
		{
			color.a -= fadeSpeed * Time.fixedDeltaTime;
			if(color.a < 0f)
				Destroy(gameObject);
			tmpro.color = color;
		}
	}

}