using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class SelectController : MonoBehaviour
{
    public GameObject cube;// Префаб куба, который будет использоваться для выделения области
	public List<GameObject> players;// Список для хранения выделенных игроков
	public LayerMask layer, layerMask;// Слои, на которых будет производиться выделение
	private Camera _cam;// Ссылка на камеру
	RaycastHit _hit;// Переменная для хранения информации о пересечении луча с объектом
	private GameObject _cubeSelection;// Ссылка на созданный куб для выделения области

	private void Awake(){
        _cam=GetComponent<Camera>();// Получаем ссылку на компонент камеры
	}     
    public void Update(){
        if(Input.GetMouseButtonDown(1) && players.Count>0){// При нажатии правой кнопки мыши и числу выделенных игроков>0
            Ray ray= _cam.ScreenPointToRay(Input.mousePosition);// Создаем луч из позиции курсора мыши на экране
			if (Physics.Raycast(ray,out RaycastHit agentTarget,1000f,layer)){// Проверяем, пересекает ли этот луч какой-либо объект на указанном слое (layer)
			                                                                 // Если пересечение есть, то сохраняем информацию о точке пересечения в переменную agentTarget
			                                                                 // Максимальная дистанция для проверки пересечения - 1000 единиц
				foreach (var el in players){// Для каждого выделенного игрока (el) из списка players
				  //NavMeshAgent agent = el.GetComponent<NavMeshAgent>();// Получаем компонент NavMeshAgent этого игрока
					el.GetComponent<NavMeshAgent>().SetDestination(agentTarget.point);// Устанавливаем новую точку назначения (destination) для агента
																					  // Точка назначения - это точка пересечения луча с объектом (agentTarget.point)
				}
			}
        }

        if(Input.GetMouseButtonDown(0)){// При нажатии левой кнопки мыши
            foreach(var el in players){
                if(el!=null){
                    el.transform.GetChild(0).gameObject.SetActive(false);//Делаем невидимым первый дочерний элемент(healf)
				}    
         } 
            players.Clear();// Очищаем список выделенных игроков
			Ray ray =_cam.ScreenPointToRay(Input.mousePosition);// Создаем луч из позиции мыши


			if (Physics.Raycast(ray, out _hit , 1000f, layer)){// Если луч пересекает объект на указанном слое
              _cubeSelection= Instantiate(cube, new Vector3(_hit.point.x,1,_hit.point.z),Quaternion.identity);// Создаем куб для выделения области на месте пересечения
			}
        }

        if(_cubeSelection){// Если куб для выделения области существует
            Ray ray =_cam.ScreenPointToRay(Input.mousePosition);// Создаем новый луч из позиции мыши

			if (Physics.Raycast(ray, out RaycastHit hitDrag, 1000f, layer)){// Если луч пересекает объект на указанном слое

                float xScale=(_hit.point.x-hitDrag.point.x)*-1;// Вычисляем масштаб куба по осям X и Z на основе расстояния между точками пересечения
				float zScale=_hit.point.z-hitDrag.point.z;

                if(xScale<0.0f && zScale<0.0f){// Устанавливаем поворот куба в зависимости от направления выделения
                    _cubeSelection.transform.localRotation=Quaternion.Euler(new Vector3(0,180,0));
                }
                else if(xScale<0.0f){
                        _cubeSelection.transform.localRotation=Quaternion.Euler(new Vector3(0,0,180));// Изменение угла поворота
				}
                else if(zScale<0.0f){
                        _cubeSelection.transform.localRotation=Quaternion.Euler(new Vector3(180,0,0));
                }
                else{
                        _cubeSelection.transform.localRotation=Quaternion.Euler(new Vector3(0,0,0));
                }
              _cubeSelection.transform.localScale=new Vector3(Math.Abs(xScale),1,Math.Abs(zScale));// Устанавливаем масштаб куба в зависимости от расстояния между точками пересечения
			}
        }

        if(Input.GetMouseButtonUp(0) && _cubeSelection){// При отпускании левой кнопки мыши и существовании куба для выделения области
            RaycastHit[] hits= Physics.BoxCastAll(// Проверяем все объекты, которые находятся внутри куба для выделения области
                _cubeSelection.transform.position,
                _cubeSelection.transform.localScale,
			    Vector3.up,
			    Quaternion.identity,
				0,
				layerMask); // Использование layerMask вместо layer

			foreach (var el in hits){// Добавляем найденные объекты в список выделенных игроков

                if(el.collider.CompareTag("Enemy")){
                    continue;
                }
                players.Add(el.transform.gameObject);
                el.transform.GetChild(0).gameObject.SetActive(true);
            }
        Destroy(_cubeSelection);// Удаляем куб для выделения области
		}
    }
}

