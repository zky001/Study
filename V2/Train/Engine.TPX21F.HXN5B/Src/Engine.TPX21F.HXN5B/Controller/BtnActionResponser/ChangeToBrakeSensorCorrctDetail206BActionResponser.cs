using System.ComponentModel.Composition;
using Engine.TPX21F.HXN5B.Resources.Keys;

namespace Engine.TPX21F.HXN5B.Controller.BtnActionResponser
{
    [Export]
    public class ChangeToBrakeSensorCorrctDetail206BActionResponser : BtnActionResponserBase
    {
        /// <summary>
        /// ��Ӧ����
        /// </summary>
        public override void ResponseClick()
        {
            NavigateTo(StateKeys.Root_�ƶ�ϵͳ_ά��������_������У׼_206BPУ׼);
        }
    }
}