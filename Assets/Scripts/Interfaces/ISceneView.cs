using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Interfaces
{
    /// <summary>
    /// Interface for scene view
    /// </summary>
    interface ISceneView: IView
    {
        /// <summary>
        /// Name of the scene
        /// </summary>
        string Name { get; set; }
    }
}
