// ================================================
//描 述:
//作 者:杜鑫
//创建时间:2022-06-05 18-35-37
//修改作者:杜鑫
//修改时间:2022-06-05 18-35-37
//版 本:0.1 
// ===============================================
using GameFramework.Resource;
using UnityGameFramework.Runtime;
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;

namespace Deer
{
    public class ProcedureSplash : ProcedureBase
    {
        protected override void OnEnter(ProcedureOwner procedureOwner)
        {
            base.OnEnter(procedureOwner);
            // TODO: 这里可以播放一个 Splash 动画
            // ...

            if (GameEntryMain.Base.EditorResourceMode)
            {
                // 编辑器模式
                Log.Info("Editor resource mode detected.");
                ChangeState<ProcedurePreload>(procedureOwner);
                // 可更新模式
                /*                Log.Info("Updatable resource mode detected.");
                                ChangeState<ProcedureCheckVersion>(procedureOwner);*/
            }
            else if (GameEntryMain.Resource.ResourceMode == ResourceMode.Package)
            {
                // 单机模式
                Log.Info("Package resource mode detected.");
                ChangeState<ProcedureInitResources>(procedureOwner);
            }
            else
            {
                // 可更新模式
                Log.Info("Updatable resource mode detected.");
                ChangeState<ProcedureCheckVersion>(procedureOwner);
            }
        }
    }
}