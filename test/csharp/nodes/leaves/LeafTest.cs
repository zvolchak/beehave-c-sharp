using Beehave.Nodes.Leaves;
using GdUnit4;

namespace Beehave.Tests {
    using static Assertions;

    [TestSuite]
    public partial class LeafTest {

        [TestCase]
        public void GetClassNamesValid() {
            Leaf leaf = AutoFree(new Leaf());
            AssertInt(leaf.GetClassNames().Count).IsEqual(2);

            AssertString(leaf.GetClassNames()[0]).IsEqual("BeehaveNode");
            AssertString(leaf.GetClassNames()[1]).IsEqual("Leaf");
        } // GetClassNamesValid


        [TestCase]
        public void TickThrowsNotImplementedException() {
            Leaf leaf = AutoFree(new Leaf());

            try {
                leaf.Tick(null, null);
                AssertString("").IsEqual($"{leaf.GetType()} Tick should have " +
                    $"thrown NotImplementedException!");
            } catch (System.NotImplementedException) {
                AssertBool(true).IsTrue(); // Success
            } catch (System.Exception) {
                AssertString("").IsEqual($"{leaf.GetType()} Tick should " +
                    $"have thrown NotImplementedException!");
            }
        } // TickThrowsNotImplementedException


        [TestCase]
        public void InterruptThrowsNotImplementedException() {
            Leaf leaf = AutoFree(new Leaf());

            try {
                leaf.Interrupt(null, null);
                AssertString("").IsEqual($"{leaf.GetType()} Interrupt should " +
                    $"have thrown NotImplementedException!");
            } catch (System.NotImplementedException) {
                AssertBool(true).IsTrue(); // Success
            } catch (System.Exception) {
                AssertString("").IsEqual($"{leaf.GetType()} Interrupt should " +
                    $"have thrown NotImplementedException!");
            }
        } // InterruptThrowsNotImplementedException

    } // class
} // namespace
