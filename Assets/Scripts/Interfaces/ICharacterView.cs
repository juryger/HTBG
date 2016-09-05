using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Interfaces
{
    /// <summary>
    /// Interface of View for all game characters.
    /// </summary>
    public interface ICharacterView: IView
    {
        /// <summary>
        /// x position of View
        /// </summary>
        float PositionX { get; set; }

        /// <summary>
        /// y position of View
        /// </summary>
        float PositionY { get; set; }

        /// <summary>
        /// z position of View
        /// </summary>
        float PositionZ { get; set; }
    }
}
