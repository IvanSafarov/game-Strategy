using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class CarAttak : MonoBehaviour
{
	// Переменная для здоровья объекта
	[NonSerialized] public int _health = 100;
	// Радиус обнаружения врагов
	public float radius = 70f;
	// Ссылка на префаб пули
	public GameObject bullet;
	// Переменная для хранения текущей корутины атаки
	private Coroutine _corotine = null;

	// Метод, вызываемый каждый кадр
	void Update()
	{
		// Вызов метода для обнаружения столкновений
		DetectCollistion();
	}

	// Метод для обнаружения столкновений
	private void DetectCollistion()
	{
		// Получение списка всех столкнувшихся объектов в радиусе
		Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);

		// Если в радиусе нет больше ни одного объекта и корутина атаки была запущена
		if (hitColliders.Length == 0 && _corotine != null)
		{
			// Остановка корутины атаки
			StopCoroutine(_corotine);
			_corotine = null;

			// Если объект - враг, то вернуть его на исходную позицию
			if (gameObject.CompareTag("Enemy"))
			{
				GetComponent<NavMeshAgent>().SetDestination(gameObject.transform.position);
			}
		}

		// Перебор всех столкнувшихся объектов
		foreach (var el in hitColliders)
		{
			// Если один из объектов - игрок, а другой - враг (или наоборот)
			if ((gameObject.CompareTag("Player") && el.gameObject.CompareTag("Enemy")) ||
			(gameObject.CompareTag("Enemy") && el.gameObject.CompareTag("Player")))
			{
				// Если объект - враг, то установить цель для движения
				if (gameObject.CompareTag("Enemy"))
					GetComponent<NavMeshAgent>().SetDestination(el.transform.position);

				// Если корутина атаки не запущена, то запустить ее
				if (_corotine == null)
				{
					_corotine = StartCoroutine(StartAttack(el));
				}
			}
		}
	}

	// Корутина для атаки
	IEnumerator StartAttack(Collider enemyPos)
	{
		while (true)
		{
			// Создание новой пули и установка ее позиции
			GameObject obj = Instantiate(bullet, transform.GetChild(1).position, Quaternion.identity);
			if (enemyPos.transform != null)
			{
				obj.GetComponent<BulletController>().position = enemyPos.transform.position;
			}
			// Ожидание 1 секунды перед следующей атакой
			yield return new WaitForSeconds(1f);
			// Остановка корутины атаки
			StopCoroutine(_corotine);
			_corotine = null;
		}
	}
}