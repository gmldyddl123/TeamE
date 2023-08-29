namespace monster

{
    public class M_DieState : MonsterState
    {

        Monster monster;
        //State state = State.DIE;

        public M_DieState(Monster monster)
        {

            this.monster = monster;
        }
        public void EnterState()
        {
            monster.monsterCurrentStates = this;
            monster.MonsterDieChange(true);
            monster.Die();
        }

        public void MoveLogic()
        {
          

        }


    }

}
