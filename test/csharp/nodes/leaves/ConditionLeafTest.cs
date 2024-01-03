using Beehave.Nodes.Leaves;
using GdUnit4;

namespace Beehave.Tests {
    using static Assertions;

    [TestSuite]
    public partial class ConditionLeafTest {

        [TestCase]
        public void GetClassNamesValid() {
            ConditionLeaf conditionLeaf = AutoFree(new ConditionLeaf());
            AssertInt(conditionLeaf.GetClassNames().Count).IsEqual(3);

            AssertString(conditionLeaf.GetClassNames()[0]).IsEqual("BeehaveNode");
            AssertString(conditionLeaf.GetClassNames()[1]).IsEqual("Leaf");
            AssertString(conditionLeaf.GetClassNames()[2]).IsEqual("ConditionLeaf");
        } // GetClassNamesValid

    } // class
} // namespace
