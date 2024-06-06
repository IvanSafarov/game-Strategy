using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlaceObjects : MonoBehaviour
{
	// Маска слоев, на которых можно размещать объект
	public LayerMask layer;
	// Скорость вращения объекта
	public float rotateSpeed = 60f;

	// Метод, вызываемый при запуске скрипта
	void Start()
	{
		// Вызов метода для позиционирования объекта
		PositionObject();
	}

	// Метод, вызываемый каждый кадр
	void Update()
	{
		// Вызов метода для позиционирования объекта
		PositionObject();

		// Проверка нажатия на левую кнопку мыши
		if (Input.GetMouseButtonDown(0))
		{
			// Удаление компонента PlaceObjects
			Destroy(gameObject.GetComponent<PlaceObjects>());
		}

		// Проверка нажатия на клавишу Shift
		if (Input.GetKey(KeyCode.LeftShift))
		{
			// Поворот объекта вокруг оси Y
			transform.Rotate(Time.deltaTime * Vector3.up * rotateSpeed);
		}
	}

	// Метод для позиционирования объекта
	private void PositionObject()
	{
		// Получение луча из центра экрана в направлении камеры
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		// Хранение информации о ближайшем столкновении
		RaycastHit hit;

		// Проверка столкновения луча с объектами на заданных слоях
		if (Physics.Raycast(ray, out hit, 1000f, layer))
		{
			// Установка позиции объекта в точку столкновения
			transform.position = hit.point;
		}
	}
}

