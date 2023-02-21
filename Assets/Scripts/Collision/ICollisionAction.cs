namespace Collision
{
   
   /// <summary>
   /// 
   /// </summary>
        public enum CollisionActionEnum
        {
            Bounce = 0,
            SpeedChange = 1,
            Death = 2,
            Attract = 3
        }

   /// <summary>
   /// 
   /// </summary>
        public enum CollisionEffectStrengthEnum
        {
            Full = 15,
            Strong = 10,
            MedStrong = 7,
            Normal = 5,
            Half = 3,
            Low = 2,
            Lowest = 1
        }

        /// <summary>
        /// This interface is used to allow for different collision effects to be chosen for the collition action controller
        /// </summary>
        public interface ICollisionAction
        {   
            /// <summary>
            /// When a collision with collider zone happens an implmentation of this method will be called
            /// e.g. in the CollitionActionController
            /// </summary>
            /// <param name="controller"></param>
            /// <param name="collisionEffectStrengthEnum"></param>
        public void DoCollisionAction(CollisionActionController controller,
            CollisionEffectStrengthEnum collisionEffectStrengthEnum);
        /// <summary>
        /// As per DoCollisionAction this method will be called in the collision action controller on zone exit
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="collisionEffectStrengthEnum"></param>
        public void DoCollisionAreaExitAction(CollisionActionController controller,
            CollisionEffectStrengthEnum collisionEffectStrengthEnum);
    }
}