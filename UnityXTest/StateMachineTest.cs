using Moq;

namespace UnityX.StateMachine {
    public class StateA : IState {
        public void OnEnter() { }
        public void Update() { }
        public void FixedUpdate() { }
        public void OnExit() { }
    }

    public class StateB : IState {
        public void OnEnter() { }
        public void Update() { }
        public void FixedUpdate() { }
        public void OnExit() { }
    }

    [TestFixture]
    public class StateMachineTest {
        private StateMachine stateMachine;
        private Mock<IState> stateA;
        private Mock<IState> stateB;
        private Mock<IPredicate> trueCondition;
        private Mock<IPredicate> falseCondition;

        [SetUp]
        public void Setup() {
            stateMachine = new StateMachine();
            stateA = new Mock<StateA>().As<IState>();
            stateB = new Mock<StateB>().As<IState>();

            trueCondition = new Mock<IPredicate>();
            falseCondition = new Mock<IPredicate>();

            trueCondition.Setup(c => c.Evaluate()).Returns(true);
            falseCondition.Setup(c => c.Evaluate()).Returns(false);
        }

        [Test]
        public void SetState_InitializesStateCorrectly() {
            stateMachine.AddTransition(stateA.Object, stateB.Object, trueCondition.Object);
            stateMachine.SetState(stateA.Object);

            stateA.Verify(s => s.OnEnter(), Times.Once);
        }

        [Test]
        public void Update_ChangesStateWhenTransitionConditionIsTrue() {
            stateMachine.AddTransition(stateA.Object, stateB.Object, trueCondition.Object);
            stateMachine.SetState(stateA.Object);

            stateMachine.Update();

            stateB.Verify(s => s.OnEnter(), Times.Once);
            stateA.Verify(s => s.OnExit(), Times.Once);
        }

        [Test]
        public void Update_DoesNotChangeStateWhenTransitionConditionIsFalse() {
            stateMachine.AddTransition(stateA.Object, stateB.Object, falseCondition.Object);
            stateMachine.SetState(stateA.Object);

            stateMachine.Update();

            stateA.Verify(s => s.OnExit(), Times.Never);
            stateB.Verify(s => s.OnEnter(), Times.Never);
        }

        [Test]
        public void Update_CallsCurrentStateUpdateMethod() {
            stateMachine.AddAnyTransition(stateA.Object, trueCondition.Object);
            stateMachine.SetState(stateA.Object);

            stateMachine.Update();

            stateA.Verify(s => s.Update(), Times.Once);
        }

        [Test]
        public void FixedUpdate_CallsCurrentStateFixedUpdateMethod() {
            stateMachine.AddAnyTransition(stateA.Object, trueCondition.Object);
            stateMachine.SetState(stateA.Object);

            stateMachine.FixedUpdate();

            stateA.Verify(s => s.FixedUpdate(), Times.Once);
        }

        [Test]
        public void AddAnyTransition_TriggersTransitionFromAnyState() {
            stateMachine.AddTransition(stateA.Object, stateB.Object, falseCondition.Object);
            stateMachine.AddAnyTransition(stateB.Object, trueCondition.Object);
            stateMachine.SetState(stateA.Object);

            stateMachine.Update();

            stateB.Verify(s => s.OnEnter(), Times.Once);
            stateA.Verify(s => s.OnExit(), Times.Once);
        }
    }
}
