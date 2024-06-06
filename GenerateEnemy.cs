using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class GenerateEnemy : MonoBehaviour
{
	// Массив трансформов, представляющих точки, в которых будут создаваться вражеские объекты
	public Transform[] points;
	// Ссылка на префаб вражеского объекта, который будет создаваться
	public GameObject shed;

	// Метод, вызываемый при запуске скрипта
	private void Start()
	{
		// Запуск корутины для создания вражеских объектов
		StartCoroutine(SpawnFactory());
	}

	// Корутина для создания вражеских объектов
	IEnumerator SpawnFactory()
	{
		for (int i = 0; i < points.Length; i++)
		{
			// Ожидание 10 секунд перед следующим созданием
			yield return new WaitForSeconds(10f);

			// Создание нового вражеского объекта
			GameObject spawn = Instantiate(shed);

			// Удаление компонента PlaceObjects с созданного объекта
			Destroy(spawn.GetComponent<PlaceObjects>());

			// Установка позиции и вращения созданного объекта
			spawn.transform.position = points[i].position;
			spawn.transform.rotation = Quaternion.Euler(new Vector3(0, UnityEngine.Random.Range(0, 360), 0));

			// Включение скрипта AutoCarCreate, отвечающего за управление созданным объектом
			spawn.GetComponent<AutoCarCreate>().enabled = true;

			// Установка флага IsEnemy в true, чтобы созданный объект считался врагом
			spawn.GetComponent<AutoCarCreate>().IsEnemy = true;
		}
	}
}