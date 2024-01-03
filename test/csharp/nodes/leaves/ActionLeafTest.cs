using Beehave.Nodes.Leaves;
using GdUnit4;

namespace Beehave.Tests {
    using static Assertions;

    [TestSuite]
    public partial class ActionLeafTest {

        [TestCase]
        public void GetClassNamesValid() {
            ActionLeaf actionLeaf = AutoFree(new ActionLeaf());
            AssertInt(actionLeaf.GetClassNames().Count).IsEqual(3);

            AssertString(actionLeaf.GetClassNames()[0]).IsEqual("BeehaveNode");
            AssertString(actionLeaf.GetClassNames()[1]).IsEqual("Leaf");
            AssertString(actionLeaf.GetClassNames()[2]).IsEqual("ActionLeaf");
        } // GetClassNamesValid

    } // class
} // namespace
