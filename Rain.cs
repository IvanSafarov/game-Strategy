using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rain : MonoBehaviour
{
    public Light dirLight;// Ссылка на источник света в сцене
	private ParticleSystem _ps;// Ссылка на компонент ParticleSystem для имитации дождя
	private bool _isRain=false;// Флаг, указывающий, идет ли дождь в данный момент

	private void Start(){
    _ps=GetComponent<ParticleSystem>();// Получаем компонент ParticleSystem, прикрепленный к этому же объекту
	StartCoroutine(Weather());// Запускаем корутину для имитации погоды
	}
   private void Update(){
        if(_isRain && dirLight.intensity > 0.25f){
            LightIntensity(-1);// Если идет дождь и интенсивность света выше 0.25, уменьшаем интенсивность
        }
        else if(!_isRain && dirLight.intensity < 0.5f){
            LightIntensity(1);// Если не идет дождь и интенсивность света ниже 0.5, увеличиваем интенсивность
        }
   }
   private void LightIntensity(int mult){// Метод для плавного изменения интенсивности света
		dirLight.intensity+=0.1f*Time.deltaTime*mult;
   }
   IEnumerator Weather(){// Корутина для имитации погоды
    while (true){
        yield return new WaitForSeconds(UnityEngine.Random.Range(2f, 15f));
        if(_isRain){
            _ps.Stop();// Если сейчас идет дождь, останавливаем систему частиц
        }
        else{
            _ps.Play();// Если не идет дождь, запускаем систему частиц
		}
        _isRain=!_isRain;// Меняем значение флага _isRain на противоположное
		}
   }
}
