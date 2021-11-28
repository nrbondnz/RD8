namespace GameObjectCollision
{
   
    public interface IGameObjectCollisionAction
    {
        public enum GameObjectCollisionActionEnum
        {
            Bounce,
            Slow,
            Speedup
        }

        public void DoGameObjectCollisionAction(GameObjectCollisionActionController controller);
    }
}