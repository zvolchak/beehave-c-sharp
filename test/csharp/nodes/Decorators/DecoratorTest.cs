using Beehave.Nodes.Decorators;
using Beehave.Nodes.Leaves;
using GdUnit4;

namespace Beehave.Tests {
    using static Assertions;

    [TestSuite]
    public partial class DecoratorTest {

        [TestCase]
        public void GetClassNamesValid() {
            Decorator decoratorLeaf = AutoFree(new Decorator());
            AssertInt(decoratorLeaf.GetClassNames().Count).IsEqual(2);

            AssertString(decoratorLeaf.GetClassNames()[0]).IsEqual("BeehaveNode");
            AssertString(decoratorLeaf.GetClassNames()[1]).IsEqual("Decorator");
        } // GetClassNamesValid


        [TestCase]
        public void TickThrowsNotImplementedException() {
            ActionLeaf actionLeaf = AutoFree(new ActionLeaf());

            try {
                actionLeaf.Tick(null, null);
                AssertString("").IsEqual("Decorator Tick should have thrown NotImplementedException!");
            }
            catch (System.NotImplementedException) {
                AssertBool(true).IsTrue();
            }
        } // TickThrowsNotImplementedException

    } // class
} // namespace
