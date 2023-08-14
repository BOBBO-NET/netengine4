// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// namespace BobboNet.PlayerBehaviours
// {
//     public class PlayerBehaviourHealth : PlayerBehaviour
//     {
//         private Stats.PlayerStatsController statsController;
//         private bool isDead = false;

//         protected override void OnSetup()
//         {

//         }

//         private void Start()
//         {
//             statsController = GameController.GetSystem<Stats.PlayerStatsController>();
//         }




//         //
//         //  Public Methods
//         //

//         public float GetHealth()
//         {
//             return statsController.personal.currentHealth;
//         }

//         public void TakeDamage(float amount)
//         {
//             SetHealth(statsController.personal.currentHealth - amount);
//         }

//         public void Heal(float amount)
//         {
//             SetHealth(statsController.personal.currentHealth + amount);
//         }

//         public void SetHealth(float value)
//         {
//             if (value < 0)
//             {
//                 value = 0;
//             }
//             else if (value > statsController.personal.maxHealth)
//             {
//                 value = statsController.personal.maxHealth;
//             }

//             float delta = value - statsController.personal.currentHealth;

//             statsController.personal.currentHealth = value;

//             if (statsController.personal.currentHealth <= 0)
//             {
//                 Kill();
//             }
//         }

//         public void SetDead(bool value)
//         {
//             isDead = value;

//             if (value)
//             {
//                 playerController.GetBehaviour<PlayerBehaviourInventory>()?.DropAllItems();

//                 playerController.lockMovement.Lock("player_death");
//                 playerController.lockInteraction.Lock("player_death");
//                 playerController.lockCamera.Lock("player_death");

//                 //cameraController.SetSelectedCamera(PlayerCameraController.SelectedCamera.Dead);
//             }
//             else
//             {
//                 playerController.lockMovement.Unlock("player_death");
//                 playerController.lockInteraction.Unlock("player_death");
//                 playerController.lockCamera.Unlock("player_death");

//                 //cameraController.SetSelectedCamera(PlayerCameraController.SelectedCamera.POV);
//             }
//         }



//         //
//         //  Internal Logic
//         //

//         private void Kill()
//         {
//             if (isDead)
//             {
//                 return;
//             }

//             SetDead(true);
//         }
//     }
// }