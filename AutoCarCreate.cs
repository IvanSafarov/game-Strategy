using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoCarCreate : MonoBehaviour
{
	// Флаг, указывающий, что созданные машины являются врагами
	[NonSerialized] public bool IsEnemy = false;
	// Ссылка на префаб автомобиля, который будет создаваться
	public GameObject car;
	// Время между созданием автомобилей
	public float time = 5f;

	// Метод, вызываемый при запуске скрипта
	public void Start()
	{
		// Запуск корутины для создания автомобилей
		StartCoroutine(SpawnCar());
	}

	// Корутина для создания автомобилей
	IEnumerator SpawnCar()
	{
		for (int i = 1; i <= 3; i++)
		{
			// Ожидание времени перед следующим созданием
			yield return new WaitForSeconds(time);

			// Вычисление случайной позиции для создания автомобиля
			Vector3 pos = new Vector3(
				transform.GetChild(0).position.x + UnityEngine.Random.Range(3f, 7f),
				transform.GetChild(0).position.y,
				 transform.GetChild(0).position.z + UnityEngine.Random.Range(3f, 7f)
			);

			// Создание нового автомобиля
			GameObject spawn = Instantiate(car, pos, Quaternion.identity);

			// Если флаг IsEnemy установлен, установить тег "Enemy" для созданного автомобиля
			if (IsEnemy)
			{
				spawn.tag = "Enemy";
			}
		}
	}
}