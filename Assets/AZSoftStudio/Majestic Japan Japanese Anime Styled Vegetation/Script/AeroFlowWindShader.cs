using UnityEngine;

namespace AZSoftStudio.AeroFlow
{
	/// <summary>
	/// Controls AeroFlow Wind shader properties by setting global shader variables.
	/// </summary>
	public class AeroFlowWindShader : MonoBehaviour
	{
		/// <summary>
		/// The direction of the wind.
		/// </summary>
		[Tooltip("The direction of the wind as a Vector2.")]
		[SerializeField]
		[Space(10)]
		private Vector2 windDirection = new Vector2(1, 1);

		/// <summary>
		/// The bending value affected by the wind.
		/// </summary>
		[Tooltip("The bending value affected by the wind.")]
		[SerializeField]
		[Range(0, 10)]
		[Space(10)]
		private float bendingValue = 2;

		/// <summary>
		/// The speed of the wind.
		/// </summary>
		[Tooltip("The speed of the wind.")]
		[SerializeField]
		[Range(0, 20)]
		[Space(10)]
		private float windSpeed = 1;

		/// <summary>
		/// Controls the randomness of the wind, affecting areas where wind moves based object position.
		/// </summary>
		

		/// <summary>
		/// Toggles whether dynamic wind is enabled or disabled.
		/// </summary>
		[Tooltip("Enables or disables dynamic wind.")]
		[SerializeField]
		[Space(10)]
		private bool dynamicWind = false;

		/// <summary>
		/// Called when the script instance is being loaded.
		/// Updates the shader properties with initial values.
		/// </summary>
		private void Start()
		{
			UpdateShaderProperties();
		}

		/// <summary>
		/// Called when the script is loaded or a value is changed in the Inspector (Editor only).
		/// Updates the shader properties with new values.
		/// </summary>
		private void OnValidate()
		{
			UpdateShaderProperties();
		}

		/// <summary>
		/// Updates the global shader properties with current values.
		/// Checks for shader property existence to avoid runtime errors.
		/// </summary>
		private void UpdateShaderProperties()
		{
			Shader.SetGlobalVector("_Wind_Direction", windDirection);
			Shader.SetGlobalFloat("_Bending_Value", bendingValue);
			Shader.SetGlobalFloat("_Wind_Speed", windSpeed);
			Shader.SetGlobalFloat("_DynamicWind", dynamicWind ? 1f : 0f);
			
		}
	}
}
