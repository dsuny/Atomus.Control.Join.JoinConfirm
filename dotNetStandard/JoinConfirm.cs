using Atomus.Controllers;
using Atomus.Models;
using Atomus.Service;
using Atomus.Page;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atomus.Control.Join
{
    public class JoinConfirm : IAction
    {
        private AtomusPageEventHandler beforeActionEventHandler;
        private AtomusPageEventHandler afterActionEventHandler;

        #region Init
        public JoinConfirm() { }
        #endregion

        #region IO
        object IAction.ControlAction(ICore sender, AtomusPageArgs e)
        {
            Dictionary<string, string> keyValuePairs;
            try
            {
                this.beforeActionEventHandler?.Invoke(this, e);

                switch (e.Action)
                {
                    case "Save":
                        if (e.Value is Dictionary<string, string>)
                        {
                            keyValuePairs = (e.Value as Dictionary<string, string>);

                            return this.Save(keyValuePairs["EMAIL"], keyValuePairs["KEY"]);
                        }

                        throw new AtomusException("'{0}'은 처리할 수 없는 Action 입니다.".Translate(e.Action));

                    default:
                        throw new AtomusException("'{0}'은 처리할 수 없는 Action 입니다.".Translate(e.Action));
                }
            }
            finally
            {
                this.afterActionEventHandler?.Invoke(this, e);
            }
        }

        private IResponse Save(string EMAIL, string KEY)
        {
            return this.Save(new JoinConfirmModel
            {
                EMAIL = EMAIL,
                KEY = KEY
            });
        }

        #endregion

        #region Event
        event AtomusPageEventHandler IAction.BeforeActionEventHandler
        {
            add
            {
                this.beforeActionEventHandler += value;
            }
            remove
            {
                this.beforeActionEventHandler -= value;
            }
        }
        event AtomusPageEventHandler IAction.AfterActionEventHandler
        {
            add
            {
                this.afterActionEventHandler += value;
            }
            remove
            {
                this.afterActionEventHandler -= value;
            }
        }
        #endregion

        #region Etc
        #endregion
    }
}