namespace monster

{
    public class M_LongAttackState : MonsterState
    {

        State state = State.LONG_ATTACK;
        Monster monster;
        public M_LongAttackState(Monster monster)
        {
            this.monster = monster;
        }

        public void EnterState()
        {
            monster.monsterCurrentStates = this;
            monster.MonsterAnimatorChange((int)state);
        }

        public void MoveLogic()
        {

        }

    }
}