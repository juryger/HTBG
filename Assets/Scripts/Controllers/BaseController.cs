using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public abstract class BaseController
{
    /// <summary>
    /// Instance of View.
    /// </summary>
    public IView View { get; private set; }

    /// <summary>
    /// Handle event notification from View or Model.
    /// </summary>
    /// <param name="eventPath">event name</param>
    /// <param name="source">source of event</param>
    /// <param name="data">parameters of event</param>
    public abstract void Notify(string eventPath, Object source, params object[] data);

    public BaseController(IView view)
    {
        View = view;

        View.SetController(this);
    }
}
