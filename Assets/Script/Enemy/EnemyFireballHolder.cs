
using UnityEngine;

public class EnemyFireballHolder : MonoBehaviour
{
   [SerializeField] private Transform enemy;
   
   private void Uptade()
   {
       transform.localScale = enemy.localScale;
   }
}
