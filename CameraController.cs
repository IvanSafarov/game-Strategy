using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
  public float rotateSpeed=10.0f, speed=10.0f, zoomSpeed=400.0f;// Скорости вращения, перемещения и зума камеры
	private float _mult=1f;// Множитель скорости, используется для ускорения движения при нажатии Shift
	private void Update(){

        float hor = Input.GetAxis("Horizontal"); // Получаем значение горизонтальной оси (AD)
		float ver = Input.GetAxis("Vertical"); // Получаем значение вертикальной оси (WS)
        float rotate = 0f;//Устанавливаем значение поворота по умолчанию

		if (Input.GetKey(KeyCode.Q)){
        rotate=-1f;// Если нажата Q, поворачиваем камеру против часовой стрелки
		}
    else if(Input.GetKey(KeyCode.E)){
        rotate=1f;// Если нажата E, поворачиваем камеру по часовой стрелке
		}
    _mult=Input.GetKey(KeyCode.LeftShift) ? 2f: 1f; // Если нажат LeftShift, _mult = 2f (ускорение), иначе _mult = 1f (обычная скорость)

		transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime * rotate * _mult, Space.Self); // Поворачиваем камеру вокруг вертикальной оси

		Vector3 moveDir = new Vector3(hor, 0, ver);// Получаем направление движения камеры в локальных координатах

		moveDir = transform.TransformDirection(moveDir);// Переводим направление движения в глобальные координаты

		transform.position += moveDir * Time.deltaTime * _mult * speed;// Перемещаем камеру относительно глобальных координат

		transform.position += Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * zoomSpeed * transform.up;// Зум камеры с помощью колесика мыши

		transform.position = new Vector3(// Ограничиваем высоту камеры в диапазоне от -20 до 30
										transform.position.x,
										Mathf.Clamp(transform.position.y, -20f, 30f),
										transform.position.z);
	}
}
