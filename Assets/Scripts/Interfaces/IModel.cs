using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// General interface for model (MVC).
/// </summary>
public interface IModel
{
    /// <summary>
    /// Set controller for model.
    /// </summary>
    /// <param name="controller">controller</param>
    void SetController(BaseController controller);
}
