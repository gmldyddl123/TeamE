namespace monster

{
    public class M_DieState : MonsterState
    {

        Monster monster;
        State state = State.Die;

        public M_DieState(Monster monster)
        {

            this.monster = monster;
        }
        public void EnterState()
        {
            monster.MonsterAnimatorChange((int)state);
            monster.monsterCurrentStates = this;
        }

        public void MoveLogic()
        {
          

        }


    }

}
