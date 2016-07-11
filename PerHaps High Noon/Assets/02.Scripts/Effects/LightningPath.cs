using UnityEngine;
using System.Collections;

namespace BLINDED_AM_ME{
    
	public class LightningPath : MonoBehaviour {

		public float strikeFrequency = 0.5f;
		float strikeTracker = 0.0f;
		
		public float smoothness = 0.5f;
		public float zigZagIntensity = 5.0f;
		public float zigZagPerMeter = 5.0f;
		
		public LineRenderer[] lineRenderers;
		private int line_iterator = 0;

		private Vector3[] pathPoints;
		
		// Update is called once per frame
		void Update () {
		
			strikeTracker += Time.deltaTime;
			if(strikeTracker >= strikeFrequency){
				strikeTracker = 0.0f;


				LightningStrike.Strike(path:pathPoints,
				lineObject:lineRenderers[line_iterator],
				zigZagIntensity:zigZagIntensity,
				zigZagPerMeter:zigZagPerMeter,
				smoothness:smoothness);

				lineRenderers[line_iterator].GetComponent<Animator>().Play("Fade", 0, 0.0f);

				line_iterator = (line_iterator + 1) % lineRenderers.Length;
			}
		}
	}
}