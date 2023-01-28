using UnityEngine;
using UnityEditor;
using System.Collections;

//Aquired from Asset, slightly modified

[CustomPropertyDrawer(typeof(vCell))]
public class CustPropertyDrawer : PropertyDrawer {


	public override void OnGUI(Rect position,SerializedProperty property,GUIContent label){
		EditorGUI.PrefixLabel(position,label);
		Rect newposition = position;
		newposition.y += 18f;
		SerializedProperty data = property.FindPropertyRelative("stack");
		//data.rows[0][]
		for(int j=0;j<3;j++){
			SerializedProperty row = data.GetArrayElementAtIndex(j).FindPropertyRelative("cam");
			newposition.height = 18f;
			if(row.arraySize != 3)
				row.arraySize = 3;
			newposition.width = position.width/3;
			for(int i=0;i<3;i++){
				EditorGUI.PropertyField(newposition,row.GetArrayElementAtIndex(i),GUIContent.none);
				newposition.x += newposition.width;
			}

			newposition.x = position.x;
			newposition.y += 18f;
		}
	}

	public override float GetPropertyHeight(SerializedProperty property,GUIContent label){
		return 18f * 8;
	}
}
