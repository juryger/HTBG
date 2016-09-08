using System;
/// <summary>
/// General interface for view (MVVM).
/// </summary>
public interface IView : IDisposable
{
    /// <summary>
    /// Set ViewModel for view.
    /// </summary>
    /// <param name="viewModel">ViewModel</param>
    void SetViewModel(BaseViewModel viewModel);

    /// <summary>
    /// Handle event notification from View or Model.
    /// </summary>
    /// <param name="eventPath">event name</param>
    /// <param name="source">source of event</param>
    /// <param name="data">parameters of event</param>
    void Notify(string eventPath, object source, params object[] data);
}
