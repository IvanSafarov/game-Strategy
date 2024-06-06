using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletController : MonoBehaviour
{
	// Текущая позиция пули
	[NonSerialized] public Vector3 position;
	// Скорость движения пули
	public float speed = 30f;
	// Урон, наносимый пулей
	public int damage = 20;

	void Update()
	{
		// Вычислить расстояние, которое пуля должна пройти за этот кадр
		float step = speed * Time.deltaTime;
		// Переместить пулю к её целевой позиции
		transform.position = Vector3.MoveTowards(transform.position, position, step);
		// Если пуля достигла целевой позиции, уничтожить объект
		if (transform.position == position)
		{
			Destroy(gameObject);
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		// Проверить, если сталкивающийся объект - это враг или игрок
		if (other.CompareTag("Enemy") || other.CompareTag("Player"))
		{
			// Получить компонент CarAttak из сталкивающегося объекта
			CarAttak attak = other.GetComponent<CarAttak>();
			// Уменьшить здоровье сталкивающегося объекта на урон пули
			attak._health -= damage;

			// Получить трансформ индикатора здоровья из сталкивающегося объекта
			Transform healthBar = other.transform.GetChild(0).transform;
			// Уменьшить размер индикатора здоровья
			healthBar.localScale = new Vector3(
				healthBar.localScale.x - 0.3f,
				healthBar.localScale.y,
				healthBar.localScale.z
			);
			// Если здоровье сталкивающегося объекта 0 или меньше, уничтожить объект
			if (attak._health <= 0)
			{
				Destroy(other.gameObject);
			}
		}
	}
}

