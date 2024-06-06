using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPlaceBuild : MonoBehaviour
{
	// Ссылка на префаб здания, которое будет размещаться
	public GameObject building;

	// Метод, вызываемый при нажатии на кнопку
	public void PlaceBuild()
	{
		// Создание нового экземпляра здания в позиции (0, 0, 0) с нулевым вращением
		Instantiate(building, Vector3.zero, Quaternion.identity);
	}
}
