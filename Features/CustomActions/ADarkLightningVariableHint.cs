using System.Collections.Generic;

namespace DragonOfTruth01.GizmoTheFoxCCMod;

internal sealed class ADarkLightningVariableHint : AVariableHint
{
    public override List<Tooltip> GetTooltips(State s)
    {
        if (status.HasValue)
        {
            List<Tooltip> list = new List<Tooltip>();
            list.Add(new TTGlossary("action.xHint.desc", "<c=text>" + status.Value.GetLocName() + "</c>", "", secondStatus.HasValue ? (" </c>+ <c=status>" + secondStatus.Value.GetLocName() + "</c>") : "", ""));
            return list;
        }
        return new List<Tooltip>();
    }
}