using UnityEditor;
using UnityEngine;
using AZSoftStudio.AeroFlow;

namespace AZSoftStudio.AeroFlow.Lite.GUI
{
	[CustomEditor(typeof(AeroFlowWindShader))]
	public class AeroFlowWindShaderEditor : Editor
	{
		// Serialized properties
		private SerializedProperty windDirection;
		private SerializedProperty bendingValue;
		private SerializedProperty windSpeed;
		private SerializedProperty dynamicWind;

		// Enum for wind direction mode
		private enum WindDirectionMode { Basic, Advanced }
		private WindDirectionMode windDirectionMode = WindDirectionMode.Basic;

		// Angle for controlling wind direction
		private float windAngle = 0f; // Angle in degrees (0 to 360)

		private void OnEnable()
		{
			// Cache serialized properties
			windDirection = serializedObject.FindProperty("windDirection");
			bendingValue = serializedObject.FindProperty("bendingValue");
			windSpeed = serializedObject.FindProperty("windSpeed");
			dynamicWind = serializedObject.FindProperty("dynamicWind");

			// Initialize angle from current windDirection
			Vector2 direction = windDirection.vector2Value;
			windAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
			if (windAngle < 0) windAngle += 360f; // Ensure angle is positive
		}

		public override void OnInspectorGUI()
		{
			// Load serialized data into the properties
			serializedObject.Update();

			// Header styles
			GUIStyle headerStyle = new GUIStyle(EditorStyles.boldLabel)
			{
				fontStyle = FontStyle.Bold,
				fontSize = 14
			};

			GUIStyle subHeaderStyle = new GUIStyle(EditorStyles.label)
			{
				fontStyle = FontStyle.Italic,
				fontSize = 12,
				normal = { textColor = new Color(0.8f, 0.8f, 0.8f) }
			};

			// Main title
			GUILayout.Space(10);
			GUIStyle boldStyle = new GUIStyle(EditorStyles.label)
			{
				fontStyle = FontStyle.Bold,
				fontSize = 19,
				alignment = TextAnchor.MiddleCenter,
				normal = { textColor = Color.white }
			};
			GUILayout.Label("Aero Flow Wind System Lite", boldStyle);

			GUILayout.Space(15);

			// Wind Direction Section with Dropdown Menu
			GUILayout.BeginHorizontal();
			GUILayout.Label("Wind Direction", headerStyle);
			GUILayout.FlexibleSpace(); // Push the dropdown to the right
			windDirectionMode = (WindDirectionMode)EditorGUILayout.EnumPopup(windDirectionMode, GUILayout.Width(100));
			GUILayout.EndHorizontal();

			// Wind Direction Control (Based on Selected Mode)
			if (windDirectionMode == WindDirectionMode.Advanced)
			{
				// Advanced Mode: Show wind direction as a Vector2 field
				GUILayout.Label("Advanced Mode", subHeaderStyle);
				windDirection.vector2Value = EditorGUILayout.Vector2Field("Wind Direction (X, Y)", windDirection.vector2Value);
			}
			else
			{
				// Basic Mode: Show the angle control (circle + slider)
				GUILayout.Label("Basic Mode", subHeaderStyle);

				GUILayout.Space(10);
				GUILayout.Label("Wind Direction (Angle)", subHeaderStyle);

				// Draw the circle with the dot
				DrawCircleWithDot(windAngle);

				// Slider for wind angle (0–360 degrees)
				windAngle = EditorGUILayout.Slider("Angle", windAngle, 0f, 360f);

				// Calculate X and Y based on the angle
				float angleRadians = windAngle * Mathf.Deg2Rad;
				windDirection.vector2Value = new Vector2(Mathf.Cos(angleRadians), Mathf.Sin(angleRadians));
			}

			// Add some spacing before moving to the next section
			GUILayout.Space(25);

			// Other core properties (these are always visible)
			GUILayout.Label("Core Wind Properties", headerStyle);
			GUILayout.Label("Basic settings for wind behavior in the scene.", subHeaderStyle);

			EditorGUILayout.PropertyField(bendingValue);
			EditorGUILayout.PropertyField(windSpeed);

			GUILayout.Space(25);

			// Advanced Wind Dynamics Section
			GUILayout.Label("Advanced Wind Dynamics", headerStyle);
			GUILayout.Label("Dynamic and randomness controls for wind effects.", subHeaderStyle);

			EditorGUILayout.PropertyField(dynamicWind);

			GUILayout.Space(15);

			// Apply modified properties
			serializedObject.ApplyModifiedProperties();
		}


		public static class CreateAeroFlowWindMenu
		{
			[MenuItem("Tools/AZSoftStudio/Create Aero Flow Wind", false, 5)]
			private static void CreateAeroFlowWindObject()
			{
				var existingObject = Object.FindFirstObjectByType<AZSoftStudio.AeroFlow.AeroFlowWindShader>();

				if (existingObject != null)
				{
					// Warn the user in the console and focus on the existing GameObject
					Debug.LogWarning($"Aero Flow Wind System already exists in this scene: {existingObject.name}");
					Selection.activeGameObject = existingObject.gameObject; // Focus the existing object in the Hierarchy
					return;
				}

				// Create a new GameObject named "Aero Flow Wind"
				GameObject aeroFlowWindObject = new GameObject("Aero Flow Wind");

				// Attach the AeroFlowWindShader script to the GameObject
				aeroFlowWindObject.AddComponent<AZSoftStudio.AeroFlow.AeroFlowWindShader>();

				// Select the new GameObject in the hierarchy
				Selection.activeGameObject = aeroFlowWindObject;

				// Print a message to the console
				Debug.Log("Successfully created a new Aero Flow Wind System.");
			}
		}

/// <summary>
/// Draws a circle with a dot representing the current angle.
/// The dot's position updates based on the angle.
/// </summary>
/// <param name="currentAngle">The current angle (in degrees).</param>
private void DrawCircleWithDot(float currentAngle)
		{
			// Define the size of the circle
			float circleSize = 100f; // Diameter of the circle
			float halfCircleSize = circleSize / 2f;

			// Reserve space for the circle
			Rect circleRect = GUILayoutUtility.GetRect(circleSize, circleSize);
			Vector2 circleCenter = circleRect.center;

			// Draw the circle
			Handles.color = Color.gray;
			Handles.DrawWireDisc(circleCenter, Vector3.forward, halfCircleSize);

			// Draw the "+" cross
			Handles.color = Color.white;
			Handles.DrawLine(
				new Vector3(circleCenter.x - halfCircleSize, circleCenter.y, 0),
				new Vector3(circleCenter.x + halfCircleSize, circleCenter.y, 0)
			);
			Handles.DrawLine(
				new Vector3(circleCenter.x, circleCenter.y - halfCircleSize, 0),
				new Vector3(circleCenter.x, circleCenter.y + halfCircleSize, 0)
			);

			// Calculate the dot's position on the circumference
			float angleRadians = currentAngle * Mathf.Deg2Rad;
			Vector2 dotPosition = new Vector2(
				circleCenter.x + Mathf.Cos(angleRadians) * halfCircleSize,
				circleCenter.y - Mathf.Sin(angleRadians) * halfCircleSize // Y is inverted in GUI
			);

			// Draw the dot
			Handles.color = Color.green;
			Handles.CircleHandleCap(0, dotPosition, Quaternion.identity, 5f, EventType.Repaint);
		}
	}
}
