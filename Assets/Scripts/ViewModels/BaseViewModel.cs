using System;

/// <summary>
/// Base class for ViewModel (MVVM)
/// </summary>
[Serializable]
public abstract class BaseViewModel : IDisposable
{
    public BaseViewModel(IView view, BaseModel model)
    {
        View = view;
        Model = model;
        View.SetViewModel(this);
    }

    /// <summary>
    /// Instance of View.
    /// </summary>
    public IView View { get; private set; }

    /// <summary>
    /// Instance of model.
    /// </summary>
    private BaseModel Model { get; set; }

    /// <summary>
    /// Handle event notification from View or Model.
    /// </summary>
    /// <param name="eventPath">event name</param>
    /// <param name="source">source of event</param>
    /// <param name="data">parameters of event</param>
    public abstract void Notify(string eventPath, Object source, params object[] data);

    #region IDisposable Support

    protected bool disposedValue = false; // To detect redundant calls

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                View.Dispose();
                View = null;

                Model.Dispose();
                Model = null;
            }

            // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
            // TODO: set large fields to null.

            disposedValue = true;
        }
    }

    // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
    // ~BaseViewModel() {
    //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
    //   Dispose(false);
    // }

    // This code added to correctly implement the disposable pattern.
    public void Dispose()
    {
        // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        Dispose(true);
        // TODO: uncomment the following line if the finalizer is overridden above.
        // GC.SuppressFinalize(this);
    }

    #endregion
}
