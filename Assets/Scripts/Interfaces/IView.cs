using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// General interface for view (MVC).
/// </summary>
public interface IView
{
    /// <summary>
    /// Set controller for view.
    /// </summary>
    /// <param name="controller">controller</param>
    void SetController(BaseController controller);

    /// <summary>
    /// Handle event notification from View or Model.
    /// </summary>
    /// <param name="eventPath">event name</param>
    /// <param name="source">source of event</param>
    /// <param name="data">parameters of event</param>
    void Notify(string eventPath, object source, params object[] data);

    /// <summary>
    /// Handle event notification from View or Model.
    /// </summary>
    /// <param name="viewDataPath">view data identifier</param>
    /// <returns>array of data</returns>
    object[] GetViewData(string viewDataPath);

    /// <summary>
    /// Inforce view to sync state with model.
    /// </summary>
    void SyncState();
}
