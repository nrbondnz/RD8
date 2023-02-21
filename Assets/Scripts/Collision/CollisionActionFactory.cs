using System;

namespace Collision
{
    /// <summary>
    /// 
    /// </summary>
    public static class CollisionActionFactory
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="collisionActionEnum"></param>
        /// <exception cref="NotImplementedException"></exception>
        public static void SetGameObjectCollisionAction(CollisionActionController controller,
            CollisionActionEnum collisionActionEnum)
        {
            if ( collisionActionEnum == CollisionActionEnum.Bounce)
            {
                controller.gameObject.AddComponent<BounceCollisionAction>();
            }
            else if ( collisionActionEnum == CollisionActionEnum.SpeedChange)
            {
                controller.gameObject.AddComponent<SpeedChangeCollisionAction>();
            } else if (collisionActionEnum == CollisionActionEnum.Death)
            {
                controller.gameObject.AddComponent<DeathCollisionAction>();
            } else if (collisionActionEnum == CollisionActionEnum.Attract)
            {
                controller.gameObject.AddComponent<AttractCollisionAction>();
            } 
            else 
            {
                throw new NotImplementedException("Non implemented IKeyAction used in factory");
            }
        }
    }
}