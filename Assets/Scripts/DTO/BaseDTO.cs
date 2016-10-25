/// <summary>
/// Base class for all DTO objects (implement convertable interface).
/// </summary>
public abstract class BaseDTO
{
    /// <summary>
    /// Covert DTO object to corresponding Model.
    /// </summary>
    /// <param name="data">initalizating data</param>
    /// <returns>model object</returns>
    public abstract T ConvertToModel<T>(params object[] data) where T : BaseModel;
}
