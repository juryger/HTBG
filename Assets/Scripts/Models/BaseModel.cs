using System;


/// <summary>
/// General class for all models (MVVM).
/// </summary>
[Serializable]
public abstract class BaseModel : IDisposable
{
    public BaseModel()
    {
    }

    /// <summary>
    /// ViewModel for Model.
    /// </summary>
    public BaseViewModel ViewModel { get; private set; }

    /// <summary>
    /// Set ViewModel for model.
    /// </summary>
    /// <param name="viewModel">ViewModel</param>
    public virtual void SetViewModel(BaseViewModel viewModel)
    {
        if (ViewModel != null)
            throw new ApplicationException("ViewModel has been already initialized.");

        ViewModel = viewModel;
    }

    /// <summary>
    /// Allows to reset ViewModel, because for character who moving between scenes, view recreated each time.
    /// </summary>
    public virtual void ResetModel()
    {
        ViewModel = null;
    }

    /// <summary>
    /// Convert model to correspoinding DTO.
    /// </summary>
    /// <returns>corresponding DTO object</returns>
    public abstract T ConvertToDTO<T>(params object[] data) where T : BaseDTO;

    #region IDisposable Support

    protected bool disposedValue = false; // To detect redundant calls

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                // dispose managed state (managed objects).
                ViewModel = null;
            }

            // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
            // TODO: set large fields to null.

            disposedValue = true;
        }
    }

    // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
    // ~BaseModel() {
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
