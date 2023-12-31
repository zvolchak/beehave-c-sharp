#if TOOLS
using Godot;
using System.Collections.Generic;

namespace Beehave {
    public class CustomNodeMetadata {

        public Script Source { get; private set; }
        public Texture2D Icon { get; private set; }
        public string TypeName { get; private set; }
        public string NodeName { get; private set; }

        public CustomNodeMetadata(string typeName, string  nodeName, string scriptPath, string iconPath) {
            this.Source = GD.Load<Script>(scriptPath);
            this.Icon = GD.Load<Texture2D>(iconPath);

            this.TypeName = typeName;
            this.NodeName = nodeName;
        }
    } // class


    [Tool]
	public partial class plugin : EditorPlugin {

        public override void _EnterTree() {
            var customNodes = this.getCustomNodes();
            foreach(CustomNodeMetadata item in customNodes) {
                AddCustomType(item.TypeName, item.NodeName, item.Source, item.Icon);
                GD.Print($"[BeehaveCSharp]: New node {item.TypeName}->{item.NodeName} has loaded.");
            }

            GD.Print("Beehave C# plugin initialized.");
        } // EnterTree


        public override void _ExitTree() {
            var customNodes = this.getCustomNodes();
            foreach (CustomNodeMetadata item in customNodes) {
                RemoveCustomType(item.TypeName);
                GD.Print($"[BeehaveCSharp]: Node {item.TypeName}->{item.NodeName} has unloaded.");
            }
        } // ExitTree


        private List<CustomNodeMetadata> getCustomNodes() {
            var treeNode = new CustomNodeMetadata(
                "BeehaveTree",
                "Beehave",
                "res://addons/beehaveCSharp/Scripts/Nodes/BeehaveTree.cs",
                "res://addons/beehaveCSharp/Icons/tree.svg"
            );

            var blackboardNode = new CustomNodeMetadata(
                    "Blackboard",
                    "Beehave",
                    "res://addons/beehaveCSharp/Scripts/Blackboard.cs",
                    "res://addons/beehaveCSharp/Icons/blackboard.svg"
                );

            var selectorReactiveNode = new CustomNodeMetadata(
                    "SelectorReactiveComposite",
                    "Beehave",
                    "res://addons/beehaveCSharp/Scripts/Nodes/Composites/SelectorReactiveComposite.cs",
                    "res://addons/beehaveCSharp/Icons/selector_reactive.svg"
                );

            var sequenceNode = new CustomNodeMetadata(
                "SequenceComposite",
                "Beehave",
                "res://addons/beehaveCSharp/Scripts/Nodes/Composites/Sequence.cs",
                "res://addons/beehaveCSharp/Icons/sequence.svg"
            );

            var actionLeafNode = new CustomNodeMetadata(
                "ActionLeaf",
                "Beehave",
                "res://addons/beehaveCSharp/Scripts/Nodes/Leaves/ActionLeaf.cs",
                "res://addons/beehaveCSharp/Icons/action.svg"
            );

            var conditionLeafNode = new CustomNodeMetadata(
                "ConditionLeaf",
                "Beehave",
                "res://addons/beehaveCSharp/Scripts/Nodes/Leaves/ConditionLeaf.cs",
                "res://addons/beehaveCSharp/Icons/condition.svg"
            );

            var leafNode = new CustomNodeMetadata(
                "ConditionLeaf",
                "Beehave",
                "res://addons/beehaveCSharp/Scripts/Nodes/Leaves/Leaf.cs",
                "res://addons/beehaveCSharp/Icons/category_leaf.svg"
            );

            return new List<CustomNodeMetadata>() {
                treeNode,
                blackboardNode,
                selectorReactiveNode,
                sequenceNode,
                actionLeafNode,
                conditionLeafNode,
                leafNode
            };
        } // getCustomNodes


    } // class
} // Beehave
#endif
