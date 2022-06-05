// ================================================
//描 述:
//作 者:杜鑫
//创建时间:2022-06-05 18-42-47
//修改作者:杜鑫
//修改时间:2022-06-05 18-42-47
//版 本:0.1 
// ===============================================
using GameFramework;
using System;
using System.Reflection;
using UnityGameFramework.Runtime;
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;

namespace Deer
{
    public class ProcedurePreload : ProcedureBase
    {
        private Assembly _mainBusinessLogicAsm;
        private ProcedureOwner m_procedureOwner = null;
        protected override void OnEnter(ProcedureOwner procedureOwner)
        {
            base.OnEnter(procedureOwner);
            m_procedureOwner = procedureOwner;
            bool loadAssemblies = false;
            if (!loadAssemblies)
            {
                Log.Info("Skip load assemblies.");
                foreach (var asm in System.AppDomain.CurrentDomain.GetAssemblies())
                {
                    if (string.Compare(HotfixDefine.LogicMainDllName, $"{asm.GetName().Name}.dll",
                            StringComparison.Ordinal) == 0)
                    {
                        _mainBusinessLogicAsm = asm;
                        break;
                    }
                }
                RunMain();
                return;
            }
            RunMain();
        }
        private void RunMain() 
        {
            if (null == _mainBusinessLogicAsm)
            {
                Log.Fatal("Main business logic assembly missing.");
                return;
            }
            var appType = _mainBusinessLogicAsm.GetType("GameEntry");
            if (null == appType)
            {
                Log.Fatal("Main business logic type 'App' missing.");
                return;
            }
            var entryMethod = appType.GetMethod("MainApp");
            if (null == entryMethod)
            {
                Log.Fatal("Main business logic entry method 'Entry' missing.");
                return;
            }
            object[] objects = new object[] { new object[] { this, m_procedureOwner } };
            entryMethod.Invoke(appType, objects);
        }
    }
}