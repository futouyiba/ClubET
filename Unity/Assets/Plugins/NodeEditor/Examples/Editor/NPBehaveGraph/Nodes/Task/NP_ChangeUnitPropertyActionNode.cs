//------------------------------------------------------------
// 代码由工具自动生成，请勿手动修改
// 代码由工具自动生成，请勿手动修改
// 代码由工具自动生成，请勿手动修改
//------------------------------------------------------------

using ETModel;
using GraphProcessor;

namespace Plugins.NodeEditor
{
    [NodeMenuItem("NPBehave行为树/Task/修改属性", typeof (NPBehaveGraph))]
    [NodeMenuItem("NPBehave行为树/Task/修改属性", typeof (SkillGraph))]
    public class NP_ChangeUnitPropertyActionNode: NP_TaskNodeBase
    {
        public override string name => "修改属性";

        public NP_ActionNodeData NP_ActionNodeData =
                new NP_ActionNodeData() { NodeType = NodeType.Task, NpClassForStoreAction = new NP_ChangeUnitPropertyAction() };

        public override NP_NodeDataBase NP_GetNodeData()
        {
            return NP_ActionNodeData;
        }
    }
}