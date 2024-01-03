using Beehave.Nodes.Composites;
using Beehave.Nodes.Decorators;
using GdUnit4;

namespace Beehave.Tests {
    using static Assertions;

    [TestSuite]
    public partial class CompositeTest {

        [TestCase]
        public void GetClassNamesValid() {
            Composite compositeLeaf = AutoFree(new Composite());
            AssertInt(compositeLeaf.GetClassNames().Count).IsEqual(2);

            AssertString(compositeLeaf.GetClassNames()[0]).IsEqual("BeehaveNode");
            AssertString(compositeLeaf.GetClassNames()[1]).IsEqual("Composite");
        } // GetClassNamesValid


        [TestCase]
        public void TickThrowsNotImplementedException() {
            Composite compositeLeaf = AutoFree(new Composite());

            try {
                compositeLeaf.Tick(null, null);
                AssertString("No exception").IsEqual($"{compositeLeaf.GetType()} Tick should have " +
                    $"thrown NotImplementedException!");
            } catch (System.NotImplementedException) {
                AssertBool(true).IsTrue(); // Success
            } catch (System.Exception e) {
                AssertString(e.GetType().Name).IsEqual($"{compositeLeaf.GetType()} Tick should " +
                    $"have thrown NotImplementedException!");
            }
        } // TickThrowsNotImplementedException

    } // class
} // namespace
