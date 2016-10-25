using System;
/// <summary>
/// Data Transfer Object for association entity between SceneDTO and InventoryDTO.
/// </summary>
public class SceneInventoryDTO : BaseDTO
{
    public string SceneId { get; set; }
    public string InventoryId { get; set; }
    public float LocationX { get; set; }
    public float LocationY { get; set; }

    public override T ConvertToModel<T>(params object[] data)
    {
        throw new NotImplementedException();
    }
}