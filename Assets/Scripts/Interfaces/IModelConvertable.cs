/// <summary>
/// Define support of converting DTO object to corresponding Model.
/// </summary>
/// <typeparam name="T">type of model</typeparam>
public interface IModelConvertable<T> where T : BaseModel
{
    /// <summary>
    /// Covert DTO object to corresponding Model.
    /// </summary>
    /// <param name="data">initalizating data</param>
    /// <returns>model object</returns>
    T ConvertToModel(params object[] data);
}
